using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Domain.Entities;

/// <summary>
/// Represents a product color. 
/// </summary>
public class Color : Entity<int>
{
    public string Name { get; private set; } = null!;
    public string HexCode { get; private set; } = null!;

    public ICollection<ProductVariant> Variants { get; private set; } = new List<ProductVariant>();

    private Color() { }

    private Color(string name, string hexCode)
    {
        ArgumentException.ThrowIfNullOrEmpty(name);
        ArgumentException.ThrowIfNullOrEmpty(hexCode);

        Name = name;
        HexCode = hexCode;

        MarkCreated();
    }

    #region Factory Method
    /// <summary> Factory method to create a new color. </summary>
    public static Color CreateColor(string name, string hexCode)
        => new(name, hexCode);
    #endregion

    #region Update
    /// <summary> Updates the color information. </summary>
    public void UpdateColor(string name, string hexCode, string? modifiedBy = null)
    {
        if (!string.IsNullOrEmpty(name))
            Name = name;

        if (!string.IsNullOrEmpty(hexCode))
            HexCode = hexCode;

        MarkModified(modifiedBy);
    }
    #endregion

    #region Soft Delete
    /// <summary> Soft deletes this color. </summary>
    public void SoftDeleteColor(string? deletedBy = null) => MarkDeleted(deletedBy);
    #endregion

    #region Restore
    /// <summary> Restores a soft-deleted color. </summary>
    public void RestoreDeletedColor(string? restoredBy = null) => MarkRestored(restoredBy);
    #endregion
}

