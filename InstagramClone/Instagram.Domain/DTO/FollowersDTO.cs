using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class FollowersDTO
    {
        public bool IsFollow { get; set; }
        public int FollowCount { get; set; }
        public int FollowerCount { get; set; }
    }
}
