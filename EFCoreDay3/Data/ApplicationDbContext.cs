using Microsoft.EntityFrameworkCore;
using EFCoreDay3.Models;
using System;

namespace EFCoreDay3.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<OrderProduct> OrderProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seeding Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "John Doe", Email = "john@example.com", CreatedDate = new DateTime(2024, 01, 15), IsVIP = true },
                new Customer { CustomerId = 2, Name = "Alice Smith", Email = "alice@example.com", CreatedDate = new DateTime(2024, 02, 01), IsVIP = false },
                new Customer { CustomerId = 3, Name = "Robert Brown", Email = "robert@example.com", CreatedDate = new DateTime(2023, 12, 10), IsVIP = true }
            );

            // Seeding Products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Laptop", Price = 1200.99m, Stock = 10 },
                new Product { ProductId = 2, Name = "Smartphone", Price = 799.49m, Stock = 30 },
                new Product { ProductId = 3, Name = "Tablet", Price = 499.99m, Stock = 25 },
                new Product { ProductId = 4, Name = "Headphones", Price = 199.99m, Stock = 50 }
            );

            // Seeding Orders with fixed dates
            modelBuilder.Entity<Order>().HasData(
                new Order { OrderId = 1, OrderDate = new DateTime(2024, 02, 15), CustomerId = 1, IsDeleted = false },
                new Order { OrderId = 2, OrderDate = new DateTime(2024, 02, 20), CustomerId = 2, IsDeleted = false },
                new Order { OrderId = 3, OrderDate = new DateTime(2024, 01, 05), CustomerId = 1, IsDeleted = false },
                new Order { OrderId = 4, OrderDate = new DateTime(2024, 01, 25), CustomerId = 3, IsDeleted = false }
            );

            // Seeding OrderProducts (Many-to-Many Relationship)
            modelBuilder.Entity<OrderProduct>().HasData(
                new OrderProduct { OrderProductId = 1, OrderId = 1, ProductId = 1, Quantity = 2 },
                new OrderProduct { OrderProductId = 2, OrderId = 1, ProductId = 2, Quantity = 1 },
                new OrderProduct { OrderProductId = 3, OrderId = 2, ProductId = 2, Quantity = 3 },
                new OrderProduct { OrderProductId = 4, OrderId = 2, ProductId = 3, Quantity = 4 },
                new OrderProduct { OrderProductId = 5, OrderId = 3, ProductId = 4, Quantity = 6 },
                new OrderProduct { OrderProductId = 6, OrderId = 4, ProductId = 1, Quantity = 1 }
            );
        }


    }
}