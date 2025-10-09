using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a product brand.
/// </summary>
public class Brand : Entity<int>
{
    public string Name { get; private set; } = null!;

    private Brand() { }
    private Brand(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;
        
        MarkCreated();
    }

    #region Create Brand
    /// <summary> Factory method to create Brand. </summary>
    public static Brand Create(string name) => new(name);
    #endregion

    #region Update Brand 
    /// <summary> method to update Brand. </summary>
    public void Update(string name, string? modifiedBy = null)
    {
        if (!string.IsNullOrEmpty(name))
        {
            Name = name;
            
            MarkModified(modifiedBy);
        }
    }
    #endregion

    #region Soft Delete Brand
    /// <summary> method to softly delete Brand. </summary>
    public void SoftDelete(string? deletedBy = null) => MarkDeleted(deletedBy);
    #endregion
    
    #region Restore Deleted Brand
    /// <summary> method to restore softly deleted Brand. </summary>
    public void Restore(string? restoredBy = null) => MarkRestored(restoredBy);
    #endregion    
}
