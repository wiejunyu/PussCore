using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Reptile
{
    public interface IReptileHelper
    {
        /// <summary>
        /// Post请求并获取HTML
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        string GetHtml(string Url);
    }
}
