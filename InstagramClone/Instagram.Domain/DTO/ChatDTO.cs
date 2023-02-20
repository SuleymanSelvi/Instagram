using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain.DTO
{
    public class ChatDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Images { get; set; }
        public string Email { get; set; }
        public string About { get; set; }

        // DM
        public int MessageCount { get; set; }
        public bool isOnline { get; set; }
        public int isWriting { get; set; }
        public string LastMessage { get; set; }
        public bool IsCheck { get; set; }
    }
}
