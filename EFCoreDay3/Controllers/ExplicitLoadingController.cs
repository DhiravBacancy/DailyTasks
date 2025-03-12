using EFCoreDay3.Data;
using EFCoreDay3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreDay3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExplicitLoadingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ExplicitLoadingController> _logger;

        public ExplicitLoadingController(ApplicationDbContext context, ILogger<ExplicitLoadingController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("ExplicitLoadingQueries")]
        public async Task<ActionResult> GetExplicitLoadingQueries()
        {
            var response = new StringBuilder();
            response.AppendLine("<html><body>");

            // Query 1: Explicitly load Orders if CreatedDate is within the past year
            response.AppendLine("<h2>Query 1: Explicitly load Orders (CreatedDate within past year)</h2>");
            response.AppendLine(await GetCustomerWithConditionalOrders());

            // Query 2: Retrieve Orders without OrderProducts, load on demand
            response.AppendLine("<h2>Query 2: Orders with On-Demand OrderProducts</h2>");
            response.AppendLine(await GetOrdersWithOnDemandOrderProducts());

            // Query 3: Explicitly load Orders for Products with Stock < 10
            response.AppendLine("<h2>Query 3: Products with Conditional Orders (Stock < 10)</h2>");
            response.AppendLine(await GetProductsWithConditionalOrders());

            // Query 4: Eager load Customers with Orders, then explicitly load OrderProducts
            response.AppendLine("<h2>Query 4: Customers with Orders, then Explicit OrderProducts</h2>");
            response.AppendLine(await GetCustomersWithOrdersAndExplicitOrderProducts());

            response.AppendLine("</body></html>");
            return Content(response.ToString(), "text/html");
        }

        private async Task<string> GetCustomerWithConditionalOrders()
        {
            var customer = await _context.Customers.FirstOrDefaultAsync();
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Customer ID</th><th>Customer Name</th><th>Order IDs</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            if (customer != null)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{customer.CustomerId}</td>");
                tableBuilder.Append($"<td>{customer.Name}</td>");

                if (customer.CreatedDate > DateTime.UtcNow.AddYears(-1))
                {
                    await _context.Entry(customer).Collection(c => c.Orders).LoadAsync();
                    if (customer.Orders != null)
                    {
                        var orderIds = string.Join(", ", customer.Orders.Select(o => o.OrderId));
                        tableBuilder.Append($"<td>{orderIds}</td>");
                    }
                    else
                    {
                        tableBuilder.Append("<td>No Orders</td>");
                    }
                }
                else
                {
                    tableBuilder.Append("<td>Orders not loaded (CreatedDate too old)</td>");
                }
                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }

        private async Task<string> GetOrdersWithOnDemandOrderProducts()
        {
            var orders = await _context.Orders.ToListAsync();
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Order ID</th><th>Customer ID</th><th>Order Product IDs (Load on Demand)</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var order in orders)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{order.OrderId}</td>");
                tableBuilder.Append($"<td>{order.CustomerId}</td>");

                // Explicitly load OrderProducts (on demand)
                await _context.Entry(order).Collection(o => o.OrderProducts).LoadAsync();

                if (order.OrderProducts != null && order.OrderProducts.Any())
                {
                    var orderProductIds = string.Join(", ", order.OrderProducts.Select(op => op.OrderProductId));
                    tableBuilder.Append($"<td>{orderProductIds}</td>");
                }
                else
                {
                    tableBuilder.Append("<td>No Order Products</td>");
                }

                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }

        private async Task<string> GetProductsWithConditionalOrders()
        {
            var products = await _context.Products.ToListAsync();
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Product ID</th><th>Product Name</th><th>Order IDs</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var product in products)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{product.ProductId}</td>");
                tableBuilder.Append($"<td>{product.Name}</td>");

                if (product.Stock < 10)
                {
                    await _context.Entry(product).Collection(p => p.OrderProducts).LoadAsync();
                    if (product.OrderProducts != null)
                    {
                        var orderIds = string.Join(", ", product.OrderProducts.Select(op => op.OrderId));
                        tableBuilder.Append($"<td>{orderIds}</td>");
                    }
                    else
                    {
                        tableBuilder.Append("<td>No Orders</td>");
                    }
                }
                else
                {
                    tableBuilder.Append("<td>Orders not loaded (Stock >= 10)</td>");
                }

                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }

        private async Task<string> GetCustomersWithOrdersAndExplicitOrderProducts()
        {
            var customers = await _context.Customers.Include(c => c.Orders).ToListAsync();
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Customer ID</th><th>Customer Name</th><th>Order Product IDs</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var customer in customers)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{customer.CustomerId}</td>");
                tableBuilder.Append($"<td>{customer.Name}</td>");

                var orderProductIds = new StringBuilder();

                if (customer.Orders != null)
                {
                    foreach (var order in customer.Orders)
                    {
                        await _context.Entry(order).Collection(o => o.OrderProducts).LoadAsync();
                        if (order.OrderProducts != null && order.OrderProducts.Any())
                        {
                            orderProductIds.Append(string.Join(", ", order.OrderProducts.Select(op => op.OrderProductId))).Append(", ");
                        }
                    }
                }
                tableBuilder.Append($"<td>{orderProductIds.ToString().TrimEnd(',', ' ')}</td>");
                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }
    }
}