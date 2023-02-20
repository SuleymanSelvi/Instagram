using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class Storys
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Images { get; set; }

        [ForeignKey("Users")]
        public int UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public int FileDuration { get; set; }
    }
}
