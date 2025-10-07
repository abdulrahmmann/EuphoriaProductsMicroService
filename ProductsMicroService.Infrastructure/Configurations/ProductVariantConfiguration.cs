using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Infrastructure.Configurations;

public class ProductVariantConfiguration: BaseEntityConfiguration<ProductVariant>
{
    public override void Configure(EntityTypeBuilder<ProductVariant> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("ProductVariants");
        
        builder.HasKey(temp => temp.Id).HasName("PK_ProductVariantId");
        
        builder.Property(temp => temp.PriceOverride).HasColumnName("ProductPrice").HasPrecision(18, 2);
        
        builder
            .HasOne(p => p.Product)
            .WithMany(v => v.Variants)
            .HasForeignKey(p => p.ProductId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Variant_Product_Id");
        
        builder
            .HasOne(v => v.Size)
            .WithMany(s => s.Variants)
            .HasForeignKey(v => v.SizeId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Variant_Size_Id");
        
        builder
            .HasOne(c => c.Color)
            .WithMany(pv => pv.Variants)
            .HasForeignKey(c => c.ColorId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Variant_Color_Id");
    }
}