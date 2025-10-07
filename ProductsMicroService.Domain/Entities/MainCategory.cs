using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a top-level product category (Men, Women, Kids).
/// </summary>
public class MainCategory: Entity<int>
{
    /// <summary>
    /// The main category name (e.g., Men, Women, Kids).
    /// </summary>
    public string Name { get; private set; } = null!;

    /// <summary>
    /// Optional description of the main category.
    /// </summary>
    public string? Description { get; private set; }

    /// <summary>
    /// Collection of products belonging to this main category.
    /// </summary>
    public ICollection<Product> Products { get; private set; } = new List<Product>();

    /// <summary>
    /// Collection of sub-categories linked to this main category.
    /// </summary>
    public ICollection<Category> Categories { get; private set; } = new List<Category>();

    private MainCategory() { }

    private MainCategory(string name, string? description)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
        Description = description;

        MarkCreated();
    }

    #region Factory Method
    /// <summary> Factory method to create a new MainCategory. </summary>
    public static MainCategory CreateMainCategory(string name, string? description)
        => new(name, description);
    #endregion

    #region Update
    /// <summary> Updates the main category name or description. </summary>
    public void Update(string name, string? description, string? modifiedBy = null)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;

        if (!string.IsNullOrEmpty(description))
            Description = description;

        MarkModified(modifiedBy);
    }
    #endregion

    #region Soft Delete
    /// <summary> Soft deletes the main category. </summary>
    public void SoftDeleteMainCategory(string? deletedBy = null) => MarkDeleted(deletedBy);
    #endregion

    #region Restore
    /// <summary> Restores a soft-deleted main category. </summary>
    public void RestoreDeletedMainCategory(string? restoredBy = null) => MarkRestored(restoredBy);
    #endregion
}