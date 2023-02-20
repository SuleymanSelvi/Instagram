using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class UserBlockDTO
    {
        public int Id { get; set; }
        public int BlockedId { get; set; }
        public int BlockId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsBlock { get; set; }
        public bool IsFollow { get; set; }
    }
}
