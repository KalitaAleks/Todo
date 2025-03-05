using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TodoApi.API.Filters;

public class AddPaginationParameters : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.ActionDescriptor.RouteValues["action"] == "GetAll")
        {
            operation.Parameters ??= new List<OpenApiParameter>();

            // Параметр "page"
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "page",
                In = ParameterLocation.Query,
                Description = "Номер страницы",
                Schema = new OpenApiSchema
                {
                    Type = "integer",
                    Default = new OpenApiInteger(1) // Исправлено здесь
                }
            });

            // Параметр "pageSize"
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "pageSize",
                In = ParameterLocation.Query,
                Description = "Количество элементов на странице",
                Schema = new OpenApiSchema
                {
                    Type = "integer",
                    Default = new OpenApiInteger(10) // Исправлено здесь
                }
            });
        }
    }
}