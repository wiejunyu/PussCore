using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Puss.Log;
using Newtonsoft.Json;
using Puss.Data.Models;
using Puss.RabbitMQ;
using Microsoft.Extensions.Logging;

namespace Puss.Api.Filters
{
    /// <summary>
    /// 验证
    /// </summary>
    public class WebApiResultMiddleware : ActionFilterAttribute
    {
        private readonly ILogService LogService;
        private readonly IHttpContextAccessor Accessor;
        private readonly ILogger<WebApiResultMiddleware> Logger;

        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="LogService">日志接口</param>
        /// <param name="Accessor"></param>
        public WebApiResultMiddleware(ILogService LogService, IHttpContextAccessor Accessor, ILogger<WebApiResultMiddleware> Logger)
        {
            this.LogService = LogService;
            this.Accessor = Accessor;
            this.Logger = Logger;
        }

        private string ActionArguments { get; set; }
        private object Headers { get; set; }

        /// <summary>
        /// 操作执行
        /// </summary>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ActionArguments = JsonConvert.SerializeObject(context.ActionArguments);
            Headers = context.HttpContext.Request.Headers;
            base.OnActionExecuting(context);
        }

        /// <summary>
        /// 请求参数
        /// </summary>
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            
            var resultStr = "";
            try
            {
                resultStr = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<dynamic>(JsonConvert.SerializeObject(context.Result)).Value);
            }
            catch
            {
            }
            LogService.LogCollectPush(QueueKey.LogResult, context.HttpContext.Request.Path,JsonConvert.SerializeObject(Headers),ActionArguments ,resultStr , LogService.GetLoggerRepository());
        }
    }
}
