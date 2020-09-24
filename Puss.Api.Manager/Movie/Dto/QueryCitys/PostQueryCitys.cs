using System;
using System.Collections.Generic;
using System.Text;

namespace Puss.Api.Manager.MovieManager
{
    #region 结果
    #region 城市
    /// <summary>
    /// 城市结果
    /// </summary>
    public class ResultQueryCitys : ResultBase
    {
        /// <summary>
        /// 返回
        /// </summary>
        public ResultQueryCitysData result { get; set; }
    }
    /// <summary>
    /// 城市结果数据
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

    #region 影院
    /// <summary>
    /// 影院列表
    /// </summary>
    public class ResultCinemasList
    {
        /// <summary>
        /// 影院ID
        /// </summary>
        public int cinemaId { get; set; }
        /// <summary>
        /// 影院名称
        /// </summary>
        public string cinemaName { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string cityName { get; set; }
        /// <summary>
        ///  影院地址
        /// </summary>
        public string cinemaAddr { get; set; }
        /// <summary>
        /// 地区名称
        /// </summary>
        public string areaName { get; set; }
        /// <summary>
        /// 影院电话
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 影院专资编码
        /// </summary>
        public string std_cinema_id { get; set; }
        /// <summary>
        /// 区域ID（县区）
        /// </summary>
        public string regionid { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double latitude { get; set; }
    }

    /// <summary>
    /// 影院列表结果
    /// </summary>
    public class ResultQueryCinemasData
    {
        /// <summary>
        /// 影院列表
        /// </summary>
        public List<ResultCinemasList> cinemasList { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pageTotal { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int page { get; set; }
    }

    /// <summary>
    /// 结果
    /// </summary>
    public class ResultQueryCinemas : ResultBase
    {
        /// <summary>
        /// 结果
        /// </summary>
        public ResultQueryCinemasData result { get; set; }
    }
    #endregion

    #region 场次
    /// <summary>
    /// 场次
    /// </summary>
    public class ResultShows
    {
        /// <summary>
        /// 影院ID
        /// </summary>
        public string cinemaId { get; set; }
        /// <summary>
        /// 厅名称
        /// </summary>
        public string hallName { get; set; }
        /// <summary>
        /// 电影ID
        /// </summary>
        public string filmId { get; set; }
        /// <summary>
        /// 电影名称
        /// </summary>
        public string filmName { get; set; }
        /// <summary>
        /// 场次ID
        /// </summary>
        public string showId { get; set; }
        /// <summary>
        /// 播放时长
        /// </summary>
        public string duration { get; set; }
        /// <summary>
        /// 开演时间
        /// </summary>
        public string showTime { get; set; }
        /// <summary>
        /// 停售时间
        /// </summary>
        public string stopSellTime { get; set; }
        /// <summary>
        /// 场次类型国语 2D
        /// </summary>
        public string showVersionType { get; set; }
        /// <summary>
        /// 结算价格
        /// </summary>
        public string settlePrice { get; set; }
        /// <summary>
        /// 市场价
        /// </summary>
        public string price { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public string minPrice { get; set; }
        /// <summary>
        /// 语言类型  中文 原版  英文
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 影厅类型  2D 3D
        /// </summary>
        public string planType { get; set; }
    }

    /// <summary>
    /// 场次数据
    /// </summary>
    public class ResultQueryShowsData
    {
        /// <summary>
        /// 场次列表
        /// </summary>
        public List<ResultShows> showList { get; set; }
    }

    /// <summary>
    /// 场次结果
    /// </summary>
    public class ResultQueryShows : ResultBase
    {
        /// <summary>
        /// 结果
        /// </summary>
        public ResultQueryShowsData result { get; set; }
    }

    #endregion

    #region 获取热门
    /// <summary>
    /// 获取当前热映影片数据
    /// </summary>
    public class ResultHotResultData
    {
        /// <summary>
        /// 影片列表
        /// </summary>
        public List<ResultFilm> filmList { get; set; }
    }

    /// <summary>
    /// 获取当前热映影片
    /// </summary>
    public class ResultHotResult : ResultBase
    {
        /// <summary>
        /// 获取当前热映影片
        /// </summary>
        public ResultHotResultData result { get; set; }
    }
    #endregion

    #region 影片
    /// <summary>
    /// 人员信息
    /// </summary>
    public class ResultActorsDirectorDesc
    {
        /// <summary>
        /// 中文名字
        /// </summary>
        public string sc_name { get; set; }
        /// <summary>
        /// 英文名字
        /// </summary>
        public string en_name { get; set; }
        /// <summary>
        /// 角色名字
        /// </summary>
        public string act_name { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        public string avatar { get; set; }
    }

    /// <summary>
    /// 导演和演员
    /// </summary>
    public class ResultActorsDirector
    {
        /// <summary>
        /// 导演
        /// </summary>
        public List<ResultActorsDirectorDesc> director { get; set; }
        /// <summary>
        /// 演员
        /// </summary>
        public List<ResultActorsDirectorDesc> actors { get; set; }
    }

    /// <summary>
    /// 影片
    /// </summary>
    public class ResultFilm
    {
        /// <summary>
        /// 影片ID
        /// </summary>
        public int filmId { get; set; }
        /// <summary>
        /// 影片评分
        /// </summary>
        public string grade { get; set; }
        /// <summary>
        /// 中国大陆,中国香港
        /// </summary>
        public string moviearea { get; set; }
        /// <summary>
        /// 影片名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 影片时长
        /// </summary>
        public int duration { get; set; }
        /// <summary>
        /// 影片上映日期
        /// </summary>
        public string publishDate { get; set; }
        /// <summary>
        /// 影片导演
        /// </summary>
        public string director { get; set; }
        /// <summary>
        /// 影片主演
        /// </summary>
        public string cast { get; set; }
        /// <summary>
        /// 影片简介
        /// </summary>
        public string intro { get; set; }
        /// <summary>
        /// 上映类型
        /// </summary>
        public string versionTypes { get; set; }
        /// <summary>
        /// 影片语言
        /// </summary>
        public string language { get; set; }
        /// <summary>
        /// 影片类型
        /// </summary>
        public string filmTypes { get; set; }
        /// <summary>
        /// 海报URL地址
        /// </summary>
        public string pic { get; set; }
        /// <summary>
        /// 剧情照URL地址
        /// </summary>
        public List<string> imgs { get; set; }
        /// <summary>
        /// 想看人数
        /// </summary>
        public int like { get; set; }
        /// <summary>
        /// 导演、演员名字头像
        /// </summary>
        public ResultActorsDirector actors { get; set; }
        /// <summary>
        /// 专资办影片统一编码11位(12位编码去除第四位)
        /// </summary>
        public string filmCode { get; set; }
    }
    #endregion

    /// <summary>
    /// 结果
    /// </summary>
    public class ResultBase
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
    }
    #endregion

    #region 请求
    /// <summary>
    /// 请求数据基础
    /// </summary>
    public class PostBase
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

    /// <summary>
    /// 获取城市请求
    /// </summary>
    public class PostQueryCitys : PostBase
    {
    }

    /// <summary>
    /// 获取热门电影请求
    /// </summary>
    public class PostHotShowingMovies : PostBase
    {
    }

    /// <summary>
    /// 获取影院请求
    /// </summary>
    public class PostQueryCinemas : PostBase
    {
        /// <summary>
        /// 城市ID
        /// </summary>
        public string cityId { get; set; }
    }

    /// <summary>
    /// 获取场次请求
    /// </summary>
    public class PostQueryShows : PostBase
    {
        /// <summary>
        /// 影院ID
        /// </summary>
        public string cinemaId { get; set; }
    }
    #endregion
}
