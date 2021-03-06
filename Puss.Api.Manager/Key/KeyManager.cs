﻿using Puss.Enties;
using System.Threading.Tasks;
using Puss.Encrypt;

namespace Puss.Api.Manager.KeyManager
{
    /// <summary>
    /// 密匙
    /// </summary>
    public class KeyManager : IKeyManager
    {
        private readonly IEncryptService EncryptService;

        /// <summary>
        /// 密匙
        /// </summary>
        /// <param name="EncryptService"></param>
        public KeyManager(IEncryptService EncryptService) 
        {
            this.EncryptService = EncryptService;
        }

        /// <summary>
        /// 密匙加密
        /// </summary>
        /// <param name="request">密匙模型</param>
        /// <returns></returns>
        public async Task<KeyContent> EncryptKey(KeyContent request)
        {
            return await Task.Run(() =>
            {
                request.UserText = !string.IsNullOrWhiteSpace(request.UserText) ? EncryptService.DesEncrypt(request.UserText, Connection.DesEncrypt_Key) : "";
                request.PasswordText = !string.IsNullOrWhiteSpace(request.PasswordText) ? EncryptService.DesEncrypt(request.PasswordText, Connection.DesEncrypt_Key) : "";
                request.UrlText = !string.IsNullOrWhiteSpace(request.UrlText) ? EncryptService.DesEncrypt(request.UrlText, Connection.DesEncrypt_Key) : "";
                request.IphoneText = !string.IsNullOrWhiteSpace(request.IphoneText) ? EncryptService.DesEncrypt(request.IphoneText, Connection.DesEncrypt_Key) : "";
                request.MailText = !string.IsNullOrWhiteSpace(request.MailText) ? EncryptService.DesEncrypt(request.MailText, Connection.DesEncrypt_Key) : "";
                request.OtherText = !string.IsNullOrWhiteSpace(request.OtherText) ? EncryptService.DesEncrypt(request.OtherText, Connection.DesEncrypt_Key) : "";
                request.Remarks = !string.IsNullOrWhiteSpace(request.Remarks) ? EncryptService.DesEncrypt(request.Remarks, Connection.DesEncrypt_Key) : "";
                return request;
            });
        }

        /// <summary>
        /// 密匙解密
        /// </summary>
        /// <param name="request">密匙模型</param>
        /// <returns></returns>
        public async Task<KeyContent> DecryptKey(KeyContent request)
        {
            return await Task.Run(() =>
            {
                request.UserText = !string.IsNullOrWhiteSpace(request.UserText) ? EncryptService.DesDecrypt(request.UserText, Connection.DesEncrypt_Key) : "";
                request.PasswordText = !string.IsNullOrWhiteSpace(request.PasswordText) ? EncryptService.DesDecrypt(request.PasswordText, Connection.DesEncrypt_Key) : "";
                request.UrlText = !string.IsNullOrWhiteSpace(request.UrlText) ? EncryptService.DesDecrypt(request.UrlText, Connection.DesEncrypt_Key) : "";
                request.IphoneText = !string.IsNullOrWhiteSpace(request.IphoneText) ? EncryptService.DesDecrypt(request.IphoneText, Connection.DesEncrypt_Key) : "";
                request.MailText = !string.IsNullOrWhiteSpace(request.MailText) ? EncryptService.DesDecrypt(request.MailText, Connection.DesEncrypt_Key) : "";
                request.OtherText = !string.IsNullOrWhiteSpace(request.OtherText) ? EncryptService.DesDecrypt(request.OtherText, Connection.DesEncrypt_Key) : "";
                request.Remarks = !string.IsNullOrWhiteSpace(request.Remarks) ? EncryptService.DesDecrypt(request.Remarks, Connection.DesEncrypt_Key) : "";
                return request;
            });
        }
    }
}
