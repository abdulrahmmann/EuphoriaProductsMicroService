using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a product feedback or review.
/// </summary>
public class Feedback : Entity<int>
{ 
    public int Rating { get; private set; }
    public string Comment { get; private set; } = null!;

    public int ProductId { get; private set; }
    public Product? Product { get; private set; }

    private Feedback() { }

    private Feedback(int rating, string comment, int productId)
        {
            if (rating < 1 || rating > 5)
                throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

            ArgumentException.ThrowIfNullOrEmpty(comment);

            Rating = rating;
            Comment = comment;
            ProductId = productId;

            MarkCreated();
        }
    
    #region Factory Method
    /// <summary> Factory method to create a new Feedback. </summary>
    public static Feedback Create(int rating, string comment, int productId)
        => new(rating, comment, productId);
    #endregion

    #region Update
    /// <summary> Updates an existing feedback. </summary>
    public void Update(int rating, string comment, string? modifiedBy = null)
    {
        if (rating < 1 || rating > 5)
            throw new ArgumentOutOfRangeException(nameof(rating), "Rating must be between 1 and 5.");

        ArgumentException.ThrowIfNullOrEmpty(comment);

        Rating = rating;
        Comment = comment;

        MarkModified(modifiedBy);
    }
    #endregion

    #region Soft Delete
    /// <summary> Soft deletes the feedback. </summary>
    public void SoftDelete(string? deletedBy = null) => MarkDeleted(deletedBy);
    #endregion

    #region Restore
    /// <summary> Restores a previously soft-deleted feedback. </summary>
    public void Restore(string? restoredBy = null) => MarkRestored(restoredBy);
    #endregion
}