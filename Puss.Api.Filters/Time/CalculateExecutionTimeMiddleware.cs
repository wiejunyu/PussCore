using Microsoft.AspNetCore.Http;
using Puss.Data.Models;
using Puss.RabbitMQ;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Puss.Log;
using Microsoft.Extensions.Logging;

namespace Puss.Api.Filters
{
    /// <summary>
    /// CalculateExecutionTimeMiddleware
    /// </summary>
    public class CalculateExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;//下一个中间件
        private readonly ILogService LogService;
        private readonly ILogger<WebApiResultMiddleware> Logger;
        Stopwatch stopwatch;

        /// <summary>
        ///  CalculateExecutionTimeMiddleware
        /// </summary>
        /// <param name="next">next</param>
        /// <param name="LogService">日志类接口</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public CalculateExecutionTimeMiddleware(RequestDelegate next, ILogService LogService, ILogger<WebApiResultMiddleware> Logger)
        {
            this._next = next;
            this.LogService = LogService;
            this.Logger = Logger;
        }

        /// <summary>
        /// 计算接口耗时并推入MQ收集
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            stopwatch = new Stopwatch();
            stopwatch.Start();//在下一个中间价处理前，启动计时器
            await _next.Invoke(context);
            stopwatch.Stop();//所有的中间件处理完后，停止秒表。
            //记录耗时
            TimeLog tlog = new TimeLog() 
            {
                ApiPath = context.Request.Path,
                Time = stopwatch.ElapsedMilliseconds
            };
            if (!"[/][/swagger/index.html]".Contains($"[{tlog.ApiPath}]") && !tlog.ApiPath.Contains("hangfire")) 
            {
                //日志收集
                LogService.LogCollectPush(QueueKey.LogTime,context.Request.Path.ToString(), context.Connection.RemoteIpAddress.ToString(), JsonConvert.SerializeObject(tlog), LogService.GetLoggerRepository());
            }
        }
    }
}
