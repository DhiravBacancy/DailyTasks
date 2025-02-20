using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ASP.Net_Core_Day1.Middlewares
{
    public class AhmRestrictMiddleware
    {
        private readonly RequestDelegate _next;

        public AhmRestrictMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering(); // Allows multiple reads

            using (var reader = new StreamReader(httpContext.Request.Body, System.Text.Encoding.UTF8, leaveOpen: true))
            {
                var body = await reader.ReadToEndAsync(); // Read request body
                httpContext.Request.Body.Position = 0; // Reset stream position for further use

                if (!string.IsNullOrWhiteSpace(body))
                {
                    try
                    {
                        var json = JsonSerializer.Deserialize<Dictionary<string, string>>(body, new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true // Makes JSON parsing case-insensitive
                        });

                        if (json != null && json.TryGetValue("city", out var city) && city.Equals("ahmedabad", StringComparison.OrdinalIgnoreCase))
                        {
                            Console.WriteLine("Allowed: City is Ahmedabad.");
                            await _next(httpContext); // Continue to the next middleware/controller
                            return;
                        }
                    }
                    catch (JsonException)
                    {
                        Console.WriteLine("Invalid JSON format.");
                    }
                }

                // If "city" is missing or not "Ahmedabad", reject the request
                Console.WriteLine("Request blocked: City is missing or not Ahmedabad.");
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                await httpContext.Response.WriteAsync("Access Denied: City must be Ahmedabad.");
            }
        }
    }
}
