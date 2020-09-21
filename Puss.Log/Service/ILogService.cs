using log4net.Repository;
using Puss.RabbitMq;
using System;

namespace Puss.Log
{
    public interface ILogService
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
        void LogCollectPush(string QueueKey, Exception Ex, string Url, string Token, IRabbitMQPushService RabbitMQPushService);

        /// <summary>
        /// 日志收集（定时计划报错）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="sDetails">详细信息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        void LogCollectPush(string QueueKey, Exception Ex, string sDetails, IRabbitMQPushService RabbitMQPushService);

        /// <summary>
        /// 日志收集（计时）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sIP">IP</param>
        /// <param name="sMessage">消息</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        void LogCollectPush(string QueueKey, string sUrl, string sIP, string sMessage, IRabbitMQPushService RabbitMQPushService);

        /// <summary>
        /// 日志收集（返回结果记录）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sHeaders">头</param>
        /// <param name="sActionArguments">参数</param>
        /// <param name="sResult">结果</param>
        /// <param name="RabbitMQPushService">MQ类接口</param>
        void LogCollectPush(string QueueKey, string sUrl, string sHeaders, string sActionArguments, string sResult, IRabbitMQPushService RabbitMQPushService);
        #endregion

        #region 日志收集Log4Net
        ILoggerRepository GetLoggerRepository();

        /// <summary>
        /// 日志收集（报错）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="Url">路径</param>
        /// <param name="Token">Token</param>
        /// <param name="Logger">Logger</param>
        void LogCollectPush(string QueueKey, Exception Ex, string Url, string Token, ILoggerRepository Logger);

        /// <summary>
        /// 日志收集（定时计划报错）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="Ex">错误信息</param>
        /// <param name="sDetails">详细信息</param>
        /// <param name="Logger">Logger</param>
        void LogCollectPush(string QueueKey, Exception Ex, string sDetails, ILoggerRepository Logger);

        /// <summary>
        /// 日志收集（计时）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sIP">IP</param>
        /// <param name="sMessage">消息</param>
        /// <param name="Logger">Logger</param>
        void LogCollectPush(string QueueKey, string sUrl, string sIP, string sMessage, ILoggerRepository Logger);

        /// <summary>
        /// 日志收集（返回结果记录）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sUrl">Url</param>
        /// <param name="sHeaders">头</param>
        /// <param name="sActionArguments">参数</param>
        /// <param name="sResult">结果</param>
        /// <param name="Logger">Logger</param>
        void LogCollectPush(string QueueKey, string sUrl, string sHeaders, string sActionArguments, string sResult, ILoggerRepository Logger);

        /// <summary>
        /// 日志收集（Apple切面）
        /// </summary>
        /// <param name="QueueKey">队列名称</param>
        /// <param name="sActionArguments">参数</param>
        /// <param name="Logger">MQ类接口</param>
        void LogCollectPushApple(string QueueKey, string sActionArguments, ILoggerRepository Logger);
        #endregion
    }
}
