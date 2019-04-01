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
using FaceTest.Properties;
using System.Net.Sockets;
using System.Diagnostics;
using System.Web;

namespace FaceTest
{
    public partial class Form1 : Form
    {

        private Encoding encoding = Encoding.UTF8;


        public Form1()
        {
            InitializeComponent();
         }
        
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

            this.tb_FaceAddOrUpdate_PersonId.Text = settings.tb_FaceAddOrUpdate_PersonId;
            this.tb_FaceAddOrUpdate_PersonName.Text = settings.tb_FaceAddOrUpdate_PersonName;
            this.tb_FaceDelete_FaceId.Text = settings.tb_FaceDelete_FaceId;
            this.tb_FaceFind_FaceId.Text = settings.tb_FaceFind_FaceId;

            this.tb_CallBackUrl_His.Text = settings.tb_CallBackUrl_His;
            this.tb_SplitChar.Text = settings.tb_SplitChar;

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

            settings.tb_FaceAddOrUpdate_PersonId = this.tb_FaceAddOrUpdate_PersonId.Text.Trim();
            settings.tb_FaceAddOrUpdate_PersonName = this.tb_FaceAddOrUpdate_PersonName.Text.Trim();
            settings.tb_FaceDelete_FaceId = this.tb_FaceDelete_FaceId.Text.Trim();
            settings.tb_FaceFind_FaceId = this.tb_FaceFind_FaceId.Text.Trim();
            settings.tb_CallBackUrl_His = this.tb_CallBackUrl_His.Text.Trim();
            settings.tb_SplitChar = this.tb_SplitChar.Text.Trim();
            settings.Save();
        }

        #region httpWeb
        private int HttpWebPor = 8091;
        /// <summary>
        /// init httpWeb
        /// </summary>
        private void InitHttpWeb()
        {
            CHttpServiceHandler.InitHttpService(Result_Handler, HttpWebPor, showMsg);
        }
        private void Result_Handler(HttpListenerContext context)
        {
            var guid = Guid.NewGuid().ToString();
            string returnObj = null;//定义返回客户端的信息

            var request = context.Request;
            var response = context.Response;
            ////如果是js的ajax请求，还可以设置跨域的ip地址与参数
            //context.Response.AppendHeader("Access-Control-Allow-Origin", "*");//后台跨域请求，通常设置为配置文件
            //context.Response.AppendHeader("Access-Control-Allow-Headers", "ID,PW");//后台跨域参数设置，通常设置为配置文件
            //context.Response.AppendHeader("Access-Control-Allow-Method", "post");//后台跨域请求设置，通常设置为配置文件
            context.Response.ContentType = "text/plain;charset=UTF-8";//告诉客户端返回的ContentType类型为纯文本格式，编码为UTF-8
            context.Response.AddHeader("Content-type", "text/plain");//添加响应头信息
            context.Response.ContentEncoding = Encoding.UTF8;
            if (request.HttpMethod != "POST" || request.InputStream == null)
            {
                returnObj = "不是post请求或者传过来的数据为空";
                showMsg(returnObj);
                ResponseRetrun(returnObj, response);
                return;
            }
            string requestJsonString = GetRequestJsonString(request);
            showMsg(string.Format("接到请求,guid:[{0}],Ip:[{1}],Url:[{2}],JsonStringLen[{3}]",
                guid, request.RemoteEndPoint.Address, request.RawUrl, requestJsonString.Length));
            try
            {
                switch (request.RawUrl)
                {
                    case @"/Handler.ashx":
                    case @"/Handler_His.ashx":
                        //识别记录
                        DoResult_Record(requestJsonString);
                        returnObj = GetReurnString();// "{\"result\":\"1\",\"success\":\"true\",\"msg\":\"1\",\"Result\": 0,\"msgtype\": \"\"}";
                        ResponseRetrun(returnObj, response);
                        break;

                    case @"/HeartBeat.ashx":
                        //心跳包
                        DoResult_HeartBeat(requestJsonString);
                        returnObj = GetReurnString();// "{\"result\":\"1\",\"success\":\"true\",\"msg\":\"1\",\"Result\": 0,\"msgtype\": \"\"}";
                        ResponseRetrun(returnObj, response);
                        break;
                    case @"/VerifyHandler.ashx":
                        //后台请求
                        returnObj = DoResult_VerifyHandler_PersonCheck(requestJsonString);
                        showMsg(returnObj);
                        ResponseRetrun(returnObj, response);
                        break;
                }
            }
            catch (Exception ex)
            {
                showMsg(ex.ToString());
            }


        }
        /// <summary>
        /// 返回结果 
        /// </summary>
        /// <returns></returns>
        private string GetReurnString()
        {
            VerifyReturn result = new VerifyReturn();
            result.result = 1;
            result.success = true;
            result.msg = "";
            result.msgtype = 0;
            return JsonConvert.SerializeObject(result);
        }
        /// <summary>
        /// 请求返回
        /// </summary>
        /// <param name="str"></param>
        /// <param name="response"></param>
        private void ResponseRetrun(string str, HttpListenerResponse response)
        {
            //返回
            var returnByteArr = Encoding.UTF8.GetBytes(str);//设置客户端返回信息的编码
            try
            {
                using (var stream = response.OutputStream)
                {
                    stream.Write(returnByteArr, 0, returnByteArr.Length);
                }
            }
            catch (Exception ex)
            {
                showMsg(ex.ToString());
            }
        }
        /// <summary>
        /// 得到请求的字符串
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public string GetRequestJsonString(HttpListenerRequest request)
        {
            string data = null;
            var byteList = new List<byte>();
            var byteArr = new byte[222048];
            int readLen = 0;
            int len = 0;
            //接收客户端传过来的数据并转成字符串类型
            do
            {
                readLen = request.InputStream.Read(byteArr, 0, byteArr.Length);
                len += readLen;
                byteList.AddRange(byteArr);
            } while (readLen != 0);
            data = Encoding.UTF8.GetString(byteList.ToArray(), 0, len);
            string info = HttpUtility.UrlDecode(data, Encoding.UTF8);
            return info;
        }


