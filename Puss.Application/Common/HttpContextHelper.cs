using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Application.Common
{
    public class HttpContextHelper
    {
        public static string GetBody(IHttpContextAccessor Accessor) 
        {
            string body = "";
            try
            {
                using (var reader = new StreamReader(Accessor.HttpContext.Request.Body))
                {
                    body = reader.ReadToEnd();
                }
            }
            catch 
            {
            }
            return body;
        }

        public static string GetHeaders(IHttpContextAccessor Accessor)
        {
            string headers = "";
            try
            {
                headers = JsonConvert.SerializeObject(Accessor.HttpContext.Request.Headers);
            }
            catch
            {
            }
            return headers;
        }
    }
}
