using Microsoft.EntityFrameworkCore;
using SBCW.DAL.Configurations;

namespace SBCW.DAL.Data;

public class SearchContext : DbContext
{
    public SearchContext(DbContextOptions<SearchContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Models.Product> Products { get; set; }
    public DbSet<Models.Tag> Tags { get; set; }
    public DbSet<Models.ProductTag> ProductTags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        modelBuilder.ApplyConfiguration(new ProductTagConfiguration());
    }
}