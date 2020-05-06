using Puss.Data.Models;
using Puss.OAuth1;
using Puss.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

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
            if (RedisService.Exists(CommentConfig.MDM_Token) && !IsToken) return (await RedisService.GetAsync(CommentConfig.MDM_Token)).ToString();
            return await Task.Run(() =>
            {
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
                RedisService.SetAsync(CommentConfig.MDM_Token, retString, 1);
                return retString;
            });
        }
    }

    public class IphoneServiceKey
    {
        /// <summary>
        /// realm
        /// </summary>
        public static string Realm
        {
            get { return "https://mdmenrollment.apple.com/session"; }
        }

        /// <summary>
        /// consumer_key
        /// </summary>
        public static string Consumer_key
        {
            get { return "CK_addb7b64e88d62b39aaf4df8d51f92c553a18abc9f363bffe791f56af4340ef7713b6ea96248e6943ddcadeaf43d85cb"; }
        }

        /// <summary>
        /// access_token
        /// </summary>
        public static string Access_token
        {
            get { return "AT_O17074483117O21ff0e2e6294376aa5f22190e32709df3701fdd4O1588039118243"; }
        }

        /// <summary>
        /// oauth_signature_method
        /// </summary>
        public static string Oauth_signature_method
        {
            get { return "HMAC-SHA1"; }
        }


        /// <summary>
        /// consumer_secret
        /// </summary>
        public static string Consumer_secret
        {
            get { return "CS_c69ac3397ce27a60844b4839f1c05620e91ee49b"; }
        }

        /// <summary>
        /// access_secret
        /// </summary>
        public static string Access_secret
        {
            get { return "AS_151abc4e67a145da16dbc0f5e14dd13c5811338a"; }
        }
    }
}
