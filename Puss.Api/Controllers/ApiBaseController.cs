using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Puss.Data;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Enties;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// Api控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
    }
}
