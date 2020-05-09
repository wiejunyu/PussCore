using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Attendance
{
    /// <summary>
    /// 连接配置
    /// </summary>
    public class Connection
    {
        /// <summary>
        /// 主机
        /// </summary>
        //public static string Host { get => "10.0.75.1"; }
        public static string Host { get => "211.138.251.205"; }

        /// <summary>
        /// 端口
        /// </summary>
        public static int Port { get => 6186; }
        //public static int Port { get => 6000; }
    }
}
