using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface IMovie_CityManager : IDbContext<Movie_City>
    {
    }

    public class Movie_CityManager : DbContext<Movie_City>, IMovie_CityManager
    {
    }
}