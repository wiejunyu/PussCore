using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Puss.Data.Config;
using Puss.Data.Enum;
using Puss.Data.Models;
using Sugar.Enties;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Filters
{
    /// <summary>
    /// 自定义验证
    /// </summary>
    public class RequestAuthorizeAttribute : Attribute, IAsyncAuthorizationFilter
    {
        /// <summary>
        /// 身份验证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            await Task.Run(() =>
            {
                //不允许匿名访问逻辑
                if (context.ActionDescriptor.EndpointMetadata.Any(item => item is AllowAnonymousAttribute)) return;
                //判断是否开启权限验证
                if (GlobalsConfig.Configuration[ConfigurationKeys.Permission_IsOpen].ToLower() == "false") return;
                //从http请求的头里面获取身份验证信息，验证Jwt
                string sAuthorization = context.HttpContext.Request.Headers["Authorization"].ToString();
                string sToken = sAuthorization.Substring("Bearer ".Length).Trim();
                if (string.IsNullOrWhiteSpace(sToken))
                {
                    context.Result = new ObjectResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.BLLError,
                        Message = "获取不到token"
                    });
                    return;
                }
                User user = Token.TokenGetUser(sToken);
                if (user == null)
                {
                    context.Result = new ObjectResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.BLLError,
                        Message = "获取不到token"
                    });
                    return;
                }
            });
        }
    }
}
