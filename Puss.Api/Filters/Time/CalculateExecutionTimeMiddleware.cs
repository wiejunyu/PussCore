using Microsoft.AspNetCore.Http;
using Puss.Data.Models;
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
        private readonly IRabbitMQPush RabbitMQPush;
        Stopwatch stopwatch;

        /// <summary>
        ///  CalculateExecutionTimeMiddleware
        /// </summary>
        /// <param name="next"></param>
        /// <param name="RabbitMQPush">MQ接口</param>
        public CalculateExecutionTimeMiddleware(RequestDelegate next, IRabbitMQPush RabbitMQPush)
        {
            this._next = next;
            this.RabbitMQPush = RabbitMQPush;
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
            //推入MQ
            RabbitMQPush.PushMessage(RabbitMQKey.LogTime, JsonConvert.SerializeObject(tlog));
        }
    }
}
