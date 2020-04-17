﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models
{
    /// <summary>
    /// 队列Key
    /// </summary>
    public class QueueKey
    {
        /// <summary>
        /// 发送注册成功邮件
        /// </summary>
        public static string SendRegisterMessageIsEmail
        {
            get { return "SendRegisterMessageIsEmail"; }
        }

        /// <summary>
        /// 报错日志收集
        /// </summary>
        public static string LogError
        {
            get { return "LogError"; }
        }

        /// <summary>
        /// 接口耗时日志收集
        /// </summary>
        public static string LogTime
        {
            get { return "LogTime"; }
        }

        /// <summary>
        /// 定时计划日志收集
        /// </summary>
        public static string LogJob
        {
            get { return "LogJob"; }
        }
    }
}
