using Microsoft.EntityFrameworkCore;
using System;

namespace EFCoreWithoutDIApp
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        // OnConfiguring sets up the DbContext without DI (Dependency Injection)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=DHIRAV\\SQLEXPRESS01;Database=EFCoreDemo;User Id=Dhirav;Password=1606;");
            }
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
