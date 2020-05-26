using Puss.BusinessCore;
using Puss.Data.Models;
using Puss.Enties;
using System.Threading.Tasks;

namespace Puss.Api.Manager
{
    /// <summary>
    /// TestManager
    /// </summary>
    public class TestManager : ITestManager
    {
        private readonly DbContext DbContext;

        /// <summary>
        /// 密匙
        /// </summary>
        /// <param name="RedisService"></param>
        public TestManager(DbContext DbContext) 
        {
            this.DbContext = DbContext;
        }

        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Test()
        {
            return await Task.Run(() =>
            {
                return true;
            });
        }
    }
}
