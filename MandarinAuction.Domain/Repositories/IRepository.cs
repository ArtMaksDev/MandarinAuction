namespace MandarinAuction.Domain.Repositories;

public interface IRepository<T>
{
    Task<T?> Get(Guid id);
    Task Add(T entity);
    Task Update(T entity);
   Task<IEnumerable<T?>> GetAll();
}