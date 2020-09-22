using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Aop;
using Puss.Api.Manager;
using Puss.Api.Manager.MovieManager;
using Puss.Application.Common;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Enties;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 电影
    /// </summary>
    public class MovieController : AppBaseController
    {
        private readonly IMovieManager MovieManager;
        private readonly IMovie_CityManager Movie_CityManager;

        /// <summary>
        /// 电影
        /// </summary>
        public MovieController(IMovieManager MovieManager, IMovie_CityManager Movie_CityManager)
        {
            this.MovieManager = MovieManager;
            this.Movie_CityManager = Movie_CityManager;
        }

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> QueryCitys()
        {
            List<ResultQueryCitysDataList> lResultCitys = await MovieManager.QueryCitys();
            List<Movie_City> lResultMovieCity = lResultCitys.MapToList<ResultQueryCitysDataList, Movie_City>();
            //判断当前数据库是否已存在
            List<Movie_City> lMovieCity = Movie_CityManager.GetList();
            List<Movie_City> list = lMovieCity.Where(x => !lResultMovieCity.Any(p => p.cityId == x.cityId)).ToList();
            if (!list.Any()) return new ReturnResult(ReturnResultStatus.Succeed);
            return ReturnResult.ResultCalculation(() => Movie_CityManager.Insert(list));
        }

        /// <summary>
        /// 获取当前热映影片
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ReturnResult> Movies(int cityId)
        {
            List<ResultHotFilmList> list = await MovieManager.QueryCinemas(cityId);
            return new ReturnResult<List<ResultHotFilmList>>(ReturnResultStatus.Succeed, list);
        }
    }
}