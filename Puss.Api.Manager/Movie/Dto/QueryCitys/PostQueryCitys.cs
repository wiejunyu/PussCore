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

    #region 获取热门
    /// <summary>
    /// 人员信息
    /// </summary>
    public class ResultHotUserDesc
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
    public class ResultHotUser
    {
        /// <summary>
        /// 导演
        /// </summary>
        public List<ResultHotUserDesc> director { get; set; }
        /// <summary>
        /// 演员
        /// </summary>
        public List<ResultHotUserDesc> actors { get; set; }
    }

    /// <summary>
    /// 影片列表
    /// </summary>
    public class ResultHotFilmList
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
        public ResultHotUser actors { get; set; }
        /// <summary>
        /// 专资办影片统一编码11位(12位编码去除第四位)
        /// </summary>
        public string filmCode { get; set; }
    }

    /// <summary>
    /// 结果数据
    /// </summary>
    public class ResultHotResultData
    {
        /// <summary>
        /// 列表
        /// </summary>
        public List<ResultHotFilmList> filmList { get; set; }
    }

    /// <summary>
    /// 结果
    /// </summary>
    public class ResultHotResult
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
        /// 返回结果
        /// </summary>
        public ResultHotResultData result { get; set; }
    }
    #endregion
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
    /// 请求数据
    /// </summary>
    public class PostQueryCitys : PostBase
    {
    }

    /// <summary>
    /// 请求数据
    /// </summary>
    public class PostHotShowingMovies : PostBase
    {
    }

    /// <summary>
    /// 请求数据
    /// </summary>
    public class PostQueryCinemas : PostBase
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string cityId { get; set; }
    }
    #endregion

    #region Test
    //如果好用，请收藏地址，帮忙分享。
    public class MoviesItem
    {
        /// <summary>
        /// 详情
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// ID
        /// </summary>
        public string MovieId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 封面
        /// </summary>
        public string PosterUrl { get; set; }
        /// <summary>
        /// 上映方式：2D/IMAX 2D/中国巨幕2D/CINITY 2D/杜比影院 2D
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 是否预先展览
        /// </summary>
        public string IsPreShow { get; set; }
        /// <summary>
        /// 是否全球发布
        /// </summary>
        public string IsGlobalReleased { get; set; }
        /// <summary>
        /// 评分
        /// </summary>
        public double Score { get; set; }
        /// <summary>
        /// 心愿
        /// </summary>
        public int Wish { get; set; }
        /// <summary>
        /// 演员
        /// </summary>
        public string Star { get; set; }
        /// <summary>
        /// 推出日期
        /// </summary>
        public string ReleaseTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ShowInfo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ComingTitle { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ShowSt { get; set; }
        /// <summary>
        /// 栏目
        /// </summary>
        public string Category { get; set; }
    }

    public class TestRoot
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MoviesItem> Movies { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Count { get; set; }
    }
    #endregion
}
