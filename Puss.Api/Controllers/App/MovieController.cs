using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Manager.MovieManager;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Enties;
using System.Collections.Generic;
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
        private readonly IMovie_CinemasManager Movie_CinemasManager;
        private readonly DbContext DbContext;

        /// <summary>
        /// 电影
        /// </summary>
        public MovieController(IMovieManager MovieManager, IMovie_CityManager Movie_CityManager, IMovie_CinemasManager Movie_CinemasManager, DbContext DbContext)
        {
            this.MovieManager = MovieManager;
            this.Movie_CityManager = Movie_CityManager;
            this.Movie_CinemasManager = Movie_CinemasManager;
            this.DbContext = DbContext;
        }

        /// <summary>
        /// 返回当前影院场次
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ReturnResult> Test()
        {
            return new ReturnResult<List<ResultShows>>(ReturnResultStatus.Succeed, await MovieManager.QueryShows("334"));
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ReturnResult> Start()
        {
#if DEBUG
            int count = DbContext.Db.Queryable<Movie_Cinemas>().Count(x => true);
            //查出所有影院场次
            List<Movie_Shows> lMovieShows = await DbContext.Db.Queryable<Movie_Shows>().ToListAsync();
            int page = 10;
            int size = count / page;
            if (count % page > 0)
                page++;
            for (int index = 0; index <= page; index++)
            {
                BackgroundJob.Enqueue(() => MovieManager.StartUpdateShows(index, size, count, lMovieShows)); 
            }
#else
            //int count = DbContext.Db.Queryable<Movie_Cinemas>().Count(x => true);
            //int size = count / 10;
            //for (int i = 0; i <= 10; i++) 
            //{
            //    BackgroundJob.Enqueue(() => MovieManager.StartUpdateShows(i, size));
            //}
#endif
            return new ReturnResult(ReturnResultStatus.Succeed);
        }
    }
}