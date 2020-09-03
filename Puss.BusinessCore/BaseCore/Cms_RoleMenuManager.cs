using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICms_RoleMenuManager : IDbContext<Cms_RoleMenu>
    {
    }

    public class Cms_RoleMenuManager : DbContext<Cms_RoleMenu>, ICms_RoleMenuManager
    {
    }
}