using EFCoreDay3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDay3.Configuratoins
{
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.HasKey(op => op.OrderProductId);

            builder.Property(op => op.Quantity)
                .IsRequired();

            builder.HasOne(o => o.Order)
                .WithMany(c => c.OrderProducts) // Ensure Customer entity has a navigation property
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: define delete behavior


            builder.HasOne(o => o.Product)
                .WithMany(c => c.OrderProducts) // Ensure Customer entity has a navigation property
                .HasForeignKey(o => o.ProductId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: define delete behavior
        }
    }
}
