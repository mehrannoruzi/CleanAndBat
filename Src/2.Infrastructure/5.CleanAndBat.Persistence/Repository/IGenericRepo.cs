namespace CleanAndBat.Persistence.Repository;

public interface IGenericRepo<TEntity> : ITransientInjection where TEntity : class, IBaseEntity
{ }