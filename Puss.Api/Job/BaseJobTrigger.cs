using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
        private readonly IRabbitMQPush RabbitMQPush;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dueTime">到期执行时间</param>
        /// <param name="periodTime">间隔时间</param>
        /// <param name="jobExcutor">任务执行者</param>
        /// <param name="RabbitMQPush">MQ接口</param>
        protected BaseJobTrigger(TimeSpan dueTime,
             TimeSpan periodTime,
             IJobExecutor jobExcutor,
             IRabbitMQPush RabbitMQPush)
        {
            _dueTime = dueTime;
            _periodTime = periodTime;
            _jobExcutor = jobExcutor;
            this.RabbitMQPush = RabbitMQPush;
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

        private void Error(string Name, Exception e)
        {
            #region 日志记录
            string dt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string content = $"类型：{Name}\r\n";
            content += $"时间：{dt}\r\n";
            content += $"来源：{e.TargetSite.ReflectedType}.{e.TargetSite.Name}\r\n";
            content += $"内容：{e.Message}\r\n";
            RabbitMQPush.PushMessage(RabbitMQKey.LogJob, content);
            #endregion
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
    }
}
