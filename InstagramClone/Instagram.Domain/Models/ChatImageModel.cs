using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.Models
{
    public class ChatImageModel
    {
        public int UserId { get; set; }
        public IFormFile Images { get; set; }
    }
}
