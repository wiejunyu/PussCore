using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Filters
{
    /// <summary>
    /// CalculateExecutionTimeMiddlewareExtensions
    /// </summary>
    public static class CalculateExecutionTimeMiddlewareExtensions
    {
        /// <summary>
        /// UseCalculateExecutionTime
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCalculateExecutionTime(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }
            return app.UseMiddleware<CalculateExecutionTimeMiddleware>();
        }
    }
}
