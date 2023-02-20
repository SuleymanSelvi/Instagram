using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserBlockController : Controller
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<UserBlockController> _logger;
        public UserBlockController(IUnitOfWork unitOfWork, ILogger<UserBlockController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpDelete(Name ="BlockUser")]
        public InstagramApiResponse<UserBlockDTO> BlockUser(int blockedId,int blockId)
        {
            return _unitOfWork.UserBlockRepository.BlockUser(blockedId,blockId);
        }
    }
}
