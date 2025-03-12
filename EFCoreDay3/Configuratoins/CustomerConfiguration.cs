using EFCoreDay3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDay3.Configuratoins
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(c => c.CustomerId);

            builder.Property(c => c.Name)
                .IsRequired();

            builder.Property(c => c.Email)
                .IsRequired();
            builder.HasIndex(c => c.Email)
                .IsUnique();

            
            builder.Property(c => c.CreatedDate)
                .IsRequired();


            builder.HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
