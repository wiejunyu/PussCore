using AutoMapper;
using Hangfire;
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
using System;
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
        private readonly IMovie_CinemasManager Movie_CinemasManager;
        private readonly DbContext _dbContext; 

        /// <summary>
        /// 电影
        /// </summary>
        public MovieController(IMovieManager MovieManager, IMovie_CityManager Movie_CityManager, IMovie_CinemasManager Movie_CinemasManager, DbContext _dbContext)
        {
            this.MovieManager = MovieManager;
            this.Movie_CityManager = Movie_CityManager;
            this.Movie_CinemasManager = Movie_CinemasManager;
            this._dbContext = _dbContext;
        }

        /// <summary>
        /// 返回当前影院场次
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<ReturnResult> Test()
        {
            return new ReturnResult<List<Movie_Film>>(ReturnResultStatus.Succeed,await MovieManager.StartUpdateHotShowingMovies());
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
            await MovieManager.StartUpdateCinemas();
#else
            BackgroundJob.Enqueue(() => MovieManager.StartUpdateCinemas());
#endif
            return new ReturnResult(ReturnResultStatus.Succeed);
        }
    }
}