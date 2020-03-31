using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Reptile.Models
{
    public class btc112
    {
        /// <summary>
        /// ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 英文代码
        /// </summary>
        public string slug { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string name_en { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string name_cn { get; set; }

        /// <summary>
        /// 24h涨跌幅
        /// </summary>
        public string change { get; set; }

        /// <summary>
        /// 价格$
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 板块标识
        /// </summary>
        public string symbol { get; set; }

        /// <summary>
        /// 流通量
        /// </summary>
        public string supply { get; set; }

        /// <summary>
        /// 市值
        /// </summary>
        public string market_cap { get; set; }

        /// <summary>
        /// 人民币市值
        /// </summary>
        public double market_cap_cny { get; set; }

        /// <summary>
        /// 24h成交额
        /// </summary>
        public string trade_24h { get; set; }

        /// <summary>
        /// 人民币成交量
        /// </summary>
        public double volume_cny { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string cn_name { get; set; }
    }
}
