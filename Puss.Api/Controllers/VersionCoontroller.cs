using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Controllers;
using Puss.Data.Enum;
using Puss.Data.Models;
using System.Threading.Tasks;


namespace BasicStore.Api.Controllers
{
    /// <summary>
    /// 版本测试
    /// </summary>
    [ApiVersion("1.0", Deprecated = true)]
    public class VersionCoontroller : ApiBaseController
    {
        /// <summary>
        /// 版本测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> Test()
        {
            return await Task.Run(() =>
            {
                return new ReturnResult(ReturnResultStatus.Succeed, "v1");
            });
        }

        /// <summary>
        /// 版本测试1
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> Test1()
        {
            return await Task.Run(() =>
            {
                return new ReturnResult(ReturnResultStatus.Succeed, "v1");
            });
        }
    }
}

namespace BasicStore.Api.Controllers.v2
{
    /// <summary>
    /// 版本测试
    /// </summary>
    [ApiVersion("2.0", Deprecated = true)]
    public class VersionCoontroller : ApiBaseController
    {
        /// <summary>
        /// 版本测试
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> Test()
        {
            return await Task.Run(() =>
            {
                return new ReturnResult(ReturnResultStatus.Succeed, "v2");
            });
        }
    }
}

namespace BasicStore.Api.Controllers.v3
{
    /// <summary>
    /// 版本测试
    /// </summary>
    [ApiVersion("3.0")]
    public class VersionCoontroller : ApiBaseController
    {
        /// <summary>
        /// 版本测试 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> Test()
        {
            return await Task.Run(() =>
            {
                return new ReturnResult(ReturnResultStatus.Succeed, "v3");
            });
        }

        /// <summary>
        /// 版本测试1
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> Test1()
        {
            return await Task.Run(() =>
            {
                return new ReturnResult(ReturnResultStatus.Succeed, "v3");
            });
        }
    }
}