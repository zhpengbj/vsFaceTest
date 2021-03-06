﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Fleck;
using Newtonsoft.Json;

namespace FaceTest
{
    public class MModel_Ws
    {
        public delegate void DoCallBack(MModel_Ws.ResultInfo resultInfo);
        public delegate void DoMessage(IWebSocketConnection socket, TaskInfo taskInfo);
        public delegate void DoShowInfo(string str);

        public class ResultInfo
        {
            public string taskname { get; set; }
            public bool success { get; set; }
            public int result { get; set; }
            public int msgtype { get; set; }
            public string msg { get; set; }
            public string data { get; set; }

            


        }
        public static ResultInfo GetSuccess(string taskName)
        {
            ResultInfo resultInfo = new ResultInfo();
            resultInfo.taskname = taskName;
            resultInfo.success = true;
            resultInfo.result = 1;
            resultInfo.msgtype = 0;
            resultInfo.msg = "";
            resultInfo.data = "";
            return resultInfo;


        }
        public enum ETaskType
        {
            /// <summary>
            /// 设备请求接入 101
            /// </summary>
            [Description("设备请求接入")]
            U_AskConnect = 101,
            /// <summary>
            /// 新增或更新人员 201
            /// </summary>
            [Description("新增或更新人员")]
            D_Person_CreateOrUpdate = 201,
            /// <summary>
            /// 删除人员 202
            /// </summary>
            [Description("删除人员")]
            D_Person_Delete = 202,
            /// <summary>
            /// 查找人员 203
            /// </summary>
            [Description("查找人员")]
            D_Person_Find = 203,
            /// <summary>
            /// 新增或更新照片 204
            /// </summary>
            [Description("新增或更新照片")]
            D_Face_CreateOrUpdate = 204,
            /// <summary>
            /// 删除照片 205
            /// </summary>
            [Description("删除照片")]
            D_Face_Delete = 205,
            /// <summary>
            /// 查找照片 206
            /// </summary>
            [Description("查找照片")]
            D_Face_Find = 206,

            /// <summary>
            /// 新增或更新分组 207
            /// </summary>
            [Description("新增或更新分组")]
            D_PassTime_CreateOrUpdate = 207,
            /// <summary>
            /// 删除分组 208
            /// </summary>
            [Description("删除分组")]
            D_PassTime_Delete = 208,
            /// <summary>
            /// 对人员进行权限分组 209
            /// </summary>
            [Description("对人员进行权限分组")]
            D_User_SetPassTime = 209,
            /// <summary>
            /// 数据更新 210
            /// </summary>
            [Description("数据更新")]
            D_Refresh = 210,
            /// <summary>
            /// 询问设备 211
            /// </summary>
            [Description("询问设备")]
            D_GetDeviceInfo = 211,
            /// <summary>
            /// 设置时间 212
            /// </summary>
            [Description("询问设备")]
            D_SetTime = 212

        }
        /// <summary>
        /// 离线消息
        /// </summary>
        public class TaskInfo
        {
            
            public TaskInfo(ETaskType _type, string _name,string _deviceNo, string _obj)
            {
                Type = _type;
                Name = _name;
                DeviceNo = _deviceNo;
                Obj = _obj;
            }
            public ETaskType Type { get; set; }
            /// <summary>
            /// 任务的名称，唯一
            /// 用来请求回调用
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 设备的编号 
            /// </summary>
            public string DeviceNo { get; set; }
            public string Obj { get; set; }

            public string GetString()
            {
                return JsonConvert.SerializeObject(this);
            }
            /// <summary>
            /// 得到任务的编号，唯一性
            /// 暂时由：任务类型_GUID 组合
            /// </summary>
            /// <returns></returns>
            public string GetTaskName() {
                return Name;

            }
            public string GetDeviceNo()
            {
                return DeviceNo;
            }
        }
        /// <summary>
        /// 接入设备信息
        /// </summary>
        public class ClientInfo
        {
            public string Ip { get; set; }
            public int Port { get; set; }
            public string Code { get; set; }
        }

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

            public override string ToString()
            {
                return string.Format("id:[{0}],name:[{1}],card:[{2}]", id, name, cardNo);
            }
            /// <summary>
            /// 韦根卡号
            /// </summary>
            public string cardNo { get; set; }
        }
    }

}
