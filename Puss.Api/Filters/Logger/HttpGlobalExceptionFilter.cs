using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Log;
using Puss.RabbitMQ;

namespace Puss.Api.Filters
{
    /// <summary>
    /// 验证
    /// </summary>
    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogService LogService;
        private readonly IRabbitMQPushService RabbitMQPushService;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="LogService">日志接口</param>
        /// <param name="RabbitMQPushService">MQ接口</param>
        public HttpGlobalExceptionFilter(ILogService LogService, IRabbitMQPushService RabbitMQPushService)
        {
            this.LogService = LogService;
            this.RabbitMQPushService = RabbitMQPushService;
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
                LogService.LogCollectPush(QueueKey.LogError, context.Exception, RabbitMQPushService);
            }
        }
    }
}
