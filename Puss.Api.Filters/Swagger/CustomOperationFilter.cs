using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Puss.Api.Filters.Swagger
{
    public class CustomOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            #region Swagger版本描述处理
            foreach (var parameter in operation.Parameters)
            {
                var description = context.ApiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                if (parameter.Name == "version")
                {
                    parameter.Description = "填写版本号如:1.0";
                    parameter.Schema.Default = new OpenApiString(context.ApiDescription.GroupName.Replace("v", "").ToString());
                }
            }
            #endregion
        }
    }
}
