using AutoMapper;
using SBCW.BLL.DTOs.Requests;
using SBCW.BLL.DTOs.Responses;
using SBCW.DAL.Models;
using SBCW.DAL.Repositories.Interfaces;

namespace SBCW.BLL.Services.Interfaces;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    
    public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
    {
        var products = await _unitOfWork._productRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductResponse>>(products);
    }

    public async Task<ProductResponse> GetProductByIdAsync(Guid id)
    {
        var product = await _unitOfWork._productRepository.GetByIdAsync(id);
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> CreateProductAsync(ProductRequest request)
    {
        var product = _mapper.Map<Product>(request);
        await _unitOfWork._productRepository.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> UpdateProductAsync(Guid id, ProductRequest request)
    {
        var existingProduct = await _unitOfWork._productRepository.GetByIdAsync(id);
        
        _mapper.Map(request, existingProduct);

        await _unitOfWork._productRepository.UpdateAsync(existingProduct);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<ProductResponse>(existingProduct);
    }

    public async Task DeleteProductByIdAsync(Guid id)
    {
        await _unitOfWork._productRepository.DeleteByIdAsync(id);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(ProductRequest request)
    {
        var product = _mapper.Map<Product>(request);
        await _unitOfWork._productRepository.DeleteAsync(product);
        await _unitOfWork.SaveChangesAsync();
    }
    
    public Task<IEnumerable<ProductResponse>> GetProductsByTypeAsync(string type)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByTagAsync(Guid tagId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByCategoryAsync(Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByUserAsync(Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsBySearchAsync(string search)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page,
        int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page,
        int pageSize, string orderBy)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page,
        int pageSize, string orderBy, bool isDescending)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page,
        int pageSize, string orderBy, bool isDescending, bool isDeleted)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page,
        int pageSize, string orderBy, bool isDescending, bool isDeleted, bool isPublished)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ProductResponse>> GetProductsByFilterAsync(string type, string search, Guid tagId, Guid categoryId, Guid userId, int page,
        int pageSize, string orderBy, bool isDescending, bool isDeleted, bool isPublished, bool isApproved)
    {
        throw new NotImplementedException();
    }
}