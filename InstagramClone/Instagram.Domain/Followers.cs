using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class Followers
    { 

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int Follow { get; set; }
        public virtual Users UsersFollow { get; set; }

        [Required]
        [ForeignKey("Users")]
        public int Follower { get; set; }
        public virtual Users UsersFollower { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
