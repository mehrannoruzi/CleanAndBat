namespace CleanAndBat.Persistence.UnitOfWork;

public partial class AppUnitOfWork : IBatUnitOfWork, IScopedInjection
{
	private readonly AppDbContext _appDbContext;
	private readonly IServiceProvider _serviceProvider;

	public AppUnitOfWork(AppDbContext appDbContext, IServiceProvider serviceProvider)
	{
		_appDbContext = appDbContext;
		_serviceProvider = serviceProvider;
	}


	public DatabaseFacade Database { get => _appDbContext.Database; }
	public ChangeTracker ChangeTracker { get => _appDbContext.ChangeTracker; }


	public EFGenericRepo<TEntity> GetRepository<TEntity>() where TEntity : class, IBaseEntity
		=> (GenericRepo<TEntity>)_serviceProvider.GetService<IGenericRepo<TEntity>>();

	public EFBulkGenericRepo<TEntity> GetBulkRepository<TEntity>() where TEntity : class, IBaseEntity
		=> (EFBulkGenericRepo<TEntity>)_serviceProvider.GetService<IEFBulkGenericRepo<TEntity>>();


	public async Task<List<TResult>> ExecuteProcedure<TResult>(string sqlQuery, params object[] parameters) where TResult : class
		=> await _appDbContext.ExecuteProcedure<TResult>(sqlQuery, parameters);


	public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		=> _appDbContext.SaveChangesAsync(cancellationToken);

	public Task<SaveChangeResult> BatSaveChangesAsync(CancellationToken cancellationToken = default)
		=> _appDbContext.BatSaveChangesAsync(cancellationToken);

	public Task<SaveChangeResult> BatSaveChangesWithValidationAsync(CancellationToken cancellationToken = default)
		=> _appDbContext.BatSaveChangesWithValidationAsync(cancellationToken);

	public void Dispose()
	{
		_appDbContext.Dispose();

		GC.SuppressFinalize(this);
	}
}