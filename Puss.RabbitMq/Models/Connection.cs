﻿using Puss.Data.Config;

namespace Puss.RabbitMq
{
    public class Connection
    {
        /// <summary>
        /// Rabbitmq服务IP，不包含端口,一般为localhost
        /// </summary>
        public static string RabbitMQ_HostName
        {
            get
            {
                return GlobalsConfig.Configuration[ConfigurationKeys.MQ_HostName] ?? string.Empty;
            }
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public static string RabbitMQ_UserName
        {
            get
            {
                return GlobalsConfig.Configuration[ConfigurationKeys.MQ_UserName] ?? string.Empty;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public static string RabbitMQ_PassWord
        {
            get
            {
                return GlobalsConfig.Configuration[ConfigurationKeys.MQ_PassWord] ?? string.Empty;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public static string RabbitMQ_VirtualHost
        {
            get
            {
                return GlobalsConfig.Configuration[ConfigurationKeys.MQ_VirtualHost] ?? string.Empty;
            }
        }
    }
}
