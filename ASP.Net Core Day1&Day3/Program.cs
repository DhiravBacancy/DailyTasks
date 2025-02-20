//using ASP.Net_Core_Day1.Services;

using ASP.Net_Core_Day1.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services for controllers
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FileService>();
builder.Services.AddSingleton<ISingletonGuidService, SingletonGuidService>();
builder.Services.AddScoped<IScopedGuidService, ScopedGuidService>();
builder.Services.AddTransient<ITransientGuidService, TransientGuidService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/Data/AhmWeather"), appBuilder =>
{
    appBuilder.UseMiddleware<AhmRestrictMiddleware>();
});


app.MapControllers(); // Enable controllers

app.Run();
