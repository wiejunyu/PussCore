using Puss.Log;
using Puss.RabbitMQ;
using Puss.Redis;
using Puss.Reptile;
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
        /// <param name="ReptileService">Reptile类接口</param>
        public PriceJobTrigger(ILogService LogService, IRabbitMQPushService RabbitMQPushService, IRedisService RedisService, IReptileService ReptileService) :
            base(TimeSpan.Zero,
                TimeSpan.FromMinutes(1),
                new PriceJobExcutor(ReptileService,RedisService),
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
        private readonly IReptileService ReptileService;


        /// <summary>
        /// 获取价格定时计划
        /// </summary>
        /// <param name="ReptileService">Reptile类接口</param>
        /// <param name="RedisService">Redis类接口</param>
        public PriceJobExcutor(IReptileService ReptileService,IRedisService RedisService)
        {
            this.RedisService = RedisService;
            this.ReptileService = ReptileService;
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        public void StartJob()
        {
            PriceManager.GetPrice(ReptileService,RedisService);
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
