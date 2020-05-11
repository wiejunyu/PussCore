using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Attendance
{
    /// <summary>
    /// 功能号
    /// </summary>
    public class FuncNo
    {
        /// <summary>
        /// 网络连接状态查询
        /// </summary>
        public static string CONNECT_STATUS { get => "05"; }

        /// <summary>
        /// 公话认证
        /// </summary>
        public static string PHONE_AUTHEN { get => "10"; }

        /// <summary>
        /// 学生进校离校记录
        /// </summary>
        public static string STDT_SCHOOL_RECS { get => "28"; }

        /// <summary>
        /// 获取公话状态
        /// </summary>
        public static string ABT_STATUS { get => "82"; }
    }
}
