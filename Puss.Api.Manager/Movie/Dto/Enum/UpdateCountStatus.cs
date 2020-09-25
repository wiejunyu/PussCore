using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Enum
{
    /// <summary>
    /// 记录信息状态枚举
    /// </summary>
    public enum UpdateCountStatus
    {
        /// <summary>
        /// 开始计算
        /// </summary>
        [Description("开始计算")]
        Start,
        /// <summary>
        ///提交数据
        /// </summary>
        [Description("提交数据")]
        Save,
        /// <summary>
        ///全部完成
        /// </summary>
        [Description("全部完成")]
        Success,
        /// <summary>
        ///报错
        /// </summary>
        [Description("报错")]
        Error,
    }
}
