using Microsoft.EntityFrameworkCore;
using System;

namespace EfCoreDIExample.Data
{
    public class AppDbContext : DbContext
    {
        private readonly Guid _instanceId;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            _instanceId = Guid.NewGuid(); // Assign unique ID to each instance
            Console.WriteLine($"AppDbContext Instance Created: {_instanceId}");
        }

        public DbSet<Customer> Students { get; set; }

        public Guid GetInstanceId()
        {
            return _instanceId;
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
