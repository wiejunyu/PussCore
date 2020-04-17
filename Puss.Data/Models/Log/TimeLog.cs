using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models
{
    /// <summary>
    /// 时间日志
    /// </summary>
    public class TimeLog
    {
        /// <summary>
        /// Api路径
        /// </summary>
        public string ApiPath { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        public long Time { get; set; }
    }
}
