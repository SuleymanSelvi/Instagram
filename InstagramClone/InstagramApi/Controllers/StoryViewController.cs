using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace InstagramApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class StoryViewController : Controller
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<StoryViewController> _logger;

        public StoryViewController(IUnitOfWork unitOfWork, ILogger<StoryViewController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpPost(Name = "UploadStoryView")]
        public InstagramApiResponse<StoryViewDTO> UploadStoryView(StoryViewModel model)
        {
            return _unitOfWork.StoryViewRepository.UploadStoryView(model.StoryId, model.UserId);
        }
    }
}
