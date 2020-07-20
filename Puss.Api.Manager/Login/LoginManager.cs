using AutoMapper;
using Microsoft.AspNetCore.Http;
using Puss.Api.Filters;
using Puss.Application.Common;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Email;
using Puss.Redis;
using Puss.Enties;
using System;
using System.Threading.Tasks;
using Puss.Data.Config;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Puss.Log;

namespace Puss.Api.Manager
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginManager : ILoginManager
    {
        private readonly IHttpContextAccessor Accessor;
        private readonly IRedisService RedisService;
        private readonly IUserManager UserManager;
        private readonly IEmailService EmailService;
        private readonly ICodeManager CodeManager;
        private readonly ICms_SysconfigManager Cms_SysconfigManager;
        private readonly DbContext DbContext;
        private readonly IUserDetailsManager UserDetailsManager;
        private readonly ITokenService TokenService;
        private readonly ILogService LogService;

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="Accessor"></param>
        /// <param name="RedisService"></param>
        /// <param name="UserManager"></param>
        /// <param name="EmailService"></param>
        /// <param name="CodeManager"></param>
        /// <param name="Cms_SysconfigManager"></param>
        /// <param name="DbContext"></param>
        /// <param name="UserDetailsManager"></param>
        /// <param name="TokenService"></param>
        /// <param name="LogService"></param>
        public LoginManager(IHttpContextAccessor Accessor,
            IRedisService RedisService,
            IUserManager UserManager,
            IEmailService EmailService,
            ICodeManager CodeManager,
            ICms_SysconfigManager Cms_SysconfigManager,
            DbContext DbContext,
            IUserDetailsManager UserDetailsManager,
            ITokenService TokenService,
            ILogService LogService)
        {
            this.Accessor = Accessor;
            this.RedisService = RedisService;
            this.UserManager = UserManager;
            this.EmailService = EmailService;
            this.CodeManager = CodeManager;
            this.Cms_SysconfigManager = Cms_SysconfigManager;
            this.DbContext = DbContext;
            this.UserDetailsManager = UserDetailsManager;
            this.TokenService = TokenService;
            this.LogService = LogService;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="request">登陆模型</param>
        /// <returns></returns>
        public async Task<string> Login(LoginRequest request)
        {
            request.PassWord = MD5.Md5(request.PassWord);
            User account = await UserManager.GetSingleAsync(a => (a.UserName == request.UserName.Trim() || a.Phone == request.UserName.Trim() || a.Email == request.UserName.Trim()) && a.PassWord == request.PassWord);
            if (account == null) throw new AppException("账号密码错误");
            return TokenService.UserGetToken(account);
        }

        /// <summary>
        /// 登出接口
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoginOut()
        {
            return await Task.Run(() => { return TokenService.RemoveToken(); });
        }

        /// <summary>
        /// 生成验证码图片并返回图片字节
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        public async Task<byte[]> ShowValidateCode(string CodeKey)
        {
            if (string.IsNullOrWhiteSpace(CodeKey)) throw new AppException("验证码缓存标记错误");

            ValidateCode ValidateCode = new ValidateCode();
            //生成验证码，传几就是几位验证码
            string code = ValidateCode.CreateValidateCode(4);
            //保存验证码
            await RedisService.SetAsync(CommentConfig.ImageCacheCode + CodeKey, code, 10);
            //把验证码转成字节
            byte[] buffer = ValidateCode.CreateValidateGraphic(code);
            return buffer;
        }

        /// <summary>
        /// 生成验证码图片并返回图片Base64
        /// </summary>
        /// <param name="CodeKey">验证码缓存标记</param>
        /// <returns></returns>
        public async Task<string> ShowValidateCodeBase64(string CodeKey)
        {
            if (string.IsNullOrWhiteSpace(CodeKey)) throw new AppException("验证码缓存标记错误");

            ValidateCode ValidateCode = new ValidateCode();
            //生成验证码，传几就是几位验证码
            string code = ValidateCode.CreateValidateCode(4);
            //保存验证码
            await RedisService.SetAsync(CommentConfig.ImageCacheCode + CodeKey, code, 10);
            //把验证码转成字节
            byte[] buffer = ValidateCode.CreateValidateGraphic(code);
            //把验证码转成Base64
            return $"data:image/png;base64,{Convert.ToBase64String(buffer)}";
        }


        /// <summary>
        /// 邮箱获取验证码
        /// </summary>
        /// <param name="CodeKey">验证码</param>
        /// <param name="Email">邮箱</param>
        /// <returns></returns>
        public async Task<string> EmailGetCode(string CodeKey, string Email)
        {
            //读写分离强制走主库
            DbContext.Db.Ado.IsDisableMasterSlaveSeparation = true;

            DateTime dt = DateTime.Now.Date;
            //批量删除今天之前的验证码
            var t5 = DbContext.Db.Deleteable<Code>().Where(x => x.time < dt).ExecuteCommand();
            Code cModel = await CodeManager.GetSingleAsync(x => x.email == Email && x.time > dt && x.type == (int)CodeType.Email);
            //判断验证码是否超出次数
            if (cModel != null ? cModel.count >= 6 : false) throw new AppException("发送超出次数");
            //获得验证码
            string code = new ValidateCode().CreateValidateCode(6);//生成验证码，传几就是几位验证码
            //发送邮件
            Cms_Sysconfig sys = await Cms_SysconfigManager.GetSingleAsync(x => x.Id == 1);
            if (!EmailService.MailSending(Email, "宇宙物流验证码", $"您在宇宙物流的验证码是:{code},10分钟内有效", sys.Mail_From, sys.Mail_Code, sys.Mail_Host)) throw new AppException("发送失败");
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
                CodeManager.Insert(cModel);
            }
            else
            {
                cModel.count++;
            }
            #endregion
            cModel.code = code;
            //保存验证码进缓存
            await RedisService.SetAsync(CommentConfig.MailCacheCode + CodeKey, cModel, 10);

            //读写分离取消强制走主库
            DbContext.Db.Ado.IsDisableMasterSlaveSeparation = false;
            return "验证码发送成功";
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="request">注册模型</param>
        /// <returns></returns>
        public async Task<bool> UserRegister(RegisterRequest request)
        {
            #region 验证
#if DEBUG
#else
            if (UserManager.IsAny(x => x.UserName == request.UserName)) throw new AppException("该用户名已经注册过");
            if (string.IsNullOrWhiteSpace(request.Email)) throw new AppException("请输入邮箱");
            if (UserManager.IsAny(x => x.Email == request.Email)) throw new AppException("该邮箱已经注册过");
            if (string.IsNullOrWhiteSpace(request.PassWord)) throw new AppException("请输入密码");
            if (string.IsNullOrWhiteSpace(request.ConfirmPassWord)) throw new AppException("请输入确认密码");
            if (string.IsNullOrWhiteSpace(request.ConfirmPassWord)) throw new AppException("请输入确认密码");
            if (request.PassWord != request.ConfirmPassWord) throw new AppException("密码和确认密码不一致");
#endif
            #endregion

            var mapper = new MapperConfiguration(x => x.CreateMap<RegisterRequest, User>()).CreateMapper();
            User user = mapper.Map<User>(request);
            user.PassWord = MD5.Md5(user.PassWord).ToLower();
            user.CreateTime = DateTime.Now;
            user.LoginTime = DateTime.Now;
            user.IP = Accessor.HttpContext.Connection.RemoteIpAddress.ToString();
            user.Portrait = "/Image/user.png";
            user.Money = 0;

            var result = DbContext.Db.Ado.UseTran(() =>
            {
                UserManager.Insert(user);
                UserDetailsManager.Insert(new UserDetails() { UID = user.ID });
            });

#if DEBUG
#else
            //发送邮件
            Cms_Sysconfig sys = await Cms_SysconfigManager.GetSingleAsync(x => x.Id == 1);
            EmailService.MailSending(request.Email, "欢迎您注册宇宙物流", $"欢迎您注册宇宙物流", sys.Mail_From, sys.Mail_Code, sys.Mail_Host);
#endif
            return true;
        }

        /// <summary>
        /// 判断token是否有效
        /// </summary>
        /// <param name="sToken">token</param>
        /// <returns></returns>
        public async Task<bool> IsToken(string sToken)
        {
            User user = TokenService.TokenGetUser(sToken);
            if (user == null) return false;
            string sRedisToken = await RedisService.GetAsync<string>(CommentConfig.UserToken + user.ID, () => null);
            if (string.IsNullOrWhiteSpace(sRedisToken)) return false;
            if (GlobalsConfig.Configuration[ConfigurationKeys.Token_IsSignIn].ToLower() == "true")
            {
                if (sRedisToken != sToken) return false;
            }
            return true;
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public int GetUserID()
        {
            int id = int.Parse((Accessor.HttpContext.User.Identity as ClaimsIdentity).Name ?? "0");
            if (id <= 0)
            {
                string sToken = null;
                if (Accessor != null && Accessor.HttpContext.Request.Headers.ContainsKey("Authorization"))
                {
                    sToken = Accessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "").Replace("Bearer", "");
                }
                // 将字符串Token解码成Token对象;
                JwtSecurityToken _token = new JwtSecurityToken(sToken);
                return int.Parse(_token.Payload[ClaimTypes.Name].ToString());
            }
            return id;
        }
    }
}
