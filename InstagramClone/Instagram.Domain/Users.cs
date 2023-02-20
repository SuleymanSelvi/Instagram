using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Instagram.Domain
{
    public class Users
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MaxLength(30)]
        public string Name { get; set; }

        [Required, MaxLength(12)]
        public string Password { get; set; }

        [Required, MaxLength(25)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string About { get; set; }
        public string Image { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; } 
    }
}
