using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class Notifications
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Posts")]
        public int PostId { get; set; }

        [ForeignKey("Users")]
        public int PostOwnerId { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsSeen { get; set; }
    }
}
