using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceTest
{
    public class User
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userKey { get; set; }
        public string imageId { get; set; }
        public string imageKey { get; set; }
        public string imageBase64 { get; set; }
        public int direct { get; set; }

    }
    public class ResultInfo
    {
        public bool success { get; set; }
        public int result { get; set; }
        public int msgtype { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }
}
