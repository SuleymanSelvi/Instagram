using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace InstagramApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class NotificationsController : ControllerBase
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<NotificationsController> _logger;

        public NotificationsController(IUnitOfWork unitOfWork, ILogger<NotificationsController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet(Name = "GetNotifications")]
        public InstagramApiResponse<NotificationsDTO> GetNotifications(int userId)
        {
            return _unitOfWork.NotificationsRepository.GetNotifications(userId);
        }

        [HttpGet(Name = "UpdateNotifications")]
        public InstagramApiResponse<Notifications> UpdateNotifications(int userId)
        {
            return _unitOfWork.NotificationsRepository.UpdateNotifications(userId);
        }
    }
}
