using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class PostLikeDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public bool IsLike { get; set; }
        public int LikeCount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
