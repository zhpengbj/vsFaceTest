using CassiniDev;
using FaceTest.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FaceTest
{
    public partial class FrmServer : Form
    {
        delegate void dispEntryRecord(string strPersonId, string name, string DiviDesc, string DepaCode, string EntryDate);

        //delegate void deleShowUserInfo(Label labDivi, Label labDepa, Label labName, Label labTime, string diviDesc, string depaDesc, string staffName, string strEntry_dt);
        //delegate void dispLabeText(System.Windows this, string strMsg);
        static HttpListener httpobj;
        //private NamedPipeServerStream pipeServer;

        private const string PipeName = "testpipe";

        private const int PipeInBufferSize = 65535;

        private const int PipeOutBufferSize = 65535;

        private Encoding encoding = Encoding.UTF8;
        Boolean stopServer = false;
        //private Boolean bServerOn = false;//开启了服务
        DateTime theDateTimes;//记录同步时间
        public FrmServer()
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
        //NamedPipeServerStream pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 4, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
        private Settings settings = new Settings();
        /// <summary>
        /// 读取设置项
        /// </summary>
        private void LoadData()
        {


        }
        /// <summary>
        /// 保存设置项
        /// </summary>
        private void SaveData()
        {

            settings.Save();
        }

        //private void connDataBase()
        //{
        //    try
        //    {
        //        //数据库配置
        //        showMsg("测试数据库连接参数!");
        //        txtServer.Text = GetConfig.config.ServerName;
        //        txtDataBase.Text = GetConfig.config.DataBase;
        //        txtUserID.Text = GetConfig.config.UserID;
        //        txtPassWord.Text = GetConfig.config.PassWord;
        //        if (txtServer.Text == "" || txtDataBase.Text == "" || txtUserID.Text == "")
        //        {
        //            MessageBox.Show("数据库配置有误,请检查！");//SV0125
        //            this.Close();
        //        }
        //        DataSet myDs;

        //        ReadData.Glb_CnnStr = "Driver={SQL Server};Server=" + GetConfig.config.ServerName + ";Pwd=" + GetConfig.config.PassWord +
        //            ";Database=" + GetConfig.config.DataBase + ";Uid=" + GetConfig.config.UserID + ";";

        //        if (ReadData.OpenConnect_Odbc() == false)
        //        {
        //            MessageBox.Show("数据库配置失败！");//SV0125
        //            this.Close();
        //        }
        //        string strSql;
        //        string strBack = "";
        //        //===========
        //        //验证数据库的正确性
        //        strSql = "select * from tbluser where user_id='sysadmin'";// and password='" + this.txtPassWord.Text + "'";
        //        if (ReadData.OpenConnect_Odbc() == true)
        //        {
        //            myDs = new DataSet();
        //            myDs = ReadData.get_DataSet(strSql, ref strBack);
        //            if (strBack != "")
        //            {
        //                MessageBox.Show("程序错误：" + strBack);//SV0125
        //                this.Close();
        //                return;
        //            }
        //        }
        //        showMsg("数据库连接成功!");
        //    }
        //    catch (Exception err)
        //    {
        //        showMsg("connDataBase error:" + err.Message);
        //        MessageBox.Show("connDataBase error:" + err.Message);
        //        ReadData.writeErrorLog("connDataBase error:" + err.Message);
        //        throw;
        //    }
        //}

      

        private string getdataStrKey(string dataStr, string strKey)//
        {

            string strdataStrKey = "";
            try
            {
                JsonReader reader = new JsonTextReader(new StringReader(dataStr));
                if (dataStr.IndexOf(strKey) >= 0)
                {
                    while (reader.Read())
                    {
                        if (reader.Value != null)
                        {
                            if (reader.Value.ToString() == strKey)
                            {
                                reader.Read();
                                if (reader.Value != null)
                                {
                                    strdataStrKey = reader.Value.ToString();
                                }
                            }
                        }
                    }
                }
                return strdataStrKey;
            }
            catch (Exception err)
            {
                showMsg("getdataStrKey error:" + err.Message);
                //MessageBox.Show("connDataBase error:" + err.Message);
                //ReadData.writeErrorLog("getdataStrKey error:" + err.Message);
                //writeLog("getlivenessScore err:" + err.Message);//写日志记录
                //return iScore;
                throw;
            }
        }

        /// <summary>
        /// 显示人员信息
        /// </summary>
        /// <param name="result"></param>
        private void ShowInfo(string result)
        {
            try
            {
                string strEntry_dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string strFaceSN = "";
                string depaDesc = "";
                string diviDesc = "";
                string emp_type = "1";
                string staffName = "";
                string dataStr = "";
                //FaceRecord.writeDebugLog("收到数据：" + result);
                if (result.IndexOf("HeartBeat receive") == 0)
                {
                    //通过data:截取、得到接收的对象
                    dataStr = result.Substring(result.IndexOf("data:") + 5, result.Length - result.IndexOf("data:") - 5);
                    //FaceRecord.writeDebugLog("收到心跳：" + dataStr);
                    //FaceRecord.writeFaceHeartBeatLog(dataStr);
                    strFaceSN = getdataStrKey(dataStr, "deviceKey");//
                    //FaceRecord.writeDebugLog("deviceKey：" + strFaceSN);
                    //FaceRecord.updataFace_Device_state(strFaceSN);
                    //FaceRecord.writeDebugLog("更新设备状态：" + strFaceSN);
                    //如果是心跳包数据，则退出
                    return;
                }
                if (result.IndexOf("data:") < 0)
                {
                    return;
                }
                //通过data:截取、得到接收的对象
                //FaceRecord.writeDebugLog("收到data：" + result);
                dataStr = result.Substring(result.IndexOf("data:") + 5, result.Length - result.IndexOf("data:") - 5);
                //FaceRecord.writeDebugLog("提取data：" + dataStr);
                //FaceRecord.writeFaceAttnLog(dataStr);
                //result.
                Verify v = JsonConvert.DeserializeObject<Verify>(dataStr);
                //FaceRecord.writeDebugLog("收到data=>Json");
                this.Invoke((MethodInvoker)delegate
                {
                    if (v != null)
                    {
                        labPersonId.Text = "" + v.userId;
                        labStaff.Text = "" + v.userName;//用户姓名:
                        //lb_Path.Text = "照片路径:" + v.path;====>
                        strFaceSN = v.deviceKey;
                        strEntry_dt = v.time;
                        if (v.type == "1")//只有识别正确的记录才处理
                        {
                            if (!string.IsNullOrEmpty(v.path))
                            {
                                picFacePic.LoadAsync(v.path);
                            }
                            //保存识别记录，数据库，日志，上传动力加待发表，
                            string strLinkId = "";
                            //FaceRecord.queryStaffAndSaveEntry(labPersonId.Text, strEntry_dt, strFaceSN, ref emp_type, ref diviDesc, ref depaDesc, ref staffName, ref strLinkId);
                            ////显示图片，从电脑上
                            //labDivi.Text = diviDesc;
                            //labDepa.Text = depaDesc;
                            //labStaff.Text = staffName;
                            //labTime.Text = Convert.ToDateTime(strEntry_dt).ToString("HH:mm:ss");
                            //showHisStaffPhoto();//历史记录列表
                            //FaceRecord.showPhotoFromPC(emp_type, diviDesc, depaDesc, staffName, this.picStaffPhoto);//从本地找相片
                            //labTime1.Text = strEntry_dt;
                            //labStaff1.Text = staffName + "(" + depaDesc + ")";
                            //picStaffPhoto1.Image = picStaffPhoto.Image;
                            ////showMsg(v.base64 != null ? v.base64.Length.ToString() : "");
                            //FaceRecord.savePhotoToLoca(strLinkId, labPersonId.Text, strEntry_dt, this.picFacePic);
                        }
                    }
                    else
                    {
                        labPersonId.Text = "";
                        labStaff.Text = "";
                    }
                });
            }
            catch (Exception err)
            {
                showMsg("ShowInfo error:" + err.Message);
                //MessageBox.Show("ShowInfo error:" + err.Message);
                //ReadData.writeErrorLog("ShowInfo error:" + err.Message);
                throw;
            }
        }
        private void showHisStaffPhoto()
        {

            //labTime6.Text = labTime5.Text;
            //labTime5.Text = labTime4.Text;
            //labTime4.Text = labTime3.Text;
            //labTime3.Text = labTime2.Text;
            //labTime2.Text = labTime1.Text;

            //labStaff6.Text = labStaff5.Text;
            //labStaff5.Text = labStaff4.Text;
            //labStaff4.Text = labStaff3.Text;
            //labStaff3.Text = labStaff2.Text;
            //labStaff2.Text = labStaff1.Text;

            //picStaffPhoto6.Image = picStaffPhoto5.Image;
            //picStaffPhoto5.Image = picStaffPhoto4.Image;
            //picStaffPhoto4.Image = picStaffPhoto3.Image;
            //picStaffPhoto3.Image = picStaffPhoto2.Image;
            //picStaffPhoto2.Image = picStaffPhoto1.Image;

        }

        private void beginServer()
        {
            try
            {
                //1,本地管理:则在本机编辑，2，相片来源于平台，则本地不管理相片。
                //if (GetConfig.config.photoFromType.Substring(0, 1) == "2")
                //{
                //    showMsg("基础数据同步模式:" + GetConfig.config.photoFromType);
                //    Start_DownRecord_FaceEmp();// 用多线程处理
                //}
                //else
                //{
                //    if (chkBasFromServer.Checked == true)
                //    {
                //        Thread.Sleep(50);
                //        showMsg("基础数据同步模式:" + GetConfig.config.photoFromType);
                //        Thread thread_Start_DljData = new Thread(Start_DownDljData);
                //        thread_Start_DljData.IsBackground = false;
                //        thread_Start_DljData.Start();
                //        stopServer = false;
                //    }
                //}
                //HTTP方式上传考勤数据到DLJ,上传设备心跳
                //if (GetConfig.config.httpSendMessage == true)
                {
                    //StartHttp_SendMsgServerThresd();// 用多线程处理
                }
            }
            catch (Exception err)
            {
                showMsg("beginServer error:" + err.Message);
                MessageBox.Show("beginServer error:" + err.Message);
                //ReadData.writeErrorLog("beginServer error:" + err.Message);
                throw;
            }
        }

        //从动力加同步注册数据
        //private void Start_DownRecord_FaceEmp()
        //{
        //    Thread.Sleep(50);
        //    Thread thread_Http_doAutoSendMessage = new Thread(Start_DownRecord_FaceEmp_Server);
        //    thread_Http_doAutoSendMessage.IsBackground = false;
        //    thread_Http_doAutoSendMessage.Start();
        //    stopServer = false;
        //}

        //private void StartHttp_SendMsgServerThresd()
        //{
        //    Thread.Sleep(50);
        //    Thread thread_Http_doAutoSendMessage = new Thread(Start_Http_doAutoSendMessage);
        //    thread_Http_doAutoSendMessage.IsBackground = false;
        //    thread_Http_doAutoSendMessage.Start();
        //    stopServer = false;
        //}


        //void Start_DownRecord_FaceEmp_Server()//处理信息
        //{
        //    try
        //    {
        //        showMsg("*****开始同步人脸注册数据(下载人脸待注册信息,注册人脸到设备,*****");
        //        while (stopServer == false)
        //        {
        //            if (stopServer == true) return;
        //            if (this.txtScyTime.Text.Trim() == "") this.txtScyTime.Text = "2";
        //            TimeSpan midTime = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(theDateTimes);//回收时间与当前时间比较
        //            if (midTime.TotalMinutes > Convert.ToInt16(this.txtScyTime.Text)) //多久同一次
        //            {
        //                //下载人脸待注册信息
        //                DataFromDlj.DownRecord_FaceEmp();//                   
        //                showMsg("--------下载人脸待注册信息");
        //                //从下发表找到待下发人脸，下发到设备并记录状态
        //                FaceRecord.doStart_Add_Face();
        //                showMsg("********注册人脸到设备");
        //                //上传人脸注册状态到动力加平台 
        //                DataFromDlj.upRecord_FaceEmp();
        //                showMsg("========上传人脸注册结果");
        //                //记录最后一次更新时间
        //                theDateTimes = DateTime.Now;
        //            }
        //            else
        //            {
        //                Thread.Sleep(1000);
        //            }
        //        }
        //        if (stopServer == true) return;
        //    }
        //    catch (Exception err)
        //    {
        //        showMsg("beginServer error:" + err.Message);
        //        MessageBox.Show("beginServer error:" + err.Message);
        //        ReadData.writeErrorLog("beginServer error:" + err.Message);
        //        throw;
        //    }
        //}

        //private void doStart_DownDljFaceEmp_back()
        //{
        //    try
        //    {
        //        if (stopServer == true) return;
        //        Application.DoEvents();
        //        string strBack = "";
        //        if (this.txtScyTime.Text.Trim() == "") this.txtScyTime.Text = "2";
        //        TimeSpan midTime = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(theDateTimes);//回收时间与当前时间比较
        //        if (midTime.TotalMinutes > Convert.ToInt16(this.txtScyTime.Text)) //多久同一次
        //        {
        //            if (stopServer == true) return;
        //            //if (progressBar1.Value < progressBar1.Maximum) progressBar1.Value = progressBar1.Value + 1;
        //            ReadData.writeApiDataLog("开始数据同步人脸注册记录！");
        //            //listBox1.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff" + ":启动同步！"));
        //            //this.BeginInvoke(new dispSycnRecord(addListRecord), this.listBox1, "---开始数据同步！---");
        //            showMsg("---开始数据同步人脸注册记录！---");
        //            //this.progressBar1.Visible = true;
        //            Thread.Sleep(1000);
        //            //this.progressBar1.Maximum = 4;
        //            //1同步年级
        //            string strEmpTypePath = "PHOTO";//人员类型 "1";学生; 教师 strEmpTypePath = "TchPhoto"; 家长,strEmpTypePath = "FamPhoto";
        //            //SetUserInfo(string ip, string port, string Pass, string ExpDate, string FacePic, string FaceData, string CardSn, string CardNo, string UserName, string DeptName, string UserNo, ref string strBackMsg)
        //            string strFingerNo = "";//xFace,人脸id=EmpNo
        //            string strSql = "";
        //            string strFaceNo = "";
        //            string strFaceIP = "";
        //            string strPort = "";
        //            string strPass = "";

        //            string strName = "";
        //            string strDivi = "";
        //            string strDepa = "";
        //            string strDljID = "";
        //            string imageId = "";
        //            string strEmpNo = "";
        //            string pathName = "";
        //            string strSegIndex = "";
        //            strBack = "";
        //            strSql = "select divi_desc,depa_desc,Face_EmpInfo.emp_no,FingerNo,fingerName,Face_Device.faceNo,ip,port,macPsws from Face_EmpInfo left join Face_Device on  " +
        //                "Face_EmpInfo.FaceNo = Face_Device.FaceNo left join xy_staff on  Face_EmpInfo.emp_no = xy_staff.emp_no " +
        //                "left join xy_depacode on  xy_depacode.depa_cod = xy_staff.depa_cod   where Face_EmpInfo.status='2' order by  FingerNo";
        //            DataSet myDs = new DataSet();
        //            if (ReadData.OpenConnect_Odbc() == true)
        //            {
        //                myDs = ReadData.get_DataSet(strSql, ref strBack);
        //                if (strBack == "")
        //                {
        //                    if (myDs.Tables[0].Rows.Count > 0)
        //                    {
        //                        strEmpNo = myDs.Tables[0].Rows[0]["emp_no"].ToString();
        //                        strDivi = myDs.Tables[0].Rows[0]["divi_desc"].ToString();
        //                        strDepa = myDs.Tables[0].Rows[0]["depa_desc"].ToString();
        //                        strName = myDs.Tables[0].Rows[0]["fingerName"].ToString();

        //                        strFaceIP = myDs.Tables[0].Rows[0]["ip"].ToString();
        //                        strPort = myDs.Tables[0].Rows[0]["Port"].ToString();
        //                        strPass = myDs.Tables[0].Rows[0]["macPsws"].ToString();

        //                        strFingerNo = myDs.Tables[0].Rows[0]["FingerNo"].ToString();//有注册人脸记录
        //                        strFaceNo = myDs.Tables[0].Rows[0]["faceNo"].ToString();//有注册人脸记录

        //                        pathName = Application.StartupPath + "\\" + strEmpTypePath + "\\" + strDivi + "\\" + strDepa + "\\" + strName + ".jpg";

        //                        strBack = "";
        //                        imageId = strEmpNo + "_" + strName + "_" + "0";
        //                        if (File.Exists(@pathName))
        //                        {
        //                            if (xyFaceManage.SetUserInfo(strFaceIP, strPort, strPass, "2059-12-31", pathName, "", "", strFingerNo, strName, strDepa, strEmpNo, ref strBack) == true)//新增人员人脸
        //                            {
        //                                xyFaceManage.upDate_FaceEmpInfo(strFaceNo, strFingerNo, imageId, "", strEmpNo, strName, strSegIndex, "1", "下发成功!");//新增记录
        //                                //xFaceManage.addFace_EmpInfo(strFaceNo, strFingerNo, imageId, "", strEmpNo, strName, strPassTimeName, "1", "下发成功!");//新增记录
        //                                //xFaceManage.addFace_EmpFaceNo(strFingerNo, strFaceNo, strEmpNo, strName, imageId, "");//每个设备记录新增记录
        //                                strBack = "下发成功";
        //                                //showMsg((iSendCardCount).ToString() + "   ===>下发成功:" + strName);
        //                            }
        //                            else
        //                            {
        //                                strBack = "新增人脸失败:" + strBack;
        //                                if (strBack.Length > 200) strBack = strBack.Substring(200);
        //                                xyFaceManage.upDate_FaceEmpInfo(strFaceNo, strFingerNo, imageId, "", strEmpNo, strName, strSegIndex, "0", strBack);//新增记录
        //                                //xFaceManage.addFace_EmpInfo(strFaceNo, strFingerNo, imageId, "", strEmpNo, strName, strPassTimeName, "0", strBack);//新增记录
        //                                //xFaceManage.addFace_EmpFaceNo(strEmpNo, strFaceNo, strEmpNo, strName, imageId, "");//新增记录,每个设备记录
        //                                //showMsg((iSendCardCount).ToString() + "   ===> 下发失败：" + strName + "," + strBack);
        //                            }
        //                        }
        //                        else
        //                        {
        //                            strBack = "新增人脸失败,相片不存在：" + @pathName;
        //                            if (strBack.Length > 200) strBack = strBack.Substring(200);
        //                            xyFaceManage.upDate_FaceEmpInfo(strFaceNo, strFingerNo, imageId, "", strEmpNo, strName, strSegIndex, "0", strBack);//新增记录
        //                        }
        //                    }
        //                }
        //            }


        //            //6同步完成
        //            showMsg("---完成数据同步！---");
        //            //this.BeginInvoke(new dispSycnRecord(addListRecord), this.listBox1, "---完成数据同步！---");
        //            //listBox1.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff" + ":同步完成！"));
        //            if (stopServer == true) return;
        //            //4
        //            //、、Http.StartApi_Thresd();//只用来上考勤记录
        //            theDateTimes = DateTime.Now;
        //            GetConfig.config.LastScyTime = theDateTimes.ToString("yyyyMMddHHmmss");
        //            SettingsOperate.SaveSettings(GetConfig.config);
        //            //this.progressBar1.Visible = false;
        //        }
        //        else
        //        {
        //            Thread.Sleep(10000);
        //        }
        //        Application.DoEvents();
        //        return;
        //    }
        //    catch (Exception err)
        //    {
        //        showMsg("beginServer error:" + err.Message);
        //        MessageBox.Show("beginServer error:" + err.Message);
        //        ReadData.writeErrorLog("beginServer error:" + err.Message);
        //        throw;
        //    }
        //}


        void Start_Http_doAutoSendMessage()//处理信息
        {
            while (stopServer == false)
            {
                if (stopServer == true) return;
                //showMsg("*****开始同步考勤数据到平台*****");
                //SendMessage_ToDlj();//发考勤数据
                Thread.Sleep(100);
                //showMsg("----同步心跳到平台----");
                //SendDevStatus_ToDlj();//发心跳到平台
                Thread.Sleep(100);
            }
            if (stopServer == true) return;
        }

        //public void SendDevStatus_ToDlj()//发心跳到平台
        //{
        //    try
        //    {
        //        Thread.Sleep(50);
        //        string ctrller_cn = "";
        //        string strBack = "";
        //        string strSql = "select * from  Face_Device where doorState=1 and upd_dt>'" + DateTime.Now.AddSeconds(-30).ToString("yyyy-MM-dd HH:mm:ss") + "'";
        //        DataSet myDs = new DataSet();
        //        myDs = ReadData.get_DataSet(strSql, ref strBack);
        //        if (strBack != "")
        //        {
        //            ReadData.writeHttpSendMessaaeLog("后台处理机器心跳,数据查询失败,err：" + strBack + ",sql:" + strSql);
        //            showMsg(">>>>>后台处理机器心跳,数据查询失败,err：" + strBack + ",sql:" + strSql);
        //            Thread.Sleep(2000);
        //            return;
        //        }
        //        if (myDs.Tables[0].Rows.Count < 1)
        //        {
        //            Thread.Sleep(1000);
        //            return;
        //        }
        //        else
        //        {
        //            for (int iLoop = 0; iLoop <= myDs.Tables[0].Rows.Count - 1; iLoop++)
        //            {
        //                ctrller_cn = myDs.Tables[0].Rows[iLoop]["FaceNo"].ToString();
        //                ReadData.writeApi_DevCon_Log("发送心跳数据到平台，设备编号：" + ctrller_cn + ",状态：1");
        //                strBack = "";
        //                Boolean bSend = Http.DevCon(ctrller_cn, "1", ref strBack);
        //                if (bSend == true)
        //                {
        //                    showMsg(">>>>>发送心跳数据到平台,设备编号：" + ctrller_cn + ",成功!");
        //                }
        //                else
        //                {
        //                    showMsg(">>>>>发送心跳数据到平台,设备编号：" + ctrller_cn + ",失败:" + strBack);
        //                }
        //                Thread.Sleep(2000);
        //            }
        //            Thread.Sleep(10000);
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        showMsg("SendDevStatus_ToDlj(发心跳到平台 error:" + err.Message);
        //        //MessageBox.Show("beginServer error:" + err.Message);
        //        ReadData.writeErrorLog("SendDevStatus_ToDlj(发心跳到平台 error:" + err.Message);
        //        throw;
        //    }
        //}

        //public void SendMessage_ToDlj()//后台处理http发送功能
        //{
        //    string strAddTime = "";
        //    string strNowTime = "";
        //    string strSql = "";
        //    string strRemark = "";
        //    int intSendTimes = 0;
        //    Boolean bSend = false;
        //    string entry_dt = "";
        //    string card_no = "";
        //    string reader = "";
        //    string msg = "";
        //    string ctrller_cn = "";
        //    string dacc = "0";
        //    try
        //    {
        //        Thread.Sleep(200);
        //        strSql = " select top 10 *,getdate() as nowTime from xy_Http_SendMessage where mustSend=1 and (emp_no<>'' and emp_no is not null) and hasSend =0 ORDER BY SendTimes,entry_dt desc";
        //        DataSet myDs = new DataSet();
        //        myDs = ReadData.get_DataSet(strSql);
        //        if (myDs == null)
        //        {
        //            ReadData.writeHttpSendMessaaeLog("后台处理考勤数据,数据查询失败：" + strSql);
        //            showMsg(">>>>>后台处理考勤数据,数据查询失败：" + strSql);
        //            Thread.Sleep(2000);
        //            return;
        //        }
        //        if (myDs.Tables[0].Rows.Count < 1)
        //        {
        //            showMsg(">>>>>后台处理考勤数据,无数据！");
        //            Thread.Sleep(2000);
        //            return;
        //        }
        //        else
        //        {
        //            for (int iLoop = 0; iLoop <= myDs.Tables[0].Rows.Count - 1; iLoop++)
        //            {
        //                //return;
        //                entry_dt = myDs.Tables[0].Rows[iLoop]["entry_dt"].ToString();
        //                card_no = myDs.Tables[0].Rows[iLoop]["card_no"].ToString().Trim();
        //                strAddTime = myDs.Tables[0].Rows[iLoop]["add_dt"].ToString();
        //                strNowTime = myDs.Tables[0].Rows[iLoop]["nowTime"].ToString();
        //                reader = myDs.Tables[0].Rows[iLoop]["reader"].ToString();
        //                intSendTimes = Convert.ToInt16(myDs.Tables[0].Rows[iLoop]["SendTimes"].ToString());
        //                ctrller_cn = myDs.Tables[0].Rows[iLoop]["Ctrller_SN"].ToString().Trim();
        //                ///ctrller_cn = "123456";
        //                //dacc = myDs.Tables[0].Rows[iLoop]["io_Status"].ToString();
        //                //if (dacc=="")dacc="0";
        //                if (intSendTimes > 15 || card_no == "" || ctrller_cn == "") //允许延时发送(收到刷卡时间多少分钟之前的记录可以发送)
        //                {
        //                    msg = "";
        //                    strRemark = "停止发送:卡号为空、设备编号为空、超过发送次数(" + intSendTimes.ToString() + ")！！";//则标记为不用发送 
        //                    strSql = "update xy_Http_SendMessage set msg='" + msg + "',remark ='" + strRemark + "',mustSend=0, hasSend =0,upd_dt=getdate() where recno =" + myDs.Tables[0].Rows[iLoop]["recno"].ToString() + "";//处理成功改变状态
        //                }
        //                else
        //                {
        //                    string back = "";
        //                    Thread.Sleep(100);
        //                    int iKey = 49;
        //                    string TimeStamp = Http.GetTimeStamp(entry_dt);
        //                    iKey = Http.GetTimeKey(TimeStamp);
        //                    string strData = "";
        //                    if (reader == "01")
        //                    {
        //                        strData = "mid=" + GetConfig.config.httpCtrlID_in + "&iid=" + card_no + "&type=0";
        //                    }
        //                    else
        //                    {
        //                        strData = "mid=" + GetConfig.config.httpCtrlID_out + "&iid=" + card_no + "&type=1";
        //                    }

        //                    showMsg(">>>>>发送考勤数据(SendMessage.SendMessage_ToDlj): 设备编号：" + ctrller_cn + ",卡号：" + card_no + ",时间：" + Convert.ToDateTime(entry_dt).ToString("yyyyMMddHHmmss"));
        //                    ReadData.writeApi_Attn_Log("发送考勤数据(SendMessage.SendMessage_ToDlj): 设备编号：" + ctrller_cn + ",卡号：" + card_no + ",时间：" + Convert.ToDateTime(entry_dt).ToString("yyyyMMddHHmmss"));
        //                    bSend = Http.SendAttnRecordToDlj_NewKq(ctrller_cn, card_no, Convert.ToDateTime(entry_dt).ToString("yyyyMMddHHmmss"), reader.Substring(1, 1), ctrller_cn, dacc, ref back);

        //                    strRemark = "收到返回：" + back;
        //                    if (bSend == true)
        //                    {
        //                        strRemark = "发送成功！" + back;
        //                        strSql = "update xy_Http_SendMessage set  back =1,msg='" + msg + "',remark ='" + strRemark + "', SendTimes=SendTimes+1, hasSend =1,upd_dt=getdate() where recno =" + myDs.Tables[0].Rows[iLoop]["recno"].ToString() + "";//处理成功改变状态
        //                    }
        //                    else
        //                    {
        //                        strRemark = "发送失败！" + back;
        //                        strSql = "update xy_Http_SendMessage set  back =1,SendTimes=SendTimes+1,remark ='" + strRemark + "',upd_dt=getdate() where recno =" + myDs.Tables[0].Rows[iLoop]["recno"].ToString() + "";//处理不成功则次数+1
        //                    }
        //                    showMsg(strRemark);
        //                }
        //                try
        //                {
        //                    if (ReadData.Glb_OdbcConnection.State == System.Data.ConnectionState.Closed)
        //                    {
        //                        if (ReadData.OpenConnect_Odbc() == false)
        //                        {
        //                            //return false;
        //                        }
        //                    }
        //                    if (ReadData.exceInsertSql(strSql) == false)
        //                    {
        //                        if (ReadData.exceInsertSql(strSql) == false)
        //                        {
        //                            // MessageBox.Show("保存失败!");
        //                        }
        //                    }
        //                }
        //                catch (Exception err)
        //                {
        //                    //MessageBox.Show(err.Message);
        //                }
        //            }
        //        }
        //        //      }
        //        // }
        //        //}
        //        //catch (Exception err)
        //        //{
        //        //    ReadData.writeErrorLog("SendMessage.SendMessage_ToDlj:" + err.Message);
        //        //}
        //    }
        //    catch (Exception err)
        //    {
        //        showMsg("SendMessage_ToDlj error:" + err.Message);
        //        //MessageBox.Show("SendMessage_ToDlj error:" + err.Message);
        //        ReadData.writeErrorLog("SendMessage_ToDlj error:" + err.Message);
        //        throw;
        //    }
        //}


        void Start_DownDljData()//处理信息
        {
            while (stopServer == false)
            {
                if (stopServer == true) return;
                //doStart_DownDljData();

            }
            if (stopServer == true) return;
        }
        //private void doStart_DownDljData()
        //{
        //    try
        //    {
        //        if (stopServer == true) return;
        //        Application.DoEvents();
        //        string strBack = "";
        //        if (this.txtScyTime.Text.Trim() == "") this.txtScyTime.Text = "2";
        //        TimeSpan midTime = Convert.ToDateTime(DateTime.Now) - Convert.ToDateTime(theDateTimes);//回收时间与当前时间比较
        //        if (midTime.TotalMinutes > Convert.ToInt16(this.txtScyTime.Text)) //多久同一次
        //        {
        //            if (stopServer == true) return;
        //            //if (progressBar1.Value < progressBar1.Maximum) progressBar1.Value = progressBar1.Value + 1;
        //            ReadData.writeApiDataLog("开始数据同步！");
        //            //listBox1.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff" + ":启动同步！"));
        //            //this.BeginInvoke(new dispSycnRecord(addListRecord), this.listBox1, "---开始数据同步！---");
        //            showMsg("---开始数据同步！---");
        //            //this.progressBar1.Visible = true;
        //            Thread.Sleep(1000);
        //            //this.progressBar1.Maximum = 4;
        //            //1同步年级
        //            if (stopServer == true) return;
        //            ReadData.writeApiDataLog("同步年级单位信息！");
        //            //this.BeginInvoke(new dispSycnRecord(addListRecord), this.listBox1, "1 同步年级单位信息！");
        //            showMsg("1 同步年级单位信息！");
        //            //listBox1.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff" + ":同步年级/单位信息！"));
        //            // if (this.progressBar1.Value < this.progressBar1.Maximum) this.progressBar1.Value = this.progressBar1.Value + 1;
        //            Thread.Sleep(1000);
        //            strBack = Http.DownRecord_divi(false);//年级

        //            //2同步班级
        //            if (stopServer == true) return;
        //            ReadData.writeApiDataLog("同步班级科组信息！");
        //            //this.BeginInvoke(new dispSycnRecord(addListRecord), this.listBox1, "2 同步班级科组信息！");
        //            showMsg("2 同步班级科组信息！！");
        //            //if (this.progressBar1.Value < this.progressBar1.Maximum) this.progressBar1.Value = this.progressBar1.Value + 1;
        //            Thread.Sleep(1000);
        //            Http.DownRecord_Depa(false);

        //            //3同步学生
        //            if (stopServer == true) return;
        //            ReadData.writeApiDataLog("同步学生数据！");
        //            //this.BeginInvoke(new dispSycnRecord(addListRecord), this.listBox1, "3 同步学生数据！");
        //            showMsg("3 同步学生数据！");
        //            //if (this.progressBar1.Value < this.progressBar1.Maximum) this.progressBar1.Value = this.progressBar1.Value + 1;
        //            Thread.Sleep(1000);
        //            Http.DownRecord_Student(false);//学生

        //            //4  同步家长数据。。。。。";
        //            if (stopServer == true) return;
        //            showMsg("4 同步家长数据！");
        //            Thread.Sleep(1000);
        //            Http.DownRecord_Fam(false);//家长

        //            //5 同步教职工数据
        //            if (stopServer == true) return;
        //            //listBox1.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff" + ":同步教职工数据！"));
        //            showMsg("5 同步教职工数据！");
        //            Thread.Sleep(1000);
        //            Http.DownRecord_Staff(false);//教师

        //            //6同步完成
        //            showMsg("---完成数据同步！---");
        //            //this.BeginInvoke(new dispSycnRecord(addListRecord), this.listBox1, "---完成数据同步！---");
        //            //listBox1.Items.Add(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff" + ":同步完成！"));
        //            if (stopServer == true) return;
        //            //4
        //            //Http.StartApi_Thresd();//只用来上考勤记录
        //            theDateTimes = DateTime.Now;
        //            GetConfig.config.LastScyTime = theDateTimes.ToString("yyyyMMddHHmmss");
        //            SettingsOperate.SaveSettings(GetConfig.config);
        //            //this.progressBar1.Visible = false;
        //        }
        //        else
        //        {
        //            Thread.Sleep(10000);
        //        }
        //        Application.DoEvents();
        //        return;
        //    }
        //    catch (Exception err)
        //    {
        //        showMsg("doStart_DownDljData error:" + err.Message);
        //        //MessageBox.Show("SendMessage_ToDlj error:" + err.Message);
        //        ReadData.writeErrorLog("doStart_DownDljData error:" + err.Message);
        //        throw;
        //    }
        //}

        private void startXyFaceHttpService()//接收人脸机上传数据
        {
            try
            {
                //定义url及端口号，通常设置为配置文件
                string strUrl = @"http://127.0.0.1:8091/";
                showMsg("鑫源人脸设备参数： " + strUrl + "");
                //GetConfig.config.httpSendMessage 
                //提供一个简单的、可通过编程方式控制的 HTTP 协议侦听器。此类不能被继承。
                httpobj = new HttpListener();
                httpobj.Prefixes.Add(strUrl);
                //启动监听器
                httpobj.Start();
                //异步监听客户端请求，当客户端的网络请求到来时会自动执行Result委托
                //该委托没有返回值，有一个IAsyncResult接口的参数，可通过该参数获取context对象
                httpobj.BeginGetContext(Result, null);
                //Console.WriteLine("服务端初始化完毕，正在等待客户端请求,时间：{DateTime.Now.ToString()}\r\n");
                //this.BeginInvoke(new dispList(dispReciveMessage), this.listBox1,"服务端 " + strUrl + " 初始化完毕");
                showMsg("服务端 " + strUrl + " 初始化完毕");
                //Console.ReadKey();
                btnStart.Enabled = false;//防止多次启动
                //btnStop.Enabled = true;
            }
            catch (Exception err)
            {
                MessageBox.Show("数据采集服务启动出错！error:" + err.Message);
                return;
            }
        }

        private void Result(IAsyncResult ar)
        {
            try
            {
                //当接收到请求后程序流会走到这里
                //继续异步监听
                httpobj.BeginGetContext(Result, null);
                var guid = Guid.NewGuid().ToString();
                Console.ForegroundColor = ConsoleColor.White;
                showMsg("接到新的请求:" + guid);
                //this.BeginInvoke(new dispList(dispReciveMessage), this.listBox1, "接到新的请求:"+ guid + ";时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                //获得context对象
                var context = httpobj.EndGetContext(ar);
                var request = context.Request;
                var response = context.Response;
                ////如果是js的ajax请求，还可以设置跨域的ip地址与参数
                //context.Response.AppendHeader("Access-Control-Allow-Origin", "*");//后台跨域请求，通常设置为配置文件
                //context.Response.AppendHeader("Access-Control-Allow-Headers", "ID,PW");//后台跨域参数设置，通常设置为配置文件
                //context.Response.AppendHeader("Access-Control-Allow-Method", "post");//后台跨域请求设置，通常设置为配置文件
                context.Response.ContentType = "text/plain;charset=UTF-8";//告诉客户端返回的ContentType类型为纯文本格式，编码为UTF-8
                context.Response.AddHeader("Content-type", "text/plain");//添加响应头信息
                context.Response.ContentEncoding = Encoding.UTF8;
                string returnObj = null;//定义返回客户端的信息
                if (request.HttpMethod == "POST" && request.InputStream != null)
                {
                    //处理客户端发送的请求并返回处理信息
                    returnObj = HandleRequest(request, response);
                }
                else
                {
                    
                    returnObj = "不是post请求或者传过来的数据为空";
                    showMsg(returnObj);
                }
                var returnByteArr = Encoding.UTF8.GetBytes(returnObj);//设置客户端返回信息的编码
                try
                {
                    using (var stream = response.OutputStream)
                    {
                        //把处理信息返回到客户端
                        stream.Write(returnByteArr, 0, returnByteArr.Length);
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("网络蹦了：{ex.ToString()}");
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("请求处理完成：{guid},时间：{ DateTime.Now.ToString()}\r\n");
                //}
                //catch (Exception err)
                //{
                //    MessageBox.Show("数据采集服务出错！error:" + err.Message);
                //    return;
                //}
            }
            catch (Exception err)
            {
                showMsg("Result error:" + err.Message);
                //MessageBox.Show("SendMessage_ToDlj error:" + err.Message);
                //ReadData.writeErrorLog("Result error:" + err.Message);
                throw;
            }
        }

        private string HandleRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            string data = null;
            try
            {
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
                ///this.BeginInvoke(new dispList(dispReciveMessage), this.listBox1,"接收："+ data);
                if (data.Length > 200)
                {
                    showMsg("接收数据：" + data.Substring(0, 200));
                }
                else
                {
                    showMsg("接收数据：" + data);
                }
                //// string strBackMsg="";
                //// xyFaceManage.GetConfigReturn("192.168.1.144", "8080", "", data, ref strBackMsg);
                ////// string strPassWord = "36C38A04C1914E57AD85F4AC74CDFF77" + "GetConfig" + "3GKL2DFQQP";
                ////// string strCheck = xyFaceManage.MD5Encrypt(strPassWord);
                //// //"http://192.168.1.145:8077/faceapp/";
                //// //this.BeginInvoke(new dispList(dispReciveMessage), this.listBox1, "回应：" + strBackMsg);
                //// showMsg("回应数据：" + strBackMsg);


                //var user = "{"name":"Manas","gender":"Male","birthday":"1987-8-8"} "  
                //var userlist = [{"user":{"name":"Manas","gender":"Male","birthday":"1987-8-8"}},{"user":{"name":"Mohapatra","Male":"Female","birthday":"1987-7-7"}}];
                //获取得到数据data可以进行其他操作
            }
            catch (Exception ex)
            {
                response.StatusDescription = "404";
                response.StatusCode = 404;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("在接收数据时发生错误:{ex.ToString()}");
                return "在接收数据时发生错误:{ex.ToString()}";//把服务端错误信息直接返回可能会导致信息不安全，此处仅供参考
            }
            response.StatusDescription = "200";//获取或设置返回给客户端的 HTTP 状态代码的文本说明。
            response.StatusCode = 200;// 获取或设置返回给客户端的 HTTP 状态代码。
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("接收数据完成:{data.Trim()},时间：{DateTime.Now.ToString()}");
            //取CMD
            JObject jo = (JObject)JsonConvert.DeserializeObject(data);
            string strCMD = jo["CMD"].ToString();
            if (strCMD == "GetConfig")//心跳
            {
                String sRet = "{\"AutoID\": \"486D546DED58465D9CA79DF47A7EC512\",\"Ver\": \"1.0\", \"CMD\": \"GetConfig\",\"Result\": 0,\"Remark\": \"执行成功\",\"SerialNo\": \"3GKL2EL5HZ\", \"Data\": {},\"Check\": \"848c9b99fece63d64e6f6df94669f692\"}";
                showMsg("回应心跳数据：" + sRet);
                return sRet;
            }
            else
            {
                if (strCMD == "RealData")//
                {
                    string CardNo = "";
                    string CardSn = "";
                    string UserNo = "";
                    string strPhotBase64 = "";
                    string strEntry_dt = "";
                    string strFaceSN = "";
                    //xyFaceManage.GetRealData(data, ref strFaceSN, ref CardNo, ref CardSn, ref UserNo, ref strPhotBase64, ref strEntry_dt);
                    string emp_type = "";
                    string diviDesc = "";
                    string depaDesc = "";
                    string staffName = "";
                    //保存识别记录，数据库，日志，上传动力加待发表，
                    string strLinkId = "";
                    //FaceRecord.queryStaffAndSaveEntry(UserNo, strEntry_dt, strFaceSN, ref emp_type, ref diviDesc, ref depaDesc, ref staffName, ref strLinkId);
                    //将Base64String转为图片并保存
                    //存放位置
                    if (strPhotBase64 != "")
                    {
                        string strAttnPhotoPath = "FaceAttnPhoto";
                        strAttnPhotoPath = Application.StartupPath + "\\" + strAttnPhotoPath + "\\" + Convert.ToDateTime(strEntry_dt).ToString("yyyyMMdd");
                        //命名规则：设备编号_卡号_时间.jpg
                        if (!Directory.Exists(strAttnPhotoPath))
                        {
                            Directory.CreateDirectory(strAttnPhotoPath);
                        }
                        string Imagefilename = strAttnPhotoPath + "\\" + strLinkId + "_" + UserNo + "_" + Convert.ToDateTime(strEntry_dt).ToString("yyyyMMddHHmmss") + ".jpg";
                        if (File.Exists(@Imagefilename))
                        {
                            File.Delete(@Imagefilename);
                        }
                        byte[] arr2 = Convert.FromBase64String(strPhotBase64);
                        using (MemoryStream ms2 = new MemoryStream(arr2))
                        {
                            System.Drawing.Bitmap bmp2 = new System.Drawing.Bitmap(ms2);
                            bmp2.Save(Imagefilename, System.Drawing.Imaging.ImageFormat.Jpeg);
                            //bmp2.Save(filePath + ".bmp", System.Drawing.Imaging.ImageFormat.Bmp);
                            //bmp2.Save(filePath + ".gif", System.Drawing.Imaging.ImageFormat.Gif);
                            //bmp2.Save(filePath + ".png", System.Drawing.Imaging.ImageFormat.Png);
                            bmp2.Dispose();
                        }
                        picFacePic.ImageLocation = Imagefilename;
                    }
                    //==============================================
                    //显示图片，从电脑上
                    //FaceRecord.showPhotoFromPC(emp_type, diviDesc, depaDesc, staffName, this.picStaffPhoto);//从本地找相片
                    //this.BeginInvoke(new deleShowUserInfo(showUserInfo), labDivi, labDepa, labStaff, labTime, this.picStaffPhoto, labStaff1, labTime1, picStaffPhoto1, diviDesc, depaDesc, staffName, strEntry_dt);
                    //回应
                    String sRet = "{\"AutoID\":\"C59F6C53965D4875AAD7F5D895810F9B\",\"Ver\":\"1.0\",\"CMD\":\"RealData\",\"Result\": 0,\"Remark\": \"执行成功\",\"SerialNo\":\"3GKL2DFQQP\",\"Data\":{},\"Check\":\"3fafe2415e60cc326cb4422ac1060ec1\"}";
                    //{\"AutoID\": \"486D546DED58465D9CA79DF47A7EC512\",\"Ver\": \"1.0\", \"CMD\": \"GetConfig\",\"Result\": 0,\"Remark\": \"执行成功\",\"SerialNo\": \"3GKL2EL5HZ\", \"Data\": {},\"Check\": \"848c9b99fece63d64e6f6df94669f692\"}";
                    showMsg("回应心跳数据：" + sRet);
                    return sRet;
                }
                else
                {
                    showMsg("回应数据");
                    return "";
                }
            }
        }
        //void dispReciveMessage(ListBox list, string strMsg)
        //{
        //    if (list.Items.Count > 1000)
        //    {
        //        list.Items.Clear();
        //    }
        //    list.Items.Add(strMsg);
        //}
        //显示人员信息
        //delegate void dispList(ListBox list, string strMsg);
        delegate void deleShowUserInfo(Label labDivi, Label labDepa, Label labStaff, Label labTime, PictureBox picPhoto, Label labName1, Label labTime1, PictureBox picPhoto1, string diviDesc, string depaDesc, string staffName, string strEntry_dt);
        //void showUserInfo(Label labDivi, Label labDepa, Label labStaff, Label labTime, PictureBox picPhoto, Label labName1, Label labTime1, PictureBox picPhoto1, string diviDesc, string depaDesc, string staffName, string strEntry_dt)
        //{
        //    try
        //    {
        //        //labTime1.Text = labTime.Text;

        //        //picStaffPhoto1.Image = picPhoto.Image;



        //        labTime6.Text = labTime5.Text;
        //        labTime5.Text = labTime4.Text;
        //        labTime4.Text = labTime3.Text;
        //        labTime3.Text = labTime2.Text;
        //        labTime2.Text = labTime1.Text;
        //        labTime1.Text = labTime.Text;

        //        labStaff6.Text = labStaff5.Text;
        //        labStaff5.Text = labStaff4.Text;
        //        labStaff4.Text = labStaff3.Text;
        //        labStaff3.Text = labStaff2.Text;
        //        labStaff2.Text = labStaff1.Text;
        //        labStaff1.Text = labStaff.Text;

        //        picStaffPhoto6.Image = picStaffPhoto5.Image;
        //        picStaffPhoto5.Image = picStaffPhoto4.Image;
        //        picStaffPhoto4.Image = picStaffPhoto3.Image;
        //        picStaffPhoto3.Image = picStaffPhoto2.Image;
        //        picStaffPhoto2.Image = picStaffPhoto1.Image;
        //        picStaffPhoto1.Image = picPhoto.Image;

        //        labDivi.Text = diviDesc;
        //        labDepa.Text = depaDesc;
        //        labStaff.Text = staffName;
        //        labTime.Text = Convert.ToDateTime(strEntry_dt).ToString("HH:mm:ss");

        //        labTime1.Text = labTime.Text;
        //        labStaff1.Text = labStaff.Text;
        //    }
        //    catch (Exception err)
        //    {
        //        MessageBox.Show(err.Message);
        //        // return false;
        //    }
        //}

        private static CassiniDev.CassiniDevServer _HttpServer;//网页服务器

        private void StartListeningPipes2()
        {
            try
            {
                NamedPipeServerStream pipeServer = new NamedPipeServerStream(PipeName, PipeDirection.InOut, 4, PipeTransmissionMode.Message, PipeOptions.Asynchronous);
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
                        string clientName = server.GetHashCode().ToString();// server.GetImpersonationUserName();
                        //showMsg(clientName + "连接");
                        while (server.IsConnected)
                        {
                            result = sr.ReadLine();
                            if (result == null)
                                continue;
                            if (result == "bye")
                                break;
                            showMsg(result);
                            ShowInfo(result);
                            //this.Invoke((MethodInvoker)delegate {
                            //    receiveMsg.Select(receiveMsg.Text.Length, 0);
                            //    receiveMsg.ScrollToCaret();
                            //});
                        }
                        //showMsg(clientName + "断开连接，等待新的连接");
                        //this.Invoke((MethodInvoker)delegate { lsbMsg.Items.Add(clientName + "断开连接，等待新的连接"); });
                        server.Disconnect();//服务器断开，很重要！
                        server.BeginWaitForConnection(aa, server);//再次等待连接，更重要！！
                        //if (runService)
                        //{
                        //    Thread.Sleep(1000);
                        //    //如果web服务异常停止，则重新启动

                        //    StartService();
                        //}
                    }, pipeServer);

                });
            }
            catch (Exception err)
            {
                showMsg("StartListeningPipes2 error:" + err.Message);
                //MessageBox.Show("SendMessage_ToDlj error:" + err.Message);
                //ReadData.writeErrorLog("StartListeningPipes2 error:" + err.Message);
                throw;
            }
        }

        public void startxFace_HttpService()//北京汉华世讯
        {
            try
            {
                showMsg("启动网页服务器");
                //ThreadPool.QueueUserWorkItem(delegate
                //{
                //    AsyncCallback aa = null;
                //    pipeServer.BeginWaitForConnection(aa = (o) =>
                //    {
                //        NamedPipeServerStream server = (NamedPipeServerStream)o.AsyncState;
                //        server.EndWaitForConnection(o);
                //        StreamReader sr = new StreamReader(server);
                //        StreamWriter sw = new StreamWriter(server);
                //        string result = null;
                //        string clientName = server.GetImpersonationUserName();
                //        showMsg(clientName + "连接");
                //        while (server.IsConnected)
                //        {
                //            result = sr.ReadLine();
                //            if (result == null || result == "bye")
                //                break;
                //            showMsg(result);
                //            ShowInfo(result);
                //        }
                //        showMsg(clientName + "断开连接，等待新的连接");
                //        try
                //        {
                //            server.Disconnect();//服务器断开，很重要！
                //            showMsg("服务器断开");
                //        }
                //        catch (Exception ex)
                //        {
                //            showMsg("服务器断开 err:" + ex.ToString());
                //        }
                //        server.BeginWaitForConnection(aa, server);//再次等待连接，更重要！！
                //        Thread.Sleep(1000);
                //        //如果web服务异常停止，则重新启动
                //        startxFace_HttpService();
                //    }, pipeServer);
                //});
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
            catch (Exception err)
            {
                showMsg("startxFace_HttpService error:" + err.Message);
                MessageBox.Show("startxFace_HttpService error:" + err.Message);
                //ReadData.writeErrorLog("startxFace_HttpService error:" + err.Message);
                throw;
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
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    FacePicPath = Application.StartupPath + String.Format(@"\{0}\", tb_Path.Text);
        //    label1.Text = FacePicPath;
        //}
        //private string Url = "";
        //private void button3_Click(object sender, EventArgs e)
        //{
        //    Url = tb_Url.Text;
        //}
        private string Pass = "123";
        //private void button4_Click(object sender, EventArgs e)
        //{
        //    Pass = tb_Pass.Text;
        //    try
        //    {
        //        button4.Enabled = false;
        //        string postStr = string.Format("oldPass={0}&newPass={1}", Pass, Pass);
        //        //string urlOper = @"/person/createOrUpdate";
        //        string urlOper = @"/setPassWord";
        //        string url = string.Format(@"{0}{1}", Url, urlOper);
        //        ///person/createOrUpdate
        //        showMsg("url:" + url);
        //        showMsg("postStr:" + postStr);

        //        string ReturnStr = "";
        //        bool b = CHttpPost.Post(url, postStr, ref ReturnStr);
        //        if (b)
        //        {
        //            showMsg(ReturnStr);
        //            ResultInfo res = JsonConvert.DeserializeObject<ResultInfo>(ReturnStr);
        //            if (res.success)
        //            {
        //                showMsg("setPassWord 成功");
        //            }
        //            else
        //            {
        //                showMsg("有返回，但出错了：" + res.msg);
        //            }
        //        }
        //        else
        //        {
        //            showMsg("通讯失败");
        //        }

        //    }
        //    finally
        //    {
        //        button4.Enabled = true;

        //    }
        //}
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
        //private void button5_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        button5.Enabled = false;
        //        stopwatch.Start();
        //        DirectoryInfo di = new DirectoryInfo(FacePicPath);
        //        FileInfo[] fis = di.GetFiles("*.jpg");
        //        userList = new List<User>();
        //        userDic = new Dictionary<string, User>();
        //        //Parallel.ForEach(fis, b =>
        //        foreach (FileInfo b in fis)
        //        {
        //            stopwatchDetail.Reset();
        //            stopwatchDetail.Start();
        //            string aFile = b.Name;
        //            User u = GetUserByFileName(b);
        //            userList.Add(u);
        //            if (!userDic.ContainsKey(u.imageKey))
        //            {
        //                userDic.Add(u.imageKey, u);
        //            }
        //            if (cb_saveImageKey.Checked)
        //            {
        //                System.IO.File.WriteAllText(FacePicDataPath + u.FileName + "_Key.dat", u.imageKey);
        //                System.IO.File.WriteAllText(FacePicDataPath + u.FileName + "_Base64.dat", u.imageBase64);
        //            }
        //            stopwatchDetail.Stop();
        //            showMsg(string.Format("处理照片列表,FileName[{0}],用时[{1}],特征值[{2}]", b.Name, stopwatchDetail.ElapsedMilliseconds, u.imageKey));
        //        }
        //        //);
        //        stopwatch.Stop();
        //        showMsg(string.Format("处理总人数[{0}],用时[{1}]", fis.Length, stopwatch.ElapsedMilliseconds));
        //    }
        //    catch (Exception ex)
        //    {
        //        showMsg(ex.ToString());
        //    }
        //    finally
        //    {
        //        button5.Enabled = true;
        //    }
        //}


        System.Diagnostics.Stopwatch stopwatchDetail = new System.Diagnostics.Stopwatch();
        System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        public delegate void dShowInfo(string str);
        public void showMsg(string msg)
        {
            try
            {
                {
                    if (stopServer == true)
                    {
                        this.Close();
                        return;
                    }
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
                            s = string.Format("[{0}],{1}\r\n", DateTime.Now.ToString("HH:mm:ss.fff"), msg);
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
                //}
                //catch (Exception err)
                //{
                //    //writeLog("getlivenessScore err:" + err.Message);//写日志记录
                //   //throw;
                //}
            }
            catch (Exception err)
            {
                //showMsg("StartListeningPipes2 error:" + err.Message);
                //MessageBox.Show("SendMessage_ToDlj error:" + err.Message);
                //ReadData.writeErrorLog("showMsg error:" + err.Message);
                //throw;
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


        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                _HttpServer.StopServer();
                SaveData();
                stopServer = true;
                Thread.Sleep(1000);
            }
            catch (Exception err)
            {
                //writeLog("getlivenessScore err:" + err.Message);//写日志记录
                // throw;
            }
        }



        private void FrmServer_Resize(object sender, EventArgs e)
        {
            ctrl_Resize();//调整北影图的大小和位置
        }
        private void ctrl_Resize()
        {
            //this.panMain.Left = 0;
            //this.panMain.Top = 25;
            //this.panMain.Width = this.Width - 20;
            //this.panMain.Height = this.Height - 20;
            //this.panBackground.Left = (this.panMain.Width - this.panBackground.Width) / 2;
            //this.panBackground.Top = (this.panMain.Height - this.panBackground.Height) / 2;
            //this.tabShowLog.Left = (this.panMain.Width - this.tabShowLog.Width) / 2;
            //this.tabShowLog.Top = 25;

            //this.panTitle.Left = this.panBackground.Left;
        }



        private void toolStartService_Click(object sender, EventArgs e)
        {
            startxFace_HttpService();
        }



        //private void mnuExit_Click(object sender, EventArgs e)
        //{
        //    if (GetConfig.config.FaceAiDevType.Substring(0, 1) == "1")//鑫源产品/使用人脸识别功能)//xyFace模式采集刷卡数据
        //    {
        //        if (httpobj != null)
        //        {
        //            if (httpobj.IsListening)
        //            {
        //                stopServer = true;
        //                httpobj.Stop();
        //                showMsg("http服务端,停止！");
        //                btnStart.Enabled = true;
        //                btnStop.Enabled = false;
        //            }
        //        }
        //    }
        //    Close();
        //}

        private void mnuShowLog_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void mnuStar_Click(object sender, EventArgs e)
        {
            startxFace_HttpService();
        }

        //private void btnTest_Click(object sender, EventArgs e)
        //{
        //    if (GetConfig.config.FaceAiDevType == null || GetConfig.config.httpServerIP == null)
        //    {
        //        MessageBox.Show("未找到人脸设备类型配置参数！");
        //        return;
        //    }

        //    if (GetConfig.config.FaceAiDevType.Substring(0, 1) == "1")//鑫源产品
        //    {
        //        showMsg("开始测试'鑫源产品'设备!");
        //        TestXyFace();
        //    }
        //    else
        //    {
        //        showMsg("开始测试'北京汉华世讯产品'设备!");
        //        TestXFace();
        //    }
        //}




        //private void mnuShowLog_Click(object sender, EventArgs e)
        //{
        //    tabShowLog.Visible = true;
        //    tabShowLog.BringToFront();
        //}

        //private void btnClose_Click(object sender, EventArgs e)
        //{
        //    mnuShowLog.Checked = false;
        //    tabShowLog.Visible = false;
        //}

        private bool runService = false;
        private void btnStart_Click(object sender, System.EventArgs e)
        {
            try
            {
                //if (GetConfig.config.FaceAiDevType == null || GetConfig.config.httpServerIP == null)
                //{
                //    showMsg("未找到人脸设备类型配置参数！");
                //    MessageBox.Show("未找到人脸设备类型配置参数！");
                //    return;
                //}
                Thread.Sleep(200);
                beginServer();//同步动力加数据
                Thread.Sleep(200);
                startXyFaceHttpService();
                //if (GetConfig.config.FaceAiDevType.Substring(0, 1) == "1")//鑫源产品
                //{
                //    showMsg("人脸设备类型配置:" + GetConfig.config.FaceAiDevType);
                //    if (GetConfig.config.httpServerIP.ToString() == "")
                //    {
                //        MessageBox.Show("未找上传服务器IP配置参数！");
                //        return;
                //    }
                //    showMsg("上传服务器IP:" + GetConfig.config.httpServerIP);
                //    startXyFaceHttpService();//接收鑫源人脸机上传数据 httpServer
                //    stopServer = false;
                //}
                //else
                {
                    //if (GetConfig.config.FaceAiDevType.Substring(0, 1) == "2")//
                    {
                        //showMsg("人脸设备类型配置:" + GetConfig.config.FaceAiDevType);
                        //runService = true;
                        // StartListeningPipes2(); //2019-02-19
                        //StartService();
                        //startxFace_HttpService();//接收人脸机上传数据,

                    }
                    //else
                    //{
                    //    MessageBox.Show("未找到人脸设备类型配置参数！");
                    //    return;
                    //}
                }

            }
            catch (Exception err)
            {
                showMsg("btnStart_Click error:" + err.Message);
                MessageBox.Show("btnStart_Click error:" + err.Message);
                //ReadData.writeErrorLog("btnStart_Click error:" + err.Message);
                throw;
            }
        }
        public void StartService()
        {
            try
            {
                _HttpServer = new CassiniDevServer();
                string path = Application.StartupPath + "\\Home";
                int port = 8091;

                _HttpServer.StartServer(path, IPAddress.Any, port, "/", "");
                showMsg("启动完成");
            }
            catch (Exception err)
            {
                showMsg("StartService error:" + err.Message);
                MessageBox.Show("StartService error:" + err.Message);
                //ReadData.writeErrorLog("StartService error:" + err.Message);
                throw;
            }
        }

        public void StopService()
        {
            _HttpServer.StopServer();
        }

        private void btnStop_Click(object sender, System.EventArgs e)
        {
            //if (GetConfig.config.FaceAiDevType.Substring(0, 1) == "1")//鑫源产品/使用人脸识别功能)//xyFace模式采集刷卡数据
            //{
            //    if (httpobj.IsListening)
            //    {
            //        httpobj.Stop();
            //        showMsg("http服务端,停止！");
            //        btnStart.Enabled = true;
            //        btnStop.Enabled = false;
            //        runService = false;
            //        StopService();
            //    }
            //}
        }

        private void chkHttp_Send_CheckedChanged(object sender, System.EventArgs e)
        {

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            string strUrl = @"http://*:8091/";
            //string strUrl = @"http://+:8080/";
            startxFace_Handler_HttpService(strUrl);//接收鑫源人脸机上传数据 httpServer
            stopServer = false;
            return;
        }
        private void startxFace_Handler_HttpService(string strUrl)//接收人脸机上传数据心跳
        {
            try
            {
                //提供一个简单的、可通过编程方式控制的 HTTP 协议侦听器。此类不能被继承。
                httpobj = new HttpListener();
                //定义url及端口号，通常设置为配置文件
                //string strUrl = @"http://127.0.0.1:8091/"; ;//传入
                showMsg("接收人脸机上传数据： " + strUrl + "");
                httpobj.Prefixes.Add(strUrl);
                //启动监听器
                httpobj.Start();
                //异步监听客户端请求，当客户端的网络请求到来时会自动执行Result委托
                //该委托没有返回值，有一个IAsyncResult接口的参数，可通过该参数获取context对象
                httpobj.BeginGetContext(Result_Handler, null);
                //Console.WriteLine("服务端初始化完毕，正在等待客户端请求,时间：{DateTime.Now.ToString()}\r\n");
                //this.BeginInvoke(new dispList(dispReciveMessage), this.listBox1,"服务端 " + strUrl + " 初始化完毕");
                showMsg("服务端 " + strUrl + " 初始化完毕");
                //ReadData.writeHttpListenerLog("==================服务端 " + strUrl + " 初始化完毕=================");
            }

            catch (Exception err)
            {
                showMsg("startxFace_Handler_HttpService error:" + err.Message);
                MessageBox.Show("startxFace_Handler_HttpService error:" + err.Message);
                //ReadData.writeErrorLog("startxFace_Handler_HttpService error:" + err.Message);
                //ReadData.writeHttpListenerLog("==================服务端初始化失败" + err.Message);
            }
        }

//        [30:33],解析,downNewApkUrl[http://192.168.8.100:8091/Update.ashx]
//[30:33],解析,getNewApkVersionUrl[http://192.168.8.100:8091/GetUpdate.ashx]
//[30:33],解析,heartBeatUrl[http://192.168.8.100:8091/HeartBeat.ashx]
//[30:33],解析,identifyCallBack[http://192.168.8.100:8091/Handler.ashx]
//[30:33],解析,identifyCallBack_His[http://192.168.8.100:8091/Handler_His.ashx]
//[30:33],解析,verifyCallBack[http://192.168.8.100:8091/VerifyHandler.ashx]


        //Handler
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
            var request = context.Request;
            var response = context.Response;
            ////如果是js的ajax请求，还可以设置跨域的ip地址与参数
            //context.Response.AppendHeader("Access-Control-Allow-Origin", "*");//后台跨域请求，通常设置为配置文件
            //context.Response.AppendHeader("Access-Control-Allow-Headers", "ID,PW");//后台跨域参数设置，通常设置为配置文件
            //context.Response.AppendHeader("Access-Control-Allow-Method", "post");//后台跨域请求设置，通常设置为配置文件
            context.Response.ContentType = "text/plain;charset=UTF-8";//告诉客户端返回的ContentType类型为纯文本格式，编码为UTF-8
            context.Response.AddHeader("Content-type", "text/plain");//添加响应头信息
            context.Response.ContentEncoding = Encoding.UTF8;
            string returnObj = null;//定义返回客户端的信息
            if (request.RawUrl.IndexOf("Handler.ashx") >= 0) //实时识别记录
            {
                showMsg("");
                showMsg("接到请求,guid:" + guid + "," + request.RemoteEndPoint.Address + "," + "Handler.ashx");
                //ReadData.writeHttpListenerLog("接到请求,guid:" + guid + "," + request.RemoteEndPoint.Address + "," + "Handler.ashx");
            }
            else
            {
                showMsg("接到请求,guid:" + guid + "," + request.RemoteEndPoint.Address + "," + request.RawUrl);
                //ReadData.writeHttpListenerLog("接到请求,guid:" + guid + "," + request.RemoteEndPoint.Address + "," + request.RawUrl);
            }
            //showMsg("");
            if (request.HttpMethod == "POST" && request.InputStream != null)

            {
                //处理客户端发送的请求并返回处理信息
                returnObj = HandleRequest_Handler(request, response);
            }
            else
            {
                returnObj = "不是post请求或者传过来的数据为空";
                showMsg(returnObj);
            }
            var returnByteArr = Encoding.UTF8.GetBytes(returnObj);//设置客户端返回信息的编码
            try
            {
                using (var stream = response.OutputStream)
                {
                    //把处理信息返回到客户端
                    stream.Write(returnByteArr, 0, returnByteArr.Length);
                    //ReadData.writeHttpListenerLog("回应请求:" + returnObj);
                }
            }
            catch (Exception ex)
            {
                //Console.ForegroundColor = ConsoleColor.Red;
                //Console.WriteLine("网络蹦了：{ex.ToString()}");
                //ReadData.writeHttpListenerLog("回应请求,error:" + ex.Message);
            }
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("请求处理完成：{guid},时间：{ DateTime.Now.ToString()}\r\n");
        }

        //Handler
        public string HandleRequest_Handler(HttpListenerRequest request, HttpListenerResponse response)
        {
            string data = null;
            //try
            //{
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
            //FaceRecord.writeDebugLog("HandleRequest_Handler：" + data);
            string info = System.Web.HttpUtility.UrlDecode(data, System.Text.Encoding.UTF8);
            //FaceRecord.writeDebugLog("HandleRequest_Handler：" + info);
            String Ret = "{\"result\":\"1\",\"success\":\"true\",\"msg\":\"1\",\"Result\": 0,\"msgtype\": \"\"}";
            if (info.IndexOf("verify") >= 0) //识别记录
            {
                //ReadData.writeHttpListenerLog("接到识别记录,verify:" + info.Substring(0, 100));
                do_EntryRecord(info);//处理考勤数据
            }
            else
            {
                Ret = "{\"result\":\"0\",\"success\":\"true\",\"msg\":\"1\",\"Result\": 0,\"msgtype\": \"\"}";
            }
            if (info.IndexOf("info") >= 0)//心跳
            {
                //ReadData.writeHttpListenerLog("接到心跳数据,info:" + info.Substring(0, 100));
                do_HeartBeat(info);
            }
            else
            {
                //ReadData.writeHttpListenerLog("收到异常数据，不用处理,info:" + info);
            }
            //回应
            //String Ret = "{\"result\":\"1\",\"success\":\"true\",\"msg\":\"1\",\"Result\": 0,\"msgtype\": \"\"}";
            return Ret;
        }

        public string do_EntryRecord(string info)
        {
            //"verify={\"banch\":\"2019_03_01_14_22_19_1_167\",\"base64\":\"/9j/4AAQSkZJRgA
            //UUUUAFFFFABRRRQAUUUUAJS0UlIBaKKKBn/2Q==\",\"deviceKey\":\"u02v2-225su-k925s-uk543-23yz5\",\"direction\":1,\"guid\":
            //\"248431c9-a9b5-4c07-b734-af586089d018\",\"id\":534,\"ip\":\"192.168.1.123\",\"machineCode\":\"u02v2-225su-k925s-uk543-23yz5\",
            //\"path\":\"ftp://null:8010/FacePic/2019-02-28/verify/success/13/test2_2019.02.28.13.57.54_0.8880075.jpg\",
            //\"sendPassType\":1,\"time\":\"2019-02-28 13:57:54\",\"type\":1,\"userId\":\"1004440443\",\"userName\":\"test2\"}"
            string dataStr = "";
            string strFaceSN = "";
            string strRet = "";
            //if (info.IndexOf("verify") >= 0) //识别记录
            //{
            dataStr = info.Substring(info.IndexOf("verify=") + 7, info.Length - info.IndexOf("verify=") - 7);
            Verify v = JsonConvert.DeserializeObject<Verify>(dataStr);
            if (v != null)
            {
                string emp_type = "";
                string diviDesc = "";
                string depaDesc = "";
                string staffName = "";
                string strLinkId = "";
                //lb_PersonId.Text = "用户ID:" + v.userId;
                //lb_PersonName.Text = "用户姓名:" + v.userName;
                //lb_Path.Text = "照片路径:" + v.path;
                strFaceSN = v.deviceKey;
                string strEntry_dt = v.time;

                if (v.type == "1")//只有识别正确的记录才处理
                {
                    //保存识别记录，数据库，日志，上传动力加待发表，
                    string strPersonId = v.userId;
                    string strIP = v.IP;
                    showMsg("verify data = serId:" + strPersonId + ",time:" + strEntry_dt + ",ip:" + strIP + ",userName:" + staffName + ",FaceSN:" + strFaceSN);
                    //ReadData.writeHttpListenerLog("识别数据,userId:" + strPersonId + ",time:" + strEntry_dt + ",ip:" + strIP + ",userName:" + staffName + ",FaceSN:" + strFaceSN);

                    if (!string.IsNullOrEmpty(v.path))
                    {
                        picFacePic.LoadAsync(v.path);
                    }
                    //FaceRecord.queryStaffAndSaveEntry(strPersonId, strEntry_dt, strFaceSN, ref emp_type, ref diviDesc, ref depaDesc, ref staffName, ref strLinkId);
                    //FaceRecord.writeFaceAttnLog(strLinkId + "," + strFaceSN + "," + strPersonId + "," + strEntry_dt + "," + staffName);
                    //showMsg(v.base64 != null ? v.base64.Length.ToString() : "");
                    //显示图片，从电脑上
                    //显示在界面的列表上
                    this.BeginInvoke(new dispEntryRecord(dispEntry), strPersonId, staffName, diviDesc, depaDesc, strEntry_dt);

                    //labDivi.Text = diviDesc;
                    //labDepa.Text = depaDesc;
                    //labStaff.Text = staffName;
                    //labTime.Text = Convert.ToDateTime(strEntry_dt).ToString("HH:mm:ss");                   
                    //FaceRecord.showPhotoFromPC(emp_type, diviDesc, depaDesc, staffName, this.picStaffPhoto);//从本地找相片

                    //显示图片，从电脑上
                    //FaceRecord.showPhotoFromPC(emp_type, diviDesc, depaDesc, staffName, this.picStaffPhoto);//从本地找相片
                    //this.BeginInvoke(new deleShowUserInfo(showUserInfo), labDivi, labDepa, labStaff, labTime, this.picStaffPhoto, labStaff1, labTime1, picStaffPhoto1, diviDesc, depaDesc, staffName, strEntry_dt);

                    //回应
                    strRet = "{\"result\":\"1\",\"success\":\"true\",\"msg\":\"1\",\"Result\": 0,\"msgtype\": \"\"}";
                    return strRet;
                    //showHisStaffPhoto();//历史记录列表
                    //labTime1.Text = strEntry_dt;
                    //labStaff1.Text = staffName + "(" + depaDesc + ")";
                    //picStaffPhoto1.Image = picStaffPhoto.Image;
                    //showMsg(v.base64 != null ? v.base64.Length.ToString() : "");
                    //FaceRecord.savePhotoToLoca(strLinkId, strPersonId, strEntry_dt, picFacePic);
                    //回应;
                }
            }
            return strRet;
        }
        public void dispEntry(string strPersonId, string staffName, string strDiviDesc, string strDepaDesc, string strEntry_dt)
        {

            //labDivi.Text = strDiviDesc;
            //labDepa.Text = strDepaDesc;
            labStaff.Text = staffName;
            //labTime.Text = Convert.ToDateTime(strEntry_dt).ToString("HH:mm:ss");
            labPersonId.Text = strPersonId;

        }
        //处理心跳数据，得到机器码、设备id,更新状态等
        public void do_HeartBeat(string info)
        {
            //info={"availMem":"1395.46","buildModel":"WISLINK-V28","deviceKey":"u02v2-225su-k925s-uk543-23yz5","deviceMachineCode":"u02v2-225su-k925s-uk543-23yz5",
            //"deviceType":1,"disk":2.7,"diskInfo":"93.10%[可用2.7G,共2.9G]","faceCount":4,"ip":"192.168.1.123","isAuth":1,
            //"isInited":1,"memory":"204.07","personCount":4,"runtime":"2:11","sendCount":264,"starttime":"2019-03-04 09:36:59",
            //"time":"2019-03-04 11:48:28","totalDisk":2.9,"totalMem":2013.5273,"version":"V1.0.6.317"}
            string dataStr = "";
            string strFaceSN = "";
            string strFaceCount = "";
            string strIP = "";
            string strTime = "";
            dataStr = info.Substring(info.IndexOf("info=") + 5, info.Length - info.IndexOf("info=") - 5);
            JsonReader readerHead = new JsonTextReader(new StringReader(dataStr));
            string deviceMachineCode = "";
            strFaceSN = "";
            while (readerHead.Read())
            {
                if (readerHead.Value != null)
                {
                    if (readerHead.Value.ToString() == "deviceMachineCode")
                    {
                        readerHead.Read();
                        if (readerHead.Value != null) deviceMachineCode = readerHead.Value.ToString();//返回机器编号，主健
                    }
                    if (readerHead.Value.ToString() == "deviceKey")
                    {
                        readerHead.Read();
                        if (readerHead.Value != null) strFaceSN = readerHead.Value.ToString();
                    }
                    if (readerHead.Value.ToString() == "faceCount")
                    {
                        readerHead.Read();
                        if (readerHead.Value != null) strFaceCount = readerHead.Value.ToString();//返回机器编号，主健
                    }
                    if (readerHead.Value.ToString() == "ip")
                    {
                        readerHead.Read();
                        if (readerHead.Value != null) strIP = readerHead.Value.ToString();
                    }
                    if (readerHead.Value.ToString() == "time")
                    {
                        readerHead.Read();
                        if (readerHead.Value != null) strTime = readerHead.Value.ToString();
                    }
                }
            }
            showMsg("HeartBeat data = deviceMachineCode:" + deviceMachineCode + ",FaceSN:" + strFaceSN + ",FaceCount:" + strFaceCount + ",IP:" + strIP + ",Time:" + strTime);
            //ReadData.writeHttpListenerLog("心跳数据,deviceMachineCode:" + deviceMachineCode + ",FaceSN:" + strFaceSN + ",FaceCount:" + strFaceCount + ",IP:" + strIP + ",Time:" + strTime);
        }

        private void menuStrip2_ItemClicked(object sender, System.Windows.Forms.ToolStripItemClickedEventArgs e)
        {

        }

        private void FrmServer_Load(object sender, EventArgs e)
        {
            try
            {
                showMsg("读取配置文件!");
                //this.chkHttp_Send.Checked = GetConfig.config.httpSendMessage;//发送方式为HTTP方式
                //this.chkBasFromServer.Checked = GetConfig.config.Api_BasFromServer; //基础数据从云平台同步
                //this.txtApi_Url.Text = GetConfig.config.Api_Url;
                //this.txtSch_ID.Text = GetConfig.config.Sch_ID;
                //this.txtApi_Acc.Text = GetConfig.config.Api_Acc;
                //this.txtApi_Psw.Text = GetConfig.config.Api_Psw;
                //this.txtSchoolName.Text = GetConfig.config.Api_SchoolName;//学校名称；
                //this.txtScyTime.Text = GetConfig.config.ScyTime;
                //connDataBase();
                LoadData();
                //设置照片路径
                //button2_Click(null, null);
                //设置设备URL
                //button3_Click(null, null);
                //labStaff.Text = "";
                //labTime.Text = "";
                //labDepa.Text = "";
                //labDivi.Text = "";
                //labPersonId.Text = "";
                ctrl_Resize();//调整北影图的大小和位置
                //StartListeningPipes2(); //2019-02-19
            }
            catch (Exception err)
            {
                MessageBox.Show("Form1_Load error:" + err.Message);
                //ReadData.writeErrorLog("Form1_Load error:" + err.Message);
                throw;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            receiveMsg.Clear();
        }
    }






}
