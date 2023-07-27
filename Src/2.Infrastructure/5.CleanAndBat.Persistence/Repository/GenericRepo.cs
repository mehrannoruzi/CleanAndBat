namespace CleanAndBat.Persistence.Repository;

public class GenericRepo<TEntity> : EFGenericRepo<TEntity>, IGenericRepo<TEntity> where TEntity : class, IBaseEntity
{
	public GenericRepo(AppDbContext appDbContext) : base(appDbContext) { }
}