using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Puss.Data.Enum;
using Puss.Data.Models;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Filters
{
    /// <summary>
    /// 验证
    /// </summary>
    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<HttpGlobalExceptionFilter> _logger;
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="logger"></param>
        public HttpGlobalExceptionFilter(ILogger<HttpGlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 报错进入
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            var actionName = context.HttpContext.Request.RouteValues["controller"] + "/" + context.HttpContext.Request.RouteValues["action"];
            if (context.Exception is AppException)
            {
                context.Result = new JsonResult(new ReturnResult()
                {
                    Status = (int)ReturnResultStatus.BLLError,
                    Message = context.Exception.Message
                });
                context.ExceptionHandled = true;
            }
            else
            {
                //拦截处理
                if (!context.ExceptionHandled)
                {
                    context.Result = new JsonResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.BLLError,
                        Message = "网络错误"
                    });
                    context.ExceptionHandled = true;
                }
            }
            //记录数据库日志
            #region 日志记录
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _logger.LogWarning($"--------{dt} Error Start--------");
            var ex = context.Exception;
            string content = "类型：错误代码\r\n";
            content += "时间：" + dt + "\r\n";
            content += "来源：" + ex.TargetSite.ReflectedType.ToString() + "." + ex.TargetSite.Name + "\r\n";
            content += "内容：" + ex.Message + "\r\n";
            _logger.LogWarning(content);
            _logger.LogWarning($"--------{dt} Error End--------");
            #endregion
        }
    }
}
