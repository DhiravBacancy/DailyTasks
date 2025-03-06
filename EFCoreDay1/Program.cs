var builder = WebApplication.CreateBuilder(args);

// Register services (no DbContext registration needed)
builder.Services.AddControllers(); // Add controllers to the DI container

var app = builder.Build();

// Configure HTTP request pipeline
app.MapControllers();

app.Run();