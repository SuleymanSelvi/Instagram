using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class PostsCommentLike
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("PostsComment")]
        public int PostCommentId { get; set; }
        public virtual PostsComment PostsComment { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users Users { get; set; }

        [Required]
        public int LikeType { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
