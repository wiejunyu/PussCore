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
        private readonly ILoginManager LoginManager;

        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="LoginManager"></param>
        public UserController(
            ILoginManager LoginManager
            )
        {
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
            return File(LoginManager.ShowValidateCode(CodeKey), @"image/jpeg");
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
            return new ReturnResult(ReturnResultStatus.Succeed,await LoginManager.ShowValidateCodeBase64(CodeKey));
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
            return new ReturnResult(ReturnResultStatus.Succeed ,await LoginManager.EmailGetCode(CodeKey, Email));
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
            return await ReturnResult.ResultCalculation(() =>
            {
                return LoginManager.UserRegister(request);
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
            return new ReturnResult(ReturnResultStatus.Succeed, await LoginManager.Login(request));
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
                    return LoginManager.LoginOut().Result;
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
            bool bToken = await LoginManager.IsToken(sToken);
            return ReturnResult.ResultCalculation(() => bToken);
        }
    }
}