using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a user's wishlist item.
/// </summary>
public class Wishlist : Entity<int>
{
    public int UserId { get; private set; }
    public int ProductId { get; private set; }
    public Product? Product { get; private set; } = null!;

    private Wishlist() { }

    private Wishlist(int userId, int productId)
    {
        if (userId <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be a positive integer.");

        if (productId <= 0)
            throw new ArgumentOutOfRangeException(nameof(productId), "ProductId must be a positive integer.");

        UserId = userId;
        ProductId = productId;

        MarkCreated();
    }

    #region Create Wishlist
    /// <summary> Factory method to create a Wishlist item. </summary>
    public static Wishlist Create(int userId, int productId)
        => new(userId, productId);
    #endregion

    #region Update Wishlist
    /// <summary> Method to update Wishlist item. </summary>
    public void Update(int userId, int productId, string? modifiedBy = null)
    {
        if (userId <= 0)
            throw new ArgumentOutOfRangeException(nameof(userId), "UserId must be a positive integer.");

        if (productId <= 0)
            throw new ArgumentOutOfRangeException(nameof(productId), "ProductId must be a positive integer.");

        UserId = userId;
        ProductId = productId;

        MarkModified(modifiedBy);
    }
    #endregion

    #region Soft Delete Wishlist
    /// <summary> Method to softly delete Wishlist item. </summary>
    public void SoftDelete(string? deletedBy = null)
        => MarkDeleted(deletedBy);
    #endregion

    #region Restore Deleted Wishlist
    /// <summary> Method to restore softly deleted Wishlist item. </summary>
    public void Restore(string? restoredBy = null)
        => MarkRestored(restoredBy);
    #endregion
}