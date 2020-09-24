using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Puss.Api.Manager.KeyManager;
using Puss.Api.Manager.LoginManager;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Enties;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 密匙
    /// </summary>
    public class KeyController : AppBaseController
    {
        private readonly DbContext DbContext;
        private readonly IKeySectionManager KeySectionManager;
        private readonly IKeyContentManager KeyContentManager;
        private readonly ILoginManager LoginManager;
        private readonly IKeyManager KeyManager;

        /// <summary>
        /// 密匙
        /// </summary>
        /// <param name="DbContext"></param>
        /// <param name="KeySectionManager"></param>
        /// <param name="KeyContentManager"></param>
        /// <param name="LoginManager"></param>
        /// <param name="KeyManager"></param>
        public KeyController(
            DbContext DbContext,
            IKeySectionManager KeySectionManager,
            IKeyContentManager KeyContentManager,
            ILoginManager LoginManager,
            IKeyManager KeyManager
            )
        {
            this.DbContext = DbContext;
            this.KeySectionManager = KeySectionManager;
            this.KeyContentManager = KeyContentManager;
            this.LoginManager = LoginManager;
            this.KeyManager = KeyManager;
        }

        #region 密匙栏目
        /// <summary>
        /// 获取用户密匙栏目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ReturnResult> GetKeySectionList(int? iPageIndex, int? iPageSize)
        {
            int iUID = LoginManager.GetUserID();
            List<KeySection> list = await KeySectionManager.GetListAsync(x => x.CreateUserID == iUID);
            int iPageCount = list.Where(x => x.CreateUserID == iUID && x.Level == 0).Count();
            List<KeySection> lFather = list.Where(x => x.CreateUserID == iUID && x.Level == 0).ToList();
            return new ReturnResult<object>(ReturnResultStatus.Succeed, lFather.Select(x => new {
                title = x.Name,
                id = x.ID,
                href = $"list.html?id={x.ID}",
                children = list.Where(p => p.PID == x.ID).Select(p => new {
                    title = p.Name,
                    id = p.ID,
                    href = $"list.html?id={p.ID}"
                })
            }));
        }

        /// <summary>
        /// 获取用户密匙栏目
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ReturnResult> GetKeySectionListAll(int? Level)
        {
            int iUID = LoginManager.GetUserID();
            List<KeySection> list = await KeySectionManager.GetListAsync(x => x.CreateUserID == iUID);
            if (Level.HasValue) 
            {
                list = list.Where(x => x.Level == Level).ToList();
            }
            return new ReturnResult<List<KeySection>>(ReturnResultStatus.Succeed,list);
        }

        /// <summary>
        /// 写入密匙栏目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> AddKeySection([FromBody]CreateKeySection request)
        {
            return await Task.Run(() =>
            {
                if (string.IsNullOrWhiteSpace(request.Name)) throw new AppException("栏目名称不能为空");
                if (int.Parse(request.Level) < 0) throw new AppException("栏目等级不能为空");
                if (int.Parse(request.Level) > 0 && int.Parse(request.PID) < 0) throw new AppException("父ID不能为空");
                int iUID = LoginManager.GetUserID();
                var mapper = new MapperConfiguration(x => x.CreateMap<CreateKeySection, KeySection>()).CreateMapper();
                KeySection keySection = mapper.Map<KeySection>(request);
                if (keySection.Level == 0) keySection.PID = 0;
                keySection.CreateUserID = iUID;
                return ReturnResult.ResultCalculation(() => KeySectionManager.Insert(keySection));
            });
        }

        /// <summary>
        /// 编辑密匙栏目
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> EditKeySection([FromBody]EditKeySection request)
        {
            if (int.Parse(request.ID) <= 0) throw new AppException("栏目ID不能为空");
            if (int.Parse(request.Level) < 0) throw new AppException("栏目等级不能为空");
            if (int.Parse(request.Level) > 0 && int.Parse(request.PID) < 0) throw new AppException("父ID不能为空");
            if (string.IsNullOrWhiteSpace(request.Name)) throw new AppException("栏目名称不能为空");
            KeySection keySection = await KeySectionManager.GetByIdAsync(int.Parse(request.ID));
            int iUID = LoginManager.GetUserID();
            if (keySection.CreateUserID != iUID) throw new AppException(ReturnResultStatus.Illegal,"非法操作");

            var mapper = new MapperConfiguration(x => x.CreateMap<EditKeySection, KeySection>()).CreateMapper();
            keySection = mapper.Map<KeySection>(request);
            if (keySection.Level == 0) keySection.PID = 0;
            int iCount = await DbContext.Db.Queryable<KeySection>().Where(x => x.PID == keySection.ID).CountAsync();
            if (keySection.Level == 1 && iCount > 0) 
            {
                throw new AppException("还存在子栏目不能移动到其他栏目下"); 
            }
            keySection.CreateUserID = iUID;
            return ReturnResult.ResultCalculation(() => KeySectionManager.Update(keySection));
        }

        /// <summary>
        /// 编辑密匙栏目名称
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> EditKeySectionByName([FromBody]EditKeySectionByName request)
        {
            if (int.Parse(request.ID) <= 0) throw new AppException("栏目ID不能为空");
            if (string.IsNullOrWhiteSpace(request.Name)) throw new AppException("栏目名称不能为空");
            KeySection keySection = await KeySectionManager.GetByIdAsync(int.Parse(request.ID));
            int iUID = LoginManager.GetUserID();
            if (keySection.CreateUserID != iUID) throw new AppException(ReturnResultStatus.Illegal,"非法操作");
            keySection.Name = request.Name;
            return ReturnResult.ResultCalculation(() => KeySectionManager.Update(keySection));
        }

        /// <summary>
        /// 删除密匙栏目
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> DeleteKeySection(int id)
        {
            return await Task.Run(() =>
            {
                if (id <= 0) throw new AppException("栏目ID不能为空");
                int iUID = LoginManager.GetUserID();
                if(!KeySectionManager.IsAny(x => x.CreateUserID == iUID && x.ID == id)) throw new AppException(ReturnResultStatus.Illegal, "非法操作");
                DbContext.Db.Ado.UseTran(() =>
                {
                    KeySectionManager.Delete(id);
                    DbContext.Db.Deleteable<KeyContent>().Where(x => x.SectionID == id).ExecuteCommand();
                });
                return new ReturnResult(ReturnResultStatus.Succeed);
            });
        }

        /// <summary>
        /// 获取用户密匙栏目详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ReturnResult> GetKeyKeySectionDetailed(int ID)
        {
            if (ID <= 0) throw new AppException("密匙栏目不能为空");
            int iUID = LoginManager.GetUserID();
            KeySection keySection = await KeySectionManager.GetSingleAsync(x => x.CreateUserID == iUID && x.ID == ID);
            if (keySection == null) throw new AppException(ReturnResultStatus.Illegal, "非法操作");
            return new ReturnResult(ReturnResultStatus.Succeed, JsonConvert.SerializeObject(new
            {
                keySection.ID,
                keySection.Name,
                keySection.Remarks,
                keySection.Level,
                keySection.PID,
            }));
        }
        #endregion

        #region 密匙
        /// <summary>
        /// 获取用户密匙列表
        /// </summary>
        /// <param name="ID">密匙栏目ID</param>
        /// <param name="iPageIndex">第几页</param>
        /// <param name="iPageSize">每页几条数据</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ReturnResult> GetKeyList(int ID,int? iPageIndex, int? iPageSize)
        {
            if (ID <= 0) throw new AppException("栏目不能为空");
            int iUID = LoginManager.GetUserID();
            int iPageCount = await DbContext.Db.Queryable<KeyContent>().Where(x => x.CreateUserID == iUID && x.SectionID == ID).CountAsync();
            List<KeyContent> list = await KeyContentManager.GetPageListAsync(x => x.CreateUserID == iUID && x.SectionID == ID, new SqlSugar.PageModel { PageIndex = iPageIndex ?? 1, PageSize = iPageSize ?? 10, PageCount = iPageCount });
            return new ReturnResult<object>(ReturnResultStatus.Succeed,
            new
            {
                list,
                pages = (int)System.Math.Ceiling((double)iPageCount / (double)(iPageSize ?? 10)),
                count = iPageCount,
            });
        }

        /// <summary>
        /// 获取用户密匙详细
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ReturnResult> GetKeyDetailed(int ID)
        {
            if (ID <= 0) throw new AppException("密匙不能为空");
            int iUID = LoginManager.GetUserID();
            KeyContent keyContent = await KeyContentManager.GetSingleAsync(x => x.CreateUserID == iUID && x.ID == ID);
            if(keyContent == null) throw new AppException(ReturnResultStatus.Illegal,"非法操作");
            keyContent = await KeyManager.DecryptKey(keyContent);
            return new ReturnResult(ReturnResultStatus.Succeed, JsonConvert.SerializeObject(new 
            {
                keyContent.ID,
                keyContent.SectionID,
                keyContent.Name,
                keyContent.UserText,
                keyContent.PasswordText,
                keyContent.UrlText,
                keyContent.IphoneText,
                keyContent.MailText,
                keyContent.OtherText,
                keyContent.Remarks,
                keyContent.IsStart,
            }));
        }

        /// <summary>
        /// 写入密匙
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> AddKey([FromBody]CreateKey request)
        {
            if (int.Parse(request.SectionID) <= 0) throw new AppException("栏目不能为空");
            if (string.IsNullOrWhiteSpace(request.Name)) throw new AppException("名称不能为空");

            int iUID = LoginManager.GetUserID();
            var mapper = new MapperConfiguration(x => x.CreateMap<CreateKey, KeyContent>()).CreateMapper();
            KeyContent keyContent = mapper.Map<KeyContent>(request);
            keyContent.CreateUserID = iUID;
            keyContent = await KeyManager.EncryptKey(keyContent);
            return ReturnResult.ResultCalculation(() => KeyContentManager.Insert(keyContent));
        }

        /// <summary>
        /// 编辑密匙
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> EditKey([FromBody]EditKey request)
        {
            if (int.Parse(request.SectionID) <= 0) throw new AppException("栏目不能为空");
            if (string.IsNullOrWhiteSpace(request.Name)) throw new AppException("名称不能为空");
            KeyContent keyContent = await KeyContentManager.GetByIdAsync(int.Parse(request.ID));
            int iUID = LoginManager.GetUserID();
            if (keyContent.CreateUserID != iUID) throw new AppException(ReturnResultStatus.Illegal, "非法操作");
            var mapper = new MapperConfiguration(x => x.CreateMap<EditKey, KeyContent>()).CreateMapper();
            mapper.Map(request, keyContent);
            keyContent.CreateUserID = iUID;
            keyContent = await KeyManager.EncryptKey(keyContent);
            return ReturnResult.ResultCalculation(() => KeyContentManager.Update(keyContent));
        }

        /// <summary>
        /// 删除密匙
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> DeleteKey(int id)
        {
            return await Task.Run(() =>
            {
                if (id <= 0) throw new AppException("栏目ID不能为空");
                int iUID = LoginManager.GetUserID();
                if (!KeyContentManager.IsAny(x => x.CreateUserID == iUID && x.ID == id)) throw new AppException(ReturnResultStatus.Illegal,"非法操作");  
                return ReturnResult.ResultCalculation(() => KeyContentManager.Delete(id));
            });
        }
        #endregion
    }
}