using Puss.RabbitMQ;
using System;
using Newtonsoft.Json;
using Puss.Enties;

namespace Puss.Log
{
    public class LogService : ILogService
    {
        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception Ex, string Url,string Token, IRabbitMQPushService RabbitMQPushService)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var LogErrorDetails = new LogErrorDetails()
            {
                CreateTime = DateTime.Now,
                Url = Url,
                Messsage = JsonConvert.SerializeObject(Ex, settings),
                Token = Token,
            };
            RabbitMQPushService.PushMessage(QueueKey, JsonConvert.SerializeObject(LogErrorDetails));
        }

        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="sDetails">详细信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception Ex,string sDetails, IRabbitMQPushService RabbitMQPushService)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            RabbitMQPushService.PushMessage(QueueKey, JsonConvert.SerializeObject(new LogJobDetails()
            {
                CreateTime = DateTime.Now,
                Name = sDetails,
                Messsage = JsonConvert.SerializeObject(Ex, settings),
            }));
        }

        /// <summary>
        /// 日志收集
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sIP">IP</param>
        /// <param name="sMessage">错误消息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, string sUrl,string sIP,string sMessage, IRabbitMQPushService RabbitMQPushService)
        {
            RabbitMQPushService.PushMessage(QueueKey, JsonConvert.SerializeObject(new
            {
                CreateTime = DateTime.Now,
                Url = sUrl,
                IP = sIP,
                Message = sMessage
            }));
        }
    }
}
