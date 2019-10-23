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

        public string cardNo { get; set; }

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
        /// 设备编号，用户可以自定义
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
                id, type, userId, userName, path, SendPassType == 0 ? "实时" : "历史", score);
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
        /// <summary>
        /// 结果 ，设置成1就行
        /// </summary>
        public int result { get; set; }
        /// <summary>
        /// 是否成功，影响设备后续的操作，比如开闸，韦根输出，文字，语音提示等
        /// </summary>
        public bool success { get; set; }

        /// <summary>
        /// 提示类型，影响语音提示
        /// </summary>
        public int msgtype { get; set; }
        /// <summary>
        /// 提示文字，影响文字提示
        /// </summary>
        public string msg { get; set; }

        public object data { get; set; }
    }
    public class VerifyReturnTest
    {
        public int result { get; set; }
        public bool success { get; set; }

        public string msg { get; set; }
    }
    public class HeartBeatReturn
    {
        /// <summary>
        /// 系统时间，用于设备校对时间
        /// </summary>
        public string time { get; set; }
    }

    /// <summary>
    /// 运行参数
    /// </summary>
    public class AppRunConfig
    {
        /// <summary>
        /// 尝试次数
        /// 可选：1,2,3,4,5
        /// </summary>
        public int attemptCount { get; set; }
        /// <summary>
        /// 变焦的比例
        /// 可选：0，0.5，1,1.5，2
        /// </summary>
        public float cameraMaxZoom { get; set; }
        /// <summary>
        /// 保留空间的比例
        /// </summary>
        public int deleteFile_Disk { get; set; }
        /// <summary>
        /// 是arm系统 
        /// </summary>
        public bool isArm { get; set; }
        /// <summary>
        /// 启用灯光
        /// </summary>
        public bool isLigth { get; set; }
        /// <summary>
        /// 启用活检
        /// </summary>
        public bool isOpenHacker { get; set; }
        /// <summary>
        /// 启用播放语音
        /// </summary>
        public bool isPlaySound { get; set; }
        /// <summary>
        /// 启用识别二维码
        /// </summary>
        public bool isQR { get; set; }
        /// <summary>
        /// 保存识别照片
        /// </summary>
        public bool isSaveImage { get; set; }
        /// <summary>
        /// 启用平台验证
        /// </summary>
        public bool isThirdPlatform { get; set; }
        /// <summary>
        /// 启用写log
        /// 调试问题时使用，工作状态正是不要启用。因为log文件会很大
        /// </summary>
        public bool isWriteLog { get; set; }
        /// <summary>
        /// 公司名称
        /// </summary>
        public string sCompanyName { get; set; }
        /// <summary>
        /// 停留时间
        /// 单位：秒
        /// </summary>
        public int sameFaceNexeRecognizeDt { get; set; }
        /// <summary>
        /// 识别图片时的质量
        /// 可选：0，0.5，1,1.5，2
        /// </summary>
        public float verifyScore { get; set; }
        /// <summary>
        /// 识别阀值
        /// </summary>
        public float verifyThreshold { get; set; }
    }

    /// <summary>
    /// 推送参数
    /// </summary>
    public class AppSendConfig
    {
        public string SendPassConfigStr { get; set; }
        public int SendPassType { get; set; }
        /// <summary>
        /// 设备ID
        /// 用户自定义
        /// </summary>
        public string devId { get; set; }
        /// <summary>
        /// 设备方向
        /// 1：进，2：出
        /// </summary>
        public string inOut { get; set; }
        /// <summary>
        /// 历史记录推送时间间隙
        /// 单位：秒
        /// </summary>
        public int sendHisDataInterval { get; set; }
    }
    /// <summary>
    /// 更新参数
    /// </summary>
    public class AppUpdaeDataConfig
    {
        public string UpdateDataType { get; set; }
        public int UpdateDataInterval { get; set; }
    }

    #region 对象

    /// <summary>
    /// IP设置
    /// </summary>
    public class NetworkInfo
    {

        public string ip { get; set; }
        public string subnetMask { get; set; }

        public string gateway { get; set; }

        public bool isDHCPMod { get; set; }
        public string DNS1 { get; set; }
        public string DNS2 { get; set; }
    }
    /// <summary>
    /// 管理URL类
    /// </summary>
    public class UrlPar
    {
        public string downNewApkUrl { get; set; }
        public string getNewApkVersionUrl { get; set; }
        public string heartBeatUrl { get; set; }
        public string identifyCallBack { get; set; }
        public string identifyCallBack_His { get; set; }
        public string verifyCallBack { get; set; }
        /// <summary>
        /// 设备运行日志
        /// 包括启动、App重启，设备重启和设备请求授权
        /// </summary>
        public string devRunLogUrl { get; set; }
    }
    /// <summary>
    /// 心跳包数据
    /// </summary>
    public class DevicesHeartBeat
    {
        /// <summary>
        /// 设备编号，可用户自定义，如果没有自定义，则返回机器码。
        /// </summary>
        public string deviceKey { get; set; }
        /// <summary>
        /// 设备类型
        /// 具体定义可咨询厂家
        /// </summary>
        public int deviceType { get; set; }
        /// <summary>
        /// 设备的机器码
        /// </summary>
        public string deviceMachineCode { get; set; }
        /// <summary>
        /// 运行时间
        /// 格式：天d小时h分钟m
        /// </summary>
        public string runtime { get; set; }
        /// <summary>
        /// 系统运行时间
        /// </summary>
        public string systemruntime { get; set; }
        /// <summary>
        /// APP启动的时间
        /// </summary>
        public string starttime { get; set; }
        /// <summary>
        /// 当前系统时间
        /// </summary>
        public string time { get; set; }
        public string ip { get; set; }
        public int personCount { get; set; }
        public int faceCount { get; set; }
        public int PassTimeCount { get; set; }
        public string version { get; set; }
        /// <summary>
        /// APP占用内存
        /// </summary>
        public float memory { get; set; }
        /// <summary>
        /// 系统可用内存
        /// </summary>
        public float availMem { get; set; }
        /// <summary>
        /// 系统总内存
        /// </summary>
        public float totalMem { get; set; }
        /// <summary>
        /// 硬盘大小
        /// </summary>
        public float totalDisk { get; set; }
        /// <summary>
        /// 设备的机器名
        /// </summary>
        public string buildModel { get; set; }
        /// <summary>
        /// 是否初始化
        /// 如果通信密码非空，则认为是初始化完成。
        /// 如果通信密码为空，则认为未初始化，不能操作设备
        /// </summary>
        public int isInited { get; set; }
        /// <summary>
        /// 是否授权
        /// </summary>
        public int isAuth { get; set; }
        /// <summary>
        /// 已使用的硬盘
        /// </summary>
        public float disk { get; set; }
        public bool isRoot { get; set; }
    }
    public class DevicesInfo_GetUpdate
    {
        /// <summary>
        /// 设备编号，可用户自定义，如果没有自定义，则返回机器码。
        /// </summary>
        public string deviceKey { get; set; }
        /// <summary>
        /// 设备的机器码
        /// </summary>
        public string deviceMachineCode { get; set; }
        /// <summary>
        /// 版本号名称
        /// </summary>
        public string version { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public int versionCode { get; set; }
    }
    public class FaceFind
    {
        public string faceId { get; set; }
        public string personId { get; set; }
        public int direct { get; set; }
        //public string faceImageFaileName { get; set; }
        public string faceImageKey { get; set; }
        public override string ToString()
        {
            return String.Format("faceId:[{0}],personId:[{1}],direct:[{2}],faceImageKey:[{3}]",
                faceId, personId, direct, faceImageKey);
        }

    }
    public class Face
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 人员姓名，不更新人员数据
        /// 只是根据此数据生成设备注册照片名
        /// </summary>
        public string userName { get; set; }
        //public string faceId { get; set; }
        /// <summary>
        /// 人员照片的序号
        /// </summary>
        public int direct { get; set; }
        /// <summary>
        /// 照片ID，唯一
        /// </summary>
        public string imageId { get; set; }
        /// <summary>
        /// 照片key,可用照片的md5值
        /// </summary>
        //public string faceImageFileName { get; set; }
        public string imageKey { get; set; }
        /// <summary>
        /// 照片base64
        /// </summary>
        public string imageBase64 { get; set; }
    }
    /// <summary>
    /// 人员带照片的对象
    /// </summary>
    public class PersonAndFace
    {
        /// <summary>
        /// 人员信息
        /// </summary>
        public Person person { get; set; }
        /// <summary>
        /// 对应的照片的信息
        /// </summary>
        public Face face { get; set; }
    }
    /// <summary>
    /// 人员对象
    /// </summary>
    public class Person
    {
        /// <summary>
        /// ID 
        /// 如果传入，格式：[A-Za-z0-9]{0,32}
        /// 可以为空，系统会自动生成ID，并在返回数据中体现
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 韦根卡号
        /// </summary>
        public string cardNo { get; set; }
        public override string ToString()
        {
            return String.Format("id:[{0}],name:[{1}],card:[{2}]", id, name, cardNo);
        }
       
    }
    public class PersonHaveTime:Person
    {
        public string remark { get; set; }
        /// <summary>
        /// 时段名称
        /// </summary>
        public string passTimeTypeName { get; set; }
        public override string ToString()
        {
            return String.Format("id:[{0}],name:[{1}],card:[{2}],remark:[{3}],passTimeTypeName:[{4}]", id, name, cardNo, remark, passTimeTypeName);
        }
    }
    public class UserSetPassTime
    {
        /// <summary>
        /// 用户ID 
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 时段的名称
        /// </summary>
        public string passTimeName { get; set; }
    }
    public class PassTimes
    {
        /// <summary>
        /// 时段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 时段列表
        /// </summary>
        public List<PassTime> passTimeList { get; set; }
    }
    /// <summary>
    /// 时段，有星期列表和时间段列表
    /// </summary>
    public class PassTime
    {

        /// <summary>
        /// 星期列表 值在[1，7]
        /// </summary>
        public List<string> WeekList;
        /// <summary>
        /// 时间段列表，可以多个
        /// </summary>
        public List<PassTimeOne> PassTimeByWeekList;
    }
    /// <summary>
    /// 时段段对象
    /// </summary>
    public class PassTimeOne
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name;
        /// <summary>
        /// 开始时间 hh:mi:ss
        /// </summary>
        public string Dt1;
        /// <summary>
        /// 结果时间 hh:mi:ss
        /// </summary>
        public string Dt2;
    }
    /// <summary>
    /// 当天识别的记录情况
    /// </summary>
    public class TodayRecord
    {
        /// <summary>
        /// 当天所有识别记录数
        /// </summary>
        public int All;
        /// <summary>
        /// 当天实时推送记录数
        /// </summary>
        public int SendNow;
        /// <summary>
        /// 当天历史推送记录数
        /// </summary>
        public int SendHis;
        /// <summary>
        /// 当天未推送记录数
        /// </summary>
        public int NotSend;
        /// <summary>
        /// 当天识别记录，但因为未设置回调URL，则不用回调
        /// </summary>
        public int NoCallUrl;


    }
    #endregion


    #region updateData 
    public class MUpdateDataResponse
    {
        public int code;
        public List<MUpdateData_User> data;
        public string msg;
    }
    public class MUpdateData_User
    {
        /***
         * 更新唯一标志
         */
        public string updateId;
        /***
         * 操作类型
         * 1: 增加数据
         * 2: 删除数据
         * 3: 修改数据
         */
        public int operateFlag;
        /***
         * 人员Id
         */
        public string userId;
        /***
         * 人员姓名
         */
        public string userName;
        /***
         * 照片的url
         */
        public string userFacePic;
        /***
         * 人员备注
         * 主要用于显示
         */
        public string userRemarks;
    }

    public class MUpdateDataCallbackRequest
    {
        public string machineNo { get; set; }
        public List<MUpdateDataResult> dataList;
    }
    public class MUpdateDataResult
    {
        public string updateId;
        public int code;
        public string msg;
    }
    #endregion

    #region 上传回调_002 冠宇
    /// <summary>
    /// 不包括照片
    /// </summary>
    public class MRecogRecord_002_Main
    {

        public int id;
        public string guid;
        public string machineCode;
        public string machineNo;
        public int recogType;
        public string userId;
        public string userName;
        public int ioDirection;
        public string ioTime;
        public float recogScore;
        public int sendPassType;

    }
    public class MRecogRecord_002:MRecogRecord_002_Main
    {

        public string base64;

    }
    public class MRecogRecordRuturn
    {
        public int code { get; set; }

        public string msg { get; set; }
    }
    #endregion

    public class MShowInfoDisplay
    {
        public int logic;
        /// <summary>
        /// 操作类型:1:人脸,2:卡片,3:二维码
        /// </summary>
        public int optype;
        public string accountInfo;
        public string name;
        public string registerImg;
        public bool showOK;
        public bool showCancel;
        public bool defaultResult;
        /// <summary>
        /// 余额
        /// </summary>
        public string balance;
        /// <summary>
        /// 消费金额
        /// </summary>
        public string monetary;
        /// <summary>
        /// 显示内容
        /// </string>
        public string displayText;
        public int displayTextColor;
        public int displayTextSize;

        /// <summary>
        /// 语音播放内容
        /// </summary>
        public string displayVoice;
        /// <summary>
        /// 页面等待时间
        /// 单位：秒,如果不传，默认5秒
        /// </summary>
        public int displayTime = 5;
    }

}
