using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models.Api
{
    /// <summary>
    /// 价格类
    /// </summary>
    public class Price
    {
        /// <summary>
        /// 板块标识
        /// </summary>
        public string symbol { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public string price { get; set; }
    }
}
