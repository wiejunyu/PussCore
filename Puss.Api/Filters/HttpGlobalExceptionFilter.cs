using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Puss.Data.Enum;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Filters
{
    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;
        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            var actionName = context.HttpContext.Request.RouteValues["controller"] + "/" + context.HttpContext.Request.RouteValues["action"];
            //拦截处理
            if (!context.ExceptionHandled)
            {
                context.Result = new JsonResult(new 
                {
                    status = false,
                    message = "网络错误"
                });
                context.ExceptionHandled = true;
            }
            //记录数据库日志
            #region 日志记录
            string errorCode = DateTime.Now.ToString("yyMMddHHmmss");
            ExceptionLog exceptionLog = new ExceptionLog();
            JsonSerializerSettings setting = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            exceptionLog.Response = JsonConvert.SerializeObject(context, setting);
            exceptionLog.Error_code = errorCode;
            exceptionLog.Exception_Type = (int)ExceptionTypeEnum.System;
            exceptionLog.Url = Url;
            exceptionLog.CreateTime = DateTime.Now;
            exceptionLog.Request = "";
            new ExceptionLogManager().Insert(exceptionLog);
            #endregion
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
}
