using SBCW.DAL.Models;

namespace SBCW.DAL.Repositories.Interfaces;

public interface IProductTagRepository : IGenericRepository<ProductTag>
{
    Task<ProductTag> GetByDoubleIdAsync(Guid productId, Guid tagId);
    Task DeleteByDoubleIdAsync(Guid productId, Guid tagId);
}