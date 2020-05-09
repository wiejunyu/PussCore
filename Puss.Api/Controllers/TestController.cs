using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMQ;
using Puss.Enties;
using System;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using System.Xml;
using Puss.Api.Aop;
using Hangfire;
using Puss.Api.Job;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : ApiBaseController
    {
        private readonly IEmailService EmailService;
        private readonly IRabbitMQPushService RabbitMQPushService;
        private readonly IUserManager UserManager;
        private readonly ICms_SysconfigManager Cms_SysconfigManager;
        private readonly ILogErrorDetailsManager LogErrorDetailsManager;
        private readonly ILogJobDetailsManager LogJobDetailsManager;

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="EmailService"></param>
        /// <param name="RabbitMQPushService"></param>
        /// <param name="UserManager"></param>
        /// <param name="Cms_SysconfigManager"></param>
        /// <param name="LogErrorDetailsManager"></param>
        /// <param name="LogJobDetailsManager"></param>
        public TestController(IEmailService EmailService, IRabbitMQPushService RabbitMQPushService, IUserManager UserManager, ICms_SysconfigManager Cms_SysconfigManager, ILogErrorDetailsManager LogErrorDetailsManager, ILogJobDetailsManager LogJobDetailsManager)
        {
            this.EmailService = EmailService;
            this.RabbitMQPushService = RabbitMQPushService;
            this.UserManager = UserManager;
            this.Cms_SysconfigManager = Cms_SysconfigManager;
        }

        /// <summary>
        /// 登录测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> Login()
        {
            return await Task.Run(() =>
            {
                return new ReturnResult(ReturnResultStatus.Succeed);
            });
        }

        /// <summary>
        /// 非登录测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> NotLogin()
        {
            return await Task.Run(() =>
            {
                return new ReturnResult(ReturnResultStatus.Succeed);
            });
        }

        /// <summary>
        /// MQ入队
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> PushMessage()
        {
            string str = null;
            JsonConvert.DeserializeObject(str);
            await RabbitMQPushService.PushMessage(QueueKey.SendRegisterMessageIsEmail, "1013422066@qq.com");
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// MQ出队
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> PullMessage()
        {
            //return await Task.Run(() =>
            //{
            //    RabbitMQPushService.PullMessage(QueueKey.SendRegisterMessageIsEmail, (Message) =>
            //    {
            //        Cms_Sysconfig sys = Cms_SysconfigManager.GetById(1);
            //        return EmailService.MailSending(Message, "欢迎你注册宇宙物流", "欢迎你注册宇宙物流", sys.Mail_From, sys.Mail_Code, sys.Mail_Host);
            //    });
            //    return new ReturnResult(ReturnResultStatus.Succeed);
            //});
            LogManager.Log();
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// 测试读写分离
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> GetEmail()
        {
            var User = await UserManager.GetByIdAsync(29);
            return new ReturnResult(ReturnResultStatus.Succeed, User.Email);
        }

        /// <summary>
        /// 执行后台作业
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> SetBackgroundJob()
        {
            return await Task.Run(() =>
            {
                BackgroundJob.Enqueue(() => RabbitMQPushService.PushMessage(QueueKey.AttendanceStart, "开始"));
                return new ReturnResult(ReturnResultStatus.Succeed);
            });
        }
    }
}