using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Infrastructure.Configurations;

public class BrandConfiguration: BaseEntityConfiguration<Brand>
{
    public override void Configure(EntityTypeBuilder<Brand> builder)
    {
        base.Configure(builder);

        builder.ToTable("Brands");
        
        builder.HasKey(temp => temp.Id).HasName("PK_BrandId");

        builder.HasIndex(temp => temp.Name);
        
        builder.Property(temp => temp.Name).IsRequired().HasColumnName("BrandName").HasMaxLength(60);
    }
}