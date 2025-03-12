﻿// <auto-generated />
using System;
using EFCoreDay3.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFCoreDay3.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EFCoreDay3.Models.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsVIP")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            CustomerId = 1,
                            CreatedDate = new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "john@example.com",
                            IsVIP = true,
                            Name = "John Doe"
                        },
                        new
                        {
                            CustomerId = 2,
                            CreatedDate = new DateTime(2024, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "alice@example.com",
                            IsVIP = false,
                            Name = "Alice Smith"
                        },
                        new
                        {
                            CustomerId = 3,
                            CreatedDate = new DateTime(2023, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "robert@example.com",
                            IsVIP = true,
                            Name = "Robert Brown"
                        });
                });

            modelBuilder.Entity("EFCoreDay3.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            CustomerId = 1,
                            IsDeleted = false,
                            OrderDate = new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            OrderId = 2,
                            CustomerId = 2,
                            IsDeleted = false,
                            OrderDate = new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            OrderId = 3,
                            CustomerId = 1,
                            IsDeleted = false,
                            OrderDate = new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            OrderId = 4,
                            CustomerId = 3,
                            IsDeleted = false,
                            OrderDate = new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });

            modelBuilder.Entity("EFCoreDay3.Models.OrderProduct", b =>
                {
                    b.Property<int>("OrderProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderProductId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("OrderProductId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");

                    b.HasData(
                        new
                        {
                            OrderProductId = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            OrderProductId = 2,
                            OrderId = 1,
                            ProductId = 2,
                            Quantity = 1
                        },
                        new
                        {
                            OrderProductId = 3,
                            OrderId = 2,
                            ProductId = 2,
                            Quantity = 3
                        },
                        new
                        {
                            OrderProductId = 4,
                            OrderId = 2,
                            ProductId = 3,
                            Quantity = 4
                        },
                        new
                        {
                            OrderProductId = 5,
                            OrderId = 3,
                            ProductId = 4,
                            Quantity = 6
                        },
                        new
                        {
                            OrderProductId = 6,
                            OrderId = 4,
                            ProductId = 1,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("EFCoreDay3.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            Name = "Laptop",
                            Price = 1200.99m,
                            Stock = 10
                        },
                        new
                        {
                            ProductId = 2,
                            Name = "Smartphone",
                            Price = 799.49m,
                            Stock = 30
                        },
                        new
                        {
                            ProductId = 3,
                            Name = "Tablet",
                            Price = 499.99m,
                            Stock = 25
                        },
                        new
                        {
                            ProductId = 4,
                            Name = "Headphones",
                            Price = 199.99m,
                            Stock = 50
                        });
                });

            modelBuilder.Entity("EFCoreDay3.Models.Order", b =>
                {
                    b.HasOne("EFCoreDay3.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("EFCoreDay3.Models.OrderProduct", b =>
                {
                    b.HasOne("EFCoreDay3.Models.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFCoreDay3.Models.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EFCoreDay3.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("EFCoreDay3.Models.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("EFCoreDay3.Models.Product", b =>
                {
                    b.Navigation("OrderProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
