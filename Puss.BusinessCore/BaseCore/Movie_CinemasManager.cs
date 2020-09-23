using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface IMovie_CinemasManager : IDbContext<Movie_Cinemas>
    {
    }

    public class Movie_CinemasManager : DbContext<Movie_Cinemas>, IMovie_CinemasManager
    {
    }
}