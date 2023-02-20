using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class PostCommentController : Controller
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<PostCommentController> _logger;

        public PostCommentController(IUnitOfWork unitOfWork, ILogger<PostCommentController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet(Name = "GetPostComments")]
        public InstagramApiResponse<PostCommentsDTO> GetPostComments(int postId)
        {
            return _unitOfWork.PostsCommentRepository.GetPostComments(postId);
        }

        [HttpPost(Name = "UploadComment")]
        public InstagramApiResponse<PostCommentsDTO> UploadComment(PostCommentModel model)
        {
            return _unitOfWork.PostsCommentRepository.UploadComment(model.PostId, model.UserId, model.Comment, model.SubComment);
        }

        [HttpDelete(Name ="DeletePostComment")]
        public InstagramApiResponse<PostsComment> DeletePostComment(int postCommentId)
        {
            return _unitOfWork.PostsCommentRepository.DeletePostComment(postCommentId);
        }
    }
}
