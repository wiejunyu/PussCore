using AspectInjector.Broker;
using Puss.Data.Models;
using Puss.Redis;
using System;
using System.Reflection;
using Sugar.Enties;
using System.Linq;
using System.Collections.Generic;

namespace Puss.Api.Aop
{
    /// <summary>
    /// 是否输入用户名验证切面
    /// </summary>
    [Aspect(Scope.Global)]
    [Injection(typeof(UserVerification))]
    public class UserVerification : Attribute
    {
        /// <summary>
        /// 是否输入用户名验证切面
        /// </summary>
        /// <param name="arguments">参数数组</param>
        [Advice(Kind.Before)]
        public void UserVerificationEnter(
        [Argument(Source.Arguments)] object[] arguments)
        {
            Type t = arguments[0].GetType();
            List<PropertyInfo> lPropertyInfo = t.GetProperties().ToList();
            //取得UserName
            var p = lPropertyInfo.FirstOrDefault(x => x.Name == "UserName");
            string UserName = p == null ? "" : ((p.GetValue(arguments[0], null)) ?? "").ToString();
            //取得PassWord
            p = lPropertyInfo.FirstOrDefault(x => x.Name == "PassWord");
            string PassWord = p == null ? "" : ((p.GetValue(arguments[0], null)) ?? "").ToString();

            if (string.IsNullOrWhiteSpace(UserName)) throw new AppException("用户名不能为空");
            if (string.IsNullOrWhiteSpace(PassWord)) throw new AppException("密码不能为空");
        }
    }
}
