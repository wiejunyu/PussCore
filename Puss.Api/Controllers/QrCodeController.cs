using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Puss.Data;
using Puss.Data.Config;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.QrCode;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 二维码
    /// </summary>
    public class QrCodeController : ApiBaseController
    {
        /// <summary>
        /// 传入Url获取二维码
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetQrCode")]
        public ReturnResult GetQrCode(string url)
        {
            byte[] buffer = QrCodeHelper.GetQRcode(url, 8);
            return new ReturnResult(ReturnResultStatus.Succeed, $"data:image/png;base64,{Convert.ToBase64String(buffer)}");
        }

    }
}