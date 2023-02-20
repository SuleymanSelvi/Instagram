using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstagramApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class FollowersController : Controller
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<FollowersController> _logger;

        public FollowersController(IUnitOfWork unitOfWork, ILogger<FollowersController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost(Name = "FollowUser")]
        public InstagramApiResponse<FollowersDTO> FollowUser(FollowUserModel followUserModel)
        {
            return _unitOfWork.FollowersRepository.FollowUser(followUserModel.FollowId, followUserModel.FollowerId);
        }

        [HttpGet(Name = "GetUserFollowingForChat")]
        public InstagramApiResponse<ChatDTO> GetUserFollowingForChat(int userId)
        {
            return _unitOfWork.FollowersRepository.GetUserFollowingForChat(userId);
        }

        [HttpGet(Name = "GetUserFollowing")]
        public InstagramApiResponse<UserDTO> GetUserFollowing(int userId)
        {
            return _unitOfWork.FollowersRepository.GetUserFollowing(userId);
        }

        [HttpGet(Name = "GetUserFollowers")]
        public InstagramApiResponse<UserDTO> GetUserFollowers(int userId)
        {
            return _unitOfWork.FollowersRepository.GetUserFollowers(userId);
        }
    }
}
