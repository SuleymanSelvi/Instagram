using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstagramApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StoryController : Controller
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<StoryController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public StoryController(IUnitOfWork unitOfWork, ILogger<StoryController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet(Name = "GetStorys")]
        public InstagramApiResponse<StoryDTO> GetStorys(int sessionId)
        {
            return _unitOfWork.StorysRepository.GetStorys(sessionId);
        }

        [HttpPost(Name = "UploadStory")]
        [Consumes("multipart/form-data")]
        public InstagramApiResponse<Storys> UploadStory([FromForm] UploadStoryModel model)
        {
            if (model.Images.ContentType == "video/mp4")
            {
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/StoryVideos/" + model.UserId + "/");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var newStoryVideo = RandomString(10) + ".mp4";
                string storyVideoFile = "http://localhost:5122/Images/StoryVideos/" + model.UserId + "/" + newStoryVideo;

                string filePath = Path.Combine(directoryPath, newStoryVideo);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Images.CopyTo(stream);
                }

                return _unitOfWork.StorysRepository.UploadStory(model.UserId, storyVideoFile, model.FileDuration);
            }
            else if (model.Images.ContentType == "image/jpeg")
            {
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/StoryImages/" + model.UserId + "/");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var newStoryImage = RandomString(10) + ".jpg";
                string storyImageFile = "http://localhost:5122/Images/StoryImages/" + model.UserId + "/" + newStoryImage;

                string filePath = Path.Combine(directoryPath, newStoryImage);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Images.CopyTo(stream);
                }

                return _unitOfWork.StorysRepository.UploadStory(model.UserId, storyImageFile, model.FileDuration);
            }

            return null;
        }

        [HttpDelete(Name = "DeleteStory")]
        public InstagramApiResponse<Storys> DeleteStory(int storyId)
        {
            return _unitOfWork.StorysRepository.DeleteStory(storyId);
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
