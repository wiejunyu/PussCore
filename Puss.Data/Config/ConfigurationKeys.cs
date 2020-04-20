﻿using System;
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
        /// 从数据库连接字符串
        /// </summary>
        public static string Sql_ConnectionSon = "SqlConnection:ConnectionStringSon";
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

        #region InfluxDB
        /// <summary>
        /// IsOpen
        /// </summary>
        public static string InfluxDB_IsOpen = "InfluxDB:IsOpen";
        /// <summary>
        /// DataBaseName
        /// </summary>
        public static string InfluxDB_DataBase = "InfluxDB:DataBase";
        /// <summary>
        /// Connection
        /// </summary>
        public static string InfluxDB_Connection = "InfluxDB:Connection";
        /// <summary>
        /// UserName
        /// </summary>
        public static string InfluxDB_UserName = "InfluxDB:UserName";
        /// <summary>
        /// PassWord
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
        /// IsXml
        /// </summary>
        public static string Swagger_IsXml = "Swagger:IsXml";
        #endregion

        #region Permission
        /// <summary>
        /// IsOpen
        /// </summary>
        public static string Permission_IsOpen = "Permission:IsOpen";
        #endregion
    }
}
