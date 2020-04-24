using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMQ;
using Puss.Redis;
using Sugar.Enties;
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
        private readonly IRedisService RedisService;

        /// <summary>
        /// 测试
        /// </summary>
        /// <param name="EmailService"></param>
        /// <param name="RabbitMQPushService"></param>
        public TestController(IEmailService EmailService, IRabbitMQPushService RabbitMQPushService, IRedisService RedisService) 
        {
            this.EmailService = EmailService;
            this.RabbitMQPushService = RabbitMQPushService;
            this.RedisService = RedisService;
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
                OAuthBase oAuth = new OAuthBase();
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("realm", "https://mdmenrollment.apple.com/session");
                dic.Add("oauth_consumer_key", "CK_dce7cc016cf57f471b12386f44cadc4757f6ac63de13ad65689eb155a3dd0553d038f9edbb525c045221516a076b6fb4");
                dic.Add("oauth_token", null);
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
                    token: null,
                    tokenSecret: null,
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
    }
}