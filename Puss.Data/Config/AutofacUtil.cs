using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Config
{
    /// <summary>
    /// Autofac依赖注入服务
    /// </summary>
    public class AutofacUtil
    {
        #region Autofac
        /// <summary>
        /// Autofac依赖注入静态服务
        /// </summary>
        public static ILifetimeScope AutofacContainer { get; set; }

        /// <summary>
        /// Autofac获取服务(Single)
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetAutofacService<T>() where T : class
        {
            return AutofacContainer.Resolve<T>();
        }

        /// <summary>
        /// Autofac获取服务(请求生命周期内)
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetAutofacScopeService<T>() where T : class
        {
            return (T)GetAutofacService<IHttpContextAccessor>().HttpContext.RequestServices.GetService(typeof(T));
        }
        #endregion

        #region Sys
        /// <summary>
        /// 系统依赖注入静态服务
        /// </summary>
        public static ServiceProvider SysContainer { get; set; }

        /// <summary>
        /// 系统写入服务
        /// </summary>
        /// <param name="services"></param>
        public static void SetSysService(IServiceCollection services)
        {
            SysContainer = services.BuildServiceProvider();
        }

        /// <summary>
        /// 系统获取服务
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns></returns>
        public static T GetSysService<T>() where T : class
        {
            return SysContainer.GetService<T>();
        }
        #endregion
    }
}
