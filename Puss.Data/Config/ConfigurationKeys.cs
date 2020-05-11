using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Config
{
    /// <summary>
    /// 配置字典
    /// </summary>
    public class ConfigurationKeys
    {
        #region Sql
        /// <summary>
        /// 主数据库连接字符串
        /// </summary>
        public static string Sql_Connection = "SqlConnection:ConnectionString";
        /// <summary>
        /// 从数据库连接字符串json
        /// </summary>
        public static string Sql_ConnectionSon = "SqlConnection:ConnectionStringSon";
        /// <summary>
        /// Hangfire数据库连接字符串
        /// </summary>
        public static string Sql_HangfireConnectionString = "SqlConnection:HangfireConnectionString";
        #endregion

        #region Verification(验证统一管理)
        /// <summary>
        /// 是否开启验证码验证
        /// </summary>
        public static string Verification_Code = "Verification:Code";
        /// <summary>
        /// 是否开启Token验证
        /// </summary>
        public static string Verification_Token = "Verification:Token";
        #endregion

        #region Token
        /// <summary>
        /// token密匙
        /// </summary>
        public static string Token_SecurityKey = "Token:SecurityKey";
        /// <summary>
        /// TokenIssuer
        /// </summary>
        public static string Token_Issuer = "Token:Issuer";
        /// <summary>
        /// TokenAudience
        /// </summary>
        public static string Token_Audience = "Token:Audience";
        /// <summary>
        /// 是否单点登录
        /// </summary>
        public static string Token_IsSignIn = "Token:IsSignIn";
        /// <summary>
        /// token保存时长(分钟)
        /// </summary>
        public static string Token_Time = "Token:Time";
        #endregion

        #region MQ
        /// <summary>
        /// MQ主机地址
        /// </summary>
        public static string MQ_HostName = "MQ:HostName";
        /// <summary>
        /// MQ用户名
        /// </summary>
        public static string MQ_UserName = "MQ:UserName";
        /// <summary>
        /// MQ密码
        /// </summary>
        public static string MQ_PassWord = "MQ:PassWord";
        #endregion

        #region Redis
        /// <summary>
        /// Redis连接字符串
        /// </summary>
        public static string Redis_Connection = "Redis:Connection";
        /// <summary>
        /// Redis实例名称
        /// </summary>
        public static string Redis_InstanceName = "Redis:InstanceName";
        /// <summary>
        /// Redis默认数据库
        /// </summary>
        public static string Redis_DefaultDB = "Redis:DefaultDB";
        #endregion

        #region InfluxDB(健康检查)
        /// <summary>
        /// 是否开启健康检查
        /// </summary>
        public static string InfluxDB_IsOpen = "InfluxDB:IsOpen";
        /// <summary>
        /// 健康检查数据库
        /// </summary>
        public static string InfluxDB_DataBase = "InfluxDB:DataBase";
        /// <summary>
        /// 健康检查数据库地址
        /// </summary>
        public static string InfluxDB_Connection = "InfluxDB:Connection";
        /// <summary>
        /// 健康检查数据库名称
        /// </summary>
        public static string InfluxDB_UserName = "InfluxDB:UserName";
        /// <summary>
        /// 健康检查数据库密码
        /// </summary>
        public static string InfluxDB_PassWord = "InfluxDB:PassWord";
        /// <summary>
        /// App
        /// </summary>
        public static string InfluxDB_App = "InfluxDB:App";
        /// <summary>
        /// Env
        /// </summary>
        public static string InfluxDB_Env = "InfluxDB:Env";
        #endregion

        #region Swagger
        /// <summary>
        /// 是否生成swaggerXML文件
        /// </summary>
        public static string Swagger_IsXml = "Swagger:IsXml";
        #endregion

        #region MDM
        /// <summary>
        /// Realm
        /// </summary>
        public static string MDM_Realm = "MDM:Realm";
        /// <summary>
        /// ConsumerKey
        /// </summary>
        public static string MDM_ConsumerKey = "MDM:ConsumerKey";
        /// <summary>
        /// AccessToken
        /// </summary>
        public static string MDM_AccessToken = "MDM:AccessToken";
        /// <summary>
        /// OauthSignatureMethod
        /// </summary>
        public static string MDM_OauthSignatureMethod = "MDM:OauthSignatureMethod";
        /// <summary>
        /// ConsumerSecret
        /// </summary>
        public static string MDM_ConsumerSecret = "MDM:ConsumerSecret";
        /// <summary>
        /// AccessSecret
        /// </summary>
        public static string MDM_AccessSecret = "MDM:AccessSecret";
        /// <summary>
        /// DepEnrollmentUrl
        /// </summary>
        public static string MDM_DepEnrollmentUrl = "MDM:DepEnrollmentUrl";
        /// <summary>
        /// DepAnchorCertsUrl
        /// </summary>
        public static string MDM_DepAnchorCertsUrl = "MDM:DepAnchorCertsUrl";
        /// <summary>
        /// TrustProfileUrl
        /// </summary>
        public static string MDM_TrustProfileUrl = "MDM:TrustProfileUrl";
        /// <summary>
        /// Certificate
        /// </summary>
        public static string MDM_Certificate = "MDM:Certificate";
        #endregion

        #region Hangfire
        /// <summary>
        /// 是否开启Hangfire作业
        /// </summary>
        public static string Hangfire_IsOpen = "Hangfire:IsOpen";
        #endregion
    }
}
