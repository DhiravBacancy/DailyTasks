using EFCoreDay3.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EFCoreDay3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CombinedChallengesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CombinedChallengesController> _logger;

        public CombinedChallengesController(ApplicationDbContext context, ILogger<CombinedChallengesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet("CombinedQueries")]
        public async Task<ActionResult> GetCombinedQueries()
        {
            var response = new StringBuilder();
            response.AppendLine("<html><body>");

            // Challenge 1: Orders Eager Loading, OrderProducts Lazy
            response.AppendLine("<h2>Challenge 1: Orders Eager, OrderProducts Lazy</h2>");
            response.AppendLine(await GetOrdersEagerOrderProductsLazy());

            // Challenge 2: Orders Eager, OrderProducts Explicit (Quantity > 5)
            response.AppendLine("<h2>Challenge 2: Orders Eager, OrderProducts Explicit (Quantity > 5)</h2>");
            response.AppendLine(await GetOrdersEagerOrderProductsExplicitQuantity());

            // Challenge 3: Customer's Orders Eager, OrderProducts Explicit (VIP)
            response.AppendLine("<h2>Challenge 3: Customer Orders Eager, OrderProducts Explicit (VIP)</h2>");
            response.AppendLine(await GetCustomerOrdersEagerOrderProductsExplicitVip());

            response.AppendLine("</body></html>");
            return Content(response.ToString(), "text/html");
        }

        private async Task<string> GetOrdersEagerOrderProductsLazy()
        {
            var orders = await _context.Orders.Include(o => o.Customer).ToListAsync();
            var tableBuilder = new StringBuilder();

            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Order ID</th><th>Customer Name</th><th>Order Product IDs (Lazy)</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var order in orders)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{order.OrderId}</td>");
                tableBuilder.Append($"<td>{order.Customer?.Name}</td>");

                if (order.OrderProducts != null)
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

        private async Task<string> GetOrdersEagerOrderProductsExplicitQuantity()
        {
            var orders = await _context.Orders.Include(o => o.Customer).ToListAsync();
            var tableBuilder = new StringBuilder();

            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Order ID</th><th>Customer Name</th><th>Order Product IDs (If Quantity > 5)</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var order in orders)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{order.OrderId}</td>");
                tableBuilder.Append($"<td>{order.Customer?.Name}</td>");

                var loaded = false;
                if (order.OrderProducts != null)
                {
                    foreach (var orderProduct in order.OrderProducts)
                    {
                        if (orderProduct.Quantity > 5)
                        {
                            await _context.Entry(order).Collection(o => o.OrderProducts).LoadAsync();
                            loaded = true;
                            break;
                        }
                    }
                }

                if (loaded && order.OrderProducts != null)
                {
                    var orderProductIds = string.Join(", ", order.OrderProducts.Select(op => op.OrderProductId));
                    tableBuilder.Append($"<td>{orderProductIds}</td>");
                }
                else
                {
                    tableBuilder.Append("<td>No Order Products Loaded (Quantity <= 5)</td>");
                }

                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }

        private async Task<string> GetCustomerOrdersEagerOrderProductsExplicitVip()
        {
            var customers = await _context.Customers
                .Include(c => c.Orders) // Eagerly load Orders
                .ToListAsync();

            var tableBuilder = new StringBuilder();

            tableBuilder.Append("<table border='1'>");
            tableBuilder.Append("<thead><tr><th>Customer ID</th><th>Customer Name</th><th>Order IDs</th><th>Order Product IDs (VIP Only)</th></tr></thead>");
            tableBuilder.Append("<tbody>");

            foreach (var customer in customers)
            {
                tableBuilder.Append("<tr>");
                tableBuilder.Append($"<td>{customer.CustomerId}</td>");
                tableBuilder.Append($"<td>{customer.Name}</td>");

                var orderIds = customer.Orders != null
                    ? string.Join(", ", customer.Orders.Select(o => o.OrderId))
                    : "No Orders";
                tableBuilder.Append($"<td>{orderIds}</td>");

                var orderProductIds = new StringBuilder();

                if (customer.IsVIP) // Explicitly load OrderProducts for VIP customers
                {
                    foreach (var order in customer.Orders)
                    {
                        await _context.Entry(order).Collection(o => o.OrderProducts).LoadAsync();
                        if (order.OrderProducts != null && order.OrderProducts.Any())
                        {
                            orderProductIds.Append(string.Join(", ", order.OrderProducts.Select(op => op.OrderProductId))).Append(", ");
                        }
                    }
                    tableBuilder.Append($"<td>{orderProductIds.ToString().TrimEnd(',', ' ')}</td>");
                }
                else
                {
                    tableBuilder.Append("<td>Orders and Order Products not loaded (Not VIP)</td>");
                }

                tableBuilder.Append("</tr>");
            }

            tableBuilder.Append("</tbody></table>");
            return tableBuilder.ToString();
        }

    }
}