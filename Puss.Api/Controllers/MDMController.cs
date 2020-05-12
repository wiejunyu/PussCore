using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Data.Enum;
using Puss.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;
using Puss.Iphone;
using Puss.Api.Aop;
using Puss.RabbitMQ;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using System.Xml;
using Puss.Redis;
using Puss.Data.Config;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// MDM
    /// </summary>
    [ApiController]
    [AllowAnonymous]
    public class MDMController : ControllerBase
    {
        private readonly IHttpContextAccessor Accessor;
        private readonly IRabbitMQPushService RabbitMQPushService;
        private readonly IRedisService RedisService;


        /// <summary>
        /// MDM
        /// </summary>
        /// <param name="Accessor"></param>
        /// <param name="RabbitMQPushService"></param>
        /// <param name="RedisService"></param>
        public MDMController(IHttpContextAccessor Accessor, IRabbitMQPushService RabbitMQPushService, IRedisService RedisService) 
        {
            this.Accessor = Accessor;
            this.RabbitMQPushService = RabbitMQPushService;
            this.RedisService = RedisService;
        }

        /// <summary>
        /// 获取MDM服务器信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("MDMServiceConfig")]
        public JsonResult MDMServiceConfig()
        {
            return new JsonResult(
                new
                {
                    //CheckIn
                    dep_enrollment_url = GlobalsConfig.Configuration[ConfigurationKeys.MDM_DepEnrollmentUrl],
                    //获取证书的地址
                    dep_anchor_certs_url = GlobalsConfig.Configuration[ConfigurationKeys.MDM_DepAnchorCertsUrl],
                    //配置文件地址
                    trust_profile_url = GlobalsConfig.Configuration[ConfigurationKeys.MDM_TrustProfileUrl]
                });
        }

        /// <summary>
        /// 返回配置文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("api/MDM/GetProfile")]
        public FileResult GetProfile()
        {
            var stream = System.IO.File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "1.mobileconfig"));
            FileStreamResult result = File(stream, "application/x-apple-aspen-config", "1.mobileconfig");
            return result;
        }

        /// <summary>
        /// 返回证书
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("api/MDM/GetCertificate")]
        public JsonResult GetCertificate()
        {
            return new JsonResult(new string[] { GlobalsConfig.Configuration[ConfigurationKeys.MDM_Certificate] });
        }

        /// <summary>
        /// iphone-配置配置文件
        /// </summary>
        /// <param name="IsToken">是否需要更新token</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MDM/IphoneProfile")]
        public async Task<ReturnResult> IphoneProfile(bool IsToken = false)
        {
            string retString;
            //URL
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://mdmenrollment.apple.com/profile");

            //头部
            Dictionary<string, string> dicHeaders = new Dictionary<string, string>()
            {
                { "User-Agent", "ProfileManager-1.0" },
                { "X-Server-Protocol-Version", "2" },
                { "X-ADM-Auth-Session", await IphoneService.OAuth(IsToken,RedisService) }
            };
            foreach (var temp in dicHeaders)
            {
                request.Headers.Set(temp.Key, temp.Value);
            }

            //内容
            IphoneProfile dicBody = new IphoneProfile()
            {
                profile_name = "Test Profile",
                url = "https://118.89.182.215/GetProfile",
                allow_pairing = "true",
                is_supervised = "false",
                is_multi_user = "false",
                is_mandatory = "false",
                await_device_configured = "false",
                is_mdm_removable = "true",
                auto_advance_setup = "false",
                org_magic = "ecb58a5d-c722-4008-aa60-0d732f6234bb",
                devices = new string[1] { "FFMWPXCGHXR6" },
            };
            string Json = JsonConvert.SerializeObject(dicBody);
            byte[] byteData = Encoding.UTF8.GetBytes(Json);

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
                var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd();
                retString = responseString.ToString();
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
            return new ReturnResult(ReturnResultStatus.Succeed, retString);
        }

        /// <summary>
        /// iphone-分发
        /// </summary>
        /// <param name="IsToken">是否需要更新token</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/MDM/IphoneDevices")]
        public async Task<ReturnResult> IphoneDevices(bool IsToken = false)
        {
            string retString;
            //URL
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://mdmenrollment.apple.com/profile/devices");

            //内容
            IphoneDevices dicBody = new IphoneDevices()
            {
                profile_uuid = "36A4919A8454DB330C1DB98EED28502D",
                devices = new string[1] { "FFMWPXCGHXR6" }
            };
            string Json = JsonConvert.SerializeObject(dicBody);
            byte[] byteData = Encoding.UTF8.GetBytes(Json);

            //请求头
            Dictionary<string, string> dicHeaders = new Dictionary<string, string>()
            {
                { "User-Agent", "ProfileManager-1.0" },
                { "X-Server-Protocol-Version", "2" },
                { "X-ADM-Auth-Session", await IphoneService.OAuth(IsToken, RedisService)}
            };
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
                var responseString = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("gb2312")).ReadToEnd();
                retString = responseString.ToString();
            }
            catch (Exception ex)
            {
                throw new AppException(ex);
            }
            return new ReturnResult(ReturnResultStatus.Succeed, retString);
        }

        /// <summary>
        /// CheckIn文件流
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [AopIphone]
        [Route("api/MDM/CheckIn")]
        public object CheckIn()
        {
            //string str = $"本次推送了:{deviceId ?? "没有推送"},IP:{_accessor.HttpContext.Connection.RemoteIpAddress}";
            //RabbitMQPushService.PushMessage(QueueKey.GetGuid, str);
            var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
            if (syncIOFeature != null)
            {
                syncIOFeature.AllowSynchronousIO = true;
            }

            #region 流获取参数
            using var ms = new MemoryStream();
            string sXml = null;
            try
            {
                Request.Body.CopyTo(ms);
                var b = ms.ToArray();
                var postParamsString = Encoding.UTF8.GetString(b);
                byte[] array = Encoding.ASCII.GetBytes(postParamsString);
                MemoryStream stream = new MemoryStream(array);             //convert stream 2 string      
                StreamReader reader = new StreamReader(stream);
                sXml = JsonConvert.SerializeObject(PListNet.PList.Load(reader.BaseStream));
                sXml = $"本次推送了:{sXml ?? "没有推送"},IP:{Accessor.HttpContext.Connection.RemoteIpAddress}";
            }
            catch (Exception ex)
            {
                sXml = ex.Message;
            }
            RabbitMQPushService.PushMessage(QueueKey.GetGuid, sXml);
            #endregion

            #region 返回XML
            object objXml = new object();
            try
            {
                using MemoryStream memoryStream = new MemoryStream();
                XmlWriterSettings settings = new XmlWriterSettings
                {
                    Encoding = Encoding.UTF8,
                    Indent = true,
                    IndentChars = "\t",
                    NewLineChars = "\n"
                };
                using XmlWriter xmlWriter = XmlWriter.Create(memoryStream, settings);
                xmlWriter.WriteDocType("plist", "-//Apple Computer//DTD PLIST 1.0//EN", "http://www.apple.com/DTDs/PropertyList-1.0.dtd", null);
                xmlWriter.WriteStartElement("plist");
                xmlWriter.WriteAttributeString("version", "1.0");
                xmlWriter.WriteStartElement("dict");
                xmlWriter.WriteEndElement();
                xmlWriter.Flush();

                StreamReader reader = new StreamReader(memoryStream);
                objXml = PListNet.PList.Load(reader.BaseStream);
            }
            catch (Exception ex)
            {
                objXml = new object();
                string sMesage = ex.Message;
            }
            return objXml;
            #endregion
        }

        /// <summary>
        /// CheckIn字符串
        /// </summary>
        /// <param name="deviceId">唯一ID</param>
        /// <returns></returns>
        [HttpPut]
        [AopIphone]
        [Route("api/MDM/CheckInOfString")]
        public async Task<ReturnResult> CheckInOfString(string deviceId = null)
        {
            string str;
            try
            {
                str = $"本次推送了:{deviceId ?? "没有推送"},IP:{Accessor.HttpContext.Connection.RemoteIpAddress}";
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