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
using Microsoft.Extensions.Logging;
using Puss.Redis;
using SqlSugar;
using System.Text.RegularExpressions;

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
        private readonly ILogger<MovieManager> Logger;
        private readonly IRedisService RedisService;

        public MovieManager(
            DbContext DbContext,
            IMovie_CityManager Movie_CityManager,
            IMovie_CinemasManager Movie_CinemasManager,
            IMovie_ShowsManager Movie_ShowsManager,
            IMovie_FilmManager Movie_FilmManager,
            IMovie_MemberManager Movie_MemberManager,
            ILogger<MovieManager> Logger,
            IRedisService RedisService)
        {
            this.DbContext = DbContext;
            this.Movie_CinemasManager = Movie_CinemasManager;
            this.Movie_CityManager = Movie_CityManager;
            this.Movie_ShowsManager = Movie_ShowsManager;
            this.Movie_FilmManager = Movie_FilmManager;
            this.Movie_MemberManager = Movie_MemberManager;
            this.Logger = Logger;
            this.RedisService = RedisService;
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
        public async Task<List<ResultCitys>> QueryCitys()
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
/*            StartUpdateCitysAndCinemas();
            StartUpdateShows();*/
#endif
        }

        /// <summary>
        /// 开始更新城市和影院
        /// </summary>
        /// <returns></returns>
        public async Task StartUpdateCitysAndCinemas()
        {
            //日志收集
            Logger.LogInformation($"[Url]:StartUpdateCitysAndCinemas(开始更新城市和影院)[Time]:{DateTime.Now}");
            List<ResultCitys> lResultCitys = new List<ResultCitys>();
            List<Movie_City> lResultMovieCity = new List<Movie_City>();
            try
            {
                lResultCitys = await QueryCitys();
                lResultMovieCity = lResultCitys.MapToList<ResultCitys, Movie_City>();
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

            ////记录信息
            //UpdateCount updateCount = new UpdateCount
            //{
            //    count = lMovieCity.Count,
            //    name = "",
            //    status = (int)UpdateCountStatus.Start,
            //    current = 0
            //};
            //await RedisService.SetAsync(CommentConfig.MovieManager_UpdateCinemas, updateCount);
            foreach (var temp in lMovieCity)
            {
                //记录信息
                //updateCount.name = temp.cityName;
                //updateCount.current++;
                //await RedisService.SetAsync(CommentConfig.MovieManager_UpdateCinemas, updateCount);

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

            //记录信息
            //updateCount.name = "";
            //updateCount.status = (int)UpdateCountStatus.Save;
            //await RedisService.SetAsync(CommentConfig.MovieManager_UpdateCinemas, updateCount);

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

            //记录信息
            //updateCount.name = "";
            //updateCount.status = (int)UpdateCountStatus.Success;
            //await RedisService.SetAsync(CommentConfig.MovieManager_UpdateCinemas, updateCount);
            Logger.LogInformation($"[Url]:EndUpdateCitysAndCinemas(结束更新城市和影院)[Time]:{DateTime.Now}");
        }

        /// <summary>
        /// 开始更新场次
        /// </summary>
        /// <param name="index">第几页</param>
        /// <param name="size">每页大小</param>
        /// <param name="count">总数</param>
        /// <param name="lMovieShows">所有影院场次</param>
        /// <returns></returns>
        public async Task StartUpdateShows(int index, int size, int count, List<Movie_Shows> lMovieShows)
        {
            string redisKey = $"{CommentConfig.MovieManager_UpdateShows}{index}";

            //日志收集
            Logger.LogInformation($"[Url]:StartUpdateShows(开始更新场次)[Index]:{index}[Time]:{DateTime.Now}");
            Movie_ShowsManager.Delete(x => x.createTime < DateTime.Now.AddDays(-7));
            var lMovieCinemas = await Movie_CinemasManager.GetPageListAsync(x => true, new PageModel
            {
                PageIndex = index,
                PageSize = size,
                PageCount = count
            });

            //记录信息
            //UpdateCount updateCount = new UpdateCount
            //{
            //    count = lMovieCinemas.Count,
            //    name = "",
            //    status = (int)UpdateCountStatus.Start,
            //    current = 0
            //};
            await RedisService.SetAsync(redisKey, count);

            try
            {
                //最终需要提交的影院场次
                List<Movie_Shows> lInsertMovieShows = new List<Movie_Shows>();
                //最终需要删除的影院场次
                List<Movie_Shows> lDeleteMovieShows = new List<Movie_Shows>();

                //查询出所有场次
                List<ResultShows> lResultMovieShows = new List<ResultShows>();
                foreach (var temp in lMovieCinemas)
                {
                    //记录信息
                    //updateCount.name = temp.cityName;
                    //updateCount.current++;
                    //await RedisService.SetAsync(redisKey, updateCount);
                    try
                    {
                        lResultMovieShows.AddRange(await QueryShows(temp.cinemaId.ToString()));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError($"[Url]:ErrorPostUpdateShows(更新场次请求错误)[Index]:{index}[Id]:{temp.cinemaId}");
                    }
                }
                if (lResultMovieShows.Any()) 
                {
                    Logger.LogInformation($"cao:{index}");
                }
                //返回当前没有的场次列表
                lInsertMovieShows = lResultMovieShows.Where(x => !lMovieShows.Any(p => p.showId == int.Parse(x.showId)) && Regex.IsMatch(x.showId, @"^[+-]?\d*[.]?\d*$")).MapToList<ResultShows, Movie_Shows>();
                //写入影院ID
                lInsertMovieShows.ForEach(x =>
                {
                    x.createTime = DateTime.Now;
                });
                lInsertMovieShows.AddRange(lInsertMovieShows);

                //返回当前需要删除的影院列表
                lDeleteMovieShows = lMovieShows.Where(x => !lResultMovieShows.Any(p => int.Parse(p.showId) == x.showId)).ToList();
                //记录信息
                //updateCount.name = "";
                //updateCount.status = (int)UpdateCountStatus.Save;
                //await RedisService.SetAsync(redisKey, updateCount);

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
                });

                //记录信息
                //updateCount.name = "";
                //updateCount.status = (int)UpdateCountStatus.Success;
                //await RedisService.SetAsync(redisKey, updateCount);
                Logger.LogInformation($"[Url]:EndUpdateShows(结束更新城市和影院)[Index]:{index}[Time]:{DateTime.Now}");
            }
            catch (Exception ex)
            {
                JsonSerializerSettings settings = new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
                Logger.LogError($"[Url]:ErrorUpdateShows(更新场次错误)[Exception]:{JsonConvert.SerializeObject(ex, settings)}");
                //记录信息
                //updateCount.name = "";
                //updateCount.status = (int)UpdateCountStatus.Error;
                //await RedisService.SetAsync(redisKey, updateCount);

            }
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
