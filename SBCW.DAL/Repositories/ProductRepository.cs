using Microsoft.EntityFrameworkCore;
using SBCW.DAL.Data;
using SBCW.DAL.Models;
using SBCW.DAL.Repositories.Interfaces;

namespace SBCW.DAL.Repositories;

public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(SearchContext context) : base(context)
    {
    }

    public override Task AddAsync(Product entity)
    {
        entity.Id = Guid.NewGuid();
        return base.AddAsync(entity);
    }
}