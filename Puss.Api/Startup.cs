using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using App.Metrics;
using AspNetCoreRateLimit;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Puss.Api.Filters;
using Puss.Api.Job;
using Puss.BusinessCore;
using Puss.Data.Config;
using Puss.Redis;

namespace Puss.Api
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Startup
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            #region 配置文件
            //修改配置文件路径
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("config/appsettings.json", optional: true, reloadOnChange: true)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)//增加环境配置文件，新建项目默认有
                .AddEnvironmentVariables();
            configuration = builder.Build();
            this.Configuration = builder.Build();
            //保存配置文件
            Configuration = configuration;
            GlobalsConfig.SetBaseConfig(Configuration, env.ContentRootPath, env.WebRootPath);
            #endregion
        }

        /// <summary>
        /// Configuration   
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// ConfigureServices
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region RateLimit
            // needed to load configuration from appsettings.json
            services.AddOptions();
            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();
            //load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));
            //load ip rules from appsettings.json
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));
            // inject counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            // https://github.com/aspnet/Hosting/issues/793
            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            #endregion

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Puss API",
                    Version = "v1",
                    Description = "这是一个基于.NetCore的WebApi",
                    License = new OpenApiLicense()
                    {
                        Name = "Puss"
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "请输入你的Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                if (GlobalsConfig.Configuration[ConfigurationKeys.Swagger_IsXml].ToLower() == "true")
                {
                    c.IncludeXmlComments(Path.Combine(basePath, "Puss.Api.xml"));
                    c.IncludeXmlComments(Path.Combine(basePath, "Puss.Data.xml"));
                }
            });
            #endregion

            #region JWT身份验证
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Permission", policy => policy.Requirements.Add(new PolicyRequirement()));
            //});
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//是否验证Issuer
                        ValidateAudience = true,//是否验证Audience
                        ValidateLifetime = true,//是否验证失效时间
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//是否验证SecurityKey
                        ValidAudience = GlobalsConfig.Configuration[ConfigurationKeys.Token_Audience],//Audience
                        ValidIssuer = GlobalsConfig.Configuration[ConfigurationKeys.Token_Issuer],//Issuer，这两项和前面签发jwt的设置一致
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalsConfig.Configuration[ConfigurationKeys.Token_SecurityKey]))//拿到SecurityKey
                    };
                });
            //services.AddSingleton<IAuthorizationHandler, PolicyHandler>();
            #endregion

            #region Redis
            //redis缓存
            string _connectionString = GlobalsConfig.Configuration[ConfigurationKeys.Redis_Connection];//连接字符串
            RedisService.SetCon(_connectionString);
            #endregion

            #region IP
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            services.AddControllers();

            #region Metrics监控配置
            string IsOpen = GlobalsConfig.Configuration[ConfigurationKeys.InfluxDB_IsOpen].ToLower();
            if (IsOpen == "true")
            {
                string database = GlobalsConfig.Configuration[ConfigurationKeys.InfluxDB_DataBase];
                string InfluxDBConStr = GlobalsConfig.Configuration[ConfigurationKeys.InfluxDB_Connection];
                string app = GlobalsConfig.Configuration[ConfigurationKeys.InfluxDB_App];
                string env = GlobalsConfig.Configuration[ConfigurationKeys.InfluxDB_Env];
                string username = GlobalsConfig.Configuration[ConfigurationKeys.InfluxDB_UserName];
                string password = GlobalsConfig.Configuration[ConfigurationKeys.InfluxDB_PassWord];

                var uri = new Uri(InfluxDBConStr);

                var metrics = AppMetrics.CreateDefaultBuilder()
                .Configuration.Configure(
                options =>
                {
                    options.AddAppTag(app);
                    options.AddEnvTag(env);
                })
                .Report.ToInfluxDb(
                options =>
                {
                    options.InfluxDb.BaseUri = uri;
                    options.InfluxDb.Database = database;
                    options.InfluxDb.UserName = username;
                    options.InfluxDb.Password = password;
                    options.HttpPolicy.BackoffPeriod = TimeSpan.FromSeconds(30);
                    options.HttpPolicy.FailuresBeforeBackoff = 5;
                    options.HttpPolicy.Timeout = TimeSpan.FromSeconds(10);
                    options.FlushInterval = TimeSpan.FromSeconds(5);
                })
                .Build();

                services.AddMetrics(metrics);
                services.AddMetricsReportingHostedService();
                services.AddMetricsTrackingMiddleware();
                services.AddMetricsEndpoints();
            }
            #endregion

            #region 定时计划
            services.AddHostedService<PriceJobTrigger>();
            //services.AddHostedService<LogJobTrigger>();
            #endregion

            services.AddMvc(options =>
            {
                //全局注册log4net的异常捕获
                options.Filters.Add<HttpGlobalExceptionFilter>();
                //身份验证
                options.Filters.Add<RequestAuthorizeAttribute>();
                //记录结果日志中间件
                options.Filters.Add<WebApiResultMiddleware>();

            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #region 跨域
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                //AllowAnyOrigin:允许任何来源
                //AllowAnyMethod:允许跨域策略允许所有的方法,如Get、Post
                //AllowAnyHeader:允许任何的Header头部标题 有关头部标题如果不设置就不会进行限制
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            }));
            #endregion

            #region Autofac
            services.AddControllersWithViews().AddControllersAsServices();
            #endregion

            #region Hangfire
            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(GlobalsConfig.Configuration[ConfigurationKeys.Sql_HangfireConnectionString], new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();
            #endregion

            #region Workflow
            services.AddLogging();
            services.AddWorkflow();
            #endregion

            #region 保存系统依赖注入服务
            AutofacUtil.SetSysService(services);
            #endregion
        }

        /// <summary>
        /// Configure
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            #region Log4Net
            loggerFactory.AddLog4Net("config/Log4Net.config");
            #endregion

            #region JWT身份验证，必须在UseAuthorization之前，否者会返回401
            app.UseAuthentication();
            #endregion

            app.UseAuthorization();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            #endregion

            #region 注入Metrics
            string IsOpen = GlobalsConfig.Configuration[ConfigurationKeys.InfluxDB_IsOpen].ToLower();
            if (IsOpen == "true")
            {
                app.UseMetricsAllMiddleware();
                // Or to cherry-pick the tracking of interest
                app.UseMetricsActiveRequestMiddleware();
                app.UseMetricsErrorTrackingMiddleware();
                app.UseMetricsPostAndPutSizeTrackingMiddleware();
                app.UseMetricsRequestTrackingMiddleware();
                app.UseMetricsOAuth2TrackingMiddleware();
                app.UseMetricsApdexTrackingMiddleware();

                app.UseMetricsAllEndpoints();
                // Or to cherry-pick endpoint of interest
                app.UseMetricsEndpoint();
                app.UseMetricsTextEndpoint();
                app.UseEnvInfoEndpoint();
            }
            #endregion

            #region 记录接口执行时间中间件
            app.UseCalculateExecutionTime();
            #endregion

            #region GB2312
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            #endregion

            #region 跨域
            // 允许所有跨域，cors是在ConfigureServices方法中配置的跨域策略名称
            app.UseCors("CorsPolicy");
            #endregion

            #region Hangfire
            app.UseStaticFiles();
            app.UseHangfireDashboard("/hangfire", new DashboardOptions()
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });
            #endregion

            #region 保存Autofac依赖注入服务
            AutofacUtil.AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region IIS启动进入swagger页面
            app.Run(x =>
            {
                x.Response.Redirect("/swagger/"); //可以支持虚拟路径或者index.html这类起始页.
                return Task.FromResult(0);
            });
            #endregion
        }

        /// <summary>
        /// ConfigureContainer
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // 在这里添加服务注册
            builder.RegisterTypes(Assembly.Load("Puss.Reptile").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Application").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Email").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.QrCode").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            //builder.RegisterTypes(Assembly.Load("Puss.RabbitMQ").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Redis").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Reptile").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Log").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.BusinessCore").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Encrypt").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Api.Manager").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Api.Filters").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Api.Job").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterType<DbContext>();

            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }
    }
}
