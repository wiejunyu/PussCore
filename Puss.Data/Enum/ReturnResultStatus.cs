using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Enum
{
    /// <summary>
    /// 返回状态
    /// </summary>
    public enum ReturnResultStatus
    {
        /// <summary>
        /// 操作成功
        /// </summary>
        [Description("操作成功")]
        Succeed = 200,
        /// <summary>
        /// 数据未通过验证
        /// </summary>
        [Description("数据未通过验证")]
        UnValidate = 401,
        /// <summary>
        /// 非法操作
        /// </summary>
        [Description("非法操作")]
        Illegal = 402,
        /// <summary>
        /// 未登录
        /// </summary>
        [Description("未登录")]
        UnLogin = 403,
        /// <summary>
        /// 没有操作权限
        /// </summary>
        [Description("没有操作权限")]
        NoPermission = 404,
        /// <summary>
        /// 业务出错
        /// </summary>
        [Description("业务出错")]
        BLLError = 405,
        /// <summary>
        /// 系统找不到数据
        /// </summary>
        [Description("系统找不到数据")]
        NoData = 406,
        /// <summary>
        /// 版本错误
        /// </summary>
        [Description("版本错误")]
        VersionError = 407,
        /// <summary>
        /// 服务器出错
        /// </summary>
        [Description("服务器出错")]
        ServerError = 500
    }
}
