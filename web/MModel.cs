using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

    /// 人员，照片类
    /// </summary>
    public class User
    {
        public string userId { get; set; }
        public string userName { get; set; }
        public string userKey { get; set; }
        public string imageId { get; set; }
        public string imageKey { get; set; }
        public string imageBase64 { get; set; }
        public int direct { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string FilePath { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string FileName { get; set; }

    }
    /// <summary>
    /// 结果 
    /// </summary>
    public class ResultInfo
    {
        public bool success { get; set; }
        public int result { get; set; }
        public int msgtype { get; set; }
        public string msg { get; set; }
        public string data { get; set; }
    }
    /// <summary>
    /// 接收到人脸验证记录
    /// </summary>
    public class Verify
    {
        public string deviceKey { get; set; }
        public string userId { get; set; }
        public string userName { get; set; }
        public string type { get; set; }
        public string base64 { get; set; }

    }
    public class VerifyReturn
    {
        public int result { get; set; }
        public string success { get; set; }
    }
