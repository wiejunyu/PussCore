using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Puss.Data.Config
{
    /// <summary>
    /// 配置类
    /// </summary>
    public class GlobalsConfig
    {
        /// <summary>
        /// 配置服务
        /// </summary>
        public static IServiceProvider Services { get; set; }

        /// <summary>
        /// 配置文件
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 程序路径
        /// </summary>
        public static string ContentRootPath { get; set; }

        /// <summary>
        /// 网站路径
        /// </summary>
        public static string WebRootPath { get; set; }

        /// <summary>
        /// 写入配置
        /// </summary>
        /// <param name="config">配置文件</param>
        /// <param name="contentRootPath">程序路径</param>
        /// <param name="webRootPath">网站路径</param>
        public static void SetBaseConfig(IConfiguration config, string contentRootPath, string webRootPath)
        {
            Configuration = config;
            ContentRootPath = contentRootPath;
            WebRootPath = webRootPath;
        }
    }
}
