using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICms_LoggerManager : IDbContext<Cms_Logger>
    {
    }

    public class Cms_LoggerManager : DbContext<Cms_Logger>, ICms_LoggerManager
    {
    }
}