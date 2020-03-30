using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Filters
{
    /// <summary>
    /// 权限承载实体
    /// </summary>
    public class PolicyRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 用户权限集合
        /// </summary>
        public List<UserPermission> UserPermissions { get; private set; }
        /// <summary>
        /// 构造
        /// </summary>
        public PolicyRequirement()
        {
            //用户有权限访问的路由配置,当然可以从数据库获取
            UserPermissions = new List<UserPermission> {
                #region Home
                new UserPermission {  Url="/Api/Home/AdminLogin",IsLogin = true,IsAdmin = true,UserName="puss"},
                new UserPermission {  Url="/Api/Home/Login",IsLogin = true,IsAdmin = false},
                new UserPermission {  Url="/Api/Home/NotLogin", UserName="",IsLogin = false,IsAdmin = false},
                #endregion
                #region User
                new UserPermission {  Url="/Api/User/ShowValidateCode",IsLogin = false,IsAdmin = false},
                new UserPermission {  Url="/Api/User/EmaliGetCode", IsLogin = false,IsAdmin = false},
                new UserPermission {  Url="/Api/User/UserRegister", IsLogin = false,IsAdmin = false},
                new UserPermission {  Url="/Api/User/Login", IsLogin = false,IsAdmin = true},
                new UserPermission {  Url="/Api/User/NoLoginError", IsLogin = false,IsAdmin = false},
                #endregion
                #region QrCode
                new UserPermission {  Url="/Api/QrCode/GetQrCode", IsLogin = false,IsAdmin = false},
                #endregion
            };
        }
    }

    /// <summary>
    /// 用户权限承载实体
    /// </summary>
    public class UserPermission
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 是否需要登录
        /// </summary>
        public bool IsLogin { get; set; }
        /// <summary>
        /// 是否需要管理员权限
        /// </summary>
        public bool IsAdmin { get; set; }
    }
}
