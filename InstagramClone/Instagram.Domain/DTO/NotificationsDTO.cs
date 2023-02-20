using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class NotificationsDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int PostOwnerId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateDateAgo { get; set; }
        public bool IsSeen { get; set; }
        public string PostImage { get; set; }
    }
}
