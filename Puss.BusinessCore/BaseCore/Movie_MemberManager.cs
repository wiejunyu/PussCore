using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
    public interface IMovie_MemberManager : IDbContext<Movie_Member>
    {
    }

    public class Movie_MemberManager : DbContext<Movie_Member>, IMovie_MemberManager
    {
    }
}