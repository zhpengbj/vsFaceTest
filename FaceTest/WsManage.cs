using System;
using System.Collections.Generic;
using System.Linq;

using Fleck;
using Newtonsoft.Json;
using static FaceTest.MModel_Ws;

namespace FaceTest
{
    public class WSManage
    {
        #region 定义变量
        /// <summary>
        /// 
        /// </summary>
        private static Dictionary<string, string> ValidDevices = new Dictionary<string, string>();
        /// <summary>
        /// 客户端url以及其对应的Socket对象字典
        /// </summary>
        static Dictionary<string, IWebSocketConnection> dic_Sockets = new Dictionary<string, IWebSocketConnection>();
        
        /// <summary>
        /// 发送消息和相应回调方法的队列
        /// </summary>
        private static Dictionary<string, DoCallBack> MessageList = new Dictionary<string, DoCallBack>();


        #endregion
        /// <summary>
        /// 增加有效的设备
        /// </summary>
        /// <param name="deviceNo"></param>
        public static void AddValidDevice(string deviceNo)
        {
            if (!ValidDevices.Keys.Contains(deviceNo))
            {
                ValidDevices.Add(deviceNo, deviceNo);
            }
        }

        private static DoCallBack _DoCallBack;
        private static DoMessage _DoMessage;
        private static DoShowInfo _DoShowInfo;
        public static void Init(string Url,DoCallBack _callBack, DoMessage _doMessage, DoShowInfo _doShowInfo)
        {
            FleckLog.Level = LogLevel.Debug;
            _DoCallBack = _callBack;
            _DoMessage = _doMessage;
            _DoShowInfo = _doShowInfo;
            //var allSockets = new List<IWebSocketConnection>();



            //var server = new WebSocketServer("ws://0.0.0.0:4696");
            var server = new WebSocketServer(Url);
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    ShowInfo("Open!");
                    //allSockets.Add(socket);
                    string clientUrl = socket.ConnectionInfo.ClientIpAddress + ":" + socket.ConnectionInfo.ClientPort;
                    string deviceNo = "";
                    try
                    {
                        //从headers得到机器码
                        deviceNo = socket.ConnectionInfo.Headers["deviceno"];
                        }
                    catch
                    {

                    }
                    ShowInfo(DateTime.Now.ToString() + "|服务器:和客户端:" + clientUrl + " 建立WebSock连接！");
                    //判断是否可以接入
                    if (!string.IsNullOrEmpty(deviceNo))
                    {
                        bool isOk = ValidDevices.Keys.Contains(deviceNo);
                        ShowInfo(string.Format("{0}|服务器:请求接入结果[{1}]", DateTime.Now.ToString(), isOk));
                        if (isOk)
                        {
                            if (dic_Sockets.Keys.Contains(deviceNo)){
                                dic_Sockets[deviceNo]= socket;
                            }
                            else
                            {
                                dic_Sockets.Add(deviceNo, socket);
                            }
                            return;
                        }
                    }else
                    {
                        ShowInfo(string.Format("{0}|服务器:请求接入失败,原因:未传入DeviceNo", DateTime.Now.ToString()));
                    }
                    //如果不是有效的设备，则断开
                    socket.Close();


                };
                socket.OnClose = () =>
                {
                    ShowInfo("Close!");
                    //allSockets.Remove(socket);
                };
                socket.OnMessage = message =>
                {
                    ShowInfo(message);
                    try
                    {

                        TaskInfo taskInfo = JsonConvert.DeserializeObject<TaskInfo>(message);
                        //判断 设备的合法性
                            if (!CheckDevice(taskInfo.GetDeviceNo()))
                            {
                                return;
                            }
                        string taskName = taskInfo.GetTaskName();
                        //得到请求后的返回处理
                        if (MessageList.Keys.Contains(taskName))
                        {
                            DoCallBack callBack = MessageList[taskName];
                            if (callBack != null)
                            {
                                callBack.Invoke(taskInfo);
                                return;
                            }

                        }
                        //如果在请求回调列表中没有找到，则处理“接收”
                        _DoMessage?.Invoke(socket, taskInfo);
                    }
                    catch (Exception ex)
                    {
                        ShowInfo(ex.Message);
                    }
                };
            });


            //var input = Console.ReadLine();
            //while (input != "exit")
            //{
            //    foreach (var socket in dic_Sockets.Values)
            //    {
            //        socket.Send(input);
            //    }
            //    input = Console.ReadLine();
            //}
        }

        private static void ShowInfo(string str)
        {
            if (_DoShowInfo != null)
            {
                _DoShowInfo(str);
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
