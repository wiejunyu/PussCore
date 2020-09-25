using System;
using System.Collections.Generic;
using System.Text;

namespace Puss.Api.Manager.MovieManager
{
    /// <summary>
    /// 更新统计
    /// </summary>
    public class UpdateCount
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int count { get; set; }
        /// <summary>
        /// 当前个数
        /// </summary>
        public int current { get; set; }
        /// <summary>
        /// 当前名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int status { get; set; }
    }
}
