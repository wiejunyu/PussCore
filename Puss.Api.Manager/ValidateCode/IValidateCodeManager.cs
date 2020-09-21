using Puss.Enties;
using System.Threading.Tasks;

namespace Puss.Api.Manager.ValidateCodeManager
{
    /// <summary>
    /// 验证码
    /// </summary>
    public interface IValidateCodeManager
    {
        /// <summary>
        /// 判断图片验证码是否存在
        /// </summary>
        /// <param name="sCodeKey">密匙Key</param>
        /// <returns></returns>
        Task<bool> ExistsImage(string sCodeKey);

        /// <summary>
        /// 获取图片验证码,不存在返回空
        /// </summary>
        /// <param name="sCodeKey">密匙Key</param>
        /// <returns></returns>
        Task<string> GetImage(string sCodeKey);

        /// <summary>
        /// 判断邮箱验证码是否存在
        /// </summary>
        /// <param name="sCodeKey">密匙Key</param>
        /// <returns></returns>
        Task<bool> ExistsMail(string sCodeKey);

        /// <summary>
        /// 获取邮箱验证码,不存在返回空
        /// </summary>
        /// <param name="sCodeKey">密匙Key</param>
        /// <returns></returns>
        Task<Code> GetMail(string sCodeKey);
    }
}
