using Puss.Data.Models;
using Puss.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using Puss.Data.Models.Api;
using Puss.Reptile;
using Newtonsoft.Json;
using Puss.Reptile.Models;

namespace Puss.Api.Job
{
    /// <summary>
    /// 定时计划价格业务
    /// </summary>
    public class PriceManager
    {
        /// <summary>
        /// 获取价格
        /// </summary>
        public static void GetPrice(IRedisService RedisService)
        {
            string html = new ReptileHelper().GetHtml("http://hangqing.btc112.com/getdata.php");
            List<btc112> list = JsonConvert.DeserializeObject<List<btc112>>(html);
            list.ForEach(x =>
            {
                Price price = new Price() 
                {
                    symbol = x.symbol,
                    price = x.price
                };
                RedisService.Set(CommentConfig.Price + x.symbol.ToLowerInvariant(), price, 2);
            });
        }
    }
}
