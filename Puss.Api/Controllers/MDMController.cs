using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.OAuth1;
using Puss.RabbitMQ;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using PListNet;
using System.Xml;
using System.Runtime.Serialization.Formatters.Binary;
using Puss.Api.Aop;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    [ApiController]
    public class MDMController : ControllerBase
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly IEmailService EmailService;
        private readonly IRabbitMQPushService RabbitMQPushService;

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="EmailService"></param>
        /// <param name="RabbitMQPushService"></param>
        /// <param name="accessor"></param>
        public MDMController(IEmailService EmailService, IRabbitMQPushService RabbitMQPushService, IHttpContextAccessor accessor)
        {
            this.EmailService = EmailService;
            this.RabbitMQPushService = RabbitMQPushService;
            _accessor = accessor;
        }

        /// <summary>
        /// 获取MDM服务器信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("MDMServiceConfig")]
        [AllowAnonymous]
        public JsonResult MDMServiceConfig()
        {
            return new JsonResult(
                new { dep_enrollment_url = "https://118.89.182.215/api/Test/CheckIn" ,
                dep_anchor_certs_url = "https://118.89.182.215/GetCertificate",
                //trust_profile_url = "http://118.89.182.215:86/1.mobileconfig"
                trust_profile_url = "https://118.89.182.215/api/Test/IphoneProfile"
                }
                );
        }

        /// <summary>
        /// 返回配置文件
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetProfile")]
        [AllowAnonymous]
        public FileResult GetProfile()
        {
            var stream = System.IO.File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "1.mobileconfig"));
            return File(stream, "text/plain", "1.mobileconfig");
        }

        /// <summary>
        /// 返回证书
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetCertificate")]
        [AllowAnonymous]
        public JsonResult GetCertificate()
        {
            return new JsonResult(new string[] { });
        }
    }
}