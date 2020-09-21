
namespace Puss.Reptile
{
    public interface IReptileService
    {
        /// <summary>
        /// Post请求并获取HTML
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        string GetHtml(string Url);
    }
}
