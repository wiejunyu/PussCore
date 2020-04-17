﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Aop;
using Puss.Api.Manager;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMQ;
using Puss.Redis;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : ApiBaseController
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailService EmailService;
        private readonly IRabbitMQPushService RabbitMQPushService;
        private readonly IRedisService RedisService;

        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="EmailService"></param>
        /// <param name="RabbitMQPushService"></param>
        /// <param name="RedisService"></param>
        public UserController(
            IHttpContextAccessor accessor,
            IEmailService EmailService,
            IRabbitMQPushService RabbitMQPushService,
            IRedisService RedisService
            )
        {
            _accessor = accessor;
            this.EmailService = EmailService;
            this.RabbitMQPushService = RabbitMQPushService;
            this.RedisService = RedisService;
        }

        #region 验证码

        /// <summary>
        /// 生成验证码图片并返回图片
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        [HttpPost("ShowValidateCode")]
        [AllowAnonymous]
        public FileResult ShowValidateCode(string CodeKey)
        {
            return File(LoginManager.ShowValidateCode(CodeKey, RedisService), @"image/jpeg");
        }

        /// <summary>
        /// 生成验证码图片并返回图片Base64
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        [HttpPost("ShowValidateCodeBase64")]
        [AllowAnonymous]
        public ReturnResult ShowValidateCodeBase64(string CodeKey)
        {
            return new ReturnResult(ReturnResultStatus.Succeed,LoginManager.ShowValidateCodeBase64(CodeKey, RedisService));
        }

        /// <summary>
        /// 生成验邮箱验证码
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <param name="Email">邮箱</param>
        /// <returns></returns>
        [HttpPost("EmailGetCode")]
        [AllowAnonymous]
        public ReturnResult EmailGetCode(string CodeKey,string Email)
        {
            return new ReturnResult(ReturnResultStatus.Succeed ,LoginManager.EmailGetCode(CodeKey, Email, EmailService,RedisService));
        }
        #endregion

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("UserRegister")]
        [CodeVerification]
        [UserVerification]
        [AllowAnonymous]
        public ReturnResult UserRegister([FromBody]RegisterRequest request)
        {
            return ReturnResult.ResultCalculation(() => {
                return LoginManager.UserRegister(request, _accessor.HttpContext.Connection.RemoteIpAddress.ToString(), RabbitMQPushService);
            });
        }

        /// <summary>
        /// 登录返回Token
        /// </summary>
        /// <param name="request">登陆模型</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [CodeVerification]
        [UserVerification]
        [AllowAnonymous]
        public ReturnResult Login([FromBody]LoginRequest request)
        {
            return new ReturnResult(ReturnResultStatus.Succeed, LoginManager.Login(request));
        }
    }
}