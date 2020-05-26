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
        /// <returns></returns>
        Task<string> Login(LoginRequest request);

        /// <summary>
        /// 登出接口
        /// </summary>
        /// <returns></returns>
        Task<bool> LoginOut();

        /// <summary>
        /// 生成验证码图片并返回图片字节
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        Task<byte[]> ShowValidateCode(string CodeKey);

        /// <summary>
        /// 生成验证码图片并返回图片Base64
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        Task<string> ShowValidateCodeBase64(string CodeKey);


        /// <summary>
        /// 邮箱获取验证码
        /// </summary>
        /// <param name="CodeKey">验证码</param>
        /// <param name="Email">邮箱</param>
        /// <returns></returns>
        Task<string> EmailGetCode(string CodeKey, string Email);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request">注册模型</param>
        /// <returns></returns>
        Task<bool> UserRegister(RegisterRequest request);

        /// <summary>
        /// 判断token是否有效
        /// </summary>
        /// <param name="sToken">token</param>
        /// <returns></returns>
        Task<bool> IsToken(string sToken);

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        int GetUserID();
    }
}
