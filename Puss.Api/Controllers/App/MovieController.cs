using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Aop;
using Puss.Api.Manager;
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
        /// <summary>
        /// 电影
        /// </summary>
        public MovieController()
        {
        }

        ///// <summary>
        ///// 请求返回数据
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost]
        //[AllowAnonymous]
        //public async Task<ReturnResult> Post()
        //{
        //    Dictionary<string, string> dic = new Dictionary<string, string>();
        //    dic.Add("cityId", "2");
        //    dic.Add("channelId", "20001");
        //    dic.Add("cityId", "2");
        //    dic.Add("cityId", "2");
        //    dic.Add("cityId", "2");
        //    HttpPostHelper.getParamSrc("");
        //    return new ReturnResult(ReturnResultStatus.Succeed);
        //}
    }
}