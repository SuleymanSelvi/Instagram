using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.Models
{
    public class UploadStoryModel
    {
        public int UserId { get; set; }
        public IFormFile Images { get; set; }
        public int FileDuration { get; set; }
    }
}
