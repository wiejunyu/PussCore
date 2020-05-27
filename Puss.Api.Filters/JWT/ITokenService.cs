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
    public interface ITokenService
    {
        /// <summary>
        /// JWT身份验证用户获取Token
        /// </summary>
        /// <param name="user">用户</param>
        /// <returns></returns>
        string UserGetToken(User user);

        /// <summary>
        /// JWT身份验证Token获取用户
        /// </summary>
        /// <param name="sToken">Token</param>
        /// <returns></returns>
        User TokenGetUser(string sToken);

        /// <summary>
        /// 废除token
        /// </summary>
        /// <returns></returns>
        bool RemoveToken();
    }
}
