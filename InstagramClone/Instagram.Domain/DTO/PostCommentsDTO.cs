using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class PostCommentsDTO
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedDateAgo { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public List<PostCommentsDTO> SubComment { get; set; }
    }
}
