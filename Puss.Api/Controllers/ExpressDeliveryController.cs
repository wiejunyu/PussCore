using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Reptile;
using Newtonsoft.Json;
using Puss.Reptile.Models;
using Puss.Redis;
using Puss.Data.Models.Api;
using System.Linq;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 快递查询接口
    /// </summary>
    public class ExpressDeliveryController : ApiBaseController
    {
        private readonly IReptileHelper ReptileHelper;

        /// <summary>
        /// 快递查询接口
        /// </summary>
        public ExpressDeliveryController(IReptileHelper ReptileHelper) 
        {
            this.ReptileHelper = ReptileHelper;
        }

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