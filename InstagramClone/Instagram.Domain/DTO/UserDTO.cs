using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Images { get; set; }
        public string Email { get; set; }
        public string About { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int FollowCount { get; set; }
        public int FollowerCount { get; set; }
        public bool IsFollow { get; set; }
        public bool IsBlock { get; set; }
        public int PostCount { get; set; }
    }
}
