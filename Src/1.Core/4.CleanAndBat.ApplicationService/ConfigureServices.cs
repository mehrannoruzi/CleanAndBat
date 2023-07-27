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


		serviceCollection.AddSingleton<IMemoryCacheProvider, MemoryCacheProvider>();

		return serviceCollection;
	}
}