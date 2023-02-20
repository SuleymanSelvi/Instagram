using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class StoryDTO
    {
        public int UserId { get; set; } 
        public List<StoryDetailDTO> StoryList { get; set; }
    }

    public class StoryDetailDTO
    {
        public int Id { get; set; }
        public string Images { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateAgo { get; set; }
        public bool IsActive { get; set; }
        public string UserImage { get; set; }
        public string UserName { get; set; }
        public bool IsSeen { get; set; }
        public int? StoryDuration { get; set; }
    }
}
