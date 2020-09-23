﻿using Puss.Enties;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Puss.Api.Manager.MovieManager
{
    /// <summary>
    /// 验证码
    /// </summary>
    public interface IMovieManager
    {
        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        Task<List<ResultQueryCitysDataList>> QueryCitys();

        /// <summary>
        /// 返回当前地区影院列表
        /// </summary>
        /// <returns></returns>
        Task<List<ResultCinemasList>> QueryCinemas(string cityId);

        /// <summary>
        /// 返回当前影院场次
        /// </summary>
        /// <returns></returns>
        Task<List<ResultShows>> QueryShows(string cinemaId);

        /// <summary>
        /// 获取当前热映影片
        /// </summary>
        /// <returns></returns>
        Task<List<ResultHotFilmList>> HotShowingMovies();

        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        Task Start();
    }
}
