using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Manager;
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
        public ReturnResult<string> ShowValidateCode(string CodeKey)
        {
            return InvokeFunc(() =>
            {
                return LoginManager.ShowValidateCode(CodeKey);
            });
        }

        /// <summary>
        /// 生成验邮箱验证码
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <param name="Emali">邮箱</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("EmaliGetCode")]
        public ReturnResult<string> EmaliGetCode(string CodeKey,string Emali)
        {
            return InvokeFunc(() =>
            {
                return LoginManager.EmaliGetCode(CodeKey, Emali);
            });
        }
        #endregion

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("UserRegister")]
        public ReturnResult<bool> UserRegister([FromBody]RegisterRequest request)
        {
            return InvokeFunc(() =>
            {
                return LoginManager.UserRegister(request, _accessor.HttpContext.Connection.RemoteIpAddress.ToString());
            });
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="request">登陆模型</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost("Login")]
        public ReturnResult<string> Login([FromBody]LoginRequest request)
        {
            return InvokeFunc(() =>
            {
                return LoginManager.Login(request);
            });
        }
    }
}