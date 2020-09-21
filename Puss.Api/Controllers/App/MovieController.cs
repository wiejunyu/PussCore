using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Aop;
using Puss.Api.Manager;
using Puss.Api.Manager.MovieManager;
using Puss.Application.Common;
using Puss.Data.Enum;
using Puss.Data.Models;
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

        /// <summary>
        /// 电影
        /// </summary>
        public MovieController(IMovieManager MovieManager)
        {
            this.MovieManager = MovieManager;
        }

        /// <summary>
        /// 请求返回数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> QueryCitys()
        {
            List<ResultQueryCitysDataList> list = await MovieManager.QueryCitys();
            return new ReturnResult<List<ResultQueryCitysDataList>>(ReturnResultStatus.Succeed, list);
        }
    }
}