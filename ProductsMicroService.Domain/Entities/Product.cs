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

    public int BrandId { get; private set; }
    public Brand Brand { get; private set; } = null!;

    public ICollection<ProductVariant> Variants { get; private set; } = new List<ProductVariant>();
    public ICollection<Feedback> Feedbacks { get; private set; } = new List<Feedback>();
    public ICollection<Wishlist> Wishlists { get; private set; } = new List<Wishlist>();

    private Product() { }

    public Product(string name, string description, decimal price, int totalStock, ICollection<string> productImages, int categoryId, int subCategoryId, int brandId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(description);

        Name = name;
        Description = description;
        Price = price;
        TotalStock = totalStock;
        ProductImages = productImages;
        CategoryId = categoryId;
        SubCategoryId = subCategoryId;
        BrandId = brandId;

        MarkCreated();
    }

    #region Create
    /// <summary> Factory method to create a new Product. </summary>
    public static Product Create(string name, string description, decimal price, int totalStock, ICollection<string> productImages, int categoryId, int subCategoryId, int brandId)
        => new(name, description, price, totalStock, productImages, categoryId, subCategoryId, brandId);
    #endregion

    #region Update
    /// <summary> Updates an existing Product. </summary>
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
    #endregion
    
    #region Doft Delete
    /// <summary> Soft deletes the Product. </summary>
    public void SoftDeleteProduct(string? deletedBy = null) => MarkDeleted(deletedBy);
    #endregion

    #region Restore
    /// <summary> Restores a previously soft-deleted Product. </summary>
    public void RestoreDeletedProduct(string? restoredBy = null) => MarkRestored(restoredBy);
    #endregion
}
