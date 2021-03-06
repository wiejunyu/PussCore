﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.RabbitMq;
using Puss.Enties;
using System.Threading.Tasks;
using Puss.Api.Job;
using System;
using System.IO;
using Puss.Data.Config;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : AdminBaseController
    {
        private readonly IUserManager UserManager;
        private readonly IRabbitMQPushService RabbitMQPushService;

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="UserManager"></param>
        /// <param name="LogJobManager"></param>
        /// <param name="RabbitMQPushService"></param>
        public TestController(IUserManager UserManager, IRabbitMQPushService RabbitMQPushService)
        {
            this.UserManager = UserManager;
            this.RabbitMQPushService = RabbitMQPushService;
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
            RabbitMQPushService.PullMessage(QueueKey.SendRegisterMessageIsEmail, (Message) =>
            {
                return true;
            });
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
        /// 获取日志文件
        /// </summary>
        /// <param name="type">日志类型 QueueKey</param>
        /// <param name="date">日志日期 格式：yyyyMMdd，如20200603</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<FileResult> GetLogTxt(string type, string date = null)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(date))
                {
                    date = DateTime.Now.ToString("yyyyMMdd");
                }
                var stream = System.IO.File.OpenRead(Path.Combine(GlobalsConfig.ContentRootPath, $"log/{type}/{date}.TXT"));
                FileStreamResult result = File(stream, "application/x-apple-aspen-config", $"{date}.TXT");
                return result;
            });
        }
    }
}