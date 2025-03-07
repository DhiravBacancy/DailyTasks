using EfCoreDIExample.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Different ways to configure DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);


//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseSqlServer(connectionString), ServiceLifetime.Scoped);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
