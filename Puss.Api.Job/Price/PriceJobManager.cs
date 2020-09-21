using Puss.Data.Models;
using Puss.Redis;
using System.Collections.Generic;
using Puss.Reptile;
using Newtonsoft.Json;
using Puss.Reptile.Models;
using Puss.Api.Manager.PriceManager;

namespace Puss.Api.Job
{
    /// <summary>
    /// 定时计划价格业务
    /// </summary>
    public class PriceJobManager
    {
        /// <summary>
        /// 获取价格
        /// </summary>
        /// <param name="ReptileService">获取价格类接口</param>
        /// <param name="RedisService">Redis类接口</param>
        public static void GetPrice(IReptileService ReptileService, IRedisService RedisService)
        {
            string html = ReptileService.GetHtml("http://hangqing.btc112.com/getdata.php");
            List<btc112> list = JsonConvert.DeserializeObject<List<btc112>>(html);
            list.ForEach(x =>
            {
                Price price = new Price() 
                {
                    symbol = x.symbol,
                    price = x.price
                };
                RedisService.SetAsync(CommentConfig.Price + x.symbol.ToLowerInvariant(), price, 2);
            });
        }
    }
}
