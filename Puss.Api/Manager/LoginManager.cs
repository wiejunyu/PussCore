using AutoMapper;
using Puss.Api.Filters;
using Puss.Application.Common;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.RabbitMq;
using Puss.RabbitMQ;
using Puss.Redis;
using Sugar.Enties;
using System;
using System.Collections.Generic;

namespace Puss.Api.Manager
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginManager
    {
        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="request">登陆模型</param>
        /// <returns></returns>
        public static string Login(LoginRequest request)
        {
            request.PassWord = MD5.Md5(request.PassWord);
            User account = new UserManager().GetSingle(a => (a.UserName == request.UserName.Trim() || a.Phone == request.UserName.Trim() || a.Email == request.UserName.Trim()) && a.PassWord == request.PassWord);
            if (account == null) throw new AppException("账号密码错误");
            return Token.UserGetToken(account);
        }

        /// <summary>
        /// 生成验证码图片并返回图片字节
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        public static byte[] ShowValidateCode(string CodeKey)
        {
            if (string.IsNullOrWhiteSpace(CodeKey)) throw new AppException("验证码缓存标记错误");

            ValidateCode ValidateCode = new ValidateCode();
            //生成验证码，传几就是几位验证码
            string code = ValidateCode.CreateValidateCode(4);
            //保存验证码
            RedisHelper.Set(CommentConfig.ImageCacheCode + CodeKey, code, 10);
            //把验证码转成字节
            byte[] buffer = ValidateCode.CreateValidateGraphic(code);
            return buffer;
        }

        /// <summary>
        /// 生成验证码图片并返回图片Base64
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        public static string ShowValidateCodeBase64(string CodeKey)
        {
            if (string.IsNullOrWhiteSpace(CodeKey)) throw new AppException("验证码缓存标记错误");

            ValidateCode ValidateCode = new ValidateCode();
            //生成验证码，传几就是几位验证码
            string code = ValidateCode.CreateValidateCode(4);
            //保存验证码
            RedisHelper.Set(CommentConfig.ImageCacheCode + CodeKey, code, 10);
            //把验证码转成字节
            byte[] buffer = ValidateCode.CreateValidateGraphic(code);
            //把验证码转成Base64
            return $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
        }


        /// <summary>
        /// EmailGetCode
        /// </summary>
        /// <param name="CodeKey"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public static string EmailGetCode(string CodeKey, string Email)
        {
            LoginManager.DelCode();

            DateTime dt = DateTime.Now.Date;

            Code cModel = new CodeManager().GetSingle(x => x.email == Email && x.time > dt && x.type == (int)CodeType.Email);
            //判断验证码是否超出次数
            if (cModel != null ? cModel.count >= 6 : false) throw new AppException("发送超出次数");
            //获得验证码
            string code = new ValidateCode().CreateValidateCode(6);//生成验证码，传几就是几位验证码
                                                                   //发送邮件
            Cms_Sysconfig sys = new Cms_SysconfigManager().GetSingle(x => x.Id == 1);
            if (!EmailHelper.MailSending(Email, "宇宙物流验证码", $"您在宇宙物流的验证码是:{code},10分钟内有效", sys.Mail_From, sys.Mail_Code, sys.Mail_Host)) throw new AppException("发送失败");
            #region 保存验证码统计
            if (cModel == null)
            {
                cModel = new Code()
                {
                    type = (int)CodeType.Email,
                    email = Email,
                    time = DateTime.Now,
                    count = 1
                };
                new CodeManager().Insert(cModel);
            }
            else
            {
                cModel.count++;
            }
            #endregion
            cModel.code = code;
            //保存验证码进缓存
            RedisHelper.Set(CommentConfig.MailCacheCode + CodeKey, cModel, 10);
            return "验证码发送成功";
        }

        /// <summary>
        /// 删除冗余验证码
        /// </summary>
        public static void DelCode()
        {
            DateTime dt = DateTime.Now.Date;
            if (new CodeManager().IsAny(x => x.time < dt))
            {
                List<Code> list = new CodeManager().GetList(x => x.time < dt);
                new DbContext().Db.Ado.UseTran(() =>
                {
                    list.ForEach(x => new CodeManager().Delete(x));
                });
            }
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request">request</param>
        /// <param name="ip">ip</param>
        /// <returns></returns>
        public static bool UserRegister(RegisterRequest request, string ip)
        {
            #region 验证
            if (new UserManager().IsAny(x => x.UserName == request.UserName)) throw new AppException("该用户名已经注册过");
            if (new UserManager().IsAny(x => x.Email == request.Email)) throw new AppException("该邮箱已经注册过");
            if (string.IsNullOrWhiteSpace(request.PassWord)) throw new AppException("请输入密码");
            if (string.IsNullOrWhiteSpace(request.ConfirmPassWord)) throw new AppException("请输入确认密码");
            if (string.IsNullOrWhiteSpace(request.ConfirmPassWord)) throw new AppException("请输入确认密码");
            if (request.PassWord != request.ConfirmPassWord) throw new AppException("密码和确认密码不一致");
            #endregion

            var mapper = new MapperConfiguration(x => x.CreateMap<RegisterRequest, User>()).CreateMapper();
            User user = mapper.Map<User>(request);
            user.PassWord = MD5.Md5(user.PassWord).ToLower();
            user.CreateTime = DateTime.Now;
            user.LoginTime = DateTime.Now;
            user.IP = ip;
            user.Portrait = "/Image/user.png";
            user.Money = 0;

            var result = new UserManager().Db.Ado.UseTran(() =>
            {
                new UserManager().Insert(user);
                new UserDetailsManager().Insert(new UserDetails() { UID = user.ID });
            });
            RabbitMQPushHelper.PushMessage(RabbitMQKey.SendRegisterMessageIsEmail, request.Email);
            return true;
        }
    }
}
