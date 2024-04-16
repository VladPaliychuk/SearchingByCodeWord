using Microsoft.EntityFrameworkCore;
using SBCW.DAL.Data;
using SBCW.DAL.Exceptions;
using SBCW.DAL.Repositories.Interfaces;

namespace SBCW.DAL.Repositories;

public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    protected readonly SBCWContext _context;
    protected readonly DbSet<TEntity> _table;
    
    protected GenericRepository(SBCWContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }
    
    /// <summary>
    /// GetAllAsync
    /// </summary>
    /// <returns>IEnumerable<TEntity></returns>
    /// <exception cref="Exception"></exception>
    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _table.ToListAsync()
               ?? throw new Exception($"Couldn't retrieve entities {typeof(TEntity).Name} ");
    }

    /// <summary>
    /// GetByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns>TEntity</returns>
    /// <exception cref="EntityNotFoundException"></exception>
    public virtual async Task<TEntity> GetByIdAsync(int id)
    {
        return await _table.FindAsync(id)
               ?? throw new EntityNotFoundException($"{typeof(TEntity).Name} with id {id} not found.");
    }

    /// <summary>
    /// AddAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public virtual async Task AddAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(TEntity)} entity must not be null");
        }
        await _table.AddAsync(entity);
    }

    /// <summary>
    /// UpdateAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task UpdateAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(TEntity)} entity must not be null");
        }
        await Task.Run(() => _table.Update(entity));
    }

    /// <summary>
    /// DeleteByIdAsync
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public virtual async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id) 
                     ?? throw new EntityNotFoundException($"{typeof(TEntity).Name} with id {id} not found. " +
                                                          $"Can't delete.");
        await Task.Run(() => _table.Remove(entity));
    }

    /// <summary>
    /// DeleteAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public virtual async Task DeleteAsync(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
        }
        await Task.Run(() => _table.Remove(entity));
    }
}