using Microsoft.OpenApi.Any;

namespace CleanAndBat.Api.Filters;

public class SwaggerAuthenticateFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
		{
			if (!context.ApiDescription.CustomAttributes().Any((a) => a is AllowAnonymousAttribute)
				&& descriptor.ControllerTypeInfo.GetCustomAttribute<AuthenticateFilter>() != null)
			{
				if (operation.Parameters == null) operation.Parameters = new List<OpenApiParameter>();

				operation.Parameters.Add(new OpenApiParameter
				{
					Name = "Token",
					In = ParameterLocation.Header,
					Required = true,
					Schema = new OpenApiSchema
					{
						Type = "string",
						Default = new OpenApiString("672F2DD8-52FA-499F-1366-D556D9C25AF5")
					},
					Description = "Header Token For Authenticate Request.",
				});
			}
		}
	}
}