using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InstagramApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]

    public class PostLikeController : Controller
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<PostLikeController> _logger;

        public PostLikeController(IUnitOfWork unitOfWork, ILogger<PostLikeController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost(Name = "PostLike")]
        public InstagramApiResponse<PostLikeDTO> PostLike(PostLikeModel model)
        {
            return _unitOfWork.PostsLikeRepository.PostLike(model.postId, model.userId,model.postOwnerId);
        }

        [HttpGet(Name = "GetLikedPosts")]
        public InstagramApiResponse<PostsDTO> GetLikedPosts(int userId)
        {
            return _unitOfWork.PostsLikeRepository.GetLikedPosts(userId);
        }
    }
}
