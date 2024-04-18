using SBCW.DAL.Data;
using SBCW.DAL.Repositories.Interfaces;

namespace SBCW.DAL.Repositories;

public class UnitOfWork : IUnitOfWork
{
    protected readonly SearchContext context;
    
    public UnitOfWork(
        SearchContext context, 
        IProductRepository productRepository
        //ITagRepository tagRepository
        )
    {
        this.context = context;
        _productRepository = productRepository;
        //_tagRepository = tagRepository;
    }

    public IProductRepository _productRepository { get; }
    //public ITagRepository _tagRepository { get; }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}