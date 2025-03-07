using EFCoreDay2.Data;
using EFCoreDay2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public OrdersController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all orders with related Customer, OrderProducts, and Products
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
    {
        return await _context.Orders
            .Include(o => o.Customer) // Include the Customer related to the Order
            .Include(o => o.OrderProducts) // Include OrderProducts related to the Order
            .ThenInclude(op => op.Product) // Include Product in the OrderProduct relation
            .ToListAsync();
    }

    // Create a new order
    [HttpPost]
    public async Task<ActionResult<Order>> CreateOrder(Order order)
    {
        // Manually associate OrderProducts with the Order (if not already done)
        if (order.OrderProducts != null)
        {
            foreach (var orderProduct in order.OrderProducts)
            {
                _context.OrderProducts.Add(orderProduct);
            }
        }

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrders), new { id = order.Id }, order);
    }
}
