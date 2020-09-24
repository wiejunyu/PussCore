using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface IMovie_FilmManager : IDbContext<Movie_Film>
    {
    }

    public class Movie_FilmManager : DbContext<Movie_Film>, IMovie_FilmManager
    {
    }
}