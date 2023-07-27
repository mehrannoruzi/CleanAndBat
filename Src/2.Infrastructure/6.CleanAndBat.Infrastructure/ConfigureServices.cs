namespace CleanAndBat.Infrastructure;

public static class ConfigureServices
{
	public static IServiceCollection AddInfrastructureServices(this IServiceCollection serviceCollection)
	{
		serviceCollection.AddScoped<ISmsGatwayAdapter, KaveNegarSmsAdapter>();
		serviceCollection.AddScoped<IEmailGatwayAdapter, NajvaEmailAdapter>();

		return serviceCollection;
	}
}