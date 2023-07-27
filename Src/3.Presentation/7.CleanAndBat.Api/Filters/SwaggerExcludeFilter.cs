namespace CleanAndBat.Api.Filters;

public class SwaggerExcludeFilter : ISchemaFilter
{
	public void Apply(OpenApiSchema schema, SchemaFilterContext context)
	{
		if (schema?.Properties == null || context == null) return;
		if (context.Type.GetCustomAttribute(typeof(SwaggerExcludeAttribute), true) != null) schema.Properties = null;
	}
}