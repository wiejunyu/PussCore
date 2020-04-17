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
#if DEBUG
#else
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

            if (string.IsNullOrWhiteSpace(CodeKey)) throw new AppException("验证码Key不能为空");
            if (new RedisService().Exists(CommentConfig.ImageCacheCode + CodeKey))
            {
                if (string.IsNullOrWhiteSpace(Code)) throw new AppException("验证码不能为空");
                string code = new RedisService().Get<string>(CommentConfig.ImageCacheCode + CodeKey);
                if (string.IsNullOrWhiteSpace(code)) throw new AppException("验证码错误或已过期");
                if (code.ToLower() != Code.ToLower()) throw new AppException("验证码错误或已过期");
            }
            else if (new RedisService().Exists(CommentConfig.MailCacheCode + CodeKey))
            {
                if (string.IsNullOrWhiteSpace(Email)) throw new AppException("邮箱不能为空");
                Code code = new RedisService().Get<Code>(CommentConfig.MailCacheCode + CodeKey);
                if (code == null) throw new AppException("验证码错误或已过期");
                if (string.IsNullOrWhiteSpace(code.email) || string.IsNullOrWhiteSpace(code.code)) throw new AppException("验证码错误或已过期");
                if (code.email.ToLower() != Email.ToLower()) throw new AppException("验证码错误或已过期");
                if (code.code.ToLower() != Code.ToLower()) throw new AppException("验证码错误或已过期");
            }
            else throw new AppException("验证码错误或已过期");
#endif
        }
    }
}
