using System;
using System.Collections.Generic;
using Fleck;
using Newtonsoft.Json;
using static FaceTest.MModel_Ws;

namespace wsTest_Fleck
{
    /// <summary>
    /// 任务管理
    /// </summary>
    public class TaskManage
    {
        public static void Init()
        {
            string Url = "";
            WSManage.Init(Url, CallBack,DoMessage);

        }
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
        private static Dictionary<string, TaskInfo> taskList = new Dictionary<string, TaskInfo>();
        public static void AddTask(TaskInfo taskInfo)
        {
            ShowInfo(string.Format("{0}:Type[{1}],[{2}]", "AddTask", taskInfo.Type, taskInfo.GetString()));
            taskList.Add(taskInfo.GetTaskName(), taskInfo);
            WSManage.sendMsg(taskInfo);
        }
        public static void CallBack(TaskInfo taskInfo)
        {
            ShowInfo(string.Format("{0}:Type[{1}],[{2}]", "TaskCallBack", taskInfo.Type, taskInfo.GetString()));
            taskList.Remove(taskInfo.GetTaskName());

        }
        private static void DoMessage(IWebSocketConnection socket, TaskInfo taskInfo)
        {
            try
            {
                switch (taskInfo.Type)
                {
                    //请求接入
                    case ETaskType.U_AskConnect:
                        string deviceNo = taskInfo.Obj.ToString();
                        string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                        Console.WriteLine(string.Format("{0}|服务器:DeviceNo[{1}]-{2},请求接入", DateTime.Now.ToString(), deviceNo, clientUrl));
                        //TODO 判断是否可以接入
                        bool isOk = true;
                        Console.WriteLine(string.Format("{0}|服务器:请求接入结果[{1}]", DateTime.Now.ToString(), isOk));
                        if (!isOk)
                        {
                            return;
                        }
                        WSManage.addDevice(deviceNo, socket);

                        ResultInfo resultInfo =  GetSuccess(taskInfo.GetTaskName());
                        socket.Send(JsonConvert.SerializeObject(resultInfo));
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }
    }
}
