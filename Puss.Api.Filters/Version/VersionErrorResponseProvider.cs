
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Puss.Data.Enum;
using Puss.Data.Models;

namespace Puss.Api.Filters.Version
{
    public class VersionErrorResponseProvider : DefaultErrorResponseProvider
    {
        public override IActionResult CreateResponse(ErrorResponseContext context)
        {
            switch (context.ErrorCode)
            {
                case "UnsupportedApiVersion":
                    return new ObjectResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.VersionError,
                        Message = "无该版本"
                    });
                case "AmbiguousApiVersion":
                    return new ObjectResult(new ReturnResult()
                    {
                        Status = (int)ReturnResultStatus.VersionError,
                        Message = "头部版本与参数版本不一致"
                    });
            }
            return base.CreateResponse(context);
        }
    }
}
