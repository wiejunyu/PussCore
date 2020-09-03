using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICms_ArticleManager : IDbContext<Cms_Article>
    {
    }

    public class Cms_ArticleManager : DbContext<Cms_Article>, ICms_ArticleManager
    {
    }
}