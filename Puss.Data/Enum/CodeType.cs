﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Enum
{
    /// <summary>
    /// 验证方式枚举
    /// </summary>
    public enum CodeType
    {
        /// <summary>
        /// 邮箱验证
        /// </summary>
        [Description("邮箱验证")]
        Email,
        /// <summary>
        ///手机验证
        /// </summary>
        [Description("手机验证")]
        Phone,
    }
}
