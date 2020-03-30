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
using Puss.Data.Enum;
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
    public class HomeController : ApiBaseController
    {
        /// <summary>
        /// 登录测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("AdminLogin")]
        public ReturnResult AdminLogin()
        {
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// 登录测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public ReturnResult Login()
        {
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// 非登录测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("NotLogin")]
        public ReturnResult NotLogin()
        {
            return new ReturnResult(ReturnResultStatus.Succeed,"OK");
        }

        /// <summary>
        /// MQ入队
        /// </summary>
        /// <returns></returns>
        [HttpPost("PushMessage")]
        [AllowAnonymous]
        public ReturnResult PushMessage()
        {
            RabbitMQPushHelper.PushMessage(RabbitMQKey.SendRegisterMessageIsEmail, "1013422066@qq.com");
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// MQ出队
        /// </summary>
        /// <returns></returns>
        [HttpPost("PullMessage")]
        [AllowAnonymous]
        public ReturnResult PullMessage()
        {
            RabbitMQPushHelper.PullMessage(RabbitMQKey.SendRegisterMessageIsEmail, EmailHelper.MailSending);
            return new ReturnResult(ReturnResultStatus.Succeed);
        }
    }
}