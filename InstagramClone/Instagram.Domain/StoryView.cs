using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class StoryView
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Storys")]
        public int StoryId { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int UserId { get; set; }

        public int CreatedDate { get; set; }
    }
}
