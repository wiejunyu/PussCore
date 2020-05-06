using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using App.Metrics;
using Autofac;
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
            Configuration = configuration;
            //�����ļ�
            GlobalsConfig.SetBaseConfig(Configuration, env.ContentRootPath, env.WebRootPath);
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
            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Puss API",
                    Version = "v1",
                    Description = "����һ������.NetCore��WebApi",
                    License = new OpenApiLicense()
                    {
                        Name = "Puss"
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "���������Token",
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

            #region JWT�����֤
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Permission", policy => policy.Requirements.Add(new PolicyRequirement()));
            //});
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,//�Ƿ���֤Issuer
                        ValidateAudience = true,//�Ƿ���֤Audience
                        ValidateLifetime = true,//�Ƿ���֤ʧЧʱ��
                        ClockSkew = TimeSpan.FromSeconds(30),
                        ValidateIssuerSigningKey = true,//�Ƿ���֤SecurityKey
                        ValidAudience = GlobalsConfig.Configuration[ConfigurationKeys.Token_Audience],//Audience
                        ValidIssuer = GlobalsConfig.Configuration[ConfigurationKeys.Token_Issuer],//Issuer���������ǰ��ǩ��jwt������һ��
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(GlobalsConfig.Configuration[ConfigurationKeys.Token_SecurityKey]))//�õ�SecurityKey
                    };
                });
            //services.AddSingleton<IAuthorizationHandler, PolicyHandler>();
            #endregion

            #region Redis
            //redis����
            string _connectionString = GlobalsConfig.Configuration[ConfigurationKeys.Redis_Connection];//�����ַ���
            RedisService.SetCon(_connectionString);
            #endregion

            #region IP
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            #endregion

            services.AddControllers();

            #region Metrics�������
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

            #region ��ʱ�ƻ�
            services.AddHostedService<PriceJobTrigger>();
            //services.AddHostedService<LogJobTrigger>();
            #endregion

            services.AddMvc(options =>
            {
                //ȫ��ע��log4net���쳣����
                options.Filters.Add<HttpGlobalExceptionFilter>();
                //�����֤
                options.Filters.Add<RequestAuthorizeAttribute>();
                
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            #region ����
            services.AddCors(options => options.AddPolicy("CorsPolicy",
            builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                //.SetIsOriginAllowed(_ => true)
                //.AllowAnyOrigin()
                //.AllowAnyHeader()
                //.AllowCredentials();
            }));
            #endregion

            #region Autofac
            services.AddControllersWithViews().AddControllersAsServices();
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
            loggerFactory.AddLog4Net();
            #endregion

            #region JWT�����֤��������UseAuthorization֮ǰ�����߻᷵��401
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

            #region ע��Metrics
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

            #region ��¼�ӿ�ִ��ʱ���м��
            app.UseCalculateExecutionTime();
            #endregion

            #region GB2312
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            #endregion

            #region ����
            // �������п���cors����ConfigureServices���������õĿ����������
            app.UseCors("CorsPolicy");
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        /// <summary>
        /// ConfigureContainer
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // ��������ӷ���ע��
            builder.RegisterTypes(Assembly.Load("Puss.Reptile").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Application").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Email").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.QrCode").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.RabbitMQ").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Redis").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Reptile").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();
            builder.RegisterTypes(Assembly.Load("Puss.Log").GetTypes()).AsImplementedInterfaces().PropertiesAutowired();

            var controllerBaseType = typeof(ControllerBase);
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(t => controllerBaseType.IsAssignableFrom(t) && t != controllerBaseType)
                .PropertiesAutowired();
        }
    }
}
