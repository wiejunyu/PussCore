using Microsoft.AspNetCore.Mvc;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// Api控制器
    /// </summary>
    [Route("api/app/[controller]/[action]")]
    [ApiController]
    public class AppBaseController : ControllerBase
    {
    }
}
