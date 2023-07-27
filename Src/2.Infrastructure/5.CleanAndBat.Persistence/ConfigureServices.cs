namespace CleanAndBat.Persistence;

public static class ConfigureServices
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection serviceCollection, IConfiguration configuration)
	{
		serviceCollection.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));

		serviceCollection.AddContext<AppDbContext>(configuration.GetConnectionString("AppDbContext"));

		serviceCollection.AddScoped<AppUnitOfWork>();
		serviceCollection.AddScoped<IBatUnitOfWork, AppUnitOfWork>();

		return serviceCollection;
	}

	public static IServiceCollection AddContext<TDbContext>(this IServiceCollection serviceCollection, string conectionString) where TDbContext : DbContext
	{
		serviceCollection.AddDbContext<TDbContext>(optionBuilder =>
		{
			optionBuilder.UseSqlServer(conectionString,
				sqlServerOption =>
				{
					//sqlServerOption.MinBatchSize(1);
					//sqlServerOption.MaxBatchSize(42);
					sqlServerOption.CommandTimeout(null);
					sqlServerOption.UseRelationalNulls(true);
					sqlServerOption.MigrationsHistoryTable("__EFMigrationsHistory", "Config");
					//sqlServerOption.EnableRetryOnFailure(2, TimeSpan.FromSeconds(10), null);
				});
			//.ReplaceService<IHistoryRepository, MigrationRepository>(); ;
			//.LogTo(Console.WriteLine, LogLevel.Information);
			//.AddInterceptors(new DbContextInterceptors());
		});

		return serviceCollection;
	}

	public static IServiceCollection AddContextPool<TDbContext>(this IServiceCollection serviceCollection, string conectionString, int poolSize) where TDbContext : DbContext
	{
		serviceCollection.AddDbContextPool<TDbContext>(optionBuilder =>
		{
			optionBuilder.UseSqlServer(conectionString,
						sqlServerOption =>
						{
							//sqlServerOption.MinBatchSize(1);
							//sqlServerOption.MaxBatchSize(42);
							sqlServerOption.CommandTimeout(null);
							sqlServerOption.UseRelationalNulls(true);
							sqlServerOption.MigrationsHistoryTable("__EFMigrationsHistory", "Config");
						});
			//.LogTo(Console.WriteLine, LogLevel.Information);
			//.AddInterceptors(new DbContextInterceptors());
		}, poolSize);

		return serviceCollection;
	}
}