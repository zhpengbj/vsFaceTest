using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;


using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Threading;

using System.Net;
using CassiniDev;
using FaceTest.Properties;

namespace FaceTest
{
    public partial class Form1 : Form
    {

        //private NamedPipeServerStream pipeServer;

        private const string PipeName = "testpipe";

        private const int PipeInBufferSize = 65535;

        private const int PipeOutBufferSize = 65535;

        private Encoding encoding = Encoding.UTF8;


        public Form1()
        {
            InitializeComponent();
            //pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 4, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
            //pipeServer = new NamedPipeServerStream
            //  (
            //      PipeName,
            //      PipeDirection.InOut,
            //      4,
            //      PipeTransmissionMode.Message,
            //      PipeOptions.Asynchronous | PipeOptions.WriteThrough
            //     // PipeInBufferSize,
            //     // PipeOutBufferSize
            //   );
        }
        NamedPipeServerStream pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 4, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
        private Settings settings = new Settings();
        /// <summary>
        /// 读取设置项
        /// </summary>
        private void LoadData()
        {
            this.tb_Path.Text = settings.tb_Path;
            this.tb_Url.Text = settings.tb_Url;
            this.tb_Pass.Text = settings.tb_Pass;
            this.tb_CallBackVerifyUrl.Text = settings.tb_CallBackVerifyUrl;
            this.tb_CallBackUrl.Text = settings.tb_CallBackUrl;
            this.bt_GetApkVersion.Text = settings.bt_GetApkVersion;
            this.tb_DownApkUrl.Text = settings.tb_DownApkUrl;
            this.tb_SetPassTime_UserId.Text = settings.tb_SetPassTime_UserId;
            this.tb_SetPassTime_PassTimeName.Text = settings.tb_SetPassTime_PassTimeName;
            this.tb_HeartBeatUrl.Text = settings.tb_HeartBeatUrl;
            this.tb_DeletePassTimeName.Text = settings.tb_DeletePassTimeName;

            this.tb_PersonAddOrUpdate_PersonId.Text = settings.tb_PersonAddOrUpdate_PersonId;
            this.tb_PersonAddOrUpdate_PersonName.Text = settings.tb_PersonAddOrUpdate_PersonName;
            this.tb_PersonDelete_PersonId.Text = settings.tb_PersonDelete_PersonId;
            this.tb_PersonFind_PersonId.Text = settings.tb_PersonFind_PersonId;


        }
        /// <summary>
        /// 保存设置项
        /// </summary>
        private void SaveData()
        {
            settings.tb_Path = this.tb_Path.Text.Trim();
            settings.tb_Url = this.tb_Url.Text.Trim();
            settings.tb_Pass = this.tb_Pass.Text.Trim();
            settings.tb_CallBackVerifyUrl = this.tb_CallBackVerifyUrl.Text.Trim();
            settings.tb_CallBackUrl = this.tb_CallBackUrl.Text.Trim();
            settings.bt_GetApkVersion = this.bt_GetApkVersion.Text.Trim();
            settings.tb_DownApkUrl = this.tb_DownApkUrl.Text.Trim();
            settings.tb_SetPassTime_UserId = this.tb_SetPassTime_UserId.Text.Trim();
            settings.tb_SetPassTime_PassTimeName = this.tb_SetPassTime_PassTimeName.Text.Trim();
            settings.tb_HeartBeatUrl = this.tb_HeartBeatUrl.Text.Trim();
            settings.tb_DeletePassTimeName = this.tb_DeletePassTimeName.Text.Trim();

            settings.tb_PersonAddOrUpdate_PersonId = this.tb_PersonAddOrUpdate_PersonId.Text.Trim();
            settings.tb_PersonAddOrUpdate_PersonName = this.tb_PersonAddOrUpdate_PersonName.Text.Trim();
            settings.tb_PersonDelete_PersonId = this.tb_PersonDelete_PersonId.Text.Trim();
            settings.tb_PersonFind_PersonId = this.tb_PersonFind_PersonId.Text.Trim();
            settings.Save();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
            //设置照片路径
            button2_Click(null, null);
            //设置设备URL
            button3_Click(null, null);

            ThreadPool.QueueUserWorkItem(delegate
            {
                AsyncCallback aa = null;
                pipeServer.BeginWaitForConnection(aa = (o) =>
                  {
                      NamedPipeServerStream server = (NamedPipeServerStream)o.AsyncState;
                      server.EndWaitForConnection(o);
                      StreamReader sr = new StreamReader(server);
                      StreamWriter sw = new StreamWriter(server);
                      string result = null;
                      string clientName = server.GetImpersonationUserName();
                      showMsg(clientName + "连接");
                      while (server.IsConnected)
                      {
                          result = sr.ReadLine();
                          if (result == null || result == "bye")
                              break;
                          showMsg(result);
                          ShowInfo(result);
                          //this.Invoke((MethodInvoker)delegate {
                          //    receiveMsg.Select(receiveMsg.Text.Length, 0);
                          //    receiveMsg.ScrollToCaret();
                          //});
                      }
                      showMsg(clientName + "断开连接，等待新的连接");
                      //this.Invoke((MethodInvoker)delegate { lsbMsg.Items.Add(clientName + "断开连接，等待新的连接"); });
                      server.Disconnect();//服务器断开，很重要！
                      server.BeginWaitForConnection(aa, server);//再次等待连接，更重要！！
                      Thread.Sleep(1000);
                      //如果web服务异常停止，则重新启动
                      StartService();
                  }, pipeServer);

            });
        }
        /// <summary>
        /// 显示人员信息
        /// </summary>
        /// <param name="result"></param>
        private void ShowInfo(string result)
        {
            if (result.IndexOf("HeartBeat receive") == 0)
            {
                //如果是心跳包数据，则退出
                return;
            }
            //通过data:截取、得到接收的对象
            string dataStr = result.Substring(result.IndexOf("data:") + 5, result.Length - result.IndexOf("data:") - 5);
            Verify v = JsonConvert.DeserializeObject<Verify>(dataStr);
            this.Invoke((MethodInvoker)delegate
            {
                if (v != null)
                {
                    lb_PersonId.Text = "用户ID:"+v.userId;
                    lb_PersonName.Text = "用户姓名:" + v.userName;
                    lb_Path.Text = "照片路径:" + v.path;
                    if (!string.IsNullOrEmpty(v.path))
                    {
                        pictureBox1.LoadAsync(v.path);
                    }
                    //showMsg(v.base64 != null ? v.base64.Length.ToString() : "");
                }
                else
                {
                    lb_PersonId.Text = "";
                    lb_PersonName.Text = "";
                }
            });


        }


