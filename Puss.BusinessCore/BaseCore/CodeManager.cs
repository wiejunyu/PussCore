using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface ICodeManager : IDbContext<Code>
    {
    }

    public class CodeManager : DbContext<Code>, ICodeManager
    {
    }
}