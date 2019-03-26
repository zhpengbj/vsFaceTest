using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FaceTest
{
    /// <summary>
    /// web服务
    /// </summary>
    public class CHttpServiceHandler
    {
        private static CHttpServiceHandler _HttpServiceHandler = null;
        private static DShowMessage _ShowMessage;
        private static DDoResult _DoResult;
        private static HttpListener httpobj; 
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="doResult">处理流程</param>
        /// <param name="Port">监听端品</param>
        /// <param name="dShowMessage">日志委托</param>
        public static void  InitHttpService(DDoResult doResult, int Port, DShowMessage  dShowMessage)
        {
            if (_HttpServiceHandler==null)
            {
                _ShowMessage = dShowMessage;
                _DoResult = doResult;
                _HttpServiceHandler = new CHttpServiceHandler( Port);
                

            }
           
        }
        private CHttpServiceHandler(int Port)
        {
            string strUrl= string.Format(@"http://*:{0}/",Port);
            httpobj = new HttpListener();
            showMsg(string.Format("Start HttpListener：[{0}]", strUrl));
            httpobj.Prefixes.Add(strUrl);
            //启动监听器
            httpobj.Start();
            //异步监听客户端请求，当客户端的网络请求到来时会自动执行Result委托
            //该委托没有返回值，有一个IAsyncResult接口的参数，可通过该参数获取context对象
            httpobj.BeginGetContext(Result_Handler, null);
            //Console.WriteLine("服务端初始化完毕，正在等待客户端请求,时间：{DateTime.Now.ToString()}\r\n");
            //this.BeginInvoke(new dispList(dispReciveMessage), this.listBox1,"服务端 " + strUrl + " 初始化完毕");
            showMsg("Start HttpListener  Ok:" + strUrl );
        }

        private void Result_Handler(IAsyncResult ar)
        {
            //当接收到请求后程序流会走到这里
            //继续异步监听
            httpobj.BeginGetContext(Result_Handler, null);
            var guid = Guid.NewGuid().ToString();
            Console.ForegroundColor = ConsoleColor.White;
            //showMsg("接到新的请求:" + guid);
            //this.BeginInvoke(new dispList(dispReciveMessage), this.listBox1, "接到新的请求:"+ guid + ";时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            //获得context对象
            var context = httpobj.EndGetContext(ar);
            _DoResult?.Invoke(context);
        }
        private void showMsg(string obj)
        {
            _ShowMessage?.Invoke(obj);

        }
    }

    public delegate void DDoResult(HttpListenerContext context);
    public delegate void DShowMessage(string obj);
}
