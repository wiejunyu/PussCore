using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Puss.Data;
using Puss.Data.Config;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMq;
using Puss.RabbitMQ;
using Puss.Redis;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 首页
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ApiBaseController
    {
        /// <summary>
        /// 登录测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public ReturnResult<string> Login()
        {
            return InvokeFunc(() =>
            {
                return "OK";
            });
        }

        /// <summary>
        /// 非登录测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("NotLogin")]
        [AllowAnonymous]
        public ReturnResult<string> NotLogin()
        {
            throw new Exception("错误测试");
            return InvokeFunc(() =>
            {
                return "OK";
            });
        }

        /// <summary>
        /// MQ入队
        /// </summary>
        /// <returns></returns>
        [HttpPost("PushMessage")]
        [AllowAnonymous]
        public ReturnResult<bool> PushMessage()
        {
            return InvokeFunc(() =>
            {
                RabbitMQPushHelper.PushMessage(RabbitMQKey.SendRegisterMessageIsEmail,"1013422066@qq.com");
                return true;
            });
        }

        /// <summary>
        /// MQ出队
        /// </summary>
        /// <returns></returns>
        [HttpPost("PullMessage")]
        [AllowAnonymous]
        public ReturnResult<bool> PullMessage()
        {
            return InvokeFunc(() =>
            {
                RabbitMQPushHelper.PullMessage(RabbitMQKey.SendRegisterMessageIsEmail, EmailHelper.MailSending);
                return true;
            });
        }

        
    }
}