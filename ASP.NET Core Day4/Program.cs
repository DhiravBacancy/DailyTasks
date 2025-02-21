using ASP.NET_Core_Day4.Services;
using ASP.NET_Core_Day4.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FileService>();
builder.Services.AddSingleton<CacheService>();

builder.Services.AddMemoryCache();
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/auth/login"), appBuilder =>
{
    appBuilder.UseMiddleware<AuthMiddleware>();
});

app.UseRouting();
app.UseAuthorization();
app.MapControllers();

//app.UseAuthMiddleware();

app.Run();
