using System;
using System.Collections.Generic;
using System.Text;

namespace Puss.Api.Manager.MovieManager
{
    #region 结果
    /// <summary>
    /// 城市结果
    /// </summary>
    public class ResultQueryCitys
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
        /// <summary>
        /// 错误消息
        /// </summary>
        public string errorMsg { get; set; }
        /// <summary>
        /// data
        /// </summary>
        public ResultQueryCitysData result { get; set; }
    }
    /// <summary>
    /// 数据
    /// </summary>
    public class ResultQueryCitysData
    {
        /// <summary>
        /// 城市列表
        /// </summary>
        public List<ResultQueryCitysDataList> cityList { get; set; }
    }
    /// <summary>
    /// 城市列表
    /// </summary>
    public class ResultQueryCitysDataList
    {
        /// <summary>
        /// 城市ID
        /// </summary>
        public string cityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string cityName { get; set; }
        /// <summary>
        /// 城市首字母
        /// </summary>
        public string firstletter { get; set; }
        /// <summary>
        /// 是否为热门城市,0-非热门，1-热门
        /// </summary>
        public string ishot { get; set; }
    }
    #endregion

    #region 请求
    /// <summary>
    /// 请求数据
    /// </summary>
    public class PostQueryCitys
    {
        /// <summary>
        /// 渠道号
        /// </summary>
        public string channelId { get; set; }
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
    #endregion
}
