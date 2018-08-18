﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Newtonsoft.Json;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Collections.Specialized;

public class SendMessage
{
    private static SendMessage one = null;
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