namespace CleanAndBat.Persistence;

public class AppDbContext : BatDbContext
{
	public AppDbContext() { }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.OverrideDeleteBehavior(DeleteBehavior.Restrict);
		//builder.RegisterAllEntities<IEntity>(typeof(Role).Assembly);
		builder.ApplyConfigurationsFromAssembly(typeof(UserConfig).Assembly);
	}
}