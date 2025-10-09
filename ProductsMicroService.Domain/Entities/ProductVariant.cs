using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a product variant (color + size + stock).
/// </summary>
public class ProductVariant : Entity<int>
{
    public decimal? PriceOverride { get; private set; }
    public int Stock { get; private set; }

    public int ProductId { get; private set; }
    public Product Product { get; private set; } = null!;

    public int ColorId { get; private set; }
    public Color Color { get; private set; } = null!;

    public int SizeId { get; private set; }
    public Size Size { get; private set; } = null!;

    private ProductVariant() { }

    private ProductVariant(int productId, int colorId, int sizeId, int stock, decimal? priceOverride)
    {
        if (productId <= 0)
            throw new ArgumentOutOfRangeException(nameof(productId));
        if (colorId <= 0)
            throw new ArgumentOutOfRangeException(nameof(colorId));
        if (sizeId <= 0)
            throw new ArgumentOutOfRangeException(nameof(sizeId));
        if (stock < 0)
            throw new ArgumentOutOfRangeException(nameof(stock), "Stock cannot be negative.");

        ProductId = productId;
        ColorId = colorId;
        SizeId = sizeId;
        Stock = stock;
        PriceOverride = priceOverride;

        MarkCreated();
    }

    #region Create ProductVariant
    /// <summary> Factory method to create ProductVariant. </summary>
    public static ProductVariant Create(int productId, int colorId, int sizeId, int stock, decimal? priceOverride = null)
        => new(productId, colorId, sizeId, stock, priceOverride);
    #endregion

    #region Update ProductVariant
    /// <summary> Method to update ProductVariant. </summary>
    public void Update(int stock, decimal? priceOverride, int productId, int colorId, int sizeId, string? modifiedBy = null)
    {
        if (stock < 0)
            throw new ArgumentOutOfRangeException(nameof(stock), "Stock cannot be negative.");

        Stock = stock;
        PriceOverride = priceOverride ?? PriceOverride;
        ProductId = productId;
        ColorId = colorId;
        SizeId = sizeId;    

        MarkModified(modifiedBy);
    }
    #endregion

    #region Soft Delete ProductVariant
    /// <summary> Method to softly delete ProductVariant. </summary>
    public void SoftDelete(string? deletedBy = null)
        => MarkDeleted(deletedBy);
    #endregion

    #region Restore Deleted ProductVariant
    /// <summary> Method to restore softly deleted ProductVariant. </summary>
    public void Restore(string? restoredBy = null)
        => MarkRestored(restoredBy);
    #endregion
}
