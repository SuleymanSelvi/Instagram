using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class SavedPostsController : Controller
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<SavedPostsController> _logger;
        public SavedPostsController(IUnitOfWork unitOfWork, ILogger<SavedPostsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost(Name = "SavedPosts")]
        public InstagramApiResponse<SavedPostsDTO> SavedPosts(SavedPostsModel model)
        {
            return _unitOfWork.SavedPostsRepository.SavedPosts(model.PostId, model.UserId);
        }

        [HttpGet(Name = "GetSavedPosts")]
        public InstagramApiResponse<PostsDTO> GetSavedPosts(int userId)
        {
            return _unitOfWork.SavedPostsRepository.GetSavedPosts(userId);
        }
    }
}
