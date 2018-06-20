﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Newtonsoft.Json;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Collections.Specialized;

public partial class _Default : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {

        VerifyReturn result = new VerifyReturn();
        try
        {
            StreamReader reader = new StreamReader(Request.InputStream, System.Text.Encoding.UTF8);
            string sJsonData = reader.ReadToEnd();
            NameValueCollection nvc = HttpUtility.ParseQueryString(sJsonData);
            string Verify = "";
            if (sJsonData.Length > 0)
            {

                Verify = nvc["verify"];
            }

            // string Verify = Request.Form["verify"];

            if (!string.IsNullOrEmpty(Verify))
            {
                SendMessage.GetSendMessage().Send(Verify);
            }
            result.result = 1;
            result.success = DateTime.Now.ToString()+Verify;
            ReturnPost(result);
        }
        catch (Exception ex)
        {
            result.result = 1;
            result.success = "false"+ex.ToString();
            ReturnPost(result);
        }
    }
    public void ReturnPost(object oReturn)
    {
        string sJsonReturn = JsonConvert.SerializeObject(oReturn);
        byte[] bReturn = System.Text.Encoding.UTF8.GetBytes(sJsonReturn);
        Stream webStream = Response.OutputStream;

        webStream.Write(bReturn, 0, bReturn.Length);

        webStream.Close();
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
}