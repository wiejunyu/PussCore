using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Puss.BusinessCore;
using Puss.Data.Config;
using Puss.Data.Models;
using Puss.Enties;
using Puss.Redis;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Puss.Api.Filters
{
    /// <summary>
    /// Token
    /// </summary>
    public class TokenService : ITokenService
    {
        private readonly IRedisService RedisService;
        private readonly IUserManager UserManager;
        private readonly IHttpContextAccessor Accessor;

        /// <summary>
        /// Token
        /// </summary>
        public TokenService(IRedisService RedisService, IUserManager UserManager, IHttpContextAccessor Accessor) 
        {
            this.RedisService = RedisService;
            this.UserManager = UserManager;
            this.Accessor = Accessor;
        }

        /// <summary>
        /// JWT身份验证用户获取Token
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        public string UserGetToken(User user)
        {
            //Token信息
            var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddDays(30)).ToUnixTimeSeconds()}"),
                new Claim(ClaimTypes.Name, user.ID.ToString())
                };
            //获取Token对象
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalsConfig.Configuration[ConfigurationKeys.Token_SecurityKey]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var jToken = new JwtSecurityToken(
                issuer: GlobalsConfig.Configuration[ConfigurationKeys.Token_Issuer],
                audience: GlobalsConfig.Configuration[ConfigurationKeys.Token_Audience],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            //Token对象转成Token字符串
            string sToken = new JwtSecurityTokenHandler().WriteToken(jToken);
            RedisService.SetAsync(CommentConfig.UserToken + user.ID, sToken, int.Parse(GlobalsConfig.Configuration[ConfigurationKeys.Token_Time]));
            return sToken;
        }

        /// <summary>
        /// JWT身份验证Token获取用户
        /// </summary>
        /// <param name="sToken">Token</param>
        /// <returns></returns>
        public User TokenGetUser(string sToken)
        {
            try
            {
                // 将字符串Token解码成Token对象;
                JwtSecurityToken _token = new JwtSecurityToken(sToken);
                //用户ID
                int sUid = int.Parse(_token.Payload[ClaimTypes.Name].ToString());
                //使用Token从获取用户ID
                return UserManager.GetSingle(x => x.ID == sUid);
            }
            catch 
            {
                return null;
            }
        }

        /// <summary>
        /// 废除token
        /// </summary>
        /// <returns></returns>
        public bool RemoveToken()
        {
            try
            {
                string sToken = null;
                if (Accessor != null && Accessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    sToken = Accessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("Bearer", "");
                }
                if (string.IsNullOrWhiteSpace(sToken)) return false;
                // 将字符串Token解码成Token对象;
                User user = TokenGetUser(sToken);
                UserGetToken(user);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
