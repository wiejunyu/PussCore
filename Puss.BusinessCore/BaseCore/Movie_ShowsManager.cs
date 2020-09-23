using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface IMovie_ShowsManager : IDbContext<Movie_Shows>
    {
    }

    public class Movie_ShowsManager : DbContext<Movie_Shows>, IMovie_ShowsManager
    {
    }
}