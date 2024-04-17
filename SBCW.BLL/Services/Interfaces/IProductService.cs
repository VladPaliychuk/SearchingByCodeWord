using SBCW.BLL.DTOs.Requests;
using SBCW.BLL.DTOs.Responses;

namespace SBCW.BLL.Services.Interfaces;

public interface IProductService
{
    Task<ProductResponse> CreateProductAsync(ProductRequest productRequest);
    Task<ProductResponse> UpdateProductAsync(Guid id, ProductRequest productRequest);
    Task DeleteProductByIdAsync(Guid id);
    Task DeleteProductAsync(ProductRequest productRequest);
    Task<ProductResponse> GetProductByIdAsync(Guid id);
    Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
   
    Task<IEnumerable<ProductResponse>> GetProductsByTypeAsync(string type);
    Task<IEnumerable<ProductResponse>> GetProductsByTagAsync(Guid tagId);
    Task<IEnumerable<ProductResponse>> GetProductsByCategoryAsync(Guid categoryId);
    Task<IEnumerable<ProductResponse>> GetProductsByUserAsync(Guid userId);
    Task<IEnumerable<ProductResponse>> GetProductsBySearchAsync(string search);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page, int pageSize);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page, int pageSize, string orderBy);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page, int pageSize, string orderBy, bool isDescending);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page, int pageSize, string orderBy, bool isDescending, bool isDeleted);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page, int pageSize, string orderBy, bool isDescending, bool isDeleted, bool isPublished);
    Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, 
        Guid userId, int page, int pageSize, string orderBy, bool isDescending, bool isDeleted, bool isPublished, bool isApproved);
}