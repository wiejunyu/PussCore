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
    /// 验证码验证切面
    /// </summary>
    [Aspect(Scope.Global)]
    [Injection(typeof(CodeVerification))]
    public class CodeVerification : Attribute
    {
        /// <summary>
        /// 验证码是否传入验证
        /// </summary>
        /// <param name="name">调用的函数名称</param>
        /// <param name="arguments">参数数组</param>
        [Advice(Kind.Before)]
        public void CodeVerificationEnter(
        [Argument(Source.Name)] string name,
        [Argument(Source.Arguments)] object[] arguments)
        {
            Type t = arguments[0].GetType();
            List<PropertyInfo> lPropertyInfo = t.GetProperties().ToList();
            //取得Code
            var p = lPropertyInfo.FirstOrDefault(x => x.Name == "Code");
            string Code = p != null ? p.GetValue(arguments[0], null).ToString() : "";
            //取得CodeKey
            p = lPropertyInfo.FirstOrDefault(x => x.Name == "CodeKey");
            string CodeKey = p != null ? p.GetValue(arguments[0], null).ToString() : "";
            //取得Email
            p = lPropertyInfo.FirstOrDefault(x => x.Name == "Email");
            string Email = p != null ? p.GetValue(arguments[0], null).ToString() : "";
            //取得Phone
            p = lPropertyInfo.FirstOrDefault(x => x.Name == "Phone");
            string Phone = p != null ? p.GetValue(arguments[0], null).ToString() : "";

            //foreach (PropertyInfo pi in t.GetProperties())
            //{
            //    //获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作 
            //    string sTypeName = pi.Name;
            //    //获得值
            //    object value = pi.GetValue(arguments[0], null);//用pi.GetValue获得值
            //    switch (sTypeName) 
            //    {
            //        case "Code":
            //            {
            //                Code = value.ToString();
            //            }
            //            break;

            //        case "CodeKey":
            //            {
            //                CodeKey = value.ToString();
            //            }
            //            break;
            //        case "Email":
            //            {
            //                Email = value.ToString();
            //            }
            //            break;
            //        case "Email":
            //            {
            //                Email = value.ToString();
            //            }
            //            break;
            //        default: break;
            //    }
            //}
            if (string.IsNullOrWhiteSpace(CodeKey)) throw new AppException("验证码Key不能为空");
            if (RedisHelper.Exists(CommentConfig.ImageCacheCode + CodeKey))
            {
                if (string.IsNullOrWhiteSpace(Code)) throw new AppException("验证码不能为空");
                string code = RedisHelper.Get<string>(CommentConfig.ImageCacheCode + CodeKey);
                if (string.IsNullOrWhiteSpace(code)) throw new AppException("验证码错误或已过期");
                if (code.ToLower() != Code.ToLower()) throw new AppException("验证码错误或已过期");
            }
            else if (RedisHelper.Exists(CommentConfig.MailCacheCode + CodeKey))
            {
                if (string.IsNullOrWhiteSpace(Email)) throw new AppException("邮箱不能为空");
                Code code = RedisHelper.Get<Code>(CommentConfig.MailCacheCode + CodeKey);
                if (code == null) throw new AppException("验证码错误或已过期");
                if (string.IsNullOrWhiteSpace(code.email) || string.IsNullOrWhiteSpace(code.code)) throw new AppException("验证码错误或已过期");
                if (code.email.ToLower() != Email.ToLower()) throw new AppException("验证码错误或已过期");
                if (code.code.ToLower() != Code.ToLower()) throw new AppException("验证码错误或已过期");
            }
            else throw new AppException("验证码错误或已过期");
        }
    }
}
