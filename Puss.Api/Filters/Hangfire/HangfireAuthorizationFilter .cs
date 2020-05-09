using Hangfire.Dashboard;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Filters
{
    /// <summary>
    /// Hangfire验证重写
    /// </summary>
    public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
    {

        /// <summary>
        /// 这里写自定义规则
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public bool Authorize([NotNull] DashboardContext context)
        {
            if (context.Request.LocalIpAddress.Equals("127.0.0.1") || context.Request.LocalIpAddress.Equals("::1"))
                return true;
            else
                return false;
        }
    }
}
