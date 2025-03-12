using EFCoreDay3.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCoreDay3.Configuratoins
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(p => p.ProductId);

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Price)
                .IsRequired();

            builder.Property(p => p.Stock)
                .IsRequired();

        }
    }
}
