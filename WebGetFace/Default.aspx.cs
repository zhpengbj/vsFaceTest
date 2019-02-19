using System;
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
            SendMessage.GetSendMessage().Send("测试web服务");
            result.result = 0;
            result.success = DateTime.Now.ToString() + ":web启动成功";
            ReturnPost(result);
        }
        catch (Exception ex)
        {
            result.result = 1;
            result.success = "false" + ex.ToString();
            ReturnPost(result);
        }
    }
    public void ReturnPost(object oReturn)
    {
        string sJsonReturn = JsonConvert.SerializeObject(oReturn);
        byte[] bReturn = System.Text.Encoding.Default.GetBytes(sJsonReturn);
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

}