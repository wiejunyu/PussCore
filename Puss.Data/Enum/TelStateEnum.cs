using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Enum
{
    /// <summary>
    /// 电话状态
    /// </summary>
    public enum TelStateEnum
    {
        /// <summary>
        /// 未发送
        /// </summary>
        [Description("未发送")]
        No,

        /// <summary>
        ///已发送
        /// </summary>
        [Description("已发送")]
        Yes,
    }
}
