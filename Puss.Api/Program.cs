using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Puss.Api
{
    /// <summary>
    /// Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 开始函数
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// CreateHostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel((context, options) =>
                    {
                        //设置应用服务器Kestrel请求体最大为50MB
                        options.Limits.MaxRequestBodySize = 52428800;
                    });
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:5000");
                })
            //Autofac
            .UseServiceProviderFactory(new AutofacServiceProviderFactory());
    }
}
