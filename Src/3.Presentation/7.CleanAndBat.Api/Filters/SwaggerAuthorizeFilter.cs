namespace CleanAndBat.Api.Filters;

public class SwaggerAuthorizeFilter : IOperationFilter
{
	public void Apply(OpenApiOperation operation, OperationFilterContext context)
	{
		if (context.ApiDescription.ActionDescriptor is ControllerActionDescriptor descriptor)
		{
			if (!context.ApiDescription.CustomAttributes().Any((a) => a is AllowAnonymousAttribute) &&
				(context.ApiDescription.CustomAttributes().Any((a) => a is AuthorizeAttribute)
				|| descriptor.ControllerTypeInfo.GetCustomAttribute<AuthorizeFilter>() != null))
			{
				if (operation.Security == null) operation.Security = new List<OpenApiSecurityRequirement>();

				operation.Security.Add(new OpenApiSecurityRequirement
					{
						{
							new OpenApiSecurityScheme
							{
								Name = "Authorization",
								In = ParameterLocation.Header,
								BearerFormat = "Bearer Token",

								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							Array.Empty<string>()
						}
					});
			}
		}
	}
}