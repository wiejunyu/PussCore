using Microsoft.AspNetCore.Mvc;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// Api控制器
    /// </summary>
    [Route("api/admin/[controller]/[action]")]
    [ApiController]
    public class AdminBaseController : ControllerBase
    {
    }
}