        #endregion

        #region 处理-接收识别记录
        /// <summary>
        /// 接收识别记录
        /// </summary>
        /// <param name="result"></param>
        private void DoResult_Record(string JsonString)
        {
            //得到JSON字符串
            string dataStr = JsonString.Substring(JsonString.IndexOf("verify=") + 7, JsonString.Length - JsonString.IndexOf("verify=") - 7);
            try
            {
                showMsg(dataStr);
                Verify v = JsonConvert.DeserializeObject<Verify>(dataStr);
                ShowInfo(v);
                
                //保存db
                //this.Invoke((MethodInvoker)delegate
                //{

                //    SaveDb_VerifyRecord(v);
                //});
            }
            catch (Exception ex)
            {
            }
        }
        #endregion

        #region 处理-心跳包
        /// <summary>
        /// 处理json串-心跳包
        /// </summary>
        /// <param name="JsonString"></param>
        private void DoResult_HeartBeat(string JsonString)
        {
            //得到JSON字符串
            string dataStr = JsonString.Substring(JsonString.IndexOf("info=") + 5, JsonString.Length - JsonString.IndexOf("info=") - 5);
            showMsg(dataStr);
            //处理相关流程

        }
        #endregion

        #region 处理-后台验证
        private static int COSTCOUNT = 100;
        private static int NowCost = COSTCOUNT;
        /// <summary>
        /// 在线消费结果 
        /// </summary>
        public enum EConsumeResult
        {
            /// <summary>
            /// 成功 0
            /// </summary>
            [Description("成功")]
            Ok = 0,
            /// <summary>
            /// 人员非法 5
            /// </summary>
            [Description("人员非法")]
            NotFindPerson = 5,
            /// <summary>
            /// 不在工作时段 1
            /// </summary>
            [Description("不在工作时段")]
            NotWork = 1,
            /// <summary>
            /// 已经使用 3
            /// </summary>
            [Description("已经使用")]
            HaveUsed = 3,
            /// <summary>
            /// 其他错误 100
            /// </summary>
            [Description("其他错误")]
            Other = 100

        }
        /// <summary>
        /// 身份检验结果 
        /// </summary>
        public enum EPersonCheckResult
        {
            /// <summary>
            /// 成功 0
            /// </summary>
            [Description("成功")]
            Ok = 0,
            /// <summary>
            /// 其它，
            /// </summary>
            [Description("其它")]
            Other = 1
        }
        /// <summary>
        /// 得到消费结果 
        /// </summary>
        /// <param name="JsonString"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private EConsumeResult DoResult_VerifyHandlerByString(string JsonString, ref Verify v)
        {
            string dataStr = JsonString.Substring(JsonString.IndexOf("verify=") + 7, JsonString.Length - JsonString.IndexOf("verify=") - 7);
            v = JsonConvert.DeserializeObject<Verify>(dataStr);
            showPicByBase64(v.base64);
            return EConsumeResult.Ok;
        }
        /// <summary>
        /// 得到身份校验结果 
        /// </summary>
        /// <param name="JsonString"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private EPersonCheckResult DoResult_VerifyHandlerByString2(string JsonString, ref Verify v)
        {
            string dataStr = JsonString.Substring(JsonString.IndexOf("verify=") + 7, JsonString.Length - JsonString.IndexOf("verify=") - 7);
            v = JsonConvert.DeserializeObject<Verify>(dataStr);
            showPicByBase64(v.base64);
            return EPersonCheckResult.Ok;
        }
        
