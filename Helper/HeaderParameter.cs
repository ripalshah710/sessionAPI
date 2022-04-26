using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SessionInfoApi.Helper
{
    public class HeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            string startWith = "X";
            operation.Parameters.Add(new OpenApiParameter() { Name = startWith + "-Version", In = ParameterLocation.Header, Required = true });
            operation.Parameters.Add(new OpenApiParameter() { Name = startWith + "-SourceApp", In = ParameterLocation.Header, Required = true });
            operation.Parameters.Add(new OpenApiParameter() { Name = startWith + "-Token", In = ParameterLocation.Header, Required = true });
            operation.Parameters.Add(new OpenApiParameter() { Name = startWith + "-SessionId", In = ParameterLocation.Header, Required = true });

        }//Apply

    }
}
