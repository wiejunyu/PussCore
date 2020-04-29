using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.OAuth1;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Puss.Iphone
{
    public class Iphone
    {
        /// <summary>
        /// OAuth
        /// </summary>
        /// <returns></returns>
        public ReturnResult OAuth()
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
        }
    }
}
