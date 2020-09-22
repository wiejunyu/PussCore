using Puss.Application.Common;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Generic;
using Puss.Data.Models;
using System.Linq;

namespace Puss.Api.Manager.MovieManager
{
    /// <summary>
    /// 验证码
    /// </summary>
    public class MovieManager : IMovieManager
    {
        /// <summary>
        /// channelId
        /// </summary>
        public static string channelId = "12341389";

        /// <summary>
        /// secret
        /// </summary>
        public static string secret = "f79e39604981906b97b1e54b36fbb108";

        /// <summary>
        /// url
        /// </summary>
        public static string http = "http://dev.imanm.com";

        /// <summary>
        /// 获取地区列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResultQueryCitysDataList>> QueryCitys()
        {
            return await Task.Run(() =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("channelId", channelId);
                string sign = MD5.Md5(HttpPostHelper.GetParamSrc(dic) + secret).ToLowerInvariant();
                PostQueryCitys temp = new PostQueryCitys();
                temp.channelId = channelId;
                temp.sign = sign;
                string url = $"{http}/manman/index.php/open/partner/queryCitys";
                string result = HttpPostHelper.DoHttpPost(url, JsonConvert.SerializeObject(temp));
                ResultQueryCitys resultData = JsonConvert.DeserializeObject<ResultQueryCitys>(result);
                if (resultData.code == 0)
                {
                    return resultData.result.cityList;
                }
                else throw new AppException(resultData.errorMsg);
            });
        }

        /// <summary>
        /// 返回当前地区影院列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<ResultHotFilmList>> QueryCinemas(int cityId)
        {
            return await Task.Run(() =>
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("channelId", channelId);
                dic.Add("cityId", cityId.ToString());
                string sign = MD5.Md5(HttpPostHelper.GetParamSrc(dic) + secret).ToLowerInvariant();
                PostQueryCinemas temp = new PostQueryCinemas();
                temp.channelId = channelId;
                temp.cityId = cityId.ToString();
                temp.sign = sign;
                string url = $"{http}/manman/index.php/open/partner/queryCinemas";
                string result = HttpPostHelper.DoHttpPost(url, JsonConvert.SerializeObject(temp));
                ResultHotResult resultData = JsonConvert.DeserializeObject<ResultHotResult>(result);
                if (resultData.code == 0)
                {
                    return resultData.result.filmList;
                }
                else throw new AppException(resultData.message);
            });
        }
    }
}
