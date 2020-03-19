using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Puss.Data;
using Puss.Data.Enum;
using Puss.Data.Models;
using Sugar.Enties;

namespace Puss.Api.Controllers
{
    public class ApiBaseController : ControllerBase
    {
        private string requestBody = "";
        [ApiExplorerSettings(IgnoreApi = true)]
        public ReturnResult<T> InvokeFunc<T>(Func<T> func)
        {
            Stopwatch stop = new Stopwatch();
            stop.Start();
            var response = new ReturnResult<T>();
            try
            {
                //使用期限
                // UsingTimeOut();
                requestBody = GetPostData();
                T result = func();
                response.Status = (int)ReturnResultStatus.Succeed;
                response.Data = result;

            }
            catch (AppException exception)
            {
                response.HandleException(exception);
                response.Status = (int)ReturnResultStatus.BLLError;
            }
            catch (Exception exception)
            {
                //记录数据库日志
                #region 日志记录
                string errorCode = DateTime.Now.ToString("yyMMddHHmmss");
                ExceptionLog exceptionLog = new ExceptionLog();
                JsonSerializerSettings setting = new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                exceptionLog.Response = JsonConvert.SerializeObject(exception, setting);
                exceptionLog.Error_code = errorCode;
                exceptionLog.Exception_Type = (int)ExceptionTypeEnum.System;
                exceptionLog.Url = Url;
                exceptionLog.CreateTime = DateTime.Now;
                exceptionLog.Request = requestBody;
                response.Message = "网络异常或超时，请稍后再试！";
                response.Status = (int)ReturnResultStatus.BLLError;
                new ExceptionLogManager().Insert(exceptionLog);
                #endregion
            }

            stop.Stop();
            long runtime = stop.ElapsedMilliseconds;
            return response;
        }

        /// <summary>
        /// 获取POST传过来的数据
        /// </summary>
        /// <returns></returns>
        protected string GetPostData()
        {
            try
            {
                return getResultData(HttpContext.Request.Body);
            }
            catch (Exception xe)
            {
                return "";
            }

        }
        private string getResultData(Stream task)
        {
            string taskData = string.Empty;
            using (System.IO.Stream sm = task)
            {
                if (sm != null)
                {
                    sm.Seek(0, SeekOrigin.Begin);
                    int len = (int)sm.Length;
                    byte[] inputByts = new byte[len];
                    sm.Read(inputByts, 0, len);
                    sm.Close();
                    taskData = Encoding.UTF8.GetString(inputByts);
                }
            }
            return taskData;
        }

        /// <summary>
        /// 获取请求url
        /// </summary>
        protected string Url
        {
            get
            {
                return HttpRequestExtensions.GetAbsoluteUri(HttpContext.Request);
            }
        }
    }

    public static class HttpRequestExtensions
    {
        public static string GetAbsoluteUri(HttpRequest request)
        {
            try
            {
                return new StringBuilder()
                 .Append(request.Scheme)
                 .Append("://")
                 .Append(request.Host)
                 .Append(request.PathBase)
                 .Append(request.Path)
                 .Append(request.QueryString)
                 .ToString();
            }
            catch 
            {
                return string.Empty;
            }
        }
    }
}
