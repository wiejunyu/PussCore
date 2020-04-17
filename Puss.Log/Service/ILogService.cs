using Puss.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Puss.Log
{
    public interface ILogService
    {
        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="ex">错误信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception ex, IRabbitMQPushService RabbitMQPushService);

        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="ex">错误信息</param>
        /// <param name="sDetails">详细信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception ex, string sDetails, IRabbitMQPushService RabbitMQPushService);

        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sMessage">错误消息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, string sMessage, IRabbitMQPushService RabbitMQPushService);
    }
}
