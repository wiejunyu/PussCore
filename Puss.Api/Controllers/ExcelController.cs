using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Puss.Application.Common;
using Puss.BusinessCore;
using Puss.Data.Config;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Enties;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 表格
    /// </summary>
    [AllowAnonymous]
    public class ExcelController : ApiBaseController
    {
        private readonly ITelManager TelManager;

        /// <summary>
        /// 表格
        /// </summary>
        /// <param name="TelManager"></param>
        public ExcelController(
            ITelManager TelManager
            )
        {
            this.TelManager = TelManager;
        }

        /// <summary>
        /// 写入表格
        /// </summary>
        /// <param name="file">文件</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> SetExcel(IFormFile file)
        {
            return await Task.Run(() => {
                StreamReader reader = new StreamReader(file.OpenReadStream());
                string content = reader.ReadToEnd();
                string name = file.FileName;
                //写入配置文件
                string filename = Path.Combine(GlobalsConfig.WebRootPath, name);
                if (System.IO.File.Exists(filename))
                {
                    System.IO.File.Delete(filename);
                }
                using (FileStream fs = System.IO.File.Create(filename))
                {
                    // 复制文件
                    file.CopyTo(fs);
                    // 清空缓冲区数据
                    fs.Flush();
                }

                IWorkbook book = null;
                book = new XSSFWorkbook(filename);
                ISheet sheet = book.GetSheet("Sheet1");
                string[] str = new string[] { "tel" };
                List<Tel> list = ExcelHelper.ExportToList<Tel>(sheet, str);

                TelManager.Delete(x => true);
                TelManager.Insert(list.Select(x => new Tel{
                    tel = x.tel,
                    state = (int)TelStateEnum.No
                }).ToList());
                return new ReturnResult(ReturnResultStatus.Succeed);
            });
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="text"></param>
        /// <returns>re：如果后面没有数据了就为false，还有为true</returns>
        /// <returns>error：返回是否成功拼接数据</returns>
        [HttpPost]
        public async Task<ReturnResult> SendExcel(string text)
        {
            string str = "sms:";
            //是否发送完全部
            bool IsAll = true;
            List<Tel> list = await TelManager.GetListAsync(p => p.state == 0);
            if (list.Count == 0) throw new AppException("无可发送数据");
            if (list.Count <= 200)
            {
                foreach (Tel temp in list)
                {
                    temp.state = (int)TelStateEnum.Yes;
                    str += temp.tel + ",";
                }
                IsAll = true;
            }
            else
            {
                for (int i = 0; i < 200; i++)
                {
                    list[i].state = (int)TelStateEnum.Yes;
                    str += list[i].tel + ",";
                }
                IsAll = false;
                
            }
            TelManager.Update(list);
            str = str.Substring(0, str.Length - 1);
            str += "?body=" + text;
            return new ReturnResult<bool>(ReturnResultStatus.Succeed,IsAll, str);
        }

        /// <summary>
        /// 获取发送列表
        /// </summary>
        /// <param name="state">发送状态:0(未发送),1(已发送),不填写全部显示</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> GetTelList(int? state)
        {
            List<Tel> list = await TelManager.GetListAsync(p => true);
            if (state.HasValue)
                list = list.Where(p => p.state == state).ToList();
            return new ReturnResult<object>(ReturnResultStatus.Succeed, list.Select(x => new
            {
                发送状态 = EnumHelper.GetEnumDescription((TelStateEnum)x.state),
                电话 = x.tel
            }));
        }
    }
}