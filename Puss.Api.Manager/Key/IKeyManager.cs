using Puss.Enties;
using System.Threading.Tasks;

namespace Puss.Api.Manager.KeyManager
{
    /// <summary>
    /// 密匙
    /// </summary>
    public interface IKeyManager
    {
        /// <summary>
        /// 密匙加密
        /// </summary>
        /// <param name="request">密匙模型</param>
        /// <returns></returns>
        Task<KeyContent> EncryptKey(KeyContent request);

        /// <summary>
        /// 密匙解密
        /// </summary>
        /// <param name="request">密匙模型</param>
        /// <returns></returns>
        Task<KeyContent> DecryptKey(KeyContent request);
    }
}
