using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class PostsDTO
    {
        public int Id { get; set; }
        public string Images { get; set; }
        public string Description { get; set; }
        public string TagsId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserImage { get; set; }
        public int LikeCount { get; set; }
        public int CommentCount { get; set; }
        public bool existUserLike { get; set; }
        public bool existSaved { get; set; }
        public string CreatedDateAgo { get; set; }


        //public short Follow { get; set; }
        //public short Follower { get; set; }
        //public string UserAbout { get; set; }
        //public string TweetComment { get; set; }
        //public string SessionUserImageFile { get; set; }
    }
}
