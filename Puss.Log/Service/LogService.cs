using Puss.RabbitMQ;
using System;
using System.Collections.Generic;
using System.Text;

namespace Puss.Log
{
    public class LogService : ILogService
    {
        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="ex">错误信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception ex, IRabbitMQPushService RabbitMQPushService)
        {
            string content = "类型：错误代码\r\n";
            content += $"时间：{DateTime.Now}\r\n";
            content += $"来源：{ex.TargetSite.ReflectedType}." + ex.TargetSite.Name + "\r\n";
            content += $"内容：{ex.Message}\r\n";
            RabbitMQPushService.PushMessage(QueueKey, content);
        }

        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="ex">错误信息</param>
        /// <param name="sDetails">详细信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception ex,string sDetails, IRabbitMQPushService RabbitMQPushService)
        {
            string content = $"类型：{sDetails}\r\n";
            content += $"时间：{DateTime.Now}\r\n";
            content += $"来源：{ex.TargetSite.ReflectedType}." + ex.TargetSite.Name + "\r\n";
            content += $"内容：{ex.Message}\r\n";
            RabbitMQPushService.PushMessage(QueueKey, content);
        }

        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sMessage">错误消息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, string sMessage, IRabbitMQPushService RabbitMQPushService)
        {
            RabbitMQPushService.PushMessage(QueueKey, sMessage);
        }
    }
}
