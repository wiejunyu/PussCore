using AutoMapper;
using Microsoft.AspNetCore.Http;
using Puss.Api.Filters;
using Puss.Application.Common;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMQ;
using Puss.Redis;
using Puss.Enties;
using System;
using System.Threading.Tasks;
using Puss.Data.Config;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Puss.Api.Manager
{
    /// <summary>
    /// 登录
    /// </summary>
    public interface ILoginManager
    {
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="request">登陆模型</param>
        /// <param name="RedisService">Redis类接口</param>
        /// <param name="UserManager"></param>
        /// <returns></returns>
        Task<string> Login(LoginRequest request, IRedisService RedisService, IUserManager UserManager);

        /// <summary>
        /// 登出接口
        /// </summary>
        /// <param name="accessor">accessor</param>
        /// <param name="RedisService">Redis类接口</param>
        /// <returns></returns>
        Task<bool> LoginOut(IHttpContextAccessor accessor, IRedisService RedisService);

        /// <summary>
        /// 生成验证码图片并返回图片字节
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <param name="RedisService">Redis接口</param>
        /// <returns></returns>
        byte[] ShowValidateCode(string CodeKey, IRedisService RedisService);

        /// <summary>
        /// 生成验证码图片并返回图片Base64
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <param name="RedisService">Redis接口</param>
        /// <returns></returns>
        Task<string> ShowValidateCodeBase64(string CodeKey, IRedisService RedisService);


        /// <summary>
        /// 邮箱获取验证码
        /// </summary>
        /// <param name="CodeKey">验证码</param>
        /// <param name="Email">邮箱</param>
        /// <param name="EmailService">邮箱类接口</param>
        /// <param name="RedisService">Redis类接口</param>
        /// <param name="CodeManager"></param>
        /// <param name="Cms_SysconfigManager"></param>
        /// <param name="DbContext"></param>
        /// <returns></returns>
        Task<string> EmailGetCode(string CodeKey, string Email, IEmailService EmailService, IRedisService RedisService, ICodeManager CodeManager, ICms_SysconfigManager Cms_SysconfigManager, DbContext DbContext);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request">注册模型</param>
        /// <param name="ip">IP</param>
        /// <param name="RabbitMQPush">MQ接口</param>
        /// <param name="UserManager"></param>
        /// <param name="UserDetailsManager"></param>
        /// <param name="DbContext"></param>
        /// <returns></returns>
        Task<bool> UserRegister(RegisterRequest request, string ip, IRabbitMQPushService RabbitMQPush, IUserManager UserManager, IUserDetailsManager UserDetailsManager, DbContext DbContext);

        /// <summary>
        /// 判断token是否有效
        /// </summary>
        /// <param name="RedisService">Redis类接口</param>
        /// <param name="sToken">token</param>
        /// <returns></returns>
        Task<bool> IsToken(IRedisService RedisService, string sToken);

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        int GetUserID();
    }
}
