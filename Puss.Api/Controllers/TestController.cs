using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMQ;
using Sugar.Enties;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : ApiBaseController
    {
        private readonly IEmailHelper EmailHelper;
        private readonly IRabbitMQPushHelper RabbitMQPushHelper;

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="EmailHelper"></param>
        /// <param name="RabbitMQPushHelper"></param>
        public TestController(IEmailHelper EmailHelper,IRabbitMQPushHelper RabbitMQPushHelper) 
        {
            this.EmailHelper = EmailHelper;
            this.RabbitMQPushHelper = RabbitMQPushHelper;
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
        [AllowAnonymous]
        public ReturnResult NotLogin()
        {
            return new ReturnResult(ReturnResultStatus.Succeed, "OK");
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
            RabbitMQPushHelper.PullMessage(RabbitMQKey.SendRegisterMessageIsEmail, (Message) => {
                Cms_Sysconfig sys = new Cms_SysconfigManager().GetSingle(x => x.Id == 1);
                return EmailHelper.MailSending(Message, "欢迎你注册宇宙物流", "欢迎你注册宇宙物流", sys.Mail_From, sys.Mail_Code, sys.Mail_Host);
            });
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// 测试读写分离
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEmail")]
        [AllowAnonymous]
        public ReturnResult GetEmail()
        {
            var User = new UserManager().GetSingle(x => x.ID == 29);
            return new ReturnResult(ReturnResultStatus.Succeed, User.Email);
        }
    }
}