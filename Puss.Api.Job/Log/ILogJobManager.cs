using Puss.BusinessCore;
using Puss.Enties;
using Puss.RabbitMQ;
using Puss.Data.Models;
using Newtonsoft.Json;
using Hangfire;

namespace Puss.Api.Job
{
    /// <summary>
    /// 定时计划日志业务
    /// </summary>
    public interface ILogJobManager
    {
        /// <summary>
        /// 定时计划日志业务
        /// </summary>
        void Log();
    }
}
