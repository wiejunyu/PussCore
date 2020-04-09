using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Puss.Data.Enum;
using Puss.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Filters
{
    /// <summary>
    /// RequestAuthorizeAttribute
    /// </summary>
    public class RequestAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// OnAuthorizationAsync
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            //不允许匿名访问逻辑
            if (!context.Filters.Any(item => item is IAllowAnonymousFilter))
            {
                //从http请求的头里面获取身份验证信息，验证Jwt
                string sToken = context.HttpContext.Request.Headers["Authorization"].ToString();
                context.Result = new ObjectResult(new ReturnResult()
                {
                    Status = (int)ReturnResultStatus.BLLError,
                    Message = "获取不到token"
                });
            }
        }
    }
}
