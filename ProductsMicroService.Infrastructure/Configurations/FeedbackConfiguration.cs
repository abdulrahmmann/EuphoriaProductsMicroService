using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductsMicroService.Domain.Entities;

namespace ProductsMicroService.Infrastructure.Configurations;

public class FeedbackConfiguration: BaseEntityConfiguration<Feedback>
{
    public override void Configure(EntityTypeBuilder<Feedback> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("Feedbacks");
        
        builder.HasKey(temp => temp.Id).HasName("PK_FeedbackId");

        builder.HasIndex(temp => temp.Rating);
        
        builder.Property(f => f.Rating).HasDefaultValue(1);

        builder.Property(temp => temp.Comment).HasMaxLength(600);
        
        builder
            .HasOne(f => f.Product)
            .WithMany(p => p.Feedbacks)
            .HasForeignKey(f => f.ProductId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_Feedback_Product_Id");
    }
}