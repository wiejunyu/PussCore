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
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public static string Sql_Connection = "SqlConnection:ConnectionString";

        #region Token
        /// <summary>
        /// TokenSecurityKey
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
    }
}
