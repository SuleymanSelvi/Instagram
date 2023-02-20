using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class StoryViewDTO
    {
        public int Id { get; set; }
        public string StoryID { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSeen { get; set; }
    }
}
