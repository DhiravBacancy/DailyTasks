using EFCoreDay3.Data;
using EFCoreDay3.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EFCoreDay3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LazyLoadingController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LazyLoadingController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("LazyLoadingQueries")]
        public async Task<ActionResult> GetLazyLoadingQueries()
        {
            var response = new StringBuilder();
            response.AppendLine("<html><body>");

            response.AppendLine("<h2>Query 1: Customers with Lazy Loaded Orders</h2>");
            response.AppendLine(await GetCustomersWithLazyLoadedOrders());

            response.AppendLine("<h2>Query 2: Customers with Conditional Lazy Loaded Orders (> $500)</h2>");
            response.AppendLine(await GetCustomersWithConditionalLazyLoadedOrders());

            response.AppendLine("</body></html>");

            return Content(response.ToString(), "text/html");
        }

        private async Task<string> GetCustomersWithLazyLoadedOrders()
        {
            var customers = await _context.Customers.ToListAsync();
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Customer ID</th><th>Customer Name</th><th>Order IDs (Lazy Loaded)</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var customer in customers)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{customer.CustomerId}</td>");
                tableBuilder.Append($"<td>{customer.Name}</td>");

                if (customer.Orders != null)
                {
                    var orderIds = string.Join(", ", customer.Orders.Select(o => o.OrderId));
                    tableBuilder.Append($"<td>{orderIds}</td>");
                }
                else
                {
                    tableBuilder.Append("<td>No Orders (Lazy Loaded)</td>");
                }

                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }

        private async Task<string> GetCustomersWithConditionalLazyLoadedOrders()
        {
            var customers = await _context.Customers.ToListAsync();
            var tableBuilder = new StringBuilder();
            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Customer ID</th><th>Customer Name</th><th>Order IDs (Conditional Lazy Load)</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var customer in customers)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{customer.CustomerId}</td>");
                tableBuilder.Append($"<td>{customer.Name}</td>");

                if (customer.Orders != null)
                {
                    var highValueOrders = customer.Orders.Where(o => o.OrderProducts.Sum(op => op.Quantity * op.Product.Price) > 500).ToList();

                    if (highValueOrders.Any())
                    {
                        var orderIds = string.Join(", ", highValueOrders.Select(o => o.OrderId));
                        tableBuilder.Append($"<td>{orderIds}</td>");
                    }
                    else
                    {
                        tableBuilder.Append("<td>No High Value Orders (Lazy Loaded)</td>");
                    }
                }
                else
                {
                    tableBuilder.Append("<td>No Orders (Lazy Loaded)</td>");
                }

                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }
    }
}
