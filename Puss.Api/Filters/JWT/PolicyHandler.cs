using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Puss.Data.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Puss.Api.Filters
{
    /// <summary>
    /// PolicyHandler
    /// </summary>
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        /// <summary>
        /// HandleRequirementAsync
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            //判断是否开启权限验证
            if (GlobalsConfig.Configuration[ConfigurationKeys.Permission_IsOpen].ToLower() == "false")
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }
            //赋值用户权限
            var userPermissions = requirement.UserPermissions;
            //请求Url
            ControllerActionDescriptor controllActionDesription = new ControllerActionDescriptor();
            var http = (context.Resource as RouteEndpoint);
            var questUrl = "/" + http.RoutePattern.RawText;
            //获取当前URL权限
            var Permissions = userPermissions.SingleOrDefault(w => w.Url.ToLowerInvariant() == questUrl.ToLowerInvariant());
            if (Permissions == null) {
                context.Fail(); 
                return Task.CompletedTask; 
            }
            //判断当前URL是否需要登录
            if (Permissions.IsLogin)
            {
                //是否经过验证
                if (!context.User.Identity.IsAuthenticated)
                {
                    context.Fail();
                    return Task.CompletedTask;
                }
            }
            //判断当前URL是否需要管理员
            if (Permissions.IsLogin && Permissions.IsAdmin)
            {
                //是否经过验证
                var userName = context.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name).Value;
                if (Permissions.UserName != userName) 
                { 
                    context.Fail();
                    return Task.CompletedTask;
                }
            }
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        
    }
}
