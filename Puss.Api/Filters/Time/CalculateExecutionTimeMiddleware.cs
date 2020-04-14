using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Puss.Data.Models;
using Puss.RabbitMq;
using Puss.RabbitMQ;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Puss.Api.Filters
{
    /// <summary>
    /// CalculateExecutionTimeMiddleware
    /// </summary>
    public class CalculateExecutionTimeMiddleware
    {
        private readonly RequestDelegate _next;//下一个中间件
        private readonly ILogger _logger;
        Stopwatch stopwatch;
        /// <summary>
        /// CalculateExecutionTimeMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public CalculateExecutionTimeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            this._next = next;
            _logger = loggerFactory.CreateLogger<CalculateExecutionTimeMiddleware>();
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
            TimeLog tlog = new TimeLog();
            tlog.ApiPath = context.Request.Path;
            tlog.Time = stopwatch.ElapsedMilliseconds;
            //推入MQ
            RabbitMQPushHelper.PushMessage(RabbitMQKey.LogTime, JsonConvert.SerializeObject(tlog));
        }
    }
}
