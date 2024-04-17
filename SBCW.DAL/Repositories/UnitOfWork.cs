using SBCW.DAL.Data;
using SBCW.DAL.Repositories.Interfaces;

namespace SBCW.DAL.Repositories;

public class UnitOfWork
{
    protected readonly SearchContext context;

    public IProductRepository productRepository { get; }
    public ITagRepository tagRepository { get; }
    
    public UnitOfWork(
        SearchContext context, 
        IProductRepository productRepository, 
        ITagRepository tagRepository
        )
    {
        this.context = context;
        this.productRepository = productRepository;
        this.tagRepository = tagRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}