using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a product category.
/// </summary>
public class Category: Entity<int>
{
    public string Name { get; private set; } = null!; // Men, Women, Kids
    public string? Description { get; private set; } = null!;
    
    public ICollection<Product> Products { get; private set; } = new List<Product>();
    
    public ICollection<SubCategory> SubCategories { get; private set; } = new List<SubCategory>();

    private Category() {}
    
    private Category(string name, string? description)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
        Description = description;
        
        MarkCreated();
    }
    
    #region Create Category
    /// <summary> Factory method to create Category. </summary>
    public static Category Create(string name, string? description) 
        => new(name, description);
    #endregion
    
    #region Update Category
    /// <summary> method to update Category. </summary>
    public void Update(string name, string? description, string? modifiedBy = null)
    {
        if (!string.IsNullOrEmpty(name)) Name = name;
        if (!string.IsNullOrEmpty(description)) Description = description;
        
        MarkModified(modifiedBy);
    }
    #endregion
    
    #region Soft Delete Category
    /// <summary> method to softly delete Category. </summary>
    public void SoftDelete(string? deletedBy = null) => MarkDeleted(deletedBy);
    #endregion
    
    #region Restore Deleted Category
    /// <summary> method to restore softly deleted Category. </summary>
    public void Restor(string? restoredBy = null) => MarkRestored(restoredBy);
    #endregion
}