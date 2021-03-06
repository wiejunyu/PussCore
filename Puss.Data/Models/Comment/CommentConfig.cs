﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models
{
    /// <summary>
    /// 配置字符串类
    /// </summary>
    public class CommentConfig
    {
        #region 验证码
        /// <summary>
        /// 图片验证码RedisKey
        /// </summary>
        public static string ImageCacheCode { get { return "ImagesCacheCode:"; } }

        /// <summary>
        /// 邮箱验证码RedisKey
        /// </summary>
        public static string MailCacheCode { get { return "MailCacheCode:"; } }
        #endregion

        #region 用户Token
        /// <summary>
        /// 用户Token RedisKey
        /// </summary>
        public static string UserToken { get { return "UserToken:"; } }
        #endregion

        #region 价格
        /// <summary>
        /// 价格 RedisKey
        /// </summary>
        public static string Price { get { return "Price:"; } }
        #endregion

        #region MDM
        /// <summary>
        /// MDM Token
        /// </summary>
        public static string MDM_Token { get { return "MDM:Token"; } }
        #endregion

        #region 电影
        /// <summary>
        /// 更新影院
        /// </summary>
        public static string MovieManager_UpdateCinemas { get { return "MovieManager:UpdateCinemas"; } }
        /// <summary>
        /// 更新场次
        /// </summary>
        public static string MovieManager_UpdateShows { get { return "MovieManager:UpdateShows:"; } }
        #endregion
    }
}
