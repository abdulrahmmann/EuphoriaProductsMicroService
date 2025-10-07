using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

public class Product : Entity<int>
{
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public decimal Price { get; private set; }
    public int TotalStock { get; private set; }
    public ICollection<string> ProductImages { get; private set; } = new List<string>();

    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;

    public int SubCategoryId { get; private set; }
    public SubCategory SubCategory { get; private set; } = null!;
    
    public int MainCategoryId { get; private set; }
    public MainCategory MainCategory { get; private set; } = null!;

    public int BrandId { get; private set; }
    public Brand Brand { get; private set; } = null!;

    public ICollection<ProductVariant> Variants { get; private set; } = new List<ProductVariant>();
    public ICollection<Feedback> Feedbacks { get; private set; } = new List<Feedback>();
    public ICollection<Wishlist> Wishlists { get; private set; } = new List<Wishlist>();

    private Product() { }

    private Product(string name, string description, decimal price, int totalStock, int categoryId, int subCategoryId, int brandId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(description);

        Name = name;
        Description = description;
        Price = price;
        TotalStock = totalStock;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        BrandId = brandId;

        MarkCreated();
    }

    public static Product Create(string name, string description, decimal price, int totalStock, int categoryId, int subCategoryId, int brandId)
        => new(name, description, price, totalStock, categoryId, subCategoryId, brandId);

    public void UpdateProduct(string? name, string? description, decimal? price, int? totalStock, int? categoryId, int? brandId, string? modifiedBy = null)
    {
        if (!string.IsNullOrEmpty(name)) Name = name;
        if (!string.IsNullOrEmpty(description)) Description = description;
        if (price.HasValue) Price = price.Value;
        if (totalStock.HasValue) TotalStock = totalStock.Value;
        if (categoryId.HasValue) CategoryId = categoryId.Value;
        if (brandId.HasValue) BrandId = brandId.Value;

        MarkModified(modifiedBy);
    }

    public void SoftDeleteProduct(string? deletedBy = null) => MarkDeleted(deletedBy);

    public void RestoreDeletedProduct(string? restoredBy = null) => MarkRestored(restoredBy);
}
