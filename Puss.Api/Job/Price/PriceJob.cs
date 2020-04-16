using Microsoft.Extensions.Logging;
using Puss.RabbitMQ;
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
        /// <param name="RabbitMQPush">MQ接口</param>
        public PriceJobTrigger(IRabbitMQPush RabbitMQPush) :
            base(TimeSpan.Zero,
                TimeSpan.FromMinutes(1),
                new PriceJobExcutor(),
                RabbitMQPush)
        {
        }
    }

    /// <summary>
    /// 获取价格定时计划
    /// </summary>
    public class PriceJobExcutor
                     : IJobExecutor
    {
        /// <summary>
        /// 开始任务
        /// </summary>
        public void StartJob()
        {
            PriceManager.GetPrice();
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
