using Microsoft.Extensions.Logging;
using Puss.Api.Manager.MovieManager;
using System;

namespace Puss.Api.Job
{
    /// <summary>
    /// 电影更新城市和影院定时计划
    /// </summary>
    public class UpdateCitysAndCinemasJobTrigger : BaseJobTrigger
    {
        /// <summary>
        /// 电影更新城市和影院定时计划
        /// </summary>
        /// <param name="Logger">日志类接口</param>
        /// <param name="MovieManager">电影类接口</param>
        public UpdateCitysAndCinemasJobTrigger(ILogger<BaseJobTrigger> Logger, IMovieManager MovieManager) :
            base(TimeSpan.Zero,TimeSpan.FromMinutes(60),new UpdateCitysAndCinemasJobExcutor(MovieManager),Logger){}
    }

    /// <summary>
    /// 电影更新城市和影院定时计划
    /// </summary>
    public class UpdateCitysAndCinemasJobExcutor : IJobExecutor                
    {
        private readonly IMovieManager MovieManager;

        public UpdateCitysAndCinemasJobExcutor(IMovieManager MovieManager) 
        {
            this.MovieManager = MovieManager;
        }

        /// <summary>
        /// 开始任务
        /// </summary>
        public void StartJob()
        {
//#if DEBUG
//#else
            MovieManager.StartUpdateCitysAndCinemas();
//#endif
        }

        /// <summary>
        /// 终止任务
        /// </summary>
        public void StopJob()
        {
            //系统终止任务
        }
    }
}
