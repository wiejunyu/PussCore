using Puss.Enties;
using System.Threading.Tasks;

namespace Puss.Api.Manager
{
    /// <summary>
    /// TestManager
    /// </summary>
    public interface ITestManager
    {
        /// <summary>
        /// Test
        /// </summary>
        /// <returns></returns>
        Task<bool> Test();
    }
}
