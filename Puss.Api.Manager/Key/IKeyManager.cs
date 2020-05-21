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
using Puss.Encrypt;

namespace Puss.Api.Manager
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
        /// <param name="EncryptService">加密类接口</param>
        /// <returns></returns>
        Task<KeyContent> EncryptKey(KeyContent request, IEncryptService EncryptService);

        /// <summary>
        /// 密匙解密
        /// </summary>
        /// <param name="request">密匙模型</param>
        /// <param name="EncryptService">加密类接口</param>
        /// <returns></returns>
        Task<KeyContent> DecryptKey(KeyContent request, IEncryptService EncryptService);
    }
}
