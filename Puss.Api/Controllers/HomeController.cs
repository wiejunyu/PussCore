using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMq;
using Puss.RabbitMQ;

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