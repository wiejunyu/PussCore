using log4net.Repository;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Puss.Data.Models;
using Puss.Log;
using Puss.RabbitMQ;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Puss.Api.Job
{
    /// <summary>
    /// BaseJobTrigger
    /// </summary>
    public abstract class BaseJobTrigger
       : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly TimeSpan _dueTime;
        private readonly TimeSpan _periodTime;
        private readonly IJobExecutor _jobExcutor;
        //private readonly ILogService LogService;
        private readonly ILogger<BaseJobTrigger> Logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dueTime">到期执行时间</param>
        /// <param name="periodTime">间隔时间</param>
        /// <param name="jobExcutor">任务执行者</param>
        /// <param name="Logger">日志类接口</param>
        protected BaseJobTrigger(TimeSpan dueTime,
             TimeSpan periodTime,
             IJobExecutor jobExcutor,
             ILogger<BaseJobTrigger> Logger)
        {
            _dueTime = dueTime;
            _periodTime = periodTime;
            _jobExcutor = jobExcutor;
            this.Logger = Logger;
        }

        #region  计时器相关方法

        private void StartTimerTrigger()
        {
            if (_timer == null)
                _timer = new Timer(ExcuteJob, _jobExcutor, _dueTime, _periodTime);
            else
                _timer.Change(_dueTime, _periodTime);
        }

        private void StopTimerTrigger()
        {
            _timer?.Change(Timeout.Infinite, Timeout.Infinite);
        }

        private void ExcuteJob(object obj)
        {
            try
            {
                var excutor = obj as IJobExecutor;
                excutor?.StartJob();
            }
            catch (Exception e)
            {
                Error($"执行任务({nameof(GetType)})",e);
            }
        }
        #endregion

        /// <summary>
        ///  系统级任务执行启动
        /// </summary>
        /// <returns></returns>
        public virtual Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                StartTimerTrigger();
            }
            catch (Exception e)
            {
                Error($"启动定时任务({nameof(GetType)})", e);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        ///  系统级任务执行关闭
        /// </summary>
        /// <returns></returns>
        public virtual Task StopAsync(CancellationToken cancellationToken)
        {
            try
            {
                _jobExcutor.StopJob();
                StopTimerTrigger();
            }
            catch (Exception e)
            {
                Error($"停止定时任务({nameof(GetType)})", e);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }

        private void Error(string Name, Exception ex)
        {
            #region 日志记录
            //日志收集
            //LogService.LogCollectPush(QueueKey.LogJob, ex, Name, LogService.GetLoggerRepository());
            Logger.LogInformation($"[CreateTime]:{DateTime.Now}[Name]:{Name}[Exception]:{JsonConvert.SerializeObject(ex)}");
            #endregion
        }
    }
}
