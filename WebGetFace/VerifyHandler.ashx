<%@ WebHandler Language="C#" Class="VerifyHandler" %>

using System;
using System.Web;

using Newtonsoft.Json;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Collections.Specialized;

public class VerifyHandler : IHttpHandler {

    private static int COSTCOUNT = 100;
    private static int NowCost = COSTCOUNT;
    public void ProcessRequest(HttpContext context)
    {

        context.Response.ContentType = "text/plain";

        VerifyReturn result = new VerifyReturn();
        try
        {
            string Verify = context.Request["verify"];
            Verify v = null;
            if (!string.IsNullOrEmpty(Verify))
            {
                v = JsonConvert.DeserializeObject<Verify>(Verify);
                //SendMessage.GetSendMessage().Send(Verify);
            }
            else
            {

                Verify = "not find Verify";
            }
            NowCost--;
            result.result = 1;
            result.success = true;
            result.msg = string.Format("你好,{0}" + Environment.NewLine + "总次数[{1}],剩余[{2}]", v != null ? v.userName:"未知", COSTCOUNT,NowCost);
            result.msgtype = 0;
            context.Response.Write(JsonConvert.SerializeObject(result));
        }
        catch (Exception ex)
        {
            result.result = 1;
            result.success = false;
            context.Response.Write(JsonConvert.SerializeObject(result));
        }
    }

    public bool IsReusable {
        get {
            return false;
        }
    }

    public class SendMessage{
        private static SendMessage one =null;
        public static SendMessage GetSendMessage()
        {
            if (one == null)
            {
                one = new SendMessage();
                one.init();
            }
            return one;
        }
        private NamedPipeClientStream pipeClient;
        private StreamWriter sw;
        private void init()
        {
            pipeClient = new NamedPipeClientStream("127.0.0.1", "testpipe", PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
            //连接pipi，准备发送消息
            pipeClient.Connect();
            sw = new StreamWriter(pipeClient);

            sw.AutoFlush = true;
        }
        public void Send(string message)
        {
            sw.WriteLine(message);
        }
    }

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
        public bool success { get; set; }
        public int msgtype { get; set; }
        public string msg { get; set; }


    }

}