using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a product size.
/// </summary>
public class Size : Entity<int>
{
    public string Name { get; private set; } = null!;

    public string SizeType { get; private set; } = null!;
    public ICollection<ProductVariant> Variants { get; private set; } = new List<ProductVariant>();

    private Size() { }

    private Size(string name)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        Name = name;

        MarkCreated();
    }

    #region Create Size
    /// <summary> Factory method to create Size. </summary>
    public static Size Create(string name)
        => new(name);
    #endregion

    #region Update Size
    /// <summary> Method to update Size. </summary>
    public void Update(string name, string? modifiedBy = null)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;

        MarkModified(modifiedBy);
    }
    #endregion

    #region Soft Delete Size
    /// <summary> Method to softly delete Size. </summary>
    public void SoftDelete(string? deletedBy = null)
        => MarkDeleted(deletedBy);
    #endregion

    #region Restore Deleted Size
    /// <summary> Method to restore softly deleted Size. </summary>
    public void Restore(string? restoredBy = null)
        => MarkRestored(restoredBy);
    #endregion
}