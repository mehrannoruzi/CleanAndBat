using Serilog;
using CleanAndBat.Api;
using System.Text.Json;
using CleanAndBat.Api.Filters;
using CleanAndBat.Persistence;
using CleanAndBat.Infrastructure;
using CleanAndBat.Api.Middlewares;
using FluentValidation.AspNetCore;
using CleanAndBat.ApplicationService;
using System.Text.Json.Serialization;
using DryIoc.Microsoft.DependencyInjection;

// Configure Services
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var allowedOrigins = "MyValidOrigins";

builder.Services.AddCors(options =>
{
	options.AddPolicy(allowedOrigins, builder =>
	{
		var validOrigins = configuration.GetSection("AllowedOrigin").Value;
		if (validOrigins is not null)
			builder.WithOrigins(validOrigins.Split(";"))
				   .SetIsOriginAllowedToAllowWildcardSubdomains()
				   .AllowAnyHeader()
				   .AllowAnyMethod()
				   .AllowCredentials()
				   .WithExposedHeaders("");
	});
});

builder.Services.AddControllers(options =>
{
	options.EnableEndpointRouting = false;
	options.ReturnHttpNotAcceptable = true;
	options.RespectBrowserAcceptHeader = true;
})
.AddJsonOptions(options =>
{
	options.JsonSerializerOptions.WriteIndented = true;
	//options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
	options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
	options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});

builder.Services.AddFluentValidationAutoValidation();

builder.Services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
builder.Services.AddTransient<IJwtService, JwtService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
	option.SwaggerDoc("v1", new OpenApiInfo { Title = "CleanAndBat.Api", Version = "v1" });
	option.OperationFilter<SwaggerAuthenticateFilter>();
	option.OperationFilter<SwaggerAuthorizeFilter>();
	option.SchemaFilter<SwaggerExcludeFilter>();

	option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = @"JWT Authorization.<br> 
                        For Example: <br>
                        Authorization: Bearer xxxxxxxxxxxxxxxxxxxxxxxxxxxxx<br><br>
                        Enter the token in the text input below. <br><br>"
	});

	var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddPersistenceServices(configuration);

//builder.Services.AddHostedService<QuartzHostedService>();

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
	loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration));

builder.Host.UseServiceProviderFactory(new DryIocServiceProviderFactory());
builder.Host.ConfigureContainer<IContainer>(ConfigureContainer);

void ConfigureContainer(IContainer container)
{
	container.ConfigureDryIocContainer(configuration);
}

var app = builder.Build();


// Configure Request Pipline
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CleanAndBat.Api v1"));
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors(allowedOrigins);

app.UseMiddleware<JwtParserMiddleware>();

app.UseMiddleware<BatEnableRequestBufferingMiddleware>();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();