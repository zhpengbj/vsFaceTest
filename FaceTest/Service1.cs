using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;

using System.IO;
using System.Web;
using System.Collections.Specialized;
using System.IO.Pipes;
using System.Security.Principal;
using Newtonsoft.Json;
namespace FaceTest
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码和配置文件中的类名“Service1”。
    public class Service1 : IService1
    {
        NamedPipeClientStream pipeClient =
                    new NamedPipeClientStream("127.0.0.1", "FaceTestPip",
                        PipeDirection.InOut, PipeOptions.Asynchronous,
                        TokenImpersonationLevel.None);
        StreamWriter sw = null;
        public Service1()
        {
            pipeClient.Connect();
            sw = new StreamWriter(pipeClient);
            sw.AutoFlush = true;
        }

        public void DoWork()
        {
        }

        [WebGet(UriTemplate = "{content}")]
        public string Get(string content)
        {
            return "server echo: " + content;
        }
        [WebInvoke(UriTemplate = "PostTest", Method = "POST", RequestFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        public string PostTest(Stream stream)
        {
            StreamReader sr = new StreamReader(stream);
            string s = sr.ReadToEnd();
            sr.Dispose();
            NameValueCollection nvc = HttpUtility.ParseQueryString(s);

            string Verify = nvc["verify"];
            sw.WriteLine(Verify);
            VerifyReturn vr = new VerifyReturn();
            vr.result = 1;
            vr.success = "true";
            return JsonConvert.SerializeObject(vr);
        }


    }
}
