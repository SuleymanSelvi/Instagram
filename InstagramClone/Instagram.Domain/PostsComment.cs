using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class PostsComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Posts")]
        public int PostId { get; set; }
        public virtual Posts Posts { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users Users { get; set; }

        [Required, MaxLength(50)]
        public string Comment { get; set; }
        public DateTime CreatedDate { get; set; }
        public int SubComment { get; set; }
    }
}
