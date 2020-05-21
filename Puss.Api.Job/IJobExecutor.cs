using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Job
{
    /// <summary>
    /// IJobExecutor
    /// </summary>
    public interface IJobExecutor
    {
        /// <summary>
        /// 开始任务
        /// </summary>
        void StartJob();

        /// <summary>
        ///  结束任务
        /// </summary>
        void StopJob();
    }
}
