// This section sets up the ASP.NET Core application.
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Models;
using AspNetCoreRateLimit;

var builder = WebApplication.CreateBuilder(args);

// Configure services for the application.
builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<SwapiWrapper>();
builder.Services.AddScoped<SwapiWrapper>();
builder.Services.AddControllers();

// Configure Swagger for API documentation.
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SWAPI Wrapper API", Version = "v1" });
});

// Build the application.
var app = builder.Build();

// Enable Swagger in the development environment.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SWAPI Wrapper API v1"));
}

// Configure middleware for the application.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Start the application.
app.Run();