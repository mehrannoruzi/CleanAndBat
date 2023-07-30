using FluentValidation.AspNetCore;

namespace CleanAndBat.ApplicationService;

public static class ConfigureServices
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
	{
		#region Auth
		serviceCollection.AddScoped<IOtpService, OtpService>();
		#endregion

		#region Base
		serviceCollection.AddScoped<IUserService, UserService>();
		#endregion


		serviceCollection.AddFluentValidationAutoValidation();
		serviceCollection.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();

		serviceCollection.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();

		return serviceCollection;
	}
}