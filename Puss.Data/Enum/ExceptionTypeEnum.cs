using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Enum
{
    /// <summary>
    /// 异常类型
    /// </summary>
    public enum ExceptionTypeEnum
    {
        /// <summary>
        /// 业务异常
        /// </summary>
        [Description("业务异常")]
        Business = 1,
        /// <summary>
        ///系统异常
        /// </summary>
        [Description("系统异常")]
        System = 2,
        /// <summary>
        /// 订单
        /// </summary>
        [Description("订单")]
        Order = 3,
        /// <summary>
        /// 逻辑过程
        /// </summary>
        [Description("逻辑过程")]
        Logical = 4,
        /// <summary>
        /// 请求时间过长
        /// </summary>
        [Description("请求时间过长")]
        TimeOut = 5,
        /// <summary>
        /// 前端APP日志
        /// </summary>
        [Description("前端APP日志")]
        APP = 6
    }
}
