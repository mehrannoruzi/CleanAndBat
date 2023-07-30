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
				operation.Parameters ??= new List<OpenApiParameter>();
				operation.Parameters.Add(new OpenApiParameter
				{
					Name = "Token",
					In = ParameterLocation.Header,
					Required = true,
					Schema = new OpenApiSchema
					{
						Type = "string",
						Default = new OpenApiString("10FA579F-B9D5-4E4A-863E-6215DC2CD31E")
					},
					Description = "Header Token For Authenticate Request.",
				});
			}
		}
	}
}