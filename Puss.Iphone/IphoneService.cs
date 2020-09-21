using Puss.Data.Config;
using Puss.Data.Models;
using Puss.OAuth1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Puss.Redis;

namespace Puss.Iphone
{
    public class IphoneService
    {
        /// <summary>
        /// OAuth验证并获取token
        /// </summary>
        /// <param name="IsToken">是否需要更新token</param>
        /// <param name="RedisService">Redis类接口</param>
        /// <returns></returns>
        public static async Task<string> OAuth(bool IsToken, IRedisService RedisService)
        {
            if (await RedisService.ExistsAsync(CommentConfig.MDM_Token) && !IsToken) return (await RedisService.GetAsync<string>(CommentConfig.MDM_Token,() => null)).ToString();
            string retString = string.Empty;
            while (true)
            {
                //基本参数
                OAuthBase oAuth = new OAuthBase();
                Dictionary<string, string> dic = new Dictionary<string, string>()
                {
                    { "realm", IphoneServiceKey.Realm },
                    {"oauth_consumer_key", IphoneServiceKey.Consumer_key },
                    {"oauth_token", IphoneServiceKey.Access_token },
                    { "oauth_signature_method", IphoneServiceKey.Oauth_signature_method}
                };
                string timeStamp = oAuth.GenerateTimeStamp();
                string nonce = oAuth.GenerateNonce();

                #region 签名
                //签名
                string sSign = oAuth.GenerateSignature(
                    url: new Uri(dic["realm"]),
                    callback: null,
                    consumerKey: dic["oauth_consumer_key"],
                    consumerSecret: IphoneServiceKey.Consumer_secret,
                    token: dic["oauth_token"],
                    tokenSecret: IphoneServiceKey.Access_secret,
                    httpMethod: "GET",
                    timeStamp: timeStamp,
                    nonce: nonce,
                    signatureType: OAuthBase.SignatureTypes.HMACSHA1,
                    verifier: null,
                    normalizedUrl: out string normalizedUrl,
                    normalizedRequestParameters: out string normalizedRequestParameters);
                #endregion

                dic.Add("oauth_signature", sSign);
                dic.Add("oauth_timestamp", timeStamp);
                dic.Add("oauth_nonce", nonce);
                dic.Add("oauth_version", "1.0");

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
                    break;
                }
                catch (Exception ex)
                {
                    Thread.Sleep(30000);
                    string message = ex.Message;
                }
            }
            await RedisService.SetAsync(CommentConfig.MDM_Token, retString, 1);
            return retString;
        }
    }

    public class IphoneServiceKey
    {
        /// <summary>
        /// realm
        /// </summary>
        public static string Realm
        {
            get { return GlobalsConfig.Configuration[ConfigurationKeys.MDM_Realm]; }
        }

        /// <summary>
        /// consumer_key
        /// </summary>
        public static string Consumer_key
        {
            get { return GlobalsConfig.Configuration[ConfigurationKeys.MDM_ConsumerKey]; }
        }

        /// <summary>
        /// access_token
        /// </summary>
        public static string Access_token
        {
            get { return GlobalsConfig.Configuration[ConfigurationKeys.MDM_AccessToken]; }
        }

        /// <summary>
        /// oauth_signature_method
        /// </summary>
        public static string Oauth_signature_method
        {
            get { return GlobalsConfig.Configuration[ConfigurationKeys.MDM_OauthSignatureMethod]; }
        }


        /// <summary>
        /// consumer_secret
        /// </summary>
        public static string Consumer_secret
        {
            get { return GlobalsConfig.Configuration[ConfigurationKeys.MDM_ConsumerSecret]; }
        }

        /// <summary>
        /// access_secret
        /// </summary>
        public static string Access_secret
        {
            get { return GlobalsConfig.Configuration[ConfigurationKeys.MDM_AccessSecret]; }
        }
    }
}
