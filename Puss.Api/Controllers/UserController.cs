using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Manager;
using Puss.Data.Enum;
using Puss.Data.Models;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 用户
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private IHttpContextAccessor _accessor;
        /// <summary>
        /// 用户
        /// </summary>
        /// <param name="accessor"></param>
        public UserController(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        #region 验证码
        /// <summary>
        /// 生成验证码图片并返回图片
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("ShowValidateCode")]
        public ReturnResult ShowValidateCode(string CodeKey)
        {
            return new ReturnResult(ReturnResultStatus.Succeed,LoginManager.ShowValidateCode(CodeKey));
        }

        /// <summary>
        /// 生成验邮箱验证码
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <param name="Emali">邮箱</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("EmaliGetCode")]
        public ReturnResult EmaliGetCode(string CodeKey,string Emali)
        {
            return new ReturnResult(ReturnResultStatus.Succeed ,LoginManager.EmaliGetCode(CodeKey, Emali));
        }
        #endregion

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UserRegister")]
        public ReturnResult UserRegister([FromBody]RegisterRequest request)
        {
            return ReturnResult.ResultCalculation(() => {
                return LoginManager.UserRegister(request, _accessor.HttpContext.Connection.RemoteIpAddress.ToString());
            });
        }

        /// <summary>
        /// 登录返回Token
        /// </summary>
        /// <param name="request">登陆模型</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public ReturnResult Login([FromBody]LoginRequest request)
        {
            return new ReturnResult(ReturnResultStatus.Succeed, LoginManager.Login(request));
        }
    }
}