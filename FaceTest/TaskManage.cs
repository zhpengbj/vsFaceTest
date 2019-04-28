using System;
using System.Collections.Generic;
using Fleck;
using Newtonsoft.Json;
using static FaceTest.MModel_Ws;

namespace FaceTest
{
    /// <summary>
    /// 任务管理
    /// </summary>
    public class TaskManage
    {
        public static void Init(string Url)
        {
            WSManage.Init(Url, CallBack, DoMessage, doShowInfo);
            

        }
        #region 显示日志
        private static DoShowInfo doShowInfo;
        public static void SetShowInfo(DoShowInfo _DoShowInfo)
        {
            doShowInfo = _DoShowInfo;
        }
        private static void ShowInfo(string str)
        {
            if (doShowInfo!=null)
            {
                doShowInfo(str);
            }
        }
        #endregion
        /// <summary>
        /// 任务的回调方法
        /// </summary>
        private static Dictionary<string, TaskInfo> taskList = new Dictionary<string, TaskInfo>();
        public static void AddTask(TaskInfo taskInfo)
        {
            ShowInfo(string.Format("{0}:Type[{1}],[{2}]", "AddTask", taskInfo.Type, taskInfo.GetString()));
            taskList.Add(taskInfo.GetTaskName(), taskInfo);
            WSManage.sendMsg(taskInfo);
        }
        /// <summary>
        /// 发送任务后的回调方法
        /// </summary>
        /// <param name="taskInfo"></param>
        public static void CallBack(MModel_Ws.ResultInfo resultInfo)
        {
            //显示 
            //ShowInfo(string.Format("{0}:TaskName[{1}],"+System.Environment.NewLine+" info:[{2}]", "TaskCallBack", resultInfo.taskname, JsonConvert.SerializeObject(resultInfo)));
            ShowInfo(string.Format("{0}:TaskName[{1}]", "TaskCallBack", resultInfo.taskname));
            //从任务列表中移除
            taskList.Remove(resultInfo.taskname);

        }
        /// <summary>
        /// 处理请求数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="taskInfo"></param>
        private static void DoMessage(IWebSocketConnection socket, TaskInfo taskInfo)
        {
            try
            {
                //switch (taskInfo.Type)
                //{
                //    //请求接入
                //    case ETaskType.U_AskConnect:
                //        string deviceNo = taskInfo.DeviceNo;
                //        string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                //        ShowInfo(string.Format("{0}|服务器:DeviceNo[{1}]-{2},请求接入", DateTime.Now.ToString(), deviceNo, clientUrl));
                //        //TODO 判断是否可以接入
                //        bool isOk = true;
                //        ShowInfo(string.Format("{0}|服务器:请求接入结果[{1}]", DateTime.Now.ToString(), isOk));
                //        if (!isOk)
                //        {
                //            return;
                //        }
                //        WSManage.addDevice(deviceNo, socket);

                //        MModel_Ws.ResultInfo resultInfo =  GetSuccess(taskInfo.GetTaskName());
                //        socket.Send(JsonConvert.SerializeObject(resultInfo));
                //        break;

                //}
            }
            catch (Exception ex)
            {
                ShowInfo(ex.ToString());
            }


        }
    }
}
