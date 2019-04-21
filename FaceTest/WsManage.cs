using System;
using System.Collections.Generic;
using System.Linq;

using Fleck;
using Newtonsoft.Json;
using static FaceTest.MModel_Ws;

namespace wsTest_Fleck
{
    public class WSManage
    {
        #region 定义变量
        /// <summary>
        /// 客户端url以及其对应的Socket对象字典
        /// </summary>
        static Dictionary<string, IWebSocketConnection> dic_Sockets = new Dictionary<string, IWebSocketConnection>();
        
        /// <summary>
        /// 发送消息和相应回调方法的队列
        /// </summary>
        private static Dictionary<string, DoCallBack> MessageList = new Dictionary<string, DoCallBack>();


        #endregion


        private static DoCallBack _DoCallBack;
        private static DoMessage _DoMessage;
        public static void Init(string Url,DoCallBack _callBack, DoMessage _doMessage)
        {
            FleckLog.Level = LogLevel.Debug;
            _DoCallBack = _callBack;
            _DoMessage = _doMessage;
            //var allSockets = new List<IWebSocketConnection>();



            //var server = new WebSocketServer("ws://0.0.0.0:4696");
            var server = new WebSocketServer(Url);
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    //allSockets.Add(socket);
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    //dic_Sockets.Add(clientUrl, socket);
                    Console.WriteLine(DateTime.Now.ToString() + "|服务器:和客户端网页:" + clientUrl + " 建立WebSock连接！");

                };
                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    //allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);
                    TaskInfo taskInfo = JsonConvert.DeserializeObject<TaskInfo>(message);
                    //判断 设备的合法性
                    if (!CheckDevice(taskInfo.GetDeviceNo()))
                    {
                        return;
                    }
                    string taskName = taskInfo.GetTaskName();
                    //得到请求后的返回处理
                    DoCallBack callBack = MessageList[taskName];
                    if (callBack != null)
                    {
                        callBack.Invoke(taskInfo);
                    }
                    else
                    {
                        _DoMessage?.Invoke(socket, taskInfo);
                    }
                };
            });


            var input = Console.ReadLine();
            while (input != "exit")
            {
                foreach (var socket in dic_Sockets.Values)
                {
                    socket.Send(input);
                }
                input = Console.ReadLine();
            }
        }
        /// <summary>
        /// 通过设备NO，判断是否为合法设备
        /// </summary>
        /// <param name="DeviceNo"></param>
        /// <returns></returns>
        private static bool CheckDevice(string DeviceNo)
        {
            return dic_Sockets.Keys.Contains(DeviceNo);
        }
        /// <summary>
        /// 处理接收到的消息
        /// 是设备请求的，不是服务器下发命令给设备
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="taskInfo"></param>
        

        /// <summary>
        /// 发送信息，执行任务
        /// </summary>
        /// <param name="DeviceNo"></param>
        /// <param name="taskInfo"></param>
        public static void sendMsg(TaskInfo taskInfo)
        {
            sendMsg(taskInfo.DeviceNo, taskInfo);
        }
        private static void sendMsg(string DeviceNo, TaskInfo taskInfo)
        {
            string taskName = taskInfo.GetTaskName();
            //if (callBack != null)
            {
                MessageList.Add(taskName, _DoCallBack);
            }
            if (dic_Sockets[DeviceNo] != null)
            {
                dic_Sockets[DeviceNo].Send(taskInfo.GetString());
            }
        }

        public static void addDevice(string deviceNo, IWebSocketConnection socket)
        {
            if (!dic_Sockets.ContainsKey(deviceNo))
            {
                dic_Sockets.Add(deviceNo, socket);
            }
            else
            {
                dic_Sockets[deviceNo] = socket;
            }
        }

    }
}
