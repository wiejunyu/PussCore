using Microsoft.IdentityModel.Tokens;
using Puss.Application.Common;
using Puss.Data.Config;
using Puss.Data.Models;
using Puss.Redis;
using Sugar.Enties;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Puss.Api.Filters
{
    /// <summary>
    /// Token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// JWT身份验证用户获取Token
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public static string UserGetToken(User user)
        {
            var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name, user.UserName)
                };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalsConfig.Configuration[ConfigurationKeys.Token_SecurityKey]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jToken = new JwtSecurityToken(
                issuer: GlobalsConfig.Configuration[ConfigurationKeys.Token_Issuer],
                audience: GlobalsConfig.Configuration[ConfigurationKeys.Token_Audience],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            string sToken = new JwtSecurityTokenHandler().WriteToken(jToken);
            string sTokenMd5 = MD5.Md5(sToken);
            //保存token和用户
            RedisHelper.Set(CommentConfig.UserToken + sTokenMd5, user.ID,30);
            return sToken;
        }

        /// <summary>
        /// JWT身份验证Token获取用户
        /// </summary>
        /// <param name="sToken">Token</param>
        /// <returns></returns>
        public static User TokenGetUser(string sToken)
        {
            string sTokenMd5 = MD5.Md5(sToken);
            //判断token是否颁发过
            if (!RedisHelper.Exists(CommentConfig.UserToken + sTokenMd5)) return null;
            //使用Token从获取用户ID
            int sUid = RedisHelper.Get<int>(CommentConfig.UserToken + sTokenMd5);
            return new UserManager().GetSingle(x => x.ID == sUid);
        }
    }
}
