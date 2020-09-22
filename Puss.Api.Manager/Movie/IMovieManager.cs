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
        Task<List<ResultQueryCitysDataList>> QueryCitys();

        /// <summary>
        /// 返回当前地区影院列表
        /// </summary>
        /// <returns></returns>
        Task<List<ResultCinemasList>> QueryCinemas(string cityId);
    }
}
