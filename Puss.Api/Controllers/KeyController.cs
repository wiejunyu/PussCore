using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Puss.Api.Manager;
using Puss.BusinessCore;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Encrypt;
using Puss.Enties;
using Puss.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 密匙
    /// </summary>
    public class KeyController : ApiBaseController
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
        public async Task<ReturnResult> GetKeySectionList(int? iPageIndex)
        {
            int iUID = LoginManager.GetUserID();
            int iPageSize = 10;//每页多少条数据
            int iPageCount = await DbContext.Db.Queryable<KeySection>().Where(x => x.CreateUserID == iUID).CountAsync();
            List<KeySection> list = await KeySectionManager.GetPageListAsync(x => x.CreateUserID == iUID,new SqlSugar.PageModel { PageIndex = iPageIndex ?? 1, PageSize = iPageSize, PageCount = iPageCount });
            return new ReturnResult<object>(ReturnResultStatus.Succeed,
            new
            {
                list,
                pages = (int)System.Math.Ceiling((double)iPageCount / (double)iPageSize),
                count = iPageCount,
            });
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
                int iUID = LoginManager.GetUserID();
                var mapper = new MapperConfiguration(x => x.CreateMap<CreateKeySection, KeySection>()).CreateMapper();
                KeySection keySection = mapper.Map<KeySection>(request);
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
            if (string.IsNullOrWhiteSpace(request.Name)) throw new AppException("栏目名称不能为空");
            KeySection keySection = await KeySectionManager.GetByIdAsync(int.Parse(request.ID));
            int iUID = LoginManager.GetUserID();
            if (keySection.CreateUserID != iUID) throw new AppException((int)ReturnResultStatus.Illegal,"0", "非法操作");

            var mapper = new MapperConfiguration(x => x.CreateMap<EditKeySection, KeySection>()).CreateMapper();
            keySection = mapper.Map<KeySection>(request);
            keySection.CreateUserID = iUID;
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
                if(!KeySectionManager.IsAny(x => x.CreateUserID == iUID && x.ID == id)) throw new AppException((int)ReturnResultStatus.Illegal, "0", "非法操作");
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
            if (keySection == null) throw new AppException((int)ReturnResultStatus.Illegal, "0", "非法操作");
            return new ReturnResult(ReturnResultStatus.Succeed, JsonConvert.SerializeObject(new
            {
                keySection.ID,
                keySection.Name,
                keySection.Remarks
            }));
        }
        #endregion

        #region 密匙
        /// <summary>
        /// 获取用户密匙列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ReturnResult> GetKeyList(int ID,int? iPageIndex)
        {
            if (ID <= 0) throw new AppException("栏目不能为空");
            int iUID = LoginManager.GetUserID();
            int iPageSize = 10;//每页多少条数据
            int iPageCount = await DbContext.Db.Queryable<KeyContent>().Where(x => x.CreateUserID == iUID && x.SectionID == ID).CountAsync();
            List<KeyContent> list = await KeyContentManager.GetPageListAsync(x => x.CreateUserID == iUID && x.SectionID == ID, new SqlSugar.PageModel { PageIndex = iPageIndex ?? 1, PageSize = iPageSize, PageCount = iPageCount });
            return new ReturnResult<object>(ReturnResultStatus.Succeed,
            new
            {
                list,
                pages = (int)System.Math.Ceiling((double)iPageCount / (double)iPageSize),
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
            if(keyContent == null) throw new AppException((int)ReturnResultStatus.Illegal, "0", "非法操作");
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
            if (keyContent.CreateUserID != iUID) throw new AppException((int)ReturnResultStatus.Illegal, "0", "非法操作");
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
                if (!KeyContentManager.IsAny(x => x.CreateUserID == iUID && x.ID == id)) throw new AppException((int)ReturnResultStatus.Illegal, "0", "非法操作");  
                return ReturnResult.ResultCalculation(() => KeyContentManager.Delete(id));
            });
        }
        #endregion
    }
}