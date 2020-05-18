using Puss.RabbitMQ;
using System;

namespace Puss.Log
{
    public interface ILogService
    {
        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception Ex, string Url, string Token, IRabbitMQPushService RabbitMQPushService);

        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="sDetails">详细信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception Ex, string sDetails, IRabbitMQPushService RabbitMQPushService);

        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sIP">IP</param>
        /// <param name="sMessage">错误消息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, string sUrl, string sIP, string sMessage, IRabbitMQPushService RabbitMQPushService);
    }
}
