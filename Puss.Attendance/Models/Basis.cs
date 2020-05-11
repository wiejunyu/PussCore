using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Attendance
{
    /// <summary>
    /// 基本参数
    /// </summary>
    public class Basis
    {
        /// <summary>
        /// 序列号
        /// </summary>
        public static string Serial_No { get => "0000"; }

        /// <summary>
        /// 公话ID
        /// </summary>
        public static string Device_Id { get => "117000169649054977"; }

        /// <summary>
        /// 学生卡号
        /// </summary>
        public static string Card_Id { get => "KQK001507"; }

        /// <summary>
        /// 版本信息
        /// </summary>
        public static string VersionInfo { get => "VER1.00 2020/05/11"; }

        /// <summary>
        /// 预留监控信息
        /// </summary>
        public static string MinitorInfo { get => "111"; }
    }

    /// <summary>
    /// 进出校枚举
    /// </summary>
    public enum OptType
    {
        /// <summary>
        /// 进校
        /// </summary>
        In,

        /// <summary>
        /// 出校
        /// </summary>
        Out,
    }
}
