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
        string GetHtml(string Url);
    }
}
