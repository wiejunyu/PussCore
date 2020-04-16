namespace Puss.QrCode
{
    /// <summary>
    /// 二维码帮助类
    /// </summary>
    public interface IQrCodeHelper
    {
        /// <summary>
        /// 获取二维码
        /// </summary>
        /// <param name="url"></param>
        /// <param name="pixel"></param>
        /// <returns></returns>
        byte[] GetQRcode(string url, int pixel);
    }
}
