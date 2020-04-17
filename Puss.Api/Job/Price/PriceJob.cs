using Microsoft.Extensions.Logging;
using Puss.Log;
using Puss.RabbitMQ;
using Puss.Redis;
using System;

namespace Puss.Api.Job
{
    /// <summary>
    /// 获取价格定时计划
    /// </summary>
    public class PriceJobTrigger : BaseJobTrigger
    {
        /// <summary>
        /// 获取价格定时计划
        /// </summary>
        /// <param name="LogService">日志类接口</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        /// <param name="RedisService">Redis类接口</param>
        public PriceJobTrigger(ILogService LogService, IRabbitMQPushService RabbitMQPushService, IRedisService RedisService) :
            base(TimeSpan.Zero,
                TimeSpan.FromMinutes(1),
                new PriceJobExcutor(RedisService),
                RabbitMQPushService,
                LogService)
        {
        }
    }

    /// <summary>
    /// 获取价格定时计划
    /// </summary>
    public class PriceJobExcutor : IJobExecutor                
    {
        private readonly IRedisService RedisService;

        /// <summary>
        /// 获取价格定时计划
        /// </summary>
        /// <param name="RedisService">Redis类接口</param>
        public PriceJobExcutor(IRedisService RedisService)
        {
            this.RedisService = RedisService;
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        public void StartJob()
        {
            PriceManager.GetPrice(RedisService);
        }

        /// <summary>
        /// 终止任务
        /// </summary>
        public void StopJob()
        {
            //系统终止任务
        }
    }
}
