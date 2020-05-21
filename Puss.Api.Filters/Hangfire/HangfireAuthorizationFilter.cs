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
            //var httpcontext = context.GetHttpContext();
            //return httpcontext.User.Identity.IsAuthenticated;
            return true;
        }
    }
}
