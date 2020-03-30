using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.QrCode
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public class QrCodeHelper
    {
        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        public static byte[] GetQRcode(string url, int pixel)
        {
            QRCodeGenerator generator = new QRCodeGenerator();
            QRCodeData codeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.M, true);
            QRCode qrcode = new QRCode(codeData);
            Bitmap bitmap = qrcode.GetGraphic(pixel, Color.Black, Color.White, true);
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, ImageFormat.Jpeg);
            return ms.GetBuffer();
        }
    }
}
