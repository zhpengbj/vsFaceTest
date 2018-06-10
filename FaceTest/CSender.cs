using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Net;
using System.Net.Cache;

using System.Threading;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;


namespace FaceTest
{
    #region post 工具
    public class CHttpPost
    {
        ///// <summary>
        ///// 提交的结果
        ///// </summary>
        //public class CPostResult
        //{
        //    /// <summary>
        //    /// 返回代码
        //    /// </summary>
        //    public string OpCode { get; set; }
        //    /// <summary>
        //    /// 注释
        //    /// </summary>
        //    public string OpRes { get; set; }
        //}
        //public static CJSon_Result GetJsonResult(string JsonSttr)
        //{
        //    return JsonConvert.DeserializeObject<CJSon_Result>(JsonSttr);
        //}
        public static bool Get(string sUrl, ref string ReturnStr)
        {
            bool result = false;
            Encoding myEncoding = Encoding.UTF8;
            HttpWebRequest req;

            try
            {
                // init
                req = HttpWebRequest.Create(sUrl) as HttpWebRequest;
                req.Method = "GET";
                req.Accept = "*/*";
                req.KeepAlive = false;
                req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                // Response
                HttpWebResponse res = req.GetResponse() as HttpWebResponse;
                try
                {
                    //100	请求成功
                    //101	权限异常
                    //102	参数异常
                    //103	系统内部异常

                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream resStream = res.GetResponseStream())
                        {
                            using (StreamReader resStreamReader = new StreamReader(resStream, myEncoding))
                            {
                                ReturnStr = resStreamReader.ReadToEnd();
                                result = true;
                                //CJSon_Result one = GetJsonResult(resStreamReader.ReadToEnd());
                                //result.OpCode = one.status;
                                //result.OpRes = one.statusMessage;

                            }
                        }
                    }
                    else
                    {
                        result = false;
                        //result.OpCode = res.StatusCode.ToString();
                        //result.OpRes = res.StatusDescription ;
                    }
                    return result;
                    //OutLog("Response.ContentLength:\t{0}", res.ContentLength);
                    //OutLog("Response.ContentType:\t{0}", res.ContentType);
                    //OutLog("Response.CharacterSet:\t{0}", res.CharacterSet);
                    //OutLog("Response.ContentEncoding:\t{0}", res.ContentEncoding);
                    //OutLog("Response.IsFromCache:\t{0}", res.IsFromCache);
                    //OutLog("Response.IsMutuallyAuthenticated:\t{0}", res.IsMutuallyAuthenticated);
                    //OutLog("Response.LastModified:\t{0}", res.LastModified);
                    //OutLog("Response.Method:\t{0}", res.Method);
                    //OutLog("Response.ProtocolVersion:\t{0}", res.ProtocolVersion);
                    //OutLog("Response.ResponseUri:\t{0}", res.ResponseUri);
                    //OutLog("Response.Server:\t{0}", res.Server);
                    //OutLog("Response.StatusCode:\t{0}\t# {1}", res.StatusCode, (int)res.StatusCode);
                    //OutLog("Response.StatusDescription:\t{0}", res.StatusDescription);

                    //// header
                    //OutLog(".\t#Header:");	// 头.
                    //for (int i = 0; i < res.Headers.Count; ++i)
                    //{
                    //    OutLog("[{2}] {0}:\t{1}", res.Headers.Keys[i], res.Headers[i], i);
                    //}

                    //// 找到合适的编码
                    //Encoding encoding = null;
                    ////encoding = Encoding_FromBodyName(res.CharacterSet);	// 后来发现主体部分的字符集与Response.CharacterSet不同.
                    ////if (null == encoding) encoding = myEncoding;
                    //encoding = _ResEncoding;
                    //System.Diagnostics.Debug.WriteLine(encoding);

                    //// body
                    //OutLog(".\t#Body:");	// 主体.
                    //using (Stream resStream = res.GetResponseStream())
                    //{
                    //    using (StreamReader resStreamReader = new StreamReader(resStream, encoding))
                    //    {
                    //        OutLog(resStreamReader.ReadToEnd());
                    //    }
                    //}
                    //OutLog(".\t#OK.");	// 成功.
                }
                finally
                {
                    res.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
                //result.OpCode = "-1";
                //result.OpRes = ex.ToString().Substring(0, 500);

                return result;
            }
        }

        public static bool Post(string sUrl, string sPostData, ref string ReturnStr)
        {
            bool result = false;
            Encoding myEncoding = Encoding.UTF8;
            string sContentType = "application/x-www-form-urlencoded";
            HttpWebRequest req;

            try
            {
                // init
                req = HttpWebRequest.Create(sUrl) as HttpWebRequest;
                req.Method = "POST";
                req.Accept = "*/*";
                req.KeepAlive = false;
                req.CachePolicy = new HttpRequestCachePolicy(HttpRequestCacheLevel.NoCacheNoStore);
                byte[] bufPost = myEncoding.GetBytes(sPostData);
                req.ContentType = sContentType;
                req.ContentLength = bufPost.Length;
                Stream newStream = req.GetRequestStream();
                newStream.Write(bufPost, 0, bufPost.Length);
                newStream.Close();

                // Response
                HttpWebResponse res = req.GetResponse() as HttpWebResponse;
                try
                {
                    //100	请求成功
                    //101	权限异常
                    //102	参数异常
                    //103	系统内部异常

                    if (res.StatusCode == HttpStatusCode.OK)
                    {
                        using (Stream resStream = res.GetResponseStream())
                        {
                            using (StreamReader resStreamReader = new StreamReader(resStream, myEncoding))
                            {
                                ReturnStr = resStreamReader.ReadToEnd();
                                result = true;
                                //CJSon_Result one = GetJsonResult(resStreamReader.ReadToEnd());
                                //result.OpCode = one.status;
                                //result.OpRes = one.statusMessage;

                            }
                        }
                    }
                    else
                    {
                        result = false;
                        //result.OpCode = res.StatusCode.ToString();
                        //result.OpRes = res.StatusDescription ;
                    }
                    return result;
                }
                finally
                {
                    res.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
                //result.OpCode = "-1";
                //result.OpRes = ex.ToString().Substring(0, 500);

                return result;
            }
        }
    }
    #endregion


}
