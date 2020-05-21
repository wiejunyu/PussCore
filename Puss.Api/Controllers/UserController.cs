using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Aop;
using Puss.Api.Manager;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMQ;
using Puss.Redis;
using System.Threading.Tasks;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    public class UserController : ApiBaseController
    {
        private readonly IHttpContextAccessor Accessor;
        private readonly IEmailService EmailService;
        private readonly IRabbitMQPushService RabbitMQPushService;
        private readonly IRedisService RedisService;
        private readonly IUserManager UserManager;
        private readonly ICodeManager CodeManager;
        private readonly ICms_SysconfigManager Cms_SysconfigManager;
        private readonly IUserDetailsManager UserDetailsManager;
        private readonly DbContext DbContext;
        private readonly ILoginManager LoginManager;

        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="Accessor"></param>
        /// <param name="EmailService"></param>
        /// <param name="RabbitMQPushService"></param>
        /// <param name="RedisService"></param>
        /// <param name="UserManager"></param>
        /// <param name="CodeManager"></param>
        /// <param name="Cms_SysconfigManager"></param>
        /// <param name="UserDetailsManager"></param>
        /// <param name="DbContext"></param>
        /// <param name="LoginManager"></param>
        public UserController(
            IHttpContextAccessor Accessor,
            IEmailService EmailService,
            IRabbitMQPushService RabbitMQPushService,
            IRedisService RedisService,
            IUserManager UserManager,
            ICodeManager CodeManager,
            ICms_SysconfigManager Cms_SysconfigManager,
            IUserDetailsManager UserDetailsManager,
            DbContext DbContext,
            ILoginManager LoginManager
            )
        {
            this.Accessor = Accessor;
            this.EmailService = EmailService;
            this.RabbitMQPushService = RabbitMQPushService;
            this.RedisService = RedisService;
            this.UserManager = UserManager;
            this.CodeManager = CodeManager;
            this.Cms_SysconfigManager = Cms_SysconfigManager;
            this.UserDetailsManager = UserDetailsManager;
            this.DbContext = DbContext;
            this.LoginManager = LoginManager;
        }

        #region 验证码

        /// <summary>
        /// 生成验证码图片并返回图片
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        [HttpPost]
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
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> ShowValidateCodeBase64(string CodeKey)
        {
            return new ReturnResult(ReturnResultStatus.Succeed,await LoginManager.ShowValidateCodeBase64(CodeKey, RedisService));
        }

        /// <summary>
        /// 生成验邮箱验证码
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <param name="Email">邮箱</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> EmailGetCode(string CodeKey,string Email)
        {
            return new ReturnResult(ReturnResultStatus.Succeed ,await LoginManager.EmailGetCode(CodeKey, Email, EmailService,RedisService, CodeManager, Cms_SysconfigManager,DbContext));
        }
        #endregion

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [UserVerification]
        [CodeVerification]
        [AllowAnonymous]
        public async Task<ReturnResult> UserRegister([FromBody]RegisterRequest request)
        {
            return await Task.Run(() =>
            {
                return ReturnResult.ResultCalculation(() =>
                {
                    return LoginManager.UserRegister(request, Accessor.HttpContext.Connection.RemoteIpAddress.ToString(), RabbitMQPushService, UserManager, UserDetailsManager,DbContext).Result;
                });
            });
        }

        /// <summary>
        /// 登录返回Token
        /// </summary>
        /// <param name="request">登陆模型</param>
        /// <returns></returns>
        [HttpPost]
        [CodeVerification]
        [UserVerification]
        [AllowAnonymous]
        public async Task<ReturnResult> Login([FromBody]LoginRequest request)
        {
            return new ReturnResult(ReturnResultStatus.Succeed, await LoginManager.Login(request, RedisService, UserManager));
        }

        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> LoginOut()
        {
            return await Task.Run(() =>
            {
                return ReturnResult.ResultCalculation(() =>
                {
                    return LoginManager.LoginOut(Accessor, RedisService).Result;
                });
            });
        }

        /// <summary>
        /// 判断token是否有效
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> IsToken(string sToken)
        {
            if (string.IsNullOrWhiteSpace(sToken)) throw new AppException("Token不能为空");
            bool bToken = await LoginManager.IsToken(RedisService, sToken);
            return ReturnResult.ResultCalculation(() => bToken);
        }
    }
}