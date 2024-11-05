namespace RateMe.Core.Abstractions;

public interface IRepository<TEntity>
{
   Task<Guid> Create(TEntity entity);
   Task<List<TEntity?>> Get();
   Task<Guid> Delete(Guid id);
   Task<TEntity?> GetById(Guid id);
}