        private void button1_Click(object sender, EventArgs e)
        {
            StartService();
        }
        private static CassiniDev.CassiniDevServer _HttpServer;//网页服务器

        public void StartService()
        {
            _HttpServer = new CassiniDevServer();
            string path = Application.StartupPath + "\\Home";
            int port = 8091;
            try
            {
                _HttpServer.StartServer(path, IPAddress.Any, port, "/", "");
                showMsg("启动完成");
            }
            catch (Exception ex)
            {
                showMsg(ex.ToString());
            }
        }
        private String FacePicPath = Application.StartupPath + @"\FacePicTest\";

        /// <summary>
        /// 照片文件对应的key,只在的文件目录
        /// </summary>
        private String FacePicDataPath = Application.StartupPath + @"\FacePicKey\";
        /// <summary>
        /// 出错文件 
        /// </summary>
        private String FacePicErrPath = Application.StartupPath + @"\FacePicErr\";
        private void button2_Click(object sender, EventArgs e)
        {
            FacePicPath = Application.StartupPath + String.Format(@"\{0}\", tb_Path.Text);
            label1.Text = FacePicPath;
        }
        private string Url = "";
        private void button3_Click(object sender, EventArgs e)
        {
            Url = tb_Url.Text;
        }
        private string Pass = "123";
        private void button4_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button4.Enabled = false;
                string postStr = string.Format("oldPass={0}&newPass={1}", Pass, Pass);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setPassWord";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("setPassWord 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button4.Enabled = true;

            }
        }
        public string GetFileMd5(string filepath)
        {
            if (string.IsNullOrEmpty(filepath) || !File.Exists(filepath))
            {
                return "";
            }
            FileStream fs = new FileStream(filepath, FileMode.Open, FileAccess.Read);
            try
            {
                return BytesToHexString(new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(fs));
            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
            }
        }
        public string BytesToHexString(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return "";
            StringBuilder sb = new StringBuilder();
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
        protected byte[] ImgToByt(Image img)
        {
            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            imagedata = ms.GetBuffer();
            return imagedata;
        }

        private byte[] createTxt(string fileName)
        {
            FileStream stream = new FileInfo(fileName).OpenRead();
            byte[] buffer = new byte[stream.Length + 1];

            stream.Read(buffer, 0, Convert.ToInt32(stream.Length));

            return buffer;
        }

        protected string ImgToBase64String(string Imagefilename)
        {
            try
            {
                byte[] arr = System.IO.File.ReadAllBytes(Imagefilename);
                showMsg("ImgToBase64String arr length:" + arr.Length);
                return Convert.ToBase64String(arr, Base64FormattingOptions.InsertLineBreaks);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //图片转为base64编码的字符串
        protected string ImgToBase64String2(string Imagefilename)
        {
            try
            {
                Bitmap bmp = new Bitmap(Imagefilename);

                MemoryStream ms = new MemoryStream();
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] arr = new byte[ms.Length];
                ms.Position = 0;
                ms.Read(arr, 0, (int)ms.Length);
                ms.Close();
                showMsg("ImgToBase64String2 arr length:" + arr.Length);
                return Convert.ToBase64String(arr);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }
        private User GetUserByFileName(FileInfo file)
        {
            User u = new User();
            //得到文件名。不包括扩展名
            string fileName = file.Name.Substring(file.Name.LastIndexOf("\\") + 1, (file.Name.LastIndexOf(".") - file.Name.LastIndexOf("\\") - 1));
            u.FileName = fileName;
            u.FilePath = file.FullName;
            string[] fileNameList = fileName.Split('_');
            if (fileNameList.Length == 1)
            {
                u.userId = fileNameList[0];
                u.userName = fileNameList[0];
                u.direct = 0;
            }
            if (fileNameList.Length == 2)
            {
                u.userId = fileNameList[0];
                u.userName = fileNameList[1];
                u.direct = 0;
            }
            if (fileNameList.Length == 3)
            {
                u.userId = fileNameList[0];
                u.userName = fileNameList[1];
                u.direct = Convert.ToInt32(fileNameList[2]);
            }
            string fileNameKey = FacePicDataPath + u.FileName + "_Key.dat";
            string fileNameBase64 = FacePicDataPath + u.FileName + "_Base64.dat"; ;
            if (!System.IO.File.Exists(fileNameKey))
            {
                u.imageBase64 = ImgToBase64String(file.FullName);
                u.imageKey = GetFileMd5(file.FullName);
            }
            else
            {
                u.imageBase64 = System.IO.File.ReadAllText(fileNameBase64);
                u.imageKey = System.IO.File.ReadAllText(fileNameKey);
            }
            showMsg("u.imageBase64 length:" + u.imageBase64.Length);
            //u.imageId = fileNameList[0] + "_" + u.direct.ToString();
            u.imageId = u.imageKey;
            return u;
            //string personId = aFile.Substring(aFile.LastIndexOf("\\") + 1, (aFile.LastIndexOf(".") - aFile.LastIndexOf("\\") - 1));
        }
        List<User> userList;
        Dictionary<string, User> userDic;
        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                button5.Enabled = false;
                stopwatch.Start();
                DirectoryInfo di = new DirectoryInfo(FacePicPath);
                FileInfo[] fis = di.GetFiles("*.jpg");
                userList = new List<User>();
                userDic = new Dictionary<string, User>();
                //Parallel.ForEach(fis, b =>
                foreach (FileInfo b in fis)
                {
                    stopwatchDetail.Reset();
                    stopwatchDetail.Start();
                    string aFile = b.Name;
                    User u = GetUserByFileName(b);
                    userList.Add(u);
                    if (!userDic.ContainsKey(u.imageKey))
                    {
                        userDic.Add(u.imageKey, u);
                    }
                    if (cb_saveImageKey.Checked)
                    {
                        System.IO.File.WriteAllText(FacePicDataPath + u.FileName + "_Key.dat", u.imageKey);
                        System.IO.File.WriteAllText(FacePicDataPath + u.FileName + "_Base64.dat", u.imageBase64);
                    }
                    stopwatchDetail.Stop();
                    showMsg(string.Format("处理照片列表,FileName[{0}],用时[{1}],特征值[{2}]", b.Name, stopwatchDetail.ElapsedMilliseconds, u.imageKey));
                }
                //);
                stopwatch.Stop();
                showMsg(string.Format("处理总人数[{0}],用时[{1}]", fis.Length, stopwatch.ElapsedMilliseconds));
            }
            catch (Exception ex)
            {
                showMsg(ex.ToString());
            }
            finally
            {
                button5.Enabled = true;
            }
        }


        System.Diagnostics.Stopwatch stopwatchDetail = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public delegate void dShowInfo(string str);
        public void showMsg(string msg)
        {
            {
                //在线程里以安全方式调用控件
                if (receiveMsg.InvokeRequired)
                {
                    dShowInfo _myinvoke = new dShowInfo(showMsg);
                    receiveMsg.Invoke(_myinvoke, new object[] { msg });
                }
                else
                {
                    string s = msg;
                    if (!string.IsNullOrEmpty(msg))
                    {
                        s = string.Format("[{0}],{1}\r\n", DateTime.Now.ToString("mm:ss"), msg);
                    }
                    else
                    {
                        s = "\r\n";
                    }

                    receiveMsg.AppendText(s);
                    receiveMsg.Select(receiveMsg.Text.Length, 0);
                    receiveMsg.ScrollToCaret();
                }
            }
        }


        private string GetImageKeyList()
        {
            string ret = "";
            foreach (User us in userList)
            {
                ret += us.imageKey + ",";
            }

            return ret.ToString().TrimEnd(',');
        }
        private void SendDevRefreshData()
        {
            tb_MachineCode.Text = "";
            try
            {
                button9.Enabled = false;
                string postStr = string.Format("pass={0}", Pass);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/refresh";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        tb_MachineCode.Text = res.data;
                        showMsg("refresh 成功:"+res.msg);
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button9.Enabled = true;

            }
        }
        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                button6.Enabled = false;
                //删除imageKeyList之外的照片
                string imageKeys = GetImageKeyList();
                bool isDelete = true;
                string postStr = string.Format("pass={0}&isDelete={1}&imageKeys={2}", Pass, isDelete.ToString().ToLower(), imageKeys);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/user/findDifference";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);




                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        //需要增加的
                        List<string> imageKeyListAdd = JsonConvert.DeserializeObject<List<string>>(res.data);
                        //需要删除的
                        List<string> imageKeyListDelete = JsonConvert.DeserializeObject<List<string>>(res.msg);
                        if (imageKeyListAdd == null)
                        {
                            imageKeyListAdd = new List<string>();
                        }
                        if (imageKeyListDelete == null)
                        {
                            imageKeyListDelete = new List<string>();
                        }
                        showMsg("需要增加的记录数：" + imageKeyListAdd.Count);
                        showMsg("需要删除的记录数：" + imageKeyListDelete.Count);

                        urlOper = @"/user/createOrUpdate";
                        url = string.Format(@"{0}{1}", Url, urlOper);
                        int i = 0;
                        foreach (string key in imageKeyListAdd)
                        {
                            i++;
                            ReturnStr = "";
                            if (userDic.ContainsKey(key))
                            {
                                User u = userDic[key];
                                postStr = string.Format("pass={0}&user={1}", Pass, JsonConvert.SerializeObject(u));
                                bool bcreateImage = CHttpPost.Post(url, postStr, ref ReturnStr);
                                showMsg(string.Format("增加照片[{0}/{1}],FileName[{2}],特征值[{3}]，ReturnStr[{4}]", i, imageKeyListAdd.Count, u.userName, u.imageKey, ReturnStr));
                                if (bcreateImage)
                                {
                                    res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                                    if (res.success)
                                    {
                                        showMsg("");
                                    }
                                    else
                                    {
                                        showMsg(string.Format("有返回，但出错了[{0}]:{1}", res.msgtype, res.msg));
                                        File.Copy(u.FilePath, FacePicErrPath + "err" + res.msgtype + "_" + u.FileName + ".jpg", true);
                                    }

                                }
                                else
                                {
                                    showMsg("通讯失败");
                                }
                            }

                        }
                        //处理完成后，发消息给设备更新数据
                        SendDevRefreshData();
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }
            }
            finally
            {
                button6.Enabled = true;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //showMsg(GetTimeStamp());
            //showMsg(ConvertDateTimeInt(DateTime.Now).ToString());
            receiveMsg.Clear();
        }
        /// <summary>  
        /// 获取时间戳  
        /// </summary>  
        /// <returns></returns>  
        public static string GetTimeStamp()
        {
            return "1529743361";
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time"> DateTime时间格式</param>
        /// <returns>Unix时间戳格式</returns>
        public static int ConvertDateTimeInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
            return (int)(time - startTime).TotalSeconds;
        }
        private void button8_Click(object sender, EventArgs e)
        {
            string testStr = "1234567890";
            byte[] arr = System.Text.Encoding.Default.GetBytes(testStr);
            showMsg("arr len:" + arr.Length);
            string s = Convert.ToBase64String(arr);
            showMsg(s);
        }



        private void button8_Click_1(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button8.Enabled = false;
                string postStr = string.Format("pass={0}&callbackUrl={1}&typeId=1", Pass, tb_CallBackUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setUrl";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("setUrl 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button8.Enabled = true;

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tb_MachineCode.Text = "";
            try
            {
                button9.Enabled = false;
                string postStr = string.Format("pass={0}", Pass);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getMachineCode";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        tb_MachineCode.Text = res.data;
                        showMsg("getMachineCode 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button9.Enabled = true;

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            try
            {
                button10.Enabled = false;
                string postStr = string.Format("pass={0}&key={1}", Pass, tb_AuthorizeCode.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setAuthorize";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("setPassWord 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button10.Enabled = true;

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button8.Enabled = false;
                string postStr = string.Format("pass={0}&callbackUrl={1}&typeId=2", Pass, bt_GetApkVersion.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setUrl";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("setUrl 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button8.Enabled = true;

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button8.Enabled = false;
                string postStr = string.Format("pass={0}&callbackUrl={1}&typeId=3", Pass, tb_DownApkUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setUrl";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("setUrl 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button8.Enabled = true;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            tb_MachineCode.Text = "";
            try
            {
                button9.Enabled = false;
                string postStr = string.Format("pass={0}&timestamp={1}", Pass, GetTimeStamp());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setTime";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        tb_MachineCode.Text = res.data;
                        showMsg("setTime 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button9.Enabled = true;

            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button14.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&callbackUrl={1}&typeId=4", Pass, tb_CallBackVerifyUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setUrl";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("setUrl 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button14.Enabled = true;

            }
        }

        /// <summary>
        /// 住宿生的时段数据
        /// 有2组
        /// 1（出校时间）:周5，时段:12:00:00-13:00:00,17:00:00-18:00:00
        /// 2（回校时间）:周7，时段:17:00:00-18:00:00
        /// </summary>
        /// <returns></returns>
        private PassTimes GetNewPassTimes1()
        {
            PassTimes res = new PassTimes();

            res.Name = "住宿生";
            res.passTimeList = new List<PassTime>();
            PassTime _PassTime = new PassTime();
            _PassTime.WeekList = new List<string>();
            _PassTime.PassTimeByWeekList = new List<PassTimeOne>();
            //周5的中午，下午放学时间
            //星期5
            _PassTime.WeekList.Add("5");

            //中午
            PassTimeOne _PassTimeOne = new PassTimeOne();
            _PassTimeOne.Dt1 = "12:00:00";
            _PassTimeOne.Dt2 = "13:00:00";
            _PassTime.PassTimeByWeekList.Add(_PassTimeOne);
            //下午
            _PassTimeOne = new PassTimeOne();
            _PassTimeOne.Dt1 = "17:00:00";
            _PassTimeOne.Dt2 = "18:00:00";
            _PassTime.PassTimeByWeekList.Add(_PassTimeOne);
            //加入
            res.passTimeList.Add(_PassTime);

            //星期天的下午
            _PassTime = new PassTime();
            _PassTime.WeekList = new List<string>();
            _PassTime.PassTimeByWeekList = new List<PassTimeOne>();
            //星期7
            _PassTime.WeekList.Add("7");

            //下午
            _PassTimeOne = new PassTimeOne();
            _PassTimeOne.Dt1 = "17:00:00";
            _PassTimeOne.Dt2 = "18:00:00";
            _PassTime.PassTimeByWeekList.Add(_PassTimeOne);
            //加入
            res.passTimeList.Add(_PassTime);

            return res;
        }
        /// <summary>
        /// 营业的时段数据
        /// 周1-7
        /// 时段:08:00:00-23:00:00
        /// </summary>
        /// <returns></returns>
        private PassTimes GetNewPassTimes2()
        {
            PassTimes res = new PassTimes();

            res.Name = "正常营业";
            res.passTimeList = new List<PassTime>();
            PassTime _PassTime = new PassTime();
            _PassTime.WeekList = new List<string>();
            _PassTime.PassTimeByWeekList = new List<PassTimeOne>();
            _PassTime.WeekList.Add("1");
            _PassTime.WeekList.Add("2");
            _PassTime.WeekList.Add("3");
            _PassTime.WeekList.Add("4");
            _PassTime.WeekList.Add("5");
            _PassTime.WeekList.Add("6");
            _PassTime.WeekList.Add("7");

            //时段
            PassTimeOne _PassTimeOne = new PassTimeOne();
            _PassTimeOne.Dt1 = "08:00:00";
            _PassTimeOne.Dt2 = "23:00:00";
            _PassTime.PassTimeByWeekList.Add(_PassTimeOne);
            //加入
            res.passTimeList.Add(_PassTime);
            return res;
        }
        /// <summary>
        /// 上下班(学)的时段数据
        /// 周1-5
        /// 时段:07:00:00-08:00:00,11:00:00-13:00:00,16:00:00-18:00:00,
        /// </summary>
        /// <returns></returns>
        private PassTimes GetNewPassTimes3()
        {
            PassTimes res = new PassTimes();

            res.Name = "上下班(学)";
            res.passTimeList = new List<PassTime>();
            PassTime _PassTime = new PassTime();
            _PassTime.WeekList = new List<string>();
            _PassTime.PassTimeByWeekList = new List<PassTimeOne>();
            _PassTime.WeekList.Add("1");
            _PassTime.WeekList.Add("2");
            _PassTime.WeekList.Add("3");
            _PassTime.WeekList.Add("4");
            _PassTime.WeekList.Add("5");

            //时段
            PassTimeOne _PassTimeOne = new PassTimeOne();
            _PassTimeOne.Dt1 = "07:00:00";
            _PassTimeOne.Dt2 = "08:00:00";
            _PassTime.PassTimeByWeekList.Add(_PassTimeOne);
            _PassTimeOne = new PassTimeOne();
            _PassTimeOne.Dt1 = "11:00:00";
            _PassTimeOne.Dt2 = "13:00:00";
            _PassTime.PassTimeByWeekList.Add(_PassTimeOne);
            _PassTimeOne = new PassTimeOne();
            _PassTimeOne.Dt1 = "16:00:00";
            _PassTimeOne.Dt2 = "18:00:00";
            _PassTime.PassTimeByWeekList.Add(_PassTimeOne);
            //加入
            res.passTimeList.Add(_PassTime);
            return res;
        }
        private void button15_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button15.Enabled = false;
                PassTimes _PassTimes = GetNewPassTimes1();
                string postStr = string.Format("pass={0}&passtimes={1}", Pass, JsonConvert.SerializeObject(_PassTimes));
                string urlOper = @"/passtime/createOrUpdate";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg(string.Format("Set passtime[{0}] 成功",_PassTimes.Name));
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

                //2
                _PassTimes = GetNewPassTimes2();
                postStr = string.Format("pass={0}&passtimes={1}", Pass, JsonConvert.SerializeObject(_PassTimes));
                urlOper = @"/passtime/createOrUpdate";
                url = string.Format(@"{0}{1}", Url, urlOper);
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                ReturnStr = "";
                b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg(string.Format("Set passtime[{0}] 成功", _PassTimes.Name));
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

                //3
                _PassTimes = GetNewPassTimes3();
                postStr = string.Format("pass={0}&passtimes={1}", Pass, JsonConvert.SerializeObject(_PassTimes));
                urlOper = @"/passtime/createOrUpdate";
                url = string.Format(@"{0}{1}", Url, urlOper);
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                ReturnStr = "";
                b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg(string.Format("Set passtime[{0}] 成功", _PassTimes.Name));
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button15.Enabled = true;

            }


        }

        private void button16_Click(object sender, EventArgs e)
        {

            Pass = tb_Pass.Text;
            try
            {
                button15.Enabled = false;
                UserSetPassTime _UserSetPassTime = new UserSetPassTime();
                _UserSetPassTime.userId = tb_SetPassTime_UserId.Text.Trim();
                _UserSetPassTime.passTimeName = tb_SetPassTime_PassTimeName.Text.Trim();
                string postStr = string.Format("pass={0}&usersetpasstime={1}", Pass, JsonConvert.SerializeObject(_UserSetPassTime));
                string urlOper = @"/user/setpasstime";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("set user passtime 成功");

                        //处理完成后，发消息给设备更新数据
                        SendDevRefreshData();
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }



            }
            finally
            {
                button15.Enabled = true;

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button17.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&callbackUrl={1}&typeId=5", Pass, tb_HeartBeatUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setUrl";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("setUrl 成功");
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }

            }
            finally
            {
                button17.Enabled = true;

            }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button18.Enabled = false;
                string postStr = string.Format("pass={0}&passtimename={1}", Pass, tb_DeletePassTimeName.Text.Trim());
                string urlOper = @"/passtime/delete";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg(string.Format("passtime delete[{0}] 成功", tb_DeletePassTimeName.Text.Trim()));
                    }
                    else
                    {
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }
            }
            finally
            {
                button18.Enabled = true;

            }
        }

        private void button19_Click(object sender, EventArgs e)
        {

            Pass = tb_Pass.Text;
            try
            {
                button19.Enabled = false;
                Person  person = new Person();
                person.id = tb_PersonAddOrUpdate_PersonId.Text.Trim();
                person.name = tb_PersonAddOrUpdate_PersonName.Text.Trim();
                string postStr = string.Format("pass={0}&person={1}", Pass, JsonConvert.SerializeObject(person));
                string urlOper = @"/person/createOrUpdate";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("person createOrUpdate 成功");

                        //处理完成后，发消息给设备更新数据
                        SendDevRefreshData();
                    }
                    else
                    {
                        //-1:参数异常:person传入为空
                        //-2:参数异常:传入的person字符串转化成对象出错
                        //-3:参数异常:传入的ID格式非法，格式：[A-Za-z0-9]{0,32}
                        //-4:参数异常:系统异常

                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }



            }
            finally
            {
                button19.Enabled = true;

            }
        }

        private void button20_Click(object sender, EventArgs e)
        {

            Pass = tb_Pass.Text;
            try
            {
                
                button20.Enabled = false;
                //id如果传入-1,则会把人员和照片全部删除
                //id可以传入多个，按','分隔，
                string postStr = string.Format("pass={0}&id={1}", Pass, tb_PersonDelete_PersonId.Text.Trim());
                string urlOper = @"/person/delete";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("person delete 成功");

                        //处理完成后，发消息给设备更新数据
                        SendDevRefreshData();
                    }
                    else
                    {
                        //-1:参数异常:id传入为空
                        //-2:参数异常:按','转化成string[]出错
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }



            }
            finally
            {
                button20.Enabled = true;

            }
        }

        private void button21_Click(object sender, EventArgs e)
        {

            Pass = tb_Pass.Text;
            try
            {

                button21.Enabled = false;
                //id如果传入-1,则会返回所有人员信息
                string postStr = string.Format("pass={0}&id={1}", Pass, tb_PersonFind_PersonId.Text.Trim());
                string urlOper = @"/person/find";
                string url = string.Format(@"{0}{1}", Url, urlOper);
                ///person/createOrUpdate
                showMsg("url:" + url);
                showMsg("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg("person find 成功");
                        List<Person> list = JsonConvert.DeserializeObject<List<Person>>(res.data);
                        
                        if (list != null)
                        {
                            int personid = 0;
                            foreach (Person one in list)
                            {
                                personid++;
                                showMsg(string.Format("person[{0}]:{1}", personid, one.ToString()));
                            }
                        }
                        else
                        {
                            showMsg("无数据");
                        }

                    }
                    else
                    {
                        //-1:参数异常:id传入为空
                        showMsg("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg("通讯失败");
                }



            }
            finally
            {
                button21.Enabled = true;

            }
        }
    }
    /// <summary>
    /// 时段段对象
    /// </summary>
    public class PassTimeOne
    {
        /// <summary>
        /// 开始时间 hh:mi:ss
        /// </summary>
        public String Dt1;
        /// <summary>
        /// 结果时间 hh:mi:ss
        /// </summary>
        public String Dt2;
    }
    /// <summary>
    /// 时段，有星期列表和时间段列表
    /// </summary>
    public class PassTime
    {

        /// <summary>
        /// 星期列表 值在[1，7]
        /// </summary>
        public List<String> WeekList;
        /// <summary>
        /// 时间段列表，可以多个
        /// </summary>
        public List<PassTimeOne> PassTimeByWeekList;
    }
    public class PassTimes
    {
        /// <summary>
        /// 时段名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 时段列表
        /// </summary>
        public List<PassTime> passTimeList { get; set; }
    }

    public class UserSetPassTime
    {
        /// <summary>
        /// 用户ID 
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 时段的名称
        /// </summary>
        public string passTimeName { get; set; }
    }

    public class Person
    {
        /// <summary>
        /// ID 
        /// 如果传入，格式：[A-Za-z0-9]{0,32}
        /// 可以为空，系统会自动生成ID，并在返回数据中体现
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        public override string ToString()
        {
            return string.Format("id:[{0}],name:[{1}]",id,name);
        }
    }

}
