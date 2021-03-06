﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace Puss.Application.Common
{
    public class HttpPostHelper
    {
        public static string Post(string url, string postData)
        {
            return Post(url, postData, "application/x-www-form-urlencoded");
        }

        public static string Post(string url, string postData, string contentType)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = contentType;
            request.Method = "POST";
            request.Timeout = 300000;

            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = bytes.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            response.Close();
            return result;
        }

        public static string Post(string url, string postData, string userName, string password)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "text/html; charset=UTF-8";
            request.Method = "POST";

            string usernamePassword = userName + ":" + password;
            CredentialCache credentialCache =
                new CredentialCache { { new Uri(url), "Basic", new NetworkCredential(userName, password) } };
            request.Credentials = credentialCache;
            request.Headers.Add("Authorization",
                "Basic " + Convert.ToBase64String(new ASCIIEncoding().GetBytes(usernamePassword)));

            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = bytes.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.ASCII);
            string result = reader.ReadToEnd();
            response.Close();
            return result;
        }

        //static CookieContainer cookie = new CookieContainer();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">请求的servlet地址，不带参数</param>
        /// <param name="postData"></param>
        /// <returns>请求的参数，key=value&key1=value1</returns>
        public static string DoHttpPost(string url, string postData)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            SetHeaderValue(request.Headers, "Content-Type", "application/json");
            SetHeaderValue(request.Headers, "Accept", "application/json");
            SetHeaderValue(request.Headers, "Accept-Charset", "utf-8");
            request.Method = "POST";
            request.Timeout = 300000;

            byte[] bytes = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = bytes.Length;
            Stream writer = request.GetRequestStream();
            writer.Write(bytes, 0, bytes.Length);
            writer.Close();

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader reader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException(), Encoding.UTF8);
            string result = reader.ReadToEnd();
            response.Close();
            return result;
        }

        /// <summary>
        /// 偶发性超时时试看看
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <returns></returns>
        public static string HttpPostForTimeOut(string url, string postData)
        {
            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            //watch.Start();
            GC.Collect();
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/json";
            //request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //int a = Encoding.UTF8.GetByteCount(postData);
            request.Timeout = 20 * 600 * 1000;


            ServicePointManager.Expect100Continue = false;
            ServicePointManager.DefaultConnectionLimit = 200;

            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;

            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("utf-8")); //如果JSON有中文则是UTF-8
            myStreamWriter.Write(postData);
            myStreamWriter.Close(); //请求中止,是因为长度不够,还没写完就关闭了.

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //watch.Stop();  //停止监视
            //TimeSpan timespan = watch.Elapsed;  //获取当前实例测量得出的总时间
            //System.Diagnostics.Debug.WriteLine("打开窗口代码执行时间：{0}(毫秒)", timespan.TotalMinutes);  //总毫秒数

            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream ?? throw new InvalidOperationException(), Encoding.GetEncoding("utf-8"));
            string registerResult = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return registerResult;
        }


        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property =
                typeof(WebHeaderCollection).GetProperty("InnerCollection",
                    BindingFlags.Instance | BindingFlags.NonPublic);
            if (property != null)
            {
                if (property.GetValue(header, null) is NameValueCollection collection) collection[name] = value;
            }
        }

        /// <summary>
        /// Get请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public static string HttpGet(string Url, string contentType)
        {
            try
            {
                string retString = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                request.ContentType = contentType;

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader streamReader = new StreamReader(myResponseStream);
                retString = streamReader.ReadToEnd();
                streamReader.Close();
                myResponseStream.Close();
                return retString;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 参数按照ASCII码从小到大排序（字典序）,不带（=）
        /// </summary>
        /// <param name="dic">字典</param>
        /// <returns></returns>
        public static string GetParamSrc(Dictionary<string, string> dic)
        {
            dic = dic.OrderBy(key => key.Key).ToDictionary(keyItem => keyItem.Key, valueItem => valueItem.Value);
            var sbPara = new StringBuilder(1024);
            foreach (var para in dic.Where(para => !string.IsNullOrWhiteSpace(para.Value)))
            {
                sbPara.AppendFormat("{0}{1}", para.Key, para.Value);
            }
            return sbPara.ToString().TrimEnd('&');
        }
    }
}