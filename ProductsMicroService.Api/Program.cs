using System.Text.Json.Serialization;
using ProductsMicroService.Application;
using ProductsMicroService.Endpoints;
using ProductsMicroService.Infrastructure;
using ProductsMicroService.Middlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication();

// CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policyBuilder =>
    {
        policyBuilder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

//Register Dependency Injection
builder.Services
    .AddInfrastructureDependency(builder.Configuration)
    .AddApplicationDependency(builder.Configuration);

var app = builder.Build();

app.UseExceptionHandlingMiddleware();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthorization();

app.MapControllers();

app.MapBrandEndpoints(); 
app.MapCategoryEndpoints(); 
app.MapSubCategoryEndpoints(); 
app.MapColorEndpoints(); 
app.MapProductsEndpoints(); 

app.Run();