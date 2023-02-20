using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.Models
{
    public class UserBlock
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Users")]
        public int BlockedId { get; set; }

        [ForeignKey("Users")]
        public int BlockId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
