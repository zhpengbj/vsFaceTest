<%@ WebHandler Language="C#" Class="HeartBeat" %>

using System;
using System.Web;

using Newtonsoft.Json;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Collections.Specialized;

/// <summary>
/// 心跳包
/// </summary>
public class HeartBeat : IHttpHandler {

    private static int HeartBeatCount = 0;
    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";

        VerifyReturn result = new VerifyReturn();
        try
        {
            //接收到的数据
            string rStr = context.Request["info"];
            DevicesHeartBeat v = null;
            if (!string.IsNullOrEmpty(rStr))
            {

                HeartBeatCount++;
                v = JsonConvert.DeserializeObject<DevicesHeartBeat>(rStr);
                SendMessage.GetSendMessage().Send(string.Format("HeartBeat receive[{0}] data:{1}",HeartBeatCount,rStr));
                //SendMessage.GetSendMessage().Send(Verify);
            }
            else
            {

                rStr = "not find info";
            }
            
            result.result = 1;
            result.success = true;
            result.msg = rStr;
            result.msgtype = 0;
            context.Response.Write(JsonConvert.SerializeObject(result));
        }
        catch (Exception ex)
        {
            result.result = 1;
            result.success = false;
            result.msgtype = -100;
            result.msg = ex.Message;
            context.Response.Write(JsonConvert.SerializeObject(result));
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    //public class SendMessage{
    //    private static SendMessage one =null;
    //    public static SendMessage GetSendMessage()
    //    {
    //        if (one == null)
    //        {
    //            one = new SendMessage();
    //            one.init();
    //        }
    //        return one;
    //    }
    //    private NamedPipeClientStream pipeClient;
    //    private StreamWriter sw;
    //    private void init()
    //    {
    //        pipeClient = new NamedPipeClientStream("127.0.0.1", "testpipe", PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
    //        //连接pipi，准备发送消息
    //        pipeClient.Connect();
    //        sw = new StreamWriter(pipeClient);

    //        sw.AutoFlush = true;
    //    }
    //    public void Send(string message)
    //    {
    //        sw.WriteLine(message);
    //    }
    //}

    //public class Verify
    //{
    //    public string deviceKey { get; set; }
    //    public string userId { get; set; }
    //    public string userName { get; set; }
    //    public string type { get; set; }
    //    public string base64 { get; set; }

    //}
    //public class VerifyReturn
    //{
    //    public int result { get; set; }
    //    public bool success { get; set; }
    //    public int msgtype { get; set; }
    //    public string msg { get; set; }


    //}

}