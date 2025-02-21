using ASP.NET_Core_Day2.Middleware;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("CorsPolicy:AllowedOrigins").Value?.Split(',');
var allowMethods = builder.Configuration.GetSection("CorsPolicy:AllowMethods").Value?.Split(',');
var allowHeaders = builder.Configuration.GetSection("CorsPolicy:AllowHeaders").Value?.Split(',');


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("CustomCorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins ?? new string[] { })
              .WithMethods(allowMethods ?? new string[] { })
              .WithHeaders(allowHeaders ?? new string[] { });
    });
});

// Register other services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseRouting();


app.UseCors("CustomCorsPolicy");


app.UseErrorHandling();
app.UseLoggingMiddleware();
app.UseAuthorization();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

app.MapControllers();

app.Use(async (context, next) =>
{
    Console.WriteLine($"Inside Program.cs File");
    await next();
});

app.Run();
