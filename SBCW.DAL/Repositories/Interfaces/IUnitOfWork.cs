namespace SBCW.DAL.Repositories.Interfaces;

public interface IUnitOfWork
{
    IProductRepository _productRepository { get; }
    ITagRepository _categoryRepository { get; }
    
    Task SaveChangesAsync();
}