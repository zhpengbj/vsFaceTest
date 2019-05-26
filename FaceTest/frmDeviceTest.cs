using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace FaceTest
{
    public partial class frmDeviceTest : Form
    {
        public frmDeviceTest()
        {
            InitializeComponent();
        }

        private void frmDeviceTest_Load(object sender, EventArgs e)
        {
            TaskList.Add(1,new MTask(1, "补光灯"));
            TaskList.Add(2, new MTask(2, "风扇"));
            TaskList.Add(3, new MTask(3, "屏幕"));
            TaskList.Add(4, new MTask(4, "网口"));
            TaskList.Add(5, new MTask(5, "摄像头"));
            TaskList.Add(6, new MTask(6, "提示灯"));
            TaskList.Add(7, new MTask(7, "语音"));
            TaskList.Add(8, new MTask(8, "开关量"));
            TaskList.Add(9, new MTask(9, "韦根"));
            TaskList.Add(10, new MTask(10, "电池"));
            TaskList.Add(11, new MTask(11, "初始化"));
        }

        #region 常量

        /// <summary>
        /// 设备类型ID =6
        /// </summary>
        private readonly int DeviceType = 0;
        private readonly string PASS = "123";
        /// <summary>
        /// 出错文件 
        /// </summary>
        private readonly string FacePicErrPath = Application.StartupPath + @"\FacePicErr\";
        /// <summary>
        /// 照片文件对应的key,只在的文件目录
        /// </summary>
        private readonly string FacePicDataPath = Application.StartupPath + @"\FacePicKey\";
        private readonly string FacePicPath = Application.StartupPath + @"\FacePicTest\";
        #endregion

        #region 变量

        private List<DevicesHeartBeat> DevicList = new List<DevicesHeartBeat>();
        public Dictionary<int, MTask> TaskList = new Dictionary<int, MTask>();
        
      

        List<User> userList;
        Dictionary<string, User> userDic;
        #endregion
        #region showinfo
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
                        s = string.Format("[{0}],{1}\r\n", DateTime.Now.ToString("hh:mm:ss"), msg);
                    }
                    else
                    {
                        s = "\r\n";
                    }

                    receiveMsg2.AppendText(s);
                    receiveMsg2.Select(receiveMsg2.Text.Length, 0);
                    receiveMsg2.ScrollToCaret();
                }
            }
        }
        public delegate void dShowInfo2(string str);
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
                        s = string.Format("[{0}],{1}\r\n", DateTime.Now.ToString("hh:mm:ss"), msg);
                    }
                    else
                    {
                        s = "\r\n";
                    }
                    //if (receiveMsg.Lines.Length > 200)
                    //{
                    //    receiveMsg.Clear();
                    //}
                    receiveMsg.AppendText(s);
                    receiveMsg.Select(receiveMsg.Text.Length, 0);
                    receiveMsg.ScrollToCaret();
                }
            }
        }
        public delegate void dShowInfo(string str);
        #endregion

        
        private void button1_Click(object sender, EventArgs e)
        {
            receiveMsg.Clear();
            receiveMsg2.Clear();
            DevicList.Clear();
            GetDevices();
          
        }
        private string GetUrl(string Ip)
        {
            return string.Format(@"http://{0}:8090",Ip);

        }
        private void setPw(string Pass)
        {
            if (DevicList.Count == 0)
                return;

            try
            {
                string postStr = string.Format("oldPass={0}&newPass={1}", Pass, Pass);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/setPassWord";
                string url = string.Format(@"{0}{1}", GetUrl(DevicList[0].ip), urlOper);
                ///person/createOrUpdate
                showMsg2("url:" + url);
                showMsg2("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg2(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg2("setPassWord 成功");
                        showMsg("setPassWord 成功");
                    }
                    else
                    {
                        showMsg2("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg2("通讯失败");
                }

            }
            finally
            {

            }
        }
        private void DownUser()
        {
           

            if (DevicList.Count>0)
            {

                try
                {
                    DirectoryInfo di = new DirectoryInfo(FacePicPath);
                    //  FileInfo[] fis = di.GetFiles("*.jpg");
                    List<FileInfo> fis = di.GetFiles("*.*", SearchOption.AllDirectories).Where(s => s.Name.ToLower().EndsWith(".png") || s.Name.ToLower().EndsWith(".jpg")).ToList<FileInfo>();
                    userList = new List<User>();
                    userDic = new Dictionary<string, User>();
                    //Parallel.ForEach(fis, b =>
                    foreach (FileInfo b in fis)
                    {
                        string aFile = b.Name;
                        User u = GetUserByFileName(b);
                        userList.Add(u);
                        if (!userDic.ContainsKey(u.imageKey))
                        {
                            userDic.Add(u.imageKey, u);
                        }
                        //if (cb_saveImageKey.Checked)
                        {
                            System.IO.File.WriteAllText(FacePicDataPath + u.FileName + "_Key.dat", u.imageKey);
                            System.IO.File.WriteAllText(FacePicDataPath + u.FileName + "_Base64.dat", u.imageBase64);
                        }
                        showMsg2(string.Format("处理照片列表,FileName[{0}],特征值[{1}]", b.Name,  u.imageKey));
                    }
                    //);
                    showMsg(string.Format("处理总人数[{0}]", fis.Count ));
                }
                catch (Exception ex)
                {
                    showMsg2(ex.ToString());
                }
                finally
                {
                }

                try
                {
                    //删除imageKeyList之外的照片
                    string imageKeys = GetImageKeyList();
                    bool isDelete = true;
                    string postStr = string.Format("pass={0}&isDelete={1}&imageKeys={2}", PASS, isDelete.ToString().ToLower(), imageKeys);
                    //string urlOper = @"/person/createOrUpdate";
                    string urlOper = @"/user/findDifference";
                    string url = string.Format(@"{0}{1}", GetUrl(DevicList[0].ip), urlOper);
                    ///person/createOrUpdate
                    showMsg2("url:" + url);
                    showMsg2("postStr:" + postStr);




                    string ReturnStr = "";
                    bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                    if (b)
                    {
                        showMsg2(ReturnStr);
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
                            showMsg2("需要增加的记录数：" + imageKeyListAdd.Count);
                            showMsg2("需要删除的记录数：" + imageKeyListDelete.Count);

                            urlOper = @"/user/createOrUpdate";
                            url = string.Format(@"{0}{1}", GetUrl(DevicList[0].ip), urlOper);
                            int i = 0;
                            foreach (string key in imageKeyListAdd)
                            {
                                i++;
                                ReturnStr = "";
                                if (userDic.ContainsKey(key))
                                {
                                    User u = userDic[key];
                                    postStr = string.Format("pass={0}&user={1}", PASS, JsonConvert.SerializeObject(u));
                                    bool bcreateImage = CHttpPost.Post(url, postStr, ref ReturnStr);
                                    showMsg2(string.Format("增加照片[{0}/{1}],FileName[{2}],特征值[{3}]，ReturnStr[{4}]", i, imageKeyListAdd.Count, u.userName, u.imageKey, ReturnStr));
                                    showMsg(string.Format("增加照片[{0}/{1}],FileName[{2}],特征值[{3}]，ReturnStr[{4}]", i, imageKeyListAdd.Count, u.userName, u.imageKey, ReturnStr));
                                    if (bcreateImage)
                                    {
                                        res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                                        if (res.success)
                                        {
                                            showMsg2("");
                                        }
                                        else
                                        {
                                            showMsg2(string.Format("有返回，但出错了[{0}]:{1}", res.msgtype, res.msg));
                                            File.Copy(u.FilePath, FacePicErrPath + "err" + res.msgtype + "_" + u.FileName + ".jpg", true);
                                        }

                                    }
                                    else
                                    {
                                        showMsg2("通讯失败");
                                    }
                                }

                            }
                            //处理完成后，发消息给设备更新数据
                            SendDevRefreshData();
                        }
                        else
                        {
                            showMsg2("有返回，但出错了：" + res.msg);
                        }
                    }
                    else
                    {
                        showMsg2("通讯失败");
                    }
                }
                finally
                {
                }


            }
        }
        private void SendDevRefreshData()
        {
            try
            {
                // button9.Enabled = false;
                string postStr = string.Format("pass={0}", PASS);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/refresh";
                string url = string.Format(@"{0}{1}", GetUrl(DevicList[0].ip), urlOper);
                ///person/createOrUpdate
                showMsg2("url:" + url);
                showMsg2("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg2(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg2("refresh 成功:" + res.msg + "********************************");
                        showMsg("refresh 成功:");
                    }
                    else
                    {
                        showMsg2("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg2("通讯失败");
                }

            }
            finally
            {
                // button9.Enabled = true;

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

        private User GetUserByFileName(FileInfo file)
        {
            string tb_SplitChar = "_";
            User u = new User();
            //得到文件名。不包括扩展名
            string fileName = file.Name.Substring(file.Name.LastIndexOf("\\") + 1, (file.Name.LastIndexOf(".") - file.Name.LastIndexOf("\\") - 1));
            u.FileName = fileName;
            u.FilePath = file.FullName;

            if (!string.IsNullOrEmpty(tb_SplitChar))
            {
                string[] fileNameList = fileName.Split(tb_SplitChar.ToCharArray());
                if (fileNameList.Length == 1)
                {
                    u.userId = fileNameList[0];
                    u.userName = fileNameList[0];
                    u.cardNo = "";
                }
                if (fileNameList.Length == 2)
                {
                    u.userId = fileNameList[0];
                    u.userName = fileNameList[1];
                    u.cardNo = "";
                }
                if (fileNameList.Length == 3)
                {
                    u.userId = fileNameList[0];
                    u.userName = fileNameList[1];
                    u.cardNo = fileNameList[2];
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
        protected string ImgToBase64String(string Imagefilename)
        {
            try
            {
                byte[] arr = System.IO.File.ReadAllBytes(Imagefilename);
                showMsg2("ImgToBase64String arr length:" + arr.Length);
                return Convert.ToBase64String(arr, Base64FormattingOptions.InsertLineBreaks);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private void GetDevices()
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
                                showMsg2(String.Format("设备[{0}]:[{1}]", endpointR.Address, msg));
                                DevicesHeartBeat d = showDevInfo(msg);
                                if (d.deviceType != DeviceType)
                                {
                                    showMsg(String.Format("!!!!!!设备类型不匹配，仅支持类型[{0}]", DeviceType));
                                }
                                else
                                {
                                    DevicList.Add(d);
                                }
                            }
                        }
                    }
                    catch (SocketException ex)
                    {
                        showMsg2(ex.ToString());
                    }

                    Thread.Sleep(1000);
                }
            });
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

        private DevicesHeartBeat showDevInfo(string s)
        {
            DevicesHeartBeat device = JsonConvert.DeserializeObject<DevicesHeartBeat>(s);
            showMsg2(String.Format("解析,机器码[{0}]", device.deviceMachineCode));
            showMsg2(String.Format("解析,机器编号[{0}]", device.deviceKey));
            showMsg2(String.Format("解析,Ip[{0}]", device.ip));
            showMsg2(String.Format("解析,系统时间[{0}]", device.time));
            showMsg2(String.Format("解析,人员数[{0}]", device.personCount));
            showMsg2(String.Format("解析,照片数[{0}]", device.faceCount));
            showMsg2(String.Format("解析,运行时间[{0}]", device.runtime));
            showMsg2(String.Format("解析,版本号[{0}]", device.version));
            showMsg2(String.Format("解析,App占用内存[{0}mb]-[{1:F}%]", device.memory, device.memory / device.totalMem * 100.00));
            showMsg2(String.Format("解析,系统可用内存[{0}mb]-[{1:F}%]", device.availMem, device.availMem / device.totalMem * 100.00));
            showMsg2(String.Format("解析,系统总内存[{0}mb]", device.totalMem));
            showMsg2("");


            showMsg(String.Format("找到设备,机器码[{0}]", device.deviceMachineCode));
            showMsg(String.Format("IP:[{0}]", device.ip));
            showMsg(String.Format("设备类型[{0}]", device.deviceType));

            return device;
        }

        private void changeColor(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.ForeColor == Color.Green)
            {
                button.ForeColor = Color.Red;
                return;
            }
            button.ForeColor = Color.Green;
            doTask(button.Tag.ToString(), button.ForeColor == Color.Green);

        }
        private void doTask(string taskName,bool result)
        {
            int taskNo = Convert.ToInt32(taskName);
            TaskList[taskNo].Result = result;
            TaskList[taskNo].Dt = DateTime.Now;



        }

        private void button13_Click(object sender, EventArgs e)
        {
            setPw(PASS);
            DownUser();
        }

        private void button32_Click(object sender, EventArgs e)
        {
            try
            {
                if (DialogResult.Cancel == MessageBox.Show("是否进行初始化设备?", "请确认", MessageBoxButtons.OKCancel))
                {
                    return;
                }
                button32.Enabled = false;
                string postStr = string.Format("pass={0}&delete={1}", PASS, true);
                //string urlOper = @"/person/createOrUpdate";
                string urlOper = @"/device/reset";
                string url = string.Format(@"{0}{1}", GetUrl(DevicList[0].ip), urlOper);
                ///person/createOrUpdate
                showMsg2("url:" + url);
                showMsg2("postStr:" + postStr);

                string ReturnStr = "";
                bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
                if (b)
                {
                    showMsg2(ReturnStr);
                    ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
                    if (res.success)
                    {
                        showMsg2("reset 成功,App重启！");
                        showMsg2(res.data);
                        //SendDevRefreshData();
                    }
                    else
                    {
                        showMsg2("有返回，但出错了：" + res.msg);
                    }
                }
                else
                {
                    showMsg2("通讯失败");
                }

            }
            finally
            {
                button32.Enabled = true;

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (DevicList.Count==0)
            {
                MessageBox.Show("无设备");
                return;
            }
            string strTest = JsonConvert.SerializeObject(DevicList[0]) + "|" + JsonConvert.SerializeObject(TaskList);
            System.IO.File.WriteAllText(DevicList[0].deviceMachineCode, strTest, Encoding.UTF8);
        }
    }

    public class MTask
    {
        public string Name { get; set; }
        public int No { get; set; }
        public bool Result { get; set; }
        public DateTime Dt { get; set; }
        public MTask(int No,string Name)
        {
            this.No = No;
            this.Name = Name;
        }
    }

}
