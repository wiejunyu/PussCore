using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICms_UserInfoManager : IDbContext<Cms_UserInfo>
    {
    }

    public class Cms_UserInfoManager : DbContext<Cms_UserInfo>, ICms_UserInfoManager
    {
    }
}