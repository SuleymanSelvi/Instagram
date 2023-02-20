using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instagram.Domain
{
    public class InstagramApiResponse<T> where T : class
    {
        public bool Success  { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
        public List<T> DataList { get; set; }
        public int Count { get; set; }
        public bool OptionalBoolean { get; set; }
    }
}
