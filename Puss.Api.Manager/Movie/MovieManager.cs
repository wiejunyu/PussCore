using Puss.Application.Common;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Puss.Data.Models;
using System.Linq;
using Puss.BusinessCore;
using Puss.Enties;
using System;
using Puss.Data.Enum;

namespace Puss.Api.Manager.MovieManager
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class MovieManager : IMovieManager
    {
        private readonly IMovie_CityManager Movie_CityManager;
        private readonly IMovie_CinemasManager Movie_CinemasManager;
        private readonly IMovie_ShowsManager Movie_ShowsManager;
        private readonly IMovie_FilmManager Movie_FilmManager;
        private readonly IMovie_MemberManager Movie_MemberManager;
        private readonly DbContext DbContext;

        public MovieManager(
            DbContext DbContext, 
            IMovie_CityManager Movie_CityManager, 
            IMovie_CinemasManager Movie_CinemasManager, 
            IMovie_ShowsManager Movie_ShowsManager, 
            IMovie_FilmManager Movie_FilmManager,
            IMovie_MemberManager Movie_MemberManager)
        {
            this.DbContext = DbContext;
            this.Movie_CinemasManager = Movie_CinemasManager;
            this.Movie_CityManager = Movie_CityManager;
            this.Movie_ShowsManager = Movie_ShowsManager;
            this.Movie_FilmManager = Movie_FilmManager;
            this.Movie_MemberManager = Movie_MemberManager;
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
        public async Task<List<ResultFilm>> HotShowingMovies()
        {
            return await Task.Run(() =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("channelId", channelId);
                string sign = MD5.Md5(HttpPostHelper.GetParamSrc(dic) + secret).ToLowerInvariant();
                PostHotShowingMovies temp = new PostHotShowingMovies();
                temp.channelId = channelId;
                temp.sign = sign;
                string url = $"{http}manman/index.php/open/partner/hotShowingMovies";
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
        /// 开始更新
        /// </summary>
        /// <returns></returns>
        public async Task StartUpdate()
        {
#if DEBUG
#else
            StartUpdateCitysAndCinemas();
            StartUpdateCinemas();
#endif
        }

        /// <summary>
        /// 开始更新城市和影院
        /// </summary>
        /// <returns></returns>
        public async Task StartUpdateCitysAndCinemas()
        {
            #if DEBUG
#else
            List<ResultQueryCitysDataList> lResultCitys = new List<ResultQueryCitysDataList>();
            List<Movie_City> lResultMovieCity = new List<Movie_City>();
            try
            {
                lResultCitys = await QueryCitys();
                lResultMovieCity = lResultCitys.MapToList<ResultQueryCitysDataList, Movie_City>();
            }
            catch (Exception ex)
            {
                return;
            }
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
                List<ResultCinemasList> lResultCinemas = new List<ResultCinemasList>();
                List<Movie_Cinemas> lResultMovieCinemas = new List<Movie_Cinemas>();
                try
                {
                    lResultCinemas = await QueryCinemas(temp.cityId.ToString());
                    lResultMovieCinemas = lResultCinemas.MapToList<ResultCinemasList, Movie_Cinemas>();
                }
                catch (Exception ex)
                {
                    continue;
                }
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
#endif
        }

        /// <summary>
        /// 开始更新场次
        /// </summary>
        /// <returns></returns>
        public async Task StartUpdateCinemas()
        {
#if DEBUG
#else
            while (true)
            {
                var lMovieCinemas = await Movie_CinemasManager.GetListAsync();
                //查出所有影院场次
                List<Movie_Shows> lMovieShows = await Movie_ShowsManager.GetListAsync();
                //最终需要提交的影院场次
                List<Movie_Shows> lInsertMovieShows = new List<Movie_Shows>();
                //最终需要删除的影院场次
                List<Movie_Shows> lDeleteMovieShows = new List<Movie_Shows>();
                foreach (var temp in lMovieCinemas)
                {
                    List<ResultShows> lResultShows = new List<ResultShows>();
                    List<Movie_Shows> lResultMovieShows = new List<Movie_Shows>();
                    try
                    {
                        lResultShows = await QueryShows(temp.cinemaId.ToString());
                        lResultMovieShows = lResultShows.MapToList<ResultShows, Movie_Shows>();
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }

                    //返回当前没有的场次列表
                    var lInsertTemp = lResultMovieShows.Where(x => !lMovieShows.Any(p => p.showId == x.showId)).ToList();
                    //写入影院ID
                    lInsertTemp.ForEach(x =>
                    {
                        x.cinemaId = temp.cinemaId;
                        x.createTime = DateTime.Now;
                    });
                    lInsertMovieShows.AddRange(lInsertTemp);

                    //返回当前需要删除的影院列表
                    var lDeleteTemp = lMovieShows.Where(x => x.cinemaId == temp.cinemaId && !lResultMovieShows.Any(p => p.showId == x.showId)).ToList();
                    lDeleteMovieShows.AddRange(lDeleteTemp);
                }

                //数据库操作
                DbContext.Db.Ado.UseTran(() =>
                {
                //提交数据
                if (lInsertMovieShows.Any())
                    {
                        Movie_ShowsManager.Insert(lInsertMovieShows);
                    }
                //删除数据
                if (lDeleteMovieShows.Any())
                    {
                        Movie_ShowsManager.Delete(lDeleteMovieShows.Select(x => x.showId));
                    }
                    DateTime now = DateTime.Now.Date;
                    Movie_ShowsManager.Delete(x => x.createTime < now);
                });
            }
#endif
        }

        /// <summary>
        /// 开始更新热映影片
        /// </summary>
        /// <returns></returns>
        public async Task StartUpdateHotShowingMovies()
        {
            /*List<ResultFilm> lResultMovieCinemas = await HotShowingMovies();
            //电影列表
            List<Movie_Film> lResultMovieFilm = lResultMovieCinemas.MapToList<ResultFilm, Movie_Film>();
            //成员列表
            List<Movie_Member> lResultMovieMember = new List<Movie_Member>();
            lResultMovieCinemas.ForEach(x =>
            {
                //导演列表
                lResultMovieMember.AddRange(x.actors.director.Select(p => new Movie_Member
                {
                    sc_name = p.sc_name,
                    en_name = p.en_name,
                    act_name = p.act_name,
                    avatar = p.avatar,
                    type = (int)MovieMember.Director,
                    filmId = x.filmId,
                }));
                //演员列表
                lResultMovieMember.AddRange(x.actors.actors.Select(p => new Movie_Member
                {
                    sc_name = p.sc_name,
                    en_name = p.en_name,
                    act_name = p.act_name,
                    avatar = p.avatar,
                    type = (int)MovieMember.Actors,
                    filmId = x.filmId,
                }));
            });

            Movie_FilmManager.GetListAsync(x => x.c);

            DbContext.Db.Ado.UseTran(() =>
            {
                Movie_FilmManager.Insert(lMovieFilm);
                Movie_MemberManager.Insert(lMovieMember);
            });*/
        }
    }
}
