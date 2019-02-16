using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Collections.Specialized;
using System.Windows.Forms;

public class SendMessage
{
    private static object l = new object();
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
        try
        {
            pipeClient = new NamedPipeClientStream("127.0.0.1", "testpipe", PipeDirection.InOut, PipeOptions.Asynchronous, TokenImpersonationLevel.None);
            //连接pipi，准备发送消息
            pipeClient.Connect();
            sw = new StreamWriter(pipeClient);

            sw.AutoFlush = true;
        }catch(Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }
    public void Send(string message)
    {
        lock (l)
        {
            sw.WriteLine(message);
        }
    }
}

public class Verify
{
    public int id { get; set; }
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

public class DevicesHeartBeat
{
    public String deviceKey { get; set; }
    public String time { get; set; }
    public String ip { get; set; }
    public int personCount { get; set; }
    public int faceCount { get; set; }
    public String version { get; set; }
}