using Microsoft.EntityFrameworkCore;
using SBCW.DAL.Configurations;

namespace SBCW.DAL.Data;

public class SBCWContext : DbContext
{
    public SBCWContext(DbContextOptions<SBCWContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Models.Product> Products { get; set; } = null!;
    public DbSet<Models.Tag> Tags { get; set; } = null!;
    public DbSet<Models.ProductTag> ProductTags { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ProductConfiguration());
        modelBuilder.ApplyConfiguration(new TagConfiguration());
        
        modelBuilder.Entity<Models.ProductTag>()
            .HasKey(pt => new { pt.ProductId, pt.WordId });

        modelBuilder.Entity<Models.ProductTag>()
            .HasOne(pt => pt.Product)
            .WithMany(p => p.ProductTags)
            .HasForeignKey(pt => pt.ProductId);

        modelBuilder.Entity<Models.ProductTag>()
            .HasOne(pt => pt.Tag)
            .WithMany(t => t.ProductTags)
            .HasForeignKey(pt => pt.WordId);
    }
}