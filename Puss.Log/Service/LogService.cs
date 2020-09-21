using Puss.RabbitMq;
using System;
using Newtonsoft.Json;
using Puss.Enties;
using log4net;
using Microsoft.Extensions.Logging;
using log4net.Repository;
using log4net.Config;
using System.IO;

namespace Puss.Log
{
    public class LogService : ILogService
    {
        #region 日志收集RabbitMQ
        /// <summary>
        /// 日志收集（报错）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="Url">路径</param>
        /// <param name="Token">Token</param>
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
        /// 日志收集（定时计划报错）
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
        /// 日志收集（计时）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sIP">IP</param>
        /// <param name="sMessage">消息</param>
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

        /// <summary>
        /// 日志收集（返回结果记录）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sHeaders">头</param>
        /// <param name="sActionArguments">参数</param>
        /// <param name="sResult">结果</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        public void LogCollectPush(string QueueKey, string sUrl, string sHeaders,string sActionArguments,string sResult, IRabbitMQPushService RabbitMQPushService)
        {
            RabbitMQPushService.PushMessage(QueueKey, JsonConvert.SerializeObject(new
            {
                CreateTime = DateTime.Now,
                Url = sUrl,
                Headers = sHeaders,
                ActionArguments = sActionArguments,
                Result = sResult,
            }));
        }
        #endregion

        #region 日志收集Log4Net
        private static ILoggerRepository _loggerRepository;

        public ILoggerRepository GetLoggerRepository() 
        {
            if (_loggerRepository != null)
            {
                return _loggerRepository;
            }
            _loggerRepository = LogManager.CreateRepository(nameof(LogService));
            XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo("config/log4net.config"));
            return _loggerRepository;
        }

        /// <summary>
        /// 日志收集（报错）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="Url">路径</param>
        /// <param name="Token">Token</param>
        /// <param name="Logger">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception Ex, string Url, string Token, ILoggerRepository Logger)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            var LogErrorDetails = new LogErrorDetails()
            {
                CreateTime = DateTime.Now,
                Url = Url,
                Messsage = JsonConvert.SerializeObject(Ex, settings),
                Token = Token,
            };
            LogManager.GetLogger(Logger.Name, Url).Debug(JsonConvert.SerializeObject(LogErrorDetails));
        }

        /// <summary>
        /// 日志收集（定时计划报错）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="sDetails">详细信息</param>
        /// <param name="Logger">MQ类接口</param>
        public void LogCollectPush(string QueueKey, Exception Ex, string sDetails, ILoggerRepository Logger)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
            LogManager.GetLogger(Logger.Name, QueueKey).Error(JsonConvert.SerializeObject(new LogJobDetails()
            {
                CreateTime = DateTime.Now,
                Name = sDetails,
                Messsage = JsonConvert.SerializeObject(Ex, settings),
            }));
        }

        /// <summary>
        /// 日志收集（计时）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sIP">IP</param>
        /// <param name="sMessage">消息</param>
        /// <param name="Logger">MQ类接口</param>
        public void LogCollectPush(string QueueKey, string sUrl, string sIP, string sMessage, ILoggerRepository Logger)
        {
            LogManager.GetLogger(Logger.Name, sUrl).Info(JsonConvert.SerializeObject(new
            {
                CreateTime = DateTime.Now,
                Url = sUrl,
                IP = sIP,
                Message = sMessage
            }));
        }

        /// <summary>
        /// 日志收集（返回结果记录）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sHeaders">头</param>
        /// <param name="sActionArguments">参数</param>
        /// <param name="sResult">结果</param>
        /// <param name="Logger">MQ类接口</param>
        public void LogCollectPush(string QueueKey, string sUrl, string sHeaders, string sActionArguments, string sResult, ILoggerRepository Logger)
        {
            LogManager.GetLogger(Logger.Name, sUrl).Info(JsonConvert.SerializeObject(new
            {
                CreateTime = DateTime.Now,
                Url = sUrl,
                Headers = sHeaders,
                ActionArguments = sActionArguments,
                Result = sResult,
            }));
        }

        /// <summary>
        /// 日志收集（Apple切面）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sActionArguments">参数</param>
        /// <param name="Logger">MQ类接口</param>
        public void LogCollectPushApple(string QueueKey, string sActionArguments,ILoggerRepository Logger)
        {
            LogManager.GetLogger(Logger.Name, QueueKey).Info(JsonConvert.SerializeObject(new
            {
                CreateTime = DateTime.Now,
                ActionArguments = sActionArguments,
            }));
        }
        #endregion
    }
}
