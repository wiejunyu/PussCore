using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.RabbitMQ;
using System;

namespace Puss.Api.Filters
{
    /// <summary>
    /// 验证
    /// </summary>
    public class HttpGlobalExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IRabbitMQPush RabbitMQPush;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="RabbitMQPush">MQ接口</param>
        public HttpGlobalExceptionFilter(IRabbitMQPush RabbitMQPush)
        {
            this.RabbitMQPush = RabbitMQPush;
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

                #region 日志记录
                string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                var ex = context.Exception;
                string content = "类型：错误代码\r\n";
                content += "时间：" + dt + "\r\n";
                content += "来源：" + ex.TargetSite.ReflectedType.ToString() + "." + ex.TargetSite.Name + "\r\n";
                content += "内容：" + ex.Message + "\r\n";
                RabbitMQPush.PushMessage(RabbitMQKey.LogError, content);
                #endregion
            }
        }
    }
}
