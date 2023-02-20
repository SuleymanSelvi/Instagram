using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class UpdateProfileModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public IFormFile Image { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
    }
}
