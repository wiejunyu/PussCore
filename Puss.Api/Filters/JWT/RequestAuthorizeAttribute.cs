using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Puss.Data.Config;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Redis;
using Puss.Enties;
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
        private readonly IRedisService RedisService;

        /// <summary>
        /// 自定义验证
        /// </summary>
        /// <param name="RedisService"></param>
        public RequestAuthorizeAttribute(IRedisService RedisService) 
        {
            this.RedisService = RedisService;
        }

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
                if (GlobalsConfig.Configuration[ConfigurationKeys.Verification_Token].ToLower() == "false") return;
                //从http请求的头里面获取身份验证信息，验证Jwt
                string sAuthorization = context.HttpContext.Request.Headers["Authorization"].ToString();
                if (string.IsNullOrWhiteSpace(sAuthorization))
                {
                    context.Result = new ObjectResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.UnLogin,
                        Message = "获取不到token"
                    });
                    return;
                }
                string sToken = sAuthorization.Substring("Bearer ".Length).Trim();
                if (string.IsNullOrWhiteSpace(sToken))
                {
                    context.Result = new ObjectResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.UnLogin,
                        Message = "获取不到token"
                    });
                    return;
                }
                User user = Token.TokenGetUser(sToken);
                if (user == null)
                {
                    context.Result = new ObjectResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.UnLogin,
                        Message = "获取不到token"
                    });
                    return;
                }

                string sRedisToken = RedisService.Get<string>(CommentConfig.UserToken + user.ID, () => null);
                if (string.IsNullOrWhiteSpace(sRedisToken))
                {
                    context.Result = new ObjectResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.UnLogin,
                        Message = "获取不到token"
                    });
                    return;
                }

                if (GlobalsConfig.Configuration[ConfigurationKeys.Token_IsSignIn].ToLower() == "true") 
                {
                    if (sRedisToken != sToken)
                    {
                        context.Result = new ObjectResult(new ReturnResult()
                        {
                            Status = (int)ReturnResultStatus.UnLogin,
                            Message = "获取不到token"
                        });
                        return;
                    }
                }
            });
        }
    }
}
