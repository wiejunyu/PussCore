﻿using System;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IQrCode QrCode;

        /// <summary>
        /// 二维码
        /// </summary>
        /// <param name="QrCode"></param>
        public QrCodeController(IQrCode QrCode) 
        {
            this.QrCode = QrCode;
        }
        /// <summary>
        /// 传入Url获取二维码
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetQrCode")]
        public ReturnResult GetQrCode(string url)
        {
            byte[] buffer = QrCode.GetQRcode(url, 8);
            return new ReturnResult(ReturnResultStatus.Succeed, $"data:image/png;base64,{Convert.ToBase64String(buffer)}");
        }

    }
}