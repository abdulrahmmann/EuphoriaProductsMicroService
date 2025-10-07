using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Infrastructure.Configurations;

public class WishlistConfiguration: BaseEntityConfiguration<Wishlist>
{
    public override void Configure(EntityTypeBuilder<Wishlist> builder)
    {
        base.Configure(builder);

        builder.ToTable("Wishlists");
        
        builder.HasKey(temp => temp.Id).HasName("PK_WishlistId");

        builder.HasIndex(temp => temp.Id);
        
        builder
            .HasOne(w => w.Product)
            .WithMany(p => p.Wishlists)
            .HasForeignKey(w => w.ProductId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Wishlist_Product_Id");
    }
}