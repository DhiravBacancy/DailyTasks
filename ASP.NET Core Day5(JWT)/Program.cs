using JwtAuthApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JwtAuthApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<AuthService>();

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key) // Ensure key is at least 32 bytes
        };
    });
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
//    //options.AddPolicy("ManagerPolicy", policy => policy.RequireRole("Manager"));
//});

builder.Services.AddSingleton<UserManager>();
builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
