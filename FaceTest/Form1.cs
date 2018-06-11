using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO;

using System.ServiceModel;

using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IO.Pipes;
using System.Threading;

namespace FaceTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartService();
        }
        private ServiceHost serviceHost;
        public void StartService()
        {
            try
            {
                if (serviceHost == null)
                {
                    serviceHost = new ServiceHost(typeof(FaceTest.Service1));
                }
                if ((serviceHost.State == CommunicationState.Closed) || (serviceHost.State == CommunicationState.Created))
                    serviceHost.Open();

                int count = serviceHost.BaseAddresses.Count;
                for (int i = 0; i < count; i++)
                {
                }
                count = serviceHost.Description.Endpoints.Count;
                for (int j = 0; j < count; j++)
                {
                }
            }
            catch (Exception ex)
            {
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
            receiveMsg.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string testStr = "1234567890";
            byte[] arr = System.Text.Encoding.Default.GetBytes(testStr);
            showMsg("arr len:" + arr.Length);
            string s = Convert.ToBase64String(arr);
            showMsg(s);
        }

        NamedPipeServerStream pipeServer = new NamedPipeServerStream("FaceTestPip", PipeDirection.InOut, 4, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
        private void Form1_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(delegate
            {
                pipeServer.BeginWaitForConnection((o) =>
                {
                    NamedPipeServerStream server = (NamedPipeServerStream)o.AsyncState;
                    server.EndWaitForConnection(o);
                    StreamReader sr = new StreamReader(server);
                    StreamWriter sw = new StreamWriter(server);
                    string result = null;
                    string clientName = server.GetImpersonationUserName();
                    while (true)
                    {
                        result = sr.ReadLine();
                        if (result == null || result == "bye")
                            break;
                        try
                        {
                            Verify v = JsonConvert.DeserializeObject<Verify>(result);
                            showMsg(result);
                            lb_PersonId.Text = v.userId;
                            lb_PersonName.Text = v.userName;
                        }catch(Exception ex)
                        {
                            showMsg(ex.ToString());
                        }
                        //showMsg(string.Format("clientName:[{0}],result:[{1}]", clientName, result));
                    }
                }, pipeServer);
            });
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button8.Enabled = false;
                string postStr = string.Format("pass={0}&callbackUrl={1}", Pass, tb_CallBackUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setIdentifyCallBack";
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
                        showMsg("setIdentifyCallBack 成功");
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
    }
}
