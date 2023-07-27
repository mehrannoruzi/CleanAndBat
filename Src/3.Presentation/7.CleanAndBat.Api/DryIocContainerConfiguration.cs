namespace CleanAndBat.Api;

public static class DryIocContainerConfiguration
{
	public static void ConfigureDryIocContainer(this IRegistrator container, IConfiguration configuration)
	{
		//container.AddBatDryIocDynamicTransient(typeof(IUserService).Assembly,
		//	typeof(UserService).Assembly,
		//	typeof(MemoryCacheProvider).Assembly);

		//container.AddBatDryIocDynamicScoped(typeof(IUserService).Assembly,
		//	typeof(UserService).Assembly,
		//	typeof(AppUnitOfWork).Assembly,
		//	typeof(MemoryCacheProvider).Assembly);

		//container.AddBatDryIocDynamicSingleton(typeof(IUserService).Assembly,
		//	typeof(UserService).Assembly,
		//	typeof(AppUnitOfWork).Assembly,
		//	typeof(MemoryCacheProvider).Assembly);


		//container.Register(typeof(IGenericRepo<>), typeof(GenericRepo<>), Reuse.Transient);
	}
}