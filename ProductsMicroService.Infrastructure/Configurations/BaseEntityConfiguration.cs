using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsMicroService.Domain.Bases;

namespace ProductsMicroService.Infrastructure.Configurations;

/// <summary>
/// Base configuration for all entities inheriting from Entity&lt;int&gt;.
/// Applies shared configurations compatible with MySQL (Pomelo EF Core).
/// </summary>
public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : Entity<int>
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        // Primary key
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id)
            .ValueGeneratedOnAdd();

        // Audit columns
        builder.Property(e => e.CreatedAt)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(e => e.ModifiedAt)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(e => e.DeletedAt)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(e => e.RestoredAt)
            .HasColumnType("datetime")
            .IsRequired(false);

        builder.Property(e => e.CreatedBy)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.ModifiedBy)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.DeletedBy)
            .HasMaxLength(100)
            .IsRequired(false);

        builder.Property(e => e.RestoredBy)
            .HasMaxLength(100)
            .IsRequired(false);

        // Soft delete
        builder.Property(e => e.IsDeleted)
            .HasColumnType("bit")
            .HasDefaultValue(false);

        builder.HasQueryFilter(e => !e.IsDeleted);
    }
}