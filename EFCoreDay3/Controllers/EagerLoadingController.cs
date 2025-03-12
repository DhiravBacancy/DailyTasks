using EFCoreDay3.Data;
using EFCoreDay3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EFCoreDay3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EagerLoadingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public EagerLoadingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("EagerLoadingQueries")]
        public async Task<ActionResult> GetEagerLoadingQueries()
        {
            var last30Days = DateTime.UtcNow.AddDays(-30);

            // Query 1: Retrieve all Customers with their related Orders and OrderProducts
            var query1Result = await _context.Customers
                .Include(c => c.Orders)
                    .ThenInclude(o => o.OrderProducts)
                        .ThenInclude(op => op.Product)
                .ToListAsync();

            // Query 2: Include only Orders placed in the last 30 days and only Products with stock greater than 20.
            var query2Result = await _context.Customers
                .Include(c => c.Orders.Where(o => o.OrderDate >= last30Days))
                    .ThenInclude(o => o.OrderProducts.Where(op => op.Product.Stock > 20))
                        .ThenInclude(op => op.Product)
                .ToListAsync();

            // Query 3: Fetch Products along with the total number of Orders they are associated with.
            var query3Result = await _context.Products
                .Include(p => p.OrderProducts)
                .Select(p => new
                {
                    Product = p,
                    OrderCount = p.OrderProducts.Count()
                })
                .ToListAsync();

            // Query 4: Retrieve Orders placed in the last month, including the related Customer but excluding OrderProducts.
            var query4Result = await _context.Orders
                .Where(o => o.OrderDate >= last30Days)
                .Include(o => o.Customer)
                .ToListAsync();

            var response = new StringBuilder();
            response.AppendLine("<html><body>");

            response.AppendLine("<h2>Query 1: All Customers, Orders, and OrderProducts</h2>");
            response.AppendLine(GenerateCustomerOrderProductTable(query1Result));

            response.AppendLine("<h2>Query 2: Orders last 30 days, Products stock > 20</h2>");
            response.AppendLine(GenerateCustomerOrderProductTable(query2Result));

            response.AppendLine("<h2>Query 3: Products with Order Count</h2>");
            response.AppendLine(GenerateProductOrderCountTable(query3Result));

            response.AppendLine("<h2>Query 4: Orders last month with Customer (no OrderProducts)</h2>");
            response.AppendLine(GenerateOrderCustomerTable(query4Result));

            response.AppendLine("</body></html>");

            return Content(response.ToString(), "text/html");
        }

        private string GenerateCustomerOrderProductTable(List<Customer> customers)
        {
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Customer ID</th><th>Customer Name</th><th>Order ID</th><th>Order Date</th><th>Product ID</th><th>Product Name</th><th>Quantity</th><th>Price</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var customer in customers)
            {
                if (customer.Orders != null && customer.Orders.Any())
                {
                    foreach (var order in customer.Orders)
                    {
                        if (order.OrderProducts != null && order.OrderProducts.Any())
                        {
                            foreach (var orderProduct in order.OrderProducts)
                            {
                                tableBuilder.Append("<tr>");
                                tableBuilder.Append($"<td>{customer.CustomerId}</td>");
                                tableBuilder.Append($"<td>{customer.Name}</td>");
                                tableBuilder.Append($"<td>{order.OrderId}</td>");
                                tableBuilder.Append($"<td>{order.OrderDate}</td>");
                                tableBuilder.Append($"<td>{orderProduct.ProductId}</td>");
                                tableBuilder.Append($"<td>{orderProduct.Product?.Name}</td>");
                                tableBuilder.Append($"<td>{orderProduct.Quantity}</td>");
                                tableBuilder.Append($"<td>{orderProduct.Product?.Price}</td>");
                                tableBuilder.Append("</tr>");
                            }
                        }
                        else
                        {
                            tableBuilder.Append("<tr>");
                            tableBuilder.Append($"<td>{customer.CustomerId}</td>");
                            tableBuilder.Append($"<td>{customer.Name}</td>");
                            tableBuilder.Append($"<td>{order.OrderId}</td>");
                            tableBuilder.Append($"<td>{order.OrderDate}</td>");
                            tableBuilder.Append($"<td colspan='4'>No Products</td>");
                            tableBuilder.Append("</tr>");
                        }
                    }
                }
                else
                {
                    tableBuilder.Append("<tr>");
                    tableBuilder.Append($"<td>{customer.CustomerId}</td>");
                    tableBuilder.Append($"<td>{customer.Name}</td>");
                    tableBuilder.Append($"<td colspan='6'>No Orders</td>");
                    tableBuilder.Append("</tr>");
                }
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }

        private string GenerateProductOrderCountTable(IEnumerable<dynamic> products)
        {
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Product ID</th><th>Product Name</th><th>Order Count</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var product in products)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{product.Product.ProductId}</td>");
                tableBuilder.Append($"<td>{product.Product.Name}</td>");
                tableBuilder.Append($"<td>{product.OrderCount}</td>");
                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }

        private string GenerateOrderCustomerTable(List<Order> orders)
        {
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Order ID</th><th>Order Date</th><th>Customer ID</th><th>Customer Name</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var order in orders)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{order.OrderId}</td>");
                tableBuilder.Append($"<td>{order.OrderDate}</td>");
                tableBuilder.Append($"<td>{order.CustomerId}</td>");
                tableBuilder.Append($"<td>{order.Customer?.Name}</td>");
                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }
    }
}