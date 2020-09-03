using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICms_MenuManager : IDbContext<Cms_Menu>
    {
    }

    public class Cms_MenuManager : DbContext<Cms_Menu>, ICms_MenuManager
    {
    }
}