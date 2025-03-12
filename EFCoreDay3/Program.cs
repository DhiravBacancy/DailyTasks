using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EFCoreDay3.Data;
var builder = WebApplication.CreateBuilder(args);

// Configure Logging
builder.Logging.AddConsole();
// Configure the DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


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
        Description = "Bacancy Task"
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
