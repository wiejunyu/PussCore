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
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 测试
    /// </summary>
    public class TestController : ApiBaseController
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
        public TestController(IEmailService EmailService, IRabbitMQPushService RabbitMQPushService, IHttpContextAccessor accessor) 
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
                dic.Add("oauth_consumer_key", "CK_addb7b64e88d62b39aaf4df8d51f92c553a18abc9f363bffe791f56af4340ef7713b6ea96248e6943ddcadeaf43d85cb");
                dic.Add("oauth_token", "AT_O17074483117O21ff0e2e6294376aa5f22190e32709df3701fdd4O1588039118243");
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
                    consumerSecret: "CS_c69ac3397ce27a60844b4839f1c05620e91ee49b",
                    token: dic["oauth_token"],
                    tokenSecret: "AS_151abc4e67a145da16dbc0f5e14dd13c5811338a",
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
        public async Task<ReturnResult> SetGuid(string deviceId = null)
        {
            string str = null;
            try
            {
                str = $"本次推送了:{deviceId ?? "没有推送"},IP:{_accessor.HttpContext.Connection.RemoteIpAddress}";
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            await RabbitMQPushService.PushMessage(QueueKey.GetGuid, str);
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// iphone-配置配置文件
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        [HttpPost("IphoneProfile")]
        [AllowAnonymous]
        public async Task<ReturnResult> IphoneProfile(string token)
        {
            return await Task.Run(() =>
            {
                string retString = null;

                //头部
                Dictionary<string, string> dicHeaders = new Dictionary<string, string>();
                dicHeaders.Add("User-Agent", "ProfileManager-1.0");
                dicHeaders.Add("X-Server-Protocol-Version", "2");
                dicHeaders.Add("X-ADM-Auth-Session", token);

                //内容
                IphoneProfile dicBody = new IphoneProfile();
                dicBody.profile_name = "Test Profile";
                dicBody.url = "http://118.89.182.215:86/1.mobileconfig";
                dicBody.allow_pairing= "true";
                dicBody.is_supervised= "false";
                dicBody.is_multi_user="false";
                dicBody.is_mandatory="false";
                dicBody.await_device_configured = "false";
                dicBody.is_mdm_removable = "true";
                dicBody.auto_advance_setup = "false";
                dicBody.org_magic = "ecb58a5d-c722-4008-aa60-0d732f6234bb";
                dicBody.devices = new string[1]{ "FFMWPXCGHXR6"};
                string Json = JsonConvert.SerializeObject(dicBody);

                //URL
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://mdmenrollment.apple.com/profile");

                //内容
                byte[] byteData = Encoding.UTF8.GetBytes(Json);

                //请求头
                foreach (var temp in dicHeaders)
                {
                    request.Headers.Set(temp.Key, temp.Value);
                }

                //请求方式参数
                request.Method = "POST";
                request.ContentType = "application/json;charset=UTF-8";
                request.ContentLength = byteData.Length;

                try
                {
                    Stream myResponseStream = request.GetRequestStream();
                    myResponseStream.Write(byteData, 0, byteData.Length);
                    myResponseStream.Close();
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
                    retString = responseString.ToString();
                }
                catch (Exception ex)
                {
                    throw new AppException(ex);
                }
                return new ReturnResult(ReturnResultStatus.Succeed, retString);
            });
        }

        /// <summary>
        /// iphone-分发
        /// </summary>
        /// <param name="token">token</param>
        /// <returns></returns>
        [HttpPost("IphoneDevices")]
        [AllowAnonymous]
        public async Task<ReturnResult> IphoneDevices(string token)
        {
            return await Task.Run(() =>
            {
                string retString = null;

                //头部
                Dictionary<string, string> dicHeaders = new Dictionary<string, string>();
                dicHeaders.Add("User-Agent", "ProfileManager-1.0");
                dicHeaders.Add("X-Server-Protocol-Version", "2");
                dicHeaders.Add("X-ADM-Auth-Session", token);

                //内容
                IphoneDevices dicBody = new IphoneDevices();
                dicBody.profile_uuid = "36A4919A8454DB330C1DB98EED28502D";
                dicBody.devices = new string[1] { "FFMWPXCGHXR6" };
                string Json = JsonConvert.SerializeObject(dicBody);

                //URL
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://mdmenrollment.apple.com/profile/devices");

                //内容
                byte[] byteData = Encoding.UTF8.GetBytes(Json);

                //请求头
                foreach (var temp in dicHeaders)
                {
                    request.Headers.Set(temp.Key, temp.Value);
                }

                //请求方式参数
                request.Method = "PUT";
                request.ContentType = "application/json;charset=UTF-8";
                request.ContentLength = byteData.Length;

                try
                {
                    Stream myResponseStream = request.GetRequestStream();
                    myResponseStream.Write(byteData, 0, byteData.Length);
                    myResponseStream.Close();
                    var response = (HttpWebResponse)request.GetResponse();
                    var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("utf-8")).ReadToEnd();
                    retString = responseString.ToString();
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
        /// <param name="xml">CheckInXml</param>
        /// <returns></returns>
        [HttpPut("CheckIn")]
        [AllowAnonymous]
        [Consumes("application/xml")]
        public async Task<ReturnResult> CheckIn(CheckInXml xml)
        {
            string str = null;
            try
            {
                str = $"本次推送了:{JsonConvert.SerializeObject(xml) ?? "没有推送"},IP:{_accessor.HttpContext.Connection.RemoteIpAddress}";
            }
            catch (Exception ex)
            {
                str = ex.Message;
            }
            await RabbitMQPushService.PushMessage(QueueKey.GetGuid, str);
            return new ReturnResult(ReturnResultStatus.Succeed);
        }
    }
}