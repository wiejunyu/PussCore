
namespace Puss.Api.Manager.ValidateCodeManager
{

    /// <summary>
    /// 生成验证码图片并返回图片Base64输入模型
    /// </summary>
    public class ShowValidateCodeBase64Input
    {
        /// <summary>
        /// 验证码Key
        /// </summary>
        public string CodeKey { get; set; }
    }

    /// <summary>
    /// 生成验证码图片并返回图片Base64输出模型
    /// </summary>
    public class ShowValidateCodeBase64Output
    {
        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; set; }
    }
}
