using Puss.BusinessCore;
using Puss.Enties;
using Puss.RabbitMq;
using Puss.Data.Models;
using Newtonsoft.Json;
using Hangfire;

namespace Puss.Api.Job
{
    /// <summary>
    /// 定时计划日志业务
    /// </summary>
    public class LogJobManager : ILogJobManager
    {
        /// <summary>
        /// 定时计划日志业务
        /// </summary>
        public void Log()
        {
            //BackgroundJob.Enqueue(() => ConsumptionLogError());
            //BackgroundJob.Enqueue(() => ConsumptionLogTime());
            //BackgroundJob.Enqueue(() => ConsumptionLogJob());
        }

        ///// <summary>
        ///// 消费控制器错误日志
        ///// </summary>
        //public static void ConsumptionLogError()
        //{
        //    RabbitMQPushService RabbitMQPushService = new RabbitMQPushService();
        //    while (true)
        //    {
        //        try
        //        {
        //            RabbitMQPushService.PullMessage(QueueKey.LogError, (Message) =>
        //            {
        //                LogErrorDetailsManager LogErrorDetailsManager = new LogErrorDetailsManager();
        //                LogErrorDetailsManager.Insert(JsonConvert.DeserializeObject<LogErrorDetails>(Message));
        //                return true;
        //            });
        //        }
        //        catch 
        //        {
        //            RabbitMQPushService = new RabbitMQPushService();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 消费时间日志
        ///// </summary>
        //public static void ConsumptionLogTime()
        //{
        //    RabbitMQPushService RabbitMQPushService = new RabbitMQPushService();
        //    while (true)
        //    {
        //        try
        //        {
        //            RabbitMQPushService.PullMessage(QueueKey.LogTime, (Message) =>
        //            {
        //                return true;
        //            });
        //        }
        //        catch
        //        {
        //            RabbitMQPushService = new RabbitMQPushService();
        //        }
        //    }
        //}

        ///// <summary>
        ///// 消费定时计划错误日志
        ///// </summary>
        //public static void ConsumptionLogJob()
        //{
        //    RabbitMQPushService RabbitMQPushService = new RabbitMQPushService();
        //    while (true)
        //    {
        //        try
        //        {
        //            RabbitMQPushService.PullMessage(QueueKey.LogJob, (Message) =>
        //            {
        //                LogJobDetailsManager LogJobDetailsManager = new LogJobDetailsManager();
        //                LogJobDetailsManager.Insert(JsonConvert.DeserializeObject<LogJobDetails>(Message));
        //                return true;
        //            });
        //        }
        //        catch
        //        {
        //            RabbitMQPushService = new RabbitMQPushService();
        //        }
        //    }
        //}
    }
}
