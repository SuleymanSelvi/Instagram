using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class PostLikeModel
    {
        public int postId { get; set; }
        public int userId { get; set; }
        public int postOwnerId { get; set; }
    }
}
