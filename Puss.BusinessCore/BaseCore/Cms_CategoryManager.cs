using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICms_CategoryManager : IDbContext<Cms_Category>
    {
    }

    public class Cms_CategoryManager : DbContext<Cms_Category>, ICms_CategoryManager
    {
    }
}