using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class Posts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Images { get; set; }

        [Required, MaxLength(100)]
        public string Description { get; set; }

        [ForeignKey("Tags")]
        public int TagsId { get; set; }
        public virtual Tags Tags { get; set; }

        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual Users Users { get; set; }
    }
}
