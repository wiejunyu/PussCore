using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.OAuth1;
using Puss.RabbitMQ;
using Puss.Redis;
using Sugar.Enties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : ApiBaseController
    {
        private readonly IEmailService EmailService;
        private readonly IRabbitMQPushService RabbitMQPushService;
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="accessor"></param>
        /// <param name="EmailService"></param>
        /// <param name="RabbitMQPushService"></param>
        public TestController(IHttpContextAccessor accessor, IEmailService EmailService, IRabbitMQPushService RabbitMQPushService) 
        {
            this.EmailService = EmailService;
            this.RabbitMQPushService = RabbitMQPushService;
            _accessor = accessor;
        }
        /// <summary>
        /// 登录测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
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
        [HttpPost("NotLogin")]
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
        [HttpPost("PushMessage")]
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
        [HttpPost("PullMessage")]
        [AllowAnonymous]
        public async Task<ReturnResult> PullMessage()
        {
            return await Task.Run(() =>
            {
                RabbitMQPushService.PullMessage(QueueKey.SendRegisterMessageIsEmail, (Message) =>
                {
                    Cms_Sysconfig sys = new Cms_SysconfigManager().GetById(1);
                    return EmailService.MailSending(Message, "欢迎你注册宇宙物流", "欢迎你注册宇宙物流", sys.Mail_From, sys.Mail_Code, sys.Mail_Host);
                });
                return new ReturnResult(ReturnResultStatus.Succeed);
            });
        }

        /// <summary>
        /// 测试读写分离
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetEmail")]
        [AllowAnonymous]
        public async Task<ReturnResult> GetEmail()
        {
            var User = await new UserManager().GetByIdAsync(29);
            return new ReturnResult(ReturnResultStatus.Succeed, User.Email);
        }

        /// <summary>
        /// OAuth签名测试
        /// </summary>
        /// <returns></returns>
        [HttpPost("OAuth")]
        [AllowAnonymous]
        public async Task<ReturnResult> OAuth()
        {
            return await Task.Run(() =>
            {
                //基本参数
                OAuthBase oAuth = new OAuthBase();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("realm", "https://mdmenrollment.apple.com/session");
                dic.Add("oauth_consumer_key", "CK_dce7cc016cf57f471b12386f44cadc4757f6ac63de13ad65689eb155a3dd0553d038f9edbb525c045221516a076b6fb4");
                dic.Add("oauth_token", "AT_O17074483117O1139408d744ae5d202005d882c6fe4e3ad82fc80O1587695006526");
                dic.Add("oauth_signature_method", "HMAC-SHA1");
                string timeStamp = oAuth.GenerateTimeStamp();
                string nonce = oAuth.GenerateNonce();

                #region 签名
                //签名
                string normalizedUrl = null;
                string normalizedRequestParameters = null;
                string sSign = oAuth.GenerateSignature(
                    url: new Uri(dic["realm"]),
                    callback: null,
                    consumerKey: dic["oauth_consumer_key"],
                    consumerSecret: "CS_58826f88c61a87122d6997df1b50ff313e8c5990",
                    token: dic["oauth_token"],
                    tokenSecret: "AS_95b5db75e23d5e0605295243dcc2c6c3f84b8abc",
                    httpMethod: "GET",
                    timeStamp: timeStamp,
                    nonce: nonce,
                    signatureType: OAuthBase.SignatureTypes.HMACSHA1,
                    verifier: null,
                    normalizedUrl: out normalizedUrl,
                    normalizedRequestParameters: out normalizedRequestParameters);
                #endregion

                dic.Add("oauth_signature", sSign);
                dic.Add("oauth_timestamp", timeStamp);
                dic.Add("oauth_nonce", nonce);
                dic.Add("oauth_version", "1.0");
                string retString = string.Empty;

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://mdmenrollment.apple.com/session");
                request.Method = "GET";
                string Headers = null;
                //请求头
                foreach (var temp in dic)
                {
                    Headers += $"{temp.Key}=\"{temp.Value}\",";
                }
                Headers = Headers.Substring(0, Headers.ToString().Length - 1);

                request.Headers.Set("Authorization", $"OAuth {Headers}");
                try
                {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream myResponseStream = response.GetResponseStream();
                    StreamReader streamReader = new StreamReader(myResponseStream);
                    retString = streamReader.ReadToEnd();
                    streamReader.Close();
                    myResponseStream.Close();
                }
                catch (Exception ex)
                {
                    throw new AppException(ex);
                }
                return new ReturnResult(ReturnResultStatus.Succeed, retString);
            });
        }

        /// <summary>
        /// 获取唯一ID保存
        /// </summary>
        /// <param name="deviceId">唯一ID</param>
        /// <returns></returns>
        [HttpGet("SetGuid")]
        [AllowAnonymous]
        public async Task<ReturnResult> SetGuid(string deviceId)
        {
            await RabbitMQPushService.PushMessage(QueueKey.GetGuid, deviceId);
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("Test")]
        [AllowAnonymous]
        public async Task<ReturnResult> Test()
        {
            return await Task.Run(() => new ReturnResult(ReturnResultStatus.Succeed));
        }
    }
}