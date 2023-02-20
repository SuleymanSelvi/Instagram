using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PostController : ControllerBase
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<PostController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PostController(IUnitOfWork unitOfWork, ILogger<PostController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet(Name = "GetTopPosts")]
        public InstagramApiResponse<PostsDTO> GetTopPosts(int skip, int take, int sessionUserId)
        {
            return _unitOfWork.PostsRepository.GetTopPosts(skip, take, sessionUserId);
        }

        [HttpGet(Name = "GetPostsByFollowId")]
        public InstagramApiResponse<PostsDTO> GetPostsByFollowId(int skip, int take, int sessionUserId)
        {
            return _unitOfWork.PostsRepository.GetPostsByFollowId(skip, take, sessionUserId);
        }

        [HttpGet(Name = "GetPostsByUserId")]
        public InstagramApiResponse<PostsDTO> GetPostsByUserId(int userId)
        {
            return _unitOfWork.PostsRepository.GetPostsByUserId(userId);
        }

        [HttpGet(Name = "GetPostDetail")]
        public InstagramApiResponse<PostsDTO> GetPostDetail(int postId, int sessionUserId)
        {
            return _unitOfWork.PostsRepository.GetPostDetail(postId, sessionUserId);
        }

        [HttpPost(Name = "UploadPost")]
        [Consumes("multipart/form-data")]
        public InstagramApiResponse<Posts> UploadPost([FromForm] UploadPostModel model)
        {
            if (model.Images.ContentType == "video/mp4")
            {
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/PostVideos/" + model.UserId + "/");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var newPostVideo = RandomString(10) + ".mp4";
                string postVideoFile = "http://localhost:5122/Images/PostVideos/" + model.UserId + "/" + newPostVideo;

                string filePath = Path.Combine(directoryPath, newPostVideo);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Images.CopyTo(stream);
                }

                return _unitOfWork.PostsRepository.UploadPost(model.UserId, postVideoFile, model.Description);
            }
            else if (model.Images.ContentType == "image/jpeg/png")
            {
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/PostImages/" + model.UserId + "/");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var newPostImage = RandomString(10) + ".jpg";
                //model.Images.SaveAs(directoryPath + newPostImage);
                string postImageFile = "http://localhost:5122/Images/PostImages/" + model.UserId + "/" + newPostImage;

                string filePath = Path.Combine(directoryPath, newPostImage);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Images.CopyTo(stream);
                }

                return _unitOfWork.PostsRepository.UploadPost(model.UserId, postImageFile, model.Description);
            }

            return null;
        }

        [HttpDelete(Name = "DeletePost")]
        public InstagramApiResponse<Posts> DeletePost(int postId)
        {
            return _unitOfWork.PostsRepository.DeletePost(postId);
        }

        [NonAction]
        public string RandomString(int length)
        {
            Random random = new Random();

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
