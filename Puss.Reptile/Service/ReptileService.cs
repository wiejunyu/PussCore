﻿using HtmlAgilityPack;
using System.IO;
using System.Net;
using System.Text;

namespace Puss.Reptile
{
    public class ReptileService : IReptileService
    {
        /// <summary>
        /// Post请求并获取HTML
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public string GetHtml(string Url)
        {
            //WebProxy proxyObject = new WebProxy(IP, PORT);//这里我是用的代理。
            //向指定地址发送请求
            HttpWebRequest HttpWReq = (HttpWebRequest)WebRequest.Create(Url);
            //HttpWReq.Proxy = proxyObject;
            HttpWReq.Timeout = 10000;
            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            StreamReader sr = new StreamReader(HttpWResp.GetResponseStream(), Encoding.UTF8);
            HtmlDocument doc = new HtmlDocument();
            doc.Load(sr);
            string html = doc.DocumentNode.InnerHtml;
            sr.Close();
            HttpWResp.Close();
            HttpWReq.Abort();
            return html;
        }
    }
}
