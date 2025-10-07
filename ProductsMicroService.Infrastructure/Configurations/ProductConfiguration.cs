using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Infrastructure.Configurations;

public class ProductConfiguration: BaseEntityConfiguration<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Products");
        
        builder.HasKey(temp => temp.Id).HasName("PK_ProductId");
        
        builder.HasIndex(temp => temp.Name);
        
        builder.Property(temp => temp.Name).HasColumnName("ProductName").HasMaxLength(60);
        
        builder.Property(temp => temp.Description).HasColumnName("ProductDescription").HasMaxLength(1000);
        
        builder.Property(temp => temp.Price).HasColumnName("ProductPrice").HasPrecision(18, 2);
        
        
        builder
            .HasOne(c => c.Category)
            .WithMany(p => p.Products)
            .HasForeignKey(c => c.CategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Product_Category_Id");
        
        builder
            .HasOne(c => c.SubCategory)
            .WithMany(p => p.Products)
            .HasForeignKey(c => c.SubCategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Product_SubCategory_Id");
        
        builder
            .HasOne(c => c.MainCategory)
            .WithMany(p => p.Products)
            .HasForeignKey(c => c.MainCategoryId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Product_MainCategory_Id");
    }
}