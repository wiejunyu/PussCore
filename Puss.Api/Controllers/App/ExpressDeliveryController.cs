using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Reptile;
using Newtonsoft.Json;
using Puss.Reptile.Models;
using Puss.Redis;
using System.Linq;
using System.Threading.Tasks;
using Puss.Api.Manager.PriceManager;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 快递查询接口
    /// </summary>
    public class ExpressDeliveryController : AppBaseController
    {
        private readonly IReptileService ReptileService;
        private readonly IRedisService RedisService;

        /// <summary>
        /// 快递查询接口
        /// </summary>
        /// <param name="ReptileService"></param>
        /// <param name="RedisService"></param>
        public ExpressDeliveryController(IReptileService ReptileService, IRedisService RedisService) 
        {
            this.ReptileService = ReptileService;
            this.RedisService = RedisService;
        }

        /// <summary>
        /// 获取HTML
        /// </summary>
        /// <param name="Url">Url</param>
        /// <returns></returns>
        [HttpPost("GetHtml")]
        [AllowAnonymous]
        public async Task<ReturnResult> GetHtml(string Url)
        {
            return await Task.Run(() => 
            {
                string html = ReptileService.GetHtml(Url);
                List<btc112> list = JsonConvert.DeserializeObject<List<btc112>>(html);
                return new ReturnResult(ReturnResultStatus.Succeed, list.SingleOrDefault(x => x.symbol.ToLowerInvariant() == "btc").price);
            });
        }

        /// <summary>
        /// 获取价格
        /// </summary>
        /// <param name="symbol">板块标识</param>
        /// <returns></returns>
        [HttpPost("GetPrice")]
        public async Task<ReturnResult> GetPrice(string symbol)
        {
            Price price = await RedisService.GetAsync(CommentConfig.Price + symbol, () => new Price());
            return new ReturnResult(ReturnResultStatus.Succeed, price.price);
        }
    }
}