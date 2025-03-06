using EFCoreDay1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // Define DbSets (tables)
    public DbSet<Product> Products { get; set; }
}
