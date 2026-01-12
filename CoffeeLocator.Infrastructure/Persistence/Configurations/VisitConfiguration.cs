using CoffeeLocator.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoffeeLocator.Infrastructure.Persistence.Configurations;

public class VisitConfiguration : IEntityTypeConfiguration<Visit>
{
    public void Configure(EntityTypeBuilder<Visit> builder)
    {
      
        builder.ToTable("Visits");

        builder.HasKey(v => v.Id);

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(v => v.UserId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne<CoffeeShop>()
            .WithMany()
            .HasForeignKey(v => v.CoffeeShopId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Property(v => v.Review)
            .HasMaxLength(1000); 

        builder.Property(v => v.Rating)
            .IsRequired(false);
    }
}