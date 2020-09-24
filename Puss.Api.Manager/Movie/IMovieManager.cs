using Puss.Enties;
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
        Task<List<ResultCitys>> QueryCitys();

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
        Task<List<ResultFilm>> HotShowingMovies();


        /// <summary>
        /// 开始更新
        /// </summary>
        /// <returns></returns>
        Task StartUpdate();

        /// <summary>
        /// 开始更新城市和影院
        /// </summary>
        /// <returns></returns>
        Task StartUpdateCitysAndCinemas();

        /// <summary>
        /// 开始更新场次
        /// </summary>
        /// <param name="index">第几页</param>
        /// <param name="size">每页大小</param>
        /// <param name="count">总数</param>
        /// <param name="lMovieShows">所有影院场次</param>
        /// <returns></returns>
        Task StartUpdateShows(int index, int size,int count, List<Movie_Shows> lMovieShows);

        /// <summary>
        /// 开始更新热映影片
        /// </summary>
        /// <returns></returns>
        Task StartUpdateHotShowingMovies();
    }
}
