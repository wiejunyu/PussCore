using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Log;
using Puss.RabbitMQ;
using Newtonsoft.Json;
using Hangfire;
using Puss.Api.Job;

namespace Puss.Api.Filters
{
    /// <summary>
    /// 验证
    /// </summary>
    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogService LogService;
        private readonly IRabbitMQPushService RabbitMQPushService;
        private readonly IHttpContextAccessor Accessor;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="LogService">日志接口</param>
        /// <param name="RabbitMQPushService">MQ接口</param>
        /// <param name="Accessor"></param>
        public HttpGlobalExceptionFilter(ILogService LogService, IRabbitMQPushService RabbitMQPushService, IHttpContextAccessor Accessor)
        {
            this.LogService = LogService;
            this.RabbitMQPushService = RabbitMQPushService;
            this.Accessor = Accessor;
        }

        /// <summary>
        /// 报错进入
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
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

                
                //日志收集
                LogService.LogCollectPush(QueueKey.LogError, context.Exception, Accessor.HttpContext.Connection.RemoteIpAddress.ToString(), Accessor.HttpContext.Request.Headers["Authorization"].ToString(), RabbitMQPushService);
            }
        }
    }
}
