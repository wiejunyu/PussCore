using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Puss.Data;
using Puss.Data.Config;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.QrCode;
using Puss.Reptile;
using Newtonsoft.Json;
using Puss.Reptile.Models;
using Puss.Redis;
using Puss.Data.Models.Api;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 快递查询接口
    /// </summary>
    public class ExpressDeliveryController : ApiBaseController
    {
        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="Url">Url</param>
        /// <returns></returns>
        [HttpPost("GetHtml")]
        [AllowAnonymous]
        public ReturnResult GetHtml(string Url)
        {
            string html = ReptileHelper.GetHtml(Url);
            List<btc112> list = JsonConvert.DeserializeObject <List<btc112>>(html);
            return new ReturnResult(ReturnResultStatus.Succeed, list.SingleOrDefault(x => x.symbol.ToLowerInvariant() == "btc").price);
        }

        /// <summary>
        /// 获取价格
        /// </summary>
        /// <param name="symbol">板块标识</param>
        /// <returns></returns>
        [HttpPost("GetPrice")]
        public ReturnResult GetPrice(string symbol)
        {
            return new ReturnResult(ReturnResultStatus.Succeed, RedisHelper.Get<Price>(CommentConfig.Price + symbol, () => new Price()).price);
        }
    }
}