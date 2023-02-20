using Infrastructure.Repositories.Interfaces;
using Instagram.Domain;
using Instagram.Domain.DTO;
using Instagram.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UsersController : ControllerBase
    {
        private IUnitOfWork _unitOfWork; //dbContext
        private readonly ILogger<UsersController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UsersController(IUnitOfWork unitOfWork, ILogger<UsersController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        [HttpPost(Name = "Login")]
        public InstagramApiResponse<Users> Login(LoginModel user)
        {
            return _unitOfWork.UsersRepository.Login(user.Name, user.Password);
        }

        [HttpPost(Name = "Register")]
        public InstagramApiResponse<Users> Register(LoginModel registir)
        {
            return _unitOfWork.UsersRepository.Registir(registir.Name, registir.Password, registir.Email, registir.About);
        }

        [HttpGet(Name = "DeleteAccount")]
        public InstagramApiResponse<Users> DeleteAccount(int userId, string password)
        {
            return _unitOfWork.UsersRepository.DeleteAccount(userId, password);
        }

        [HttpGet(Name = "GetUsers")]
        public IEnumerable<UserDTO> GetUsers()
        {
            return _unitOfWork.UsersRepository.GetUsers();
        }

        [HttpGet(Name = "GetUserProfile")]
        public InstagramApiResponse<UserDTO> GetUserProfile(int userId, int sessionUserId)
        {
            return _unitOfWork.UsersRepository.GetUserProfile(userId, sessionUserId);
        }

        [HttpPost(Name = "SearchUsers")]
        public InstagramApiResponse<UserDTO> SearchUsers(SearchUsersModel model)
        {
            return _unitOfWork.UsersRepository.SearchUsers(model.UserName);
        }

        [HttpPut(Name = "UpdateProfile")]
        [Consumes("multipart/form-data")]
        public InstagramApiResponse<Users> UpdateProfile([FromForm] UpdateProfileModel model)
        {
            string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/UserImages/" + model.UserId + "/");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var newUserImage = RandomString(10) + ".jpg";
            string userImageFile = "http://localhost:5122/Images/UserImages/" + model.UserId + "/" + newUserImage;

            string filePath = Path.Combine(directoryPath, newUserImage);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                model.Image.CopyTo(stream);
            }

            return _unitOfWork.UsersRepository.UpdateProfile(model.UserId, model.Name, model.Password, userImageFile, model.Email, model.About);
        }

        [HttpPost(Name = "UploadChatImage")]
        [Consumes("multipart/form-data")]
        public string UploadChatImage([FromForm] ChatImageModel model)
        {
            if (model.Images.ContentType == "video/mp4")
            {
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/ChatVideos/" + model.UserId + "/");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var newChatVideo = RandomString(10) + ".mp4";
                string chatVideoFile = "http://localhost:5122/Images/ChatVideos/" + model.UserId + "/" + newChatVideo;

                string filePath = Path.Combine(directoryPath, newChatVideo);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Images.CopyTo(stream);
                }

                return chatVideoFile;
            }
            else if (model.Images.ContentType == "image/jpeg")
            {
                string directoryPath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images/ChatImages/" + model.UserId + "/");

                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }

                var newChatImage = RandomString(10) + ".jpg";
                string chatImageFile = "http://localhost:5122/Images/ChatImages/" + model.UserId + "/" + newChatImage;

                string filePath = Path.Combine(directoryPath, newChatImage);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    model.Images.CopyTo(stream);
                }

                return chatImageFile;
            }

            return null;
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
