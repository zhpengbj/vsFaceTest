<%@ WebHandler Language="C#" Class="Handler_His" %>

using System;
using System.Web;

using Newtonsoft.Json;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Collections.Specialized;


/// <summary>
/// 人脸识别回调，历史记录
/// </summary>
public class Handler_His : IHttpHandler {

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        VerifyReturn result = new VerifyReturn();
        try
        {
            string rStr = context.Request["verify"];

            if (!string.IsNullOrEmpty(rStr))
            {
                SendMessage.GetSendMessage().Send("Handler_His receive data:"+rStr);
                //SendMessage.GetSendMessage().Send(Verify);
            }
            else
            {

                rStr = "not find Verify";
            }
            result.result = 1;
            result.success = true;
            result.msg=DateTime.Now.ToString()+":" + rStr;
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

    

}

    
