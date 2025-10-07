using Microsoft.EntityFrameworkCore;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Infrastructure.Context;

public class ApplicationDbContext: DbContext
{
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<SubCategory> SubCategories { get; set; }
    
    public DbSet<MainCategory> MainCategories { get; set; }
    
    public DbSet<Size> Sizes { get; set; }
    
    public DbSet<Color> Colors { get; set; }
    
    public DbSet<Brand> Brands { get; set; }
    
    public DbSet<Product> Products { get; set; }
    
    public DbSet<ProductVariant> ProductVariants { get; set; }
    
    public DbSet<Wishlist> Wishlists { get; set; }
    
    public DbSet<Feedback> Feedbacks { get; set; }
    
    protected ApplicationDbContext() { }
    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        builder.Entity<Category>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<SubCategory>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<MainCategory>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Size>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Color>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Brand>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Product>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<ProductVariant>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Wishlist>().HasQueryFilter(u => !u.IsDeleted);
        builder.Entity<Feedback>().HasQueryFilter(u => !u.IsDeleted);
    }
}