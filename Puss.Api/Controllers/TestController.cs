using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMQ;
using Puss.Enties;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Puss.Api.Job;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : ApiBaseController
    {
        private readonly IRabbitMQPushService RabbitMQPushService;
        private readonly IUserManager UserManager;
        private readonly ILogJobManager LogJobManager;

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="RabbitMQPushService"></param>
        /// <param name="UserManager"></param>
        /// <param name="LogJobManager"></param>
        public TestController(IRabbitMQPushService RabbitMQPushService, IUserManager UserManager, ILogJobManager LogJobManager)
        {
            this.RabbitMQPushService = RabbitMQPushService;
            this.UserManager = UserManager;
            this.LogJobManager = LogJobManager;
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
            return await Task.Run(() =>
            {
                RabbitMQPushService.PullMessage(QueueKey.SendRegisterMessageIsEmail, (Message) =>
                {
                    return true;
                });
                return new ReturnResult(ReturnResultStatus.Succeed);
            });
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
                LogJobManager.Log();
                return new ReturnResult(ReturnResultStatus.Succeed);
            });
        }
    }
}