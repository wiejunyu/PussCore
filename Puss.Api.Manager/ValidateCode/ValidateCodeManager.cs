using Puss.Data.Models;
using Puss.Enties;
using Puss.Redis;
using System.Threading.Tasks;

namespace Puss.Api.Manager
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class ValidateCodeManager : IValidateCodeManager
    {
        private readonly IRedisService RedisService;

        /// <summary>
        /// 密匙
        /// </summary>
        /// <param name="RedisService"></param>
        public ValidateCodeManager(IRedisService RedisService) 
        {
            this.RedisService = RedisService;
        }

        /// <summary>
        /// 判断图片验证码是否存在
        /// </summary>
        /// <param name="sCodeKey">密匙Key</param>
        /// <returns></returns>
        public async Task<bool> ExistsImage(string sCodeKey)
        {
            return await Task.Run(() =>
            {
                return RedisService.Exists(CommentConfig.ImageCacheCode + sCodeKey);
            });
        }

        /// <summary>
        /// 获取图片验证码,不存在返回空
        /// </summary>
        /// <param name="sCodeKey">密匙Key</param>
        /// <returns></returns>
        public async Task<string> GetImage(string sCodeKey)
        {
            return await RedisService.GetAsync<string>(CommentConfig.ImageCacheCode + sCodeKey,() => null);
        }

        /// <summary>
        /// 判断邮箱验证码是否存在
        /// </summary>
        /// <param name="sCodeKey">密匙Key</param>
        /// <returns></returns>
        public async Task<bool> ExistsMail(string sCodeKey)
        {
            return await Task.Run(() =>
            {
                return RedisService.Exists(CommentConfig.MailCacheCode + sCodeKey);
            });
        }

        /// <summary>
        /// 获取邮箱验证码,不存在返回空
        /// </summary>
        /// <param name="sCodeKey">密匙Key</param>
        /// <returns></returns>
        public async Task<Code> GetMail(string sCodeKey)
        {
            return await RedisService.GetAsync<Code>(CommentConfig.MailCacheCode + sCodeKey, () => null);
        }
    }
}
