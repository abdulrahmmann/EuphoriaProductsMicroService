using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a sub-category inside a main category.
/// </summary>
public class SubCategory : Entity<int>
{
    public string Name { get; private set; } = null!; // Tops, Printed T-shirts, Plain T-shirts, Kurti, Boxers ...
    public string Description { get; private set; } = null!;
    
    public int CategoryId { get; private set; }
    public Category Category { get; private set; } = null!;
    
    public ICollection<Product> Products { get; private set; } = new List<Product>();

    private SubCategory() { }

    private SubCategory(string name, string? description, int categoryId)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);

        Name = name;
        Description = description ?? string.Empty;
        CategoryId = categoryId;

        MarkCreated();
    }

    #region Create SubCategory
    /// <summary> Factory method to create SubCategory. </summary>
    public static SubCategory Create(string name, string? description, int categoryId)
        => new(name, description, categoryId);
    #endregion

    #region Update SubCategory
    /// <summary> Method to update SubCategory. </summary>
    public void Update(string? name, string? description, string? modifiedBy = null)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;

        if (!string.IsNullOrEmpty(description))
            Description = description;

        MarkModified(modifiedBy);
    }
    #endregion

    #region Soft Delete SubCategory
    /// <summary> Method to softly delete SubCategory. </summary>
    public void SoftDeleteSubCategory(string? deletedBy = null)
        => MarkDeleted(deletedBy);
    #endregion

    #region Restore Deleted SubCategory
    /// <summary> Method to restore softly deleted SubCategory. </summary>
    public void RestoreDeletedSubCategory(string? restoredBy = null)
        => MarkRestored(restoredBy);
    #endregion
}