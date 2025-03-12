using EFCoreDay3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDay3.Configuratoins
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.OrderId);

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.HasOne(o => o.Customer)
                .WithMany(c => c.Orders) // Ensure Customer entity has a navigation property
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); // Optional: define delete behavior
        }
    }
}
