<%@ WebHandler Language="C#" Class="GetUpdate" %>

using System;
using System.Web;

using Newtonsoft.Json;
using System.IO;
using System.IO.Pipes;
using System.Security.Principal;
using System.Collections.Specialized;

using System.Runtime.InteropServices;
    using System.Text;


public class GetUpdate : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";

        //string Verify = context.Request["verify"];

        context.Response.Write(GetVersionByUpdateApk());

    }


    public string GetVersionByUpdateApk()
    {
        return ReadIniData("NewApk","VersionCode",null,"ApkInfo.ini");

    }
        public string ReadIniData(string Section, string Key, string NoText, string fileName)//读取INI文件
        {
            string str = System.Environment.CurrentDirectory;//获取当前文件目录
            //ini文件路径
            //HttpContext.Current.Server.MapPath("~/App_Data/" + "users.xml")
            //string str1 = "" + str + "\\"+fileName;
            string str1 = HttpContext.Current.Server.MapPath("~/" + fileName);
            if (File.Exists(str1))
            {
                StringBuilder temp = new StringBuilder(1024);
                GetPrivateProfileString(Section, Key, NoText, temp, 1024, str1);
                return temp.ToString();
            }
            else
            {
                return String.Empty;
            }
        }
    #region API函数声明

    [DllImport("kernel32")]//返回0表示失败，非0为成功
    private static extern long WritePrivateProfileString(string section, string key,string val, string filePath);

    [DllImport("kernel32")]//返回取得字符串缓冲区的长度
    private static extern long GetPrivateProfileString(string section, string key,string def, StringBuilder retVal, int size, string filePath);
    #endregion
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}

