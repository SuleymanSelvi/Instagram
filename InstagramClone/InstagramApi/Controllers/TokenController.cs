using Instagram.Domain;
using Instagram.Domain.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InstagramApi.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class TokenController : Controller
    {
        public IConfiguration _configuration;

        public TokenController(IConfiguration config)
        {
            _configuration = config;
        }

        [HttpPost(Name = "GetToken")]
        public InstagramApiResponse<UserDTO> GetToken(AuthUser _userData)
        {
            if (_userData != null && _userData.UserName != null && _userData.Password != null)
            {
                if (_userData.UserName == "Test" && _userData.Password == "123123123")
                {
                    //create claims details based on the user information
                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()), 
                        new Claim("UserName", _userData.UserName)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(1),
                        signingCredentials: signIn);

                    return new InstagramApiResponse<UserDTO>
                    {
                        Message = new JwtSecurityTokenHandler().WriteToken(token),
                        Success = true,
                    }; 
                }
                else
                {
                    return new InstagramApiResponse<UserDTO>
                    {
                        Message = "Hatalı Kullanıcı Bilgileri",
                        Success = false,
                    };
                }
            }
            else
            {
                return new InstagramApiResponse<UserDTO>
                {
                    Message = "Hatalı Kullanıcı Bilgileri",
                    Success = false,
                };
            }
        }
    }
}
