using Puss.Application.Common;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Puss.Data.Models;
using System.Linq;
using Puss.BusinessCore;
using Puss.Enties;

namespace Puss.Api.Manager.MovieManager
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class MovieManager : IMovieManager
    {
        private readonly IMovie_CityManager Movie_CityManager;
        private readonly IMovie_CinemasManager Movie_CinemasManager;
        private readonly DbContext DbContext;

        public MovieManager(DbContext DbContext, IMovie_CityManager Movie_CityManager, IMovie_CinemasManager Movie_CinemasManager)
        {
            this.Movie_CinemasManager = Movie_CinemasManager;
            this.Movie_CityManager = Movie_CityManager;
            this.DbContext = DbContext;
        }

        /// <summary>
        /// channelId
        /// </summary>
        public static string channelId = "12341389";

        /// <summary>
        /// secret
        /// </summary>
        public static string secret = "f79e39604981906b97b1e54b36fbb108";

        /// <summary>
        /// url
        /// </summary>
        public static string http = "http://dev.imanm.com/";

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResultQueryCitysDataList>> QueryCitys()
        {
            return await Task.Run(() =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("channelId", channelId);
                string sign = MD5.Md5(HttpPostHelper.GetParamSrc(dic) + secret).ToLowerInvariant();
                PostQueryCitys temp = new PostQueryCitys();
                temp.channelId = channelId;
                temp.sign = sign;
                string url = $"{http}manman/index.php/open/partner/queryCitys";
                string result = HttpPostHelper.DoHttpPost(url, JsonConvert.SerializeObject(temp));
                ResultQueryCitys resultData = JsonConvert.DeserializeObject<ResultQueryCitys>(result);
                if (resultData.code == 0)
                {
                    return resultData.result.cityList;
                }
                else throw new AppException(resultData.errorMsg);
            });
        }

        /// <summary>
        /// 返回当前地区影院列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResultCinemasList>> QueryCinemas(string cityId)
        {
            return await Task.Run(() =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("channelId", channelId);
                dic.Add("cityId", cityId);
                string sign = HttpPostHelper.GetParamSrc(dic) + secret;
                sign = MD5.Md5(sign).ToLowerInvariant();
                PostQueryCinemas temp = new PostQueryCinemas();
                temp.channelId = channelId;
                temp.cityId = cityId;
                temp.sign = sign;
                string url = $"{http}manman/index.php/open/partner/queryCinemas";
                string result = HttpPostHelper.DoHttpPost(url, JsonConvert.SerializeObject(temp));
                ResultQueryCinemas resultData = JsonConvert.DeserializeObject<ResultQueryCinemas>(result);
                if (resultData.code == 0)
                {
                    return resultData.result.cinemasList;
                }
                else throw new AppException(resultData.errorMsg);
            });
        }

        /// <summary>
        /// 返回当前影院场次
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResultShows>> QueryShows(string cinemaId)
        {
            return await Task.Run(() =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("channelId", channelId);
                dic.Add("cinemaId", cinemaId);
                string sign = HttpPostHelper.GetParamSrc(dic) + secret;
                sign = MD5.Md5(sign).ToLowerInvariant();
                PostQueryShows temp = new PostQueryShows();
                temp.channelId = channelId;
                temp.cinemaId = cinemaId;
                temp.sign = sign;
                string url = $"{http}manman/index.php/open/partner/queryShows";
                string result = HttpPostHelper.DoHttpPost(url, JsonConvert.SerializeObject(temp));
                ResultQueryShows resultData = JsonConvert.DeserializeObject<ResultQueryShows>(result);
                if (resultData.code == 0)
                {
                    return resultData.result.showList;
                }
                else throw new AppException(resultData.errorMsg);
            });
        }

        /// <summary>
        /// 获取当前热映影片
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResultHotFilmList>> HotShowingMovies()
        {
            return await Task.Run(() =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("channelId", channelId);
                string sign = MD5.Md5(HttpPostHelper.GetParamSrc(dic) + secret).ToLowerInvariant();
                PostHotShowingMovies temp = new PostHotShowingMovies();
                temp.channelId = channelId;
                temp.sign = sign;
                string url = $"{http}/manman/index.php/open/partner/hotShowingMovies";
                string result = HttpPostHelper.DoHttpPost(url, JsonConvert.SerializeObject(temp));
                ResultHotResult resultData = JsonConvert.DeserializeObject<ResultHotResult>(result);
                if (resultData.code == 0)
                {
                    return resultData.result.filmList;
                }
                else throw new AppException(resultData.message);
            });
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        public async Task Start()
        {
            while (true)
            {
                List<ResultQueryCitysDataList> lResultCitys = await QueryCitys();
                List<Movie_City> lResultMovieCity = lResultCitys.MapToList<ResultQueryCitysDataList, Movie_City>();
                //返回当前没有的城市列表
                List<Movie_City> lMovieCity = await Movie_CityManager.GetListAsync();
                List<Movie_City> lInsertMovieCity = lResultMovieCity.Where(x => !lMovieCity.Any(p => p.cityId == x.cityId)).ToList();
                //总城市列表
                lMovieCity.AddRange(lInsertMovieCity);

                //查出所有城市的影院
                List<Movie_Cinemas> lMovieCinemas = await Movie_CinemasManager.GetListAsync();
                //最终需要提交的影院列表
                List<Movie_Cinemas> lInsertMovieCinemas = new List<Movie_Cinemas>();
                //最终需要删除的影院列表
                List<Movie_Cinemas> lDeleteMovieCinemas = new List<Movie_Cinemas>();

                foreach (var temp in lMovieCity)
                {
                    List<ResultCinemasList> lResultCinemas = await QueryCinemas(temp.cityId.ToString());
                    List<Movie_Cinemas> lResultMovieCinemas = lResultCinemas.MapToList<ResultCinemasList, Movie_Cinemas>();
                    //返回当前没有的影院列表
                    var lInsertTemp = lResultMovieCinemas.Where(x => !lMovieCinemas.Any(p => p.cinemaId == x.cinemaId)).ToList();
                    //写入城市ID
                    lInsertTemp.ForEach(x => x.cityId = temp.cityId);
                    lInsertMovieCinemas.AddRange(lInsertTemp);

                    //返回当前需要删除的影院列表
                    var lDeleteTemp = lMovieCinemas.Where(x => x.cityId == temp.cityId && !lResultMovieCinemas.Any(p => p.cinemaId == x.cinemaId)).ToList();
                    lDeleteMovieCinemas.AddRange(lDeleteTemp);
                }
                //总影院列表
                lMovieCinemas.AddRange(lInsertMovieCinemas);

                ////查出所有影院场次
                //List<Movie_Shows> lMovieShows = await Movie_ShowsManager.GetListAsync();
                ////最终需要提交的影院场次
                //List<Movie_Shows> lInsertMovieShows = new List<Movie_Shows>();
                ////最终需要删除的影院场次
                //List<Movie_Shows> lDeleteMovieShows = new List<Movie_Shows>();
                //foreach (var temp in lMovieCinemas)
                //{

                //}

                //数据库操作
                DbContext.Db.Ado.UseTran(() =>
                {
                    //提交数据
                    if (lInsertMovieCity.Any())
                    {
                        Movie_CityManager.Insert(lInsertMovieCity);
                    }
                    if (lInsertMovieCinemas.Any())
                    {
                        Movie_CinemasManager.Insert(lInsertMovieCinemas);
                    }
                        //删除数据
                        if (lDeleteMovieCinemas.Any())
                    {
                        Movie_CinemasManager.Delete(lDeleteMovieCinemas.Select(x => x.cinemaId));
                    }
                });
            }
        }
    }
}
