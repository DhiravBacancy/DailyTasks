using EFCoreDay2.Data;
using EFCoreDay2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all products with related OrderProducts and Orders
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products
            .Include(p => p.OrderProducts)
            .ThenInclude(op => op.Order) // Include the orders related to products
            .ToListAsync();
    }

    // Create a new product
    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetProducts), new { id = product.Id }, product);
    }
}
