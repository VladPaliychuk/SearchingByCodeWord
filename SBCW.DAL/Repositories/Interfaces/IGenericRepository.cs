namespace SBCW.DAL.Repositories.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<IEnumerable<TEntity>> GetAllAsync();

    Task<TEntity> GetByIdAsync(Guid id);

    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task DeleteByIdAsync(Guid id);

    Task DeleteAsync(TEntity entity);
}