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
        /// <summary>
        /// 数据的标识，可以用来滤重
        /// 也可用id滤重，得考虑设备会被还原出厂，ID有可以会从1重新开始，所以增加了此数据项
        /// </summary>
        public string guid { get; set; }
        /// <summary>
        /// 本地记录的ID
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string deviceKey { get; set; }
        /// <summary>
        /// 机器码
        /// </summary>
        public string machineCode { get; set; }
        /// <summary>
        /// 识别人员编号 
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 识别人员姓名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 识别结果
        /// 1，识别正常，mes=姓名,矩形框为绿色
        /// 2，活检未通过，mes=提示信息，矩形框为黄
        /// 3，未注册，mes=提示信息，矩形框为红色  
        /// 4，找到人员，后台验证无反应 蓝色       
        /// 5，找到人员，后台验证失败 蓝色         
        /// 6，找到人员，未授权不能通过（通过时段判断） 蓝色
        /// </summary>
        public int type { get; set; }
        /// <summary>
        /// 识别抓拍时照片的Ftp
        /// </summary>
        public string path { get; set; }
        /// <summary>
        /// 识别抓拍时照片的base64
        /// </summary>
        public string base64 { get; set; }

        /// <summary>
        /// 记录类型
        /// 0：实时
        /// 1：历史
        /// </summary>
        public int SendPassType { get; set; }
        public override string ToString()
        {
            return String.Format("id[{0}],type[{1}], userId[{2}],userName[{3}],sendPassType[{5}],Score[{6}]" + (!string.IsNullOrEmpty(path) ? Environment.NewLine : "") + " path[{4}]",
                id, type, userId, userName, path, SendPassType == 0 ? "实时" : "历史",score);
        }
        /// <summary>
        /// 方向
        /// 1：进
        /// 2：出
        /// </summary>
        public int direction { get; set; }
        /// <summary>
        /// 识别时的时间
        /// </summary>
        public string time { get; set; }
        /// <summary>
        /// 推送批次
        /// 历史退送时，会有此字段 
        /// 格式：yyyy-mmm-dd_推送次数_本批次需要上传的记录数
        /// </summary>
        public string banch { get; set; }

        /// <summary>
        /// 识别人脸分数
        /// </summary>
        public float score { get; set; }

    }
    /// <summary>
    /// 返回对象
    /// </summary>
    public class VerifyReturn
    {
        public int result { get; set; }
        public bool success { get; set; }

        public int msgtype { get; set; }
        public string msg { get; set; }

        public object data { get; set; }
    }
}
