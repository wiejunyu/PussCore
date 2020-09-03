using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICms_RoleManager : IDbContext<Cms_Role>
    {
    }

    public class Cms_RoleManager : DbContext<Cms_Role>, ICms_RoleManager
    {
    }
}