using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface IKeyContentManager : IDbContext<KeyContent>
    {
    }

    public class KeyContentManager : DbContext<KeyContent>, IKeyContentManager
    {
    }
}