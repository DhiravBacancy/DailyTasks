using EFCoreDay2.Data;
using EFCoreDay2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreDay2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            var customers = await _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(op => op.Product) // Include Products for OrderProducts
                .ToListAsync();

            return Ok(customers);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCustomers), new { id = customer.Id }, customer);
        }
    }
}
