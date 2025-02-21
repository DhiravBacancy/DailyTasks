using System.Threading.Tasks;
using ASP.NET_Core_Day4.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace ASP.NET_Core_Day4.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly CacheService _cacheService;
        private readonly FileService _fileService;

        public AuthMiddleware(RequestDelegate next, CacheService cacheService, FileService fileService)
        {
            _next = next;
            _cacheService = cacheService;
            _fileService = fileService;
        }

        public async Task Invoke(HttpContext context)
        {
            Console.WriteLine("Midelware hitted");
            
            if (context.Request.Path.StartsWithSegments("/protected"))
            {
                if (!context.Request.Headers.TryGetValue("Authorization", out var guid) || string.IsNullOrEmpty(guid))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized: No GUID provided.");
                    return;
                }

                // Check cache first, then file if needed
                if (!_cacheService.ValidateGuid(guid) && !_fileService.ValidateGuid(guid))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized: Invalid GUID.");
                    return;
                }
            }

            await _next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
