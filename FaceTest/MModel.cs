using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FaceTest
{
    /// <summary>
    /// 人员，照片类
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID，唯一标识
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 用户名字
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 用户特征值 
        /// </summary>
        public string userKey { get; set; }
        /// <summary>
        /// 照片的ID
        /// </summary>
        public string imageId { get; set; }
        /// <summary>
        /// 照片的特征值 
        /// </summary>
        public string imageKey { get; set; }
        /// <summary>
        /// 照片的Base64数据
        /// </summary>
        public string imageBase64 { get; set; }
        /// <summary>
        /// 照片的序号，默认为0
        /// </summary>
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
}
