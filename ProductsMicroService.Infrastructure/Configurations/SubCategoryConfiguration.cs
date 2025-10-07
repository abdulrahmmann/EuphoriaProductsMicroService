using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Infrastructure.Configurations;

public class SubCategoryConfiguration: BaseEntityConfiguration<SubCategory>
{
    public override void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        base.Configure(builder);

        builder.ToTable("SubCategories");
        
        builder.HasKey(x => x.Id).HasName("PK_SubCategoryId");

        builder.HasIndex(temp => temp.Name);
        
        builder.Property(temp => temp.Name).IsRequired().HasColumnName("SubCategoryName").HasMaxLength(30);
        
        builder.Property(temp => temp.Description).IsRequired().HasColumnName("SubCategoryDescription").HasMaxLength(400);
    }
}