        private void showPicByBase64 (string base64)
        {
            if (string.IsNullOrEmpty(base64))
            {
                return;
            }
            try
            {
                byte[] arr = Convert.FromBase64String(base64);

                using (MemoryStream ms = new MemoryStream(arr, true))
                {
                        this.Invoke((MethodInvoker)delegate
                        {
                            pictureBox1.Image = Image.FromStream(ms);
                        });

                        
                }


            }
            catch (Exception ex)
            {
                showMsg(ex.ToString());
            }
        }
        private string DoResult_VerifyHandler_PersonCheck(string JsonString)
        {
            NowCost--;
            Verify v = null;
            EPersonCheckResult personCheckResult = DoResult_VerifyHandlerByString2(JsonString, ref v);
            showMsg(string.Format("base64.length:[{0}]", v!=null?v.base64.Length:0));
            VerifyReturn result = new VerifyReturn();
            result.result = 1;
            result.success = personCheckResult == EPersonCheckResult.Ok;
            result.msg = string.Format("你好,{0}" + Environment.NewLine + "验证{1}【{2}】",
                v != null ? v.userName : "未知用户",
                result.success ? "成功" : "失败",
                result.success ? personCheckResult.GetDescription() : ""
                );
            result.msgtype = (int)personCheckResult;
            return JsonConvert.SerializeObject(result);

        }
        private string DoResult_VerifyHandler(string JsonString)
        {
            NowCost--;
            Verify v = null;
            EConsumeResult consumeResult = DoResult_VerifyHandlerByString(JsonString, ref v);
            showMsg(string.Format("base64.length:[{0}]", v != null ? v.base64.Length : 0));
            VerifyReturn result = new VerifyReturn();
            result.result = 1;
            result.success = consumeResult == EConsumeResult.Ok;
            result.msg = string.Format("你好,{0}" + Environment.NewLine + "消费{1}【{2}】",
                v != null ? v.userName : "未知用户",
                result.success ? "成功" : "失败",
                result.success ? NowCost.ToString() : consumeResult.GetDescription()
                );
            result.msgtype = (int)consumeResult;
            return JsonConvert.SerializeObject(result);

        }
        #endregion
        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = receivePassList;
            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
            LoadData();
            //设置照片路径
            button2_Click(null, null);
            //设置设备URL
            button3_Click(null, null);
          
        }
        /// <summary>
        /// 显示人员信息
        /// </summary>
        /// <param name="result"></param>
        private void ShowInfo(string result)
        {
            try
            {
                if (result.IndexOf("HeartBeat receive") == 0)
                {
                    //如果是心跳包数据，则退出
                    return;
                }
                if (result.IndexOf("data:") < 0)
                {
                    return;
                }
                //通过data:截取、得到接收的对象
                string dataStr = result.Substring(result.IndexOf("data:") + 5, result.Length - result.IndexOf("data:") - 5);
                Verify v = JsonConvert.DeserializeObject<Verify>(dataStr);
                this.Invoke((MethodInvoker)delegate
                {
                    if (v != null)
                    {
                        lb_PersonId.Text = "用户ID:" + v.userId;
                        lb_PersonName.Text = "用户姓名:" + v.userName;
                        lb_Path.Text = "照片路径:" + v.path;
                        if (!string.IsNullOrEmpty(v.path))
                        {
                            pictureBox1.LoadAsync(v.path);
                        }
                        showInfoForGrid(v);
                        showMsg2(v.ToString());

                        //showMsg(v.base64 != null ? v.base64.Length.ToString() : "");
                    }
                    else
                    {
                        lb_PersonId.Text = "";
                        lb_PersonName.Text = "";
                    }
                });
            }
            catch (Exception ex)
            {
                showMsg(ex.ToString());
            }


        }
        private void ShowInfo(Verify v)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate
                {
                    if (v != null)
                    {
                        lb_PersonId.Text = "用户ID:" + v.userId;
                        lb_PersonName.Text = "用户姓名:" + v.userName;
                        lb_Path.Text = "照片路径:" + v.path;
                        if (!string.IsNullOrEmpty(v.path))
                        {
                            pictureBox1.LoadAsync(v.path);
                        }
                        showInfoForGrid(v);
                        showMsg2(v.ToString());

                        //showMsg(v.base64 != null ? v.base64.Length.ToString() : "");
                    }
                    else
                    {
                        lb_PersonId.Text = "";
                        lb_PersonName.Text = "";
                    }
                });
            }
            catch (Exception ex)
            {
                showMsg(ex.ToString());
            }


        }
        private List<Verify> receivePassList = new List<Verify>();


        private void button1_Click(object sender, EventArgs e)
        {
            InitHttpWeb();
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

            if (!string.IsNullOrEmpty(tb_SplitChar.Text))
            {
                string[] fileNameList = fileName.Split(tb_SplitChar.Text.ToCharArray());
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
            }
            else
            {
                //未设置文件名分隔符号
                u.userId = fileName;
                u.userName = fileName;
                u.direct = 0;
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
              //  FileInfo[] fis = di.GetFiles("*.jpg");
                List<FileInfo> fis = di.GetFiles("*.*", SearchOption.AllDirectories).Where(s => s.Name.EndsWith(".png") || s.Name.EndsWith(".jpg")).ToList<FileInfo>();
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
                showMsg(string.Format("处理总人数[{0}],用时[{1}]", fis.Count, stopwatch.ElapsedMilliseconds));
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
                    if (receiveMsg.Lines.Length>200)
                    {
                        receiveMsg.Clear();
                    }
                    receiveMsg.AppendText(s);
                    receiveMsg.Select(receiveMsg.Text.Length, 0);
                    receiveMsg.ScrollToCaret();
                }
            }
        }
        public delegate void dShowInfo2(string str);
        public void showMsg2(string msg)
        {
            {
                //在线程里以安全方式调用控件
                if (receiveMsg2.InvokeRequired)
                {
                    dShowInfo _myinvoke = new dShowInfo(showMsg2);
                    receiveMsg2.Invoke(_myinvoke, new object[] { msg });
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

                    receiveMsg2.AppendText(s + Environment.NewLine);
                    receiveMsg2.Select(receiveMsg2.Text.Length, 0);
                    receiveMsg2.ScrollToCaret();
                }
            }
        }
        public delegate void dShowInfoForGrid(Verify v);
        public void showInfoForGrid(Verify v)
        {
            {
                //在线程里以安全方式调用控件
                if (dataGridView1.InvokeRequired)
                {
                    dShowInfoForGrid _myinvoke = new dShowInfoForGrid(showInfoForGrid);
                    dataGridView1.Invoke(_myinvoke, new object[] { v });
                }
                else
                {
                    receivePassList.Add(v);
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = receivePassList;
                    //dataGridView1.DataSource = receivePassList;
                    //dataGridView1.Refresh();
                }
            }
        }
        /// <summary>
        /// 根据目录中的照片文件得到照片key列表的string，用','分隔
        /// </summary>
        /// <returns></returns>
        private string GetImageKeyList()
        {
            string ret = "";
            foreach (User us in userList)
            {
                ret += us.imageKey + ",";
            }

            return ret.ToString().TrimEnd(',');
        }

        /// <summary>
        /// 根据目录中的照片文件得到人员、照片对象列表
        /// </summary>
        /// <returns></returns>
        private List<PersonAndFace> GetPersonAndFaces()
        {
            List<PersonAndFace> res = new List<PersonAndFace>();
            foreach (User us in userList)
            {
                PersonAndFace personAndFace = new PersonAndFace();
                personAndFace.person = new Person();
                personAndFace.face = new Face();

                //person.id = tb_PersonAddOrUpdate_PersonId.Text.Trim();
                //person.name = tb_PersonAddOrUpdate_PersonName.Text.Trim();
                personAndFace.person.id = us.FileName;
                personAndFace.person.name = personAndFace.person.id;

                //face.userId = tb_FaceAddOrUpdate_PersonId.Text.Trim();
                //face.userName = tb_FaceAddOrUpdate_PersonName.Text.Trim();
                //face.direct = 0;
                //face.imageId = string.Format("{0}_{1}_{2}", face.userId, face.userName, face.direct);
                //face.imageBase64 = ImgToBase64String(pictureBox2.ImageLocation);
                //face.imageKey = GetFileMd5(pictureBox2.ImageLocation);
                personAndFace.face.userId = personAndFace.person.id;
                personAndFace.face.userName = personAndFace.person.name;
                personAndFace.face.direct = 0;
                personAndFace.face.imageId = string.Format("{0}_{1}_{2}", personAndFace.face.userId, personAndFace.face.userName, personAndFace.face.direct);
                personAndFace.face.imageBase64 = us.imageBase64;
                personAndFace.face.imageKey = us.imageKey;
                res.Add(personAndFace);

            }

            return res;

        }
        private void SendDevRefreshData()
        {
            tb_MachineCode.Text = "";
            try
            {
                // button9.Enabled = false;
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
                        showMsg("refresh 成功:" + res.msg + "********************************");
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
                // button9.Enabled = true;

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
            receiveMsg2.Clear();
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

        private PassTimes GetNewPassTimes4()
        {
            PassTimes res = new PassTimes();

            res.Name = "3";
            res.passTimeList = new List<PassTime>();
            PassTime _PassTime = new PassTime();
            _PassTime.WeekList = new List<string>();
            _PassTime.PassTimeByWeekList = new List<PassTimeOne>();
            //_PassTime.WeekList.Add("1");
            //_PassTime.WeekList.Add("2");
            //_PassTime.WeekList.Add("3");
            //_PassTime.WeekList.Add("4");
            _PassTime.WeekList.Add("5");

            //时段
            PassTimeOne _PassTimeOne = new PassTimeOne();
            _PassTimeOne.Dt1 = "07:00:00";
            _PassTimeOne.Dt2 = "23:00:00";
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
                _PassTimes = GetNewPassTimes4();
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
                Person person = new Person();
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
        private string FaceImageFileName = "";
        private void button22_Click(object sender, EventArgs e)
        {

            Pass = tb_Pass.Text;
            try
            {
                button22.Enabled = false;
                Face face = new Face();
                face.userId = tb_FaceAddOrUpdate_PersonId.Text.Trim();
                face.userName = tb_FaceAddOrUpdate_PersonName.Text.Trim();
                face.direct = 0;
                face.imageId = string.Format("{0}_{1}_{2}", face.userId, face.userName, face.direct);
                //face.faceImageFileName = FaceImageFileName;// pictureBox2.ImageLocation;
                face.imageBase64 = ImgToBase64String(pictureBox2.ImageLocation);
                face.imageKey = GetFileMd5(pictureBox2.ImageLocation);
                string postStr = string.Format("pass={0}&face={1}", Pass, JsonConvert.SerializeObject(face));
                string urlOper = @"/face/createOrUpdate";
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
                        showMsg("face createOrUpdate 成功");

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
                button22.Enabled = true;

            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "C# Corner Open File Dialog";
            //fdlg.InitialDirectory = @"c:\";   //@是取消转义字符的意思
            fdlg.Filter = "图片文件(*.jpg)|*.jpg|*.gif|*.bmp";
            //fdlg.Filter = "All files（*.*）|*.*|All files(**)|*.* ";
            /*
             * FilterIndex 属性用于选择了何种文件类型,缺省设置为0,系统取Filter属性设置第一项
             * ,相当于FilterIndex 属性设置为1.如果你编了3个文件类型，当FilterIndex ＝2时是指第2个.
             */
            fdlg.FilterIndex = 1;
            /*
             *如果值为false，那么下一次选择文件的初始目录是上一次你选择的那个目录，
             *不固定；如果值为true，每次打开这个对话框初始目录不随你的选择而改变，是固定的  
             */
            fdlg.RestoreDirectory = true;
            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                pictureBox2.ImageLocation = fdlg.FileName;
                FaceImageFileName = fdlg.SafeFileName;
                //textBox1.Text = System.IO.Path.GetFileNameWithoutExtension(fdlg.FileName);

            }
        }

        private void button23_Click(object sender, EventArgs e)
        {

            Pass = tb_Pass.Text;
            try
            {

                button23.Enabled = false;
                //id如果传入-1,则会把人员和照片全部删除
                //id可以传入多个，按','分隔，
                string postStr = string.Format("pass={0}&faceId={1}", Pass, tb_FaceDelete_FaceId.Text.Trim());
                string urlOper = @"/face/delete";
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
                        showMsg("face delete 成功");

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
                button23.Enabled = true;

            }
        }

        private void tb_FaceAddOrUpdate_PersonId_TextChanged(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {

                button24.Enabled = false;
                //id如果传入-1,则会返回所有人员信息
                string postStr = string.Format("pass={0}&id={1}", Pass, tb_FaceFind_FaceId.Text.Trim());
                string urlOper = @"/face/find";
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
                        showMsg("face find 成功");
                        List<FaceFind> list = JsonConvert.DeserializeObject<List<FaceFind>>(res.data);

                        if (list != null)
                        {
                            int personid = 0;
                            foreach (FaceFind one in list)
                            {
                                personid++;
                                showMsg(string.Format("face[{0}]:{1}", personid, one.ToString()));
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
                button24.Enabled = true;

            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button25.Enabled = false;
                string postStr = string.Format("pass={0}&callbackUrl={1}&typeId=6", Pass, tb_CallBackUrl_His.Text.Trim());
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
                    showMsg("通讯失败/");
                }

            }
            finally
            {
                button25.Enabled = true;

            }
        }
        private void button26_Click(object sender, EventArgs e)
        {
        }

        private void button27_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = receivePassList;
            //dataGridView1.Refresh();
        }

        private void button28_Click(object sender, EventArgs e)
        {
            //多少条记录后，通知设备更新一下
            int RefreshDataCount = 20;
            try
            {
                int _refreshDataCount = 0;
                int _count = 0;
                button28.Enabled = false;
                //得到人员、照片列表
                List<PersonAndFace> personAndFaces = GetPersonAndFaces();

                foreach (PersonAndFace mPersonAndFace in personAndFaces)
                {
                    _count++;
                    showMsg(string.Format("处理--[{0}]/[{1}]", _count, personAndFaces.Count));
                    _refreshDataCount++;
                    //如果对象为空，则跳过，执行下一个
                    if (mPersonAndFace == null)
                        continue;

                    //操作人员
                    #region 操作人员
                    Person person = mPersonAndFace.person;
                    if (person == null)
                        continue;
                    //person.id = tb_PersonAddOrUpdate_PersonId.Text.Trim();
                    //person.name = tb_PersonAddOrUpdate_PersonName.Text.Trim();
                    string postStr = string.Format("pass={0}&person={1}", Pass, JsonConvert.SerializeObject(person));
                    string urlOper = @"/person/createOrUpdate";
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
                            showMsg("person createOrUpdate 成功");

                            //处理完成后，发消息给设备更新数据
                            //SendDevRefreshData();
                        }
                        else
                        {
                            //-1:参数异常:person传入为空
                            //-2:参数异常:传入的person字符串转化成对象出错
                            //-3:参数异常:传入的ID格式非法，格式：[A-Za-z0-9]{0,32}
                            //-4:参数异常:系统异常

                            showMsg("有返回，但出错了：" + res.msg);
                            //跳出循环
                            break;
                        }
                    }
                    else
                    {
                        showMsg("通讯失败");
                        //跳出循环
                        break;
                    }
                    #endregion

                    #region 操作照片
                    Face face = mPersonAndFace.face;
                    if (face == null)
                        continue;
                    //face.userId = tb_FaceAddOrUpdate_PersonId.Text.Trim();
                    //face.userName = tb_FaceAddOrUpdate_PersonName.Text.Trim();
                    //face.direct = 0;
                    //face.imageId = string.Format("{0}_{1}_{2}", face.userId, face.userName, face.direct);
                    ////face.faceImageFileName = FaceImageFileName;// pictureBox2.ImageLocation;
                    //face.imageBase64 = ImgToBase64String(pictureBox2.ImageLocation);
                    //face.imageKey = GetFileMd5(pictureBox2.ImageLocation);
                    postStr = string.Format("pass={0}&face={1}", Pass, JsonConvert.SerializeObject(face));
                    urlOper = @"/face/createOrUpdate";
                    url = string.Format(@"{0}{1}", Url, urlOper);
                    ///person/createOrUpdate
                    showMsg("url:" + url);
                    showMsg("postStr:" + (postStr.Length > 100 ? postStr.Substring(0, 100) + "..." : postStr));

                    ReturnStr = "";
                    b = CHttpPost.Post(url, postStr, ref ReturnStr);
                    if (b)
                    {
                        showMsg(ReturnStr);
                        ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                        if (res.success)
                        {
                            showMsg("face createOrUpdate 成功");

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
                    #endregion

                    showMsg("");

                    //通知设备更新数据
                    if (_refreshDataCount > RefreshDataCount)
                    {
                        _refreshDataCount = 0;
                        SendDevRefreshData();
                    }

                }

                //整体处理完成后，发消息给设备更新数据
                SendDevRefreshData();


            }
            finally
            {
                button28.Enabled = true;
            }
        }

        private void button27_Click_1(object sender, EventArgs e)
        {
            timer1.Interval = 5 * 60 * 1000;
            timer1.Enabled = !timer1.Enabled;
            if (timer1.Enabled)
            {
                button28_Click(sender, e);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            button28_Click(sender, e);
        }
        private byte[] packData(byte mPackType)
        {
            //byte[] data = new byte[1024];
            //int offset = 0;


            //data[offset++] = mPackType;


            //byte[] result = new byte[offset];
            //Array.Copy(data, 0, result, 0, offset);
            //return result;
            byte[] result = new byte[1];
            result[0] = mPackType;
            return result;
        }

        private byte[] parsePack(byte[] pack)
        {
            byte PACKET_TYPE_FIND_DEVICE_RSP_11 = 0x11; // 搜索响应
            if (pack == null || pack.Length == 0)
            {
                return null;
            }

            if (pack[0] != PACKET_TYPE_FIND_DEVICE_RSP_11)
            {
                return null;
            }
            byte[] result = new byte[pack.Length - 1];
            Array.Copy(pack, 1, result, 0, pack.Length - 1);
            return result;
        }
        private void button29_Click(object sender, EventArgs e)
        {
            byte PACKET_TYPE_FIND_DEVICE_REQ_10 = 0x10; // 搜索请求

            UdpClient UDPsend = new UdpClient(new IPEndPoint(IPAddress.Any, 0));
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Broadcast, 9000);
            //其实 IPAddress.Broadcast 就是 255.255.255.255
            //下面代码与上面有相同的作用
            //IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 8080);
            byte[] buf = packData(PACKET_TYPE_FIND_DEVICE_REQ_10);//Encoding.Default.GetBytes("This is UDP broadcast" + DateTime.Now.ToString());
            Task.Factory.StartNew(() =>
            {
                while (true)
                {
                    UDPsend.Send(buf, buf.Length, endpoint);
                    try
                    {
                        int rspCount = 200;
                        IPEndPoint endpointR = new IPEndPoint(IPAddress.Any, 0);
                        while (rspCount-- > 0)
                        {
                            //recePack.setData(receData);
                            byte[] receData = parsePack(UDPsend.Receive(ref endpointR));
                            if (receData != null)
                            {
                                string msg = Encoding.UTF8.GetString(receData);
                                showMsg(String.Format("设备[{0}]:[{1}]", endpointR.Address, msg));
                                showDevInfo(msg);
                            }
                        }
                    }
                    catch (SocketException ex)
                    {
                        Console.WriteLine(ex.ToString());
                    }

                    Thread.Sleep(1000);
                }
            });
        }
        private void showDevInfo(string s)
        {
            DevicesHeartBeat device = JsonConvert.DeserializeObject<DevicesHeartBeat>(s);
            showMsg(String.Format("解析,机器码[{0}]", device.deviceMachineCode));
            showMsg(String.Format("解析,机器编号[{0}]", device.deviceKey));
            showMsg(String.Format("解析,Ip[{0}]", device.ip));
            showMsg(String.Format("解析,人员数[{0}]", device.personCount));
            showMsg(String.Format("解析,照片数[{0}]", device.faceCount));
            showMsg(String.Format("解析,运行时间[{0}]", device.runtime));
            showMsg(String.Format("解析,版本号[{0}]", device.version));
            showMsg(String.Format("解析,App占用内存[{0}mb]-[{1:F}%]", device.memory, device.memory / device.totalMem * 100.00));
            showMsg(String.Format("解析,系统可用内存[{0}mb]-[{1:F}%]", device.availMem, device.availMem / device.totalMem * 100.00));
            showMsg(String.Format("解析,系统总内存[{0}mb]", device.totalMem));
            showMsg("");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                button30.Enabled = false;
                string postStr = string.Format("pass={0}", Pass);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getAppConfig";
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
                        textBox1.Text = res.data;
                        showMsg("getAppConfig 成功");
                        showMsg(res.data);
                        showAppRunConfig(res.data);
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
                button30.Enabled = true;

            }
        }
        private void showAppRunConfig(string mes)
        {
            AppRunConfig config = JsonConvert.DeserializeObject<AppRunConfig>(mes);
            showMsg(String.Format("解析,尝试次数[{0}]", config.attemptCount));
            showMsg(String.Format("解析,变焦的比例[{0}]", config.cameraMaxZoom));
            showMsg(String.Format("解析,保留空间的比例[{0}]", config.deleteFile_Disk));
            showMsg(String.Format("解析,启用灯光[{0}]", config.isLigth));
            showMsg(String.Format("解析,启用活检[{0}]", config.isOpenHacker));
            showMsg(String.Format("解析,启用播放语音[{0}]", config.isPlaySound));
            showMsg(String.Format("解析,保存识别照片[{0}]", config.isSaveImage));
            showMsg(String.Format("解析,公司名称[{0}]", config.sCompanyName));
            showMsg(String.Format("解析,停留时间[{0}]", config.sameFaceNexeRecognizeDt));
            showMsg(String.Format("解析,变识别图片时的质量焦的比例[{0}]", config.verifyScore));
            showMsg(String.Format("解析,识别阀值[{0}]", config.verifyThreshold));
            showMsg("");
        }
        private void button31_Click(object sender, EventArgs e)
        {
            try
            {
                button31.Enabled = false;
                string postStr = string.Format("pass={0}&appconfig={1}", Pass, textBox1.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setAppConfig";
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
                        showMsg("setAppConfig 成功");
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
                button31.Enabled = true;

            }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                if (DialogResult.Cancel == MessageBox.Show("是否进行初始化设备?", "请确认", MessageBoxButtons.OKCancel))
                {
                    return;
                }
                button32.Enabled = false;
                string postStr = string.Format("pass={0}&delete={1}", Pass, checkBox1.Checked.ToString());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/device/reset";
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
                        textBox1.Text = res.data;
                        showMsg("reset 成功,App重启！");
                        showMsg(res.data);
                        //SendDevRefreshData();
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
                button32.Enabled = true;

            }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button33.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&typeId=4", Pass, tb_CallBackVerifyUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getUrl";
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
                        showMsg("getUrl 成功");
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
                button33.Enabled = true;

            }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button34.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&typeId=1", Pass, tb_CallBackVerifyUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getUrl";
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
                        showMsg("getUrl 成功");
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
                button34.Enabled = true;

            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button35.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&typeId=6", Pass, tb_CallBackVerifyUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getUrl";
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
                        showMsg("getUrl 成功");
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
                button35.Enabled = true;

            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button36.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&typeId=2", Pass, tb_CallBackVerifyUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getUrl";
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
                        showMsg("getUrl 成功");
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
                button36.Enabled = true;

            }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button37.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&typeId=3", Pass, tb_CallBackVerifyUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getUrl";
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
                        showMsg("getUrl 成功");
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
                button37.Enabled = true;

            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button38.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&typeId=5", Pass, tb_CallBackVerifyUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getUrl";
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
                        showMsg("getUrl 成功");
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
                button38.Enabled = true;

            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button40.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&typeId=0", Pass, tb_CallBackVerifyUrl.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getUrl";
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
                        showMsg("getUrl 成功");
                        showUrl(res.data);
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
                button40.Enabled = true;

            }
        }
        private void showUrl(string mes)
        {
            UrlPar urlPar= JsonConvert.DeserializeObject<UrlPar>(mes);
            showMsg(String.Format("解析,downNewApkUrl[{0}]", urlPar.downNewApkUrl));
            showMsg(String.Format("解析,getNewApkVersionUrl[{0}]", urlPar.getNewApkVersionUrl));
            showMsg(String.Format("解析,heartBeatUrl[{0}]", urlPar.heartBeatUrl));
            showMsg(String.Format("解析,identifyCallBack[{0}]", urlPar.identifyCallBack));
            showMsg(String.Format("解析,identifyCallBack_His[{0}]", urlPar.identifyCallBack_His));
            showMsg(String.Format("解析,verifyCallBack[{0}]", urlPar.verifyCallBack));
            showMsg("");
        }

        private string setUrlJsonStr()
        {
            UrlPar urlPar = new UrlPar();
            urlPar.verifyCallBack = tb_CallBackVerifyUrl.Text.Trim();
            urlPar.identifyCallBack = tb_CallBackUrl.Text.Trim();
            urlPar.identifyCallBack_His = tb_CallBackUrl_His.Text.Trim();
            urlPar.getNewApkVersionUrl = bt_GetApkVersion.Text.Trim();
            urlPar.downNewApkUrl = tb_DownApkUrl.Text.Trim();
            urlPar.heartBeatUrl = tb_HeartBeatUrl.Text.Trim();
            return JsonConvert.SerializeObject(urlPar);
        }
        private void button39_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button39.Enabled = false;
                //验证URL为type=4
                string postStr = string.Format("pass={0}&callbackUrl={1}&typeId=0", Pass, setUrlJsonStr());
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
                button39.Enabled = true;

            }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                button30.Enabled = false;
                string postStr = string.Format("pass={0}", Pass);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getSendConfig";
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
                        textBox2.Text = res.data;
                        showMsg("getSendConfig 成功");
                        showMsg(res.data);
                        showAppSendConfig(res.data);
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
                button30.Enabled = true;

            }
        }
        private void showAppSendConfig(string mes)
        {
            AppSendConfig config = JsonConvert.DeserializeObject<AppSendConfig>(mes);
            showMsg(String.Format("解析,设备ID[{0}]", config.devId));
            showMsg(String.Format("解析,设备方向[{0}]", config.inOut));
            showMsg(String.Format("解析,历史记录推送时间间隙[{0}]", config.sendHisDataInterval));
            showMsg("");
        }
        private void button43_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            try
            {
                if (DialogResult.Cancel == MessageBox.Show("是否重启App?", "请确认", MessageBoxButtons.OKCancel))
                {
                    return;
                }
                button43.Enabled = false;
                string postStr = string.Format("pass={0}", Pass);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/device/restart";
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
                        textBox1.Text = res.data;
                        showMsg("restart 成功");
                        showMsg(res.data);
                        //SendDevRefreshData();
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
                button43.Enabled = true;

            }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            try
            {
                button41.Enabled = false;
                string postStr = string.Format("pass={0}&sendconfig={1}", Pass, textBox2.Text.Trim());
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setSendConfig";
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
                        showMsg("setSendConfig 成功");
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
                button41.Enabled = true;

            }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            string Url = string.Format("http://{0}:{1}", "127.0.0.1", 8091);
            Process.Start(Url);
        }

        private void button45_Click(object sender, EventArgs e)
        {
            Pass = tb_Pass.Text;
            try
            {
                button45.Enabled = false;
                string postStr = string.Format("pass={0}", Pass);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/getTodayRecord";
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
                        showMsg("getTodayRecord 成功");
                        showTodayRecord(res.data);
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
                button45.Enabled = true;

            }
        }
        private void showTodayRecord(string mes)
        {
            TodayRecord today = JsonConvert.DeserializeObject<TodayRecord>(mes);
            showMsg(String.Format("解析,当天所有识别记录数[{0}]", today.All));
            showMsg(String.Format("解析,当天实时推送记录数[{0}]", today.SendNow));
            showMsg(String.Format("解析,当天历史推送记录数[{0}]", today.SendHis));
            showMsg(String.Format("解析,当天未推送记录数[{0}]", today.NotSend));
            showMsg(String.Format("解析,当天识别记录，但因为未设置回调URL，则不用回调[{0}]", today.NoCallUrl));
            showMsg("");
        }

        private void button46_Click(object sender, EventArgs e)
        {
            timer2.Enabled = true;
        }
        private int button29Client = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            button29_Click(sender, e);
            button29Client++;
            button46.Text = button29Client.ToString();
        }
    }
    /// <summary>
    /// 当天识别的记录情况
    /// </summary>
    public class TodayRecord
    {
        /// <summary>
        /// 当天所有识别记录数
        /// </summary>
        public int All;
        /// <summary>
        /// 当天实时推送记录数
        /// </summary>
        public int SendNow;
        /// <summary>
        /// 当天历史推送记录数
        /// </summary>
        public int SendHis;
        /// <summary>
        /// 当天未推送记录数
        /// </summary>
        public int NotSend;
        /// <summary>
        /// 当天识别记录，但因为未设置回调URL，则不用回调
        /// </summary>
        public int NoCallUrl;


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

    /// <summary>
    /// 人员对象
    /// </summary>
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
            return string.Format("id:[{0}],name:[{1}]", id, name);
        }
    }
    /// <summary>
    /// 人员带照片的对象
    /// </summary>
    public class PersonAndFace
    {
        /// <summary>
        /// 人员信息
        /// </summary>
        public Person person { get; set; }
        /// <summary>
        /// 对应的照片的信息
        /// </summary>
        public Face face { get; set; }
    }
    public class Face
    {
        /// <summary>
        /// 人员id
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 人员姓名，不更新人员数据
        /// 只是根据此数据生成设备注册照片名
        /// </summary>
        public string userName { get; set; }
        //public string faceId { get; set; }
        /// <summary>
        /// 人员照片的序号
        /// </summary>
        public int direct { get; set; }
        /// <summary>
        /// 照片ID，唯一
        /// </summary>
        public string imageId { get; set; }
        /// <summary>
        /// 照片key,可用照片的md5值
        /// </summary>
        //public string faceImageFileName { get; set; }
        public string imageKey { get; set; }
        /// <summary>
        /// 照片base64
        /// </summary>
        public string imageBase64 { get; set; }
    }
    public class FaceFind
    {
        public string faceId { get; set; }
        public string personId { get; set; }
        public int direct { get; set; }
        public string faceImageFaileName { get; set; }
        public string faceImageKey { get; set; }
        public override string ToString()
        {
            return string.Format("faceId:[{0}],personId:[{1}],direct:[{2}],faceImageFaileName:[{3}],faceImageKey:[{4}]",
                faceId, personId, direct, faceImageFaileName, faceImageKey);
        }

    }
    /// <summary>
    /// 心跳包数据
    /// </summary>
    public class DevicesHeartBeat
    {
        /// <summary>
        /// 设备编号，可用户自定义，如果没有自定义，则返回机器码。
        /// </summary>
        public String deviceKey { get; set; }
        /// <summary>
        /// 设备类型
        /// 具体定义可咨询厂家
        /// </summary>
        public int deviceType { get; set; }
        /// <summary>
        /// 设备的机器码
        /// </summary>
        public String deviceMachineCode { get; set; }
        /// <summary>
        /// 运行时间
        /// 格式：天d小时h分钟m
        /// </summary>
        public String runtime { get; set; }
        /// <summary>
        /// APP启动的时间
        /// </summary>
        public String starttime { get; set; }
        /// <summary>
        /// 当前系统时间
        /// </summary>
        public String time { get; set; }
        public String ip { get; set; }
        public int personCount { get; set; }
        public int faceCount { get; set; }
        public String version { get; set; }
        /// <summary>
        /// APP占用内存
        /// </summary>
        public float memory { get; set; }
        /// <summary>
        /// 系统可用内存
        /// </summary>
        public float availMem { get; set; }
        /// <summary>
        /// 系统总内存
        /// </summary>
        public float totalMem { get; set; }
        /// <summary>
        /// 硬盘大小
        /// </summary>
        public float totalDisk { get; set; }
        /// <summary>
        /// 设备的机器名
        /// </summary>
        public string buildModel { get; set; }
        /// <summary>
        /// 是否初始化
        /// 如果通信密码非空，则认为是初始化完成。
        /// 如果通信密码为空，则认为未初始化，不能操作设备
        /// </summary>
        public int isInited { get; set; }
        /// <summary>
        /// 是否授权
        /// </summary>
        public int isAuth { get; set; }
        /// <summary>
        /// 已使用的硬盘
        /// </summary>
        public float disk { get; set; }
    }


    /// <summary>
    /// 管理URL类
    /// </summary>
    public class UrlPar
    {
        public string downNewApkUrl { get; set; }
        public string getNewApkVersionUrl { get; set; }
        public string heartBeatUrl { get; set; }
        public string identifyCallBack { get; set; }
        public string identifyCallBack_His { get; set; }
        public string verifyCallBack { get; set; }
    }


}
