using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICms_SysconfigManager : IDbContext<Cms_Sysconfig>
    {
    }

    public class Cms_SysconfigManager : DbContext<Cms_Sysconfig>, ICms_SysconfigManager
    {
    }
}