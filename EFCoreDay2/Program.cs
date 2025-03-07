using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EFCoreDay2.Data;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
builder.Logging.AddConsole();

// Add DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
           .EnableSensitiveDataLogging() // Show query parameter values (for debugging only!)
           .EnableDetailedErrors()
);

// Add Controllers
builder.Services.AddControllers();

// ✅ Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "A simple example of a .NET 8 Web API with EF Core"
    });
});

var app = builder.Build();

// ✅ Ensure Swagger is available in Development mode
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        c.RoutePrefix = "swagger"; // Ensure Swagger is at /swagger
    });
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
