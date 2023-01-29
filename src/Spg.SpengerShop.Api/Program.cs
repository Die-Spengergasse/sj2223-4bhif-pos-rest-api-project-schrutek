//using Microsoft.EntityFrameworkCore;
//using Spg.SpengerShop.Infrastructure;

using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.Services;
using Spg.SpengerShop.Application.Services.Customers.Queries;
using Spg.SpengerShop.Application.Validators;
using Spg.SpengerShop.DbExtensions;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Filter;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.Repository.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddTransient<IAddUpdateableProductService, ProductService>();
builder.Services.AddTransient<IReadOnlyProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddTransient<IRequestHandler<GetFilteredCustomerQuery, IQueryable<Customer>>, GetFilteredCustomerHandler>();
builder.Services.AddTransient<IRequestHandler<GetCustomerByIdQuery, Customer>, GetCustomerByIdQueryHandler>();

builder.Services.AddTransient<IRepository<Customer>, CustomerRepository>();

builder.Services.AddTransient<IReadOnlyRepositoryBase<Customer>, ReadOnlyRepositoryBase<Customer>>();

builder.Services.ConfigureSqLite(connectionString);

// Global Filter
//builder.Services.AddControllers(config =>
//{
//    config.Filters.Add(new ValidationFilterAttribute());
//}); 
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// API-Versioning
// NuGet: Microsoft.AspNetCore.Mvc.Versioning
//        Microsoft.AspNetCore.Mvc.Versioning.ApiExplorer
builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("api-version"),
        new HeaderApiVersionReader("X-Version"),
        new MediaTypeApiVersionReader("ver"));
});
builder.Services.AddVersionedApiExplorer(
    options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });

// CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:7042");
        policy.WithHeaders("ACCESS-CONTROL-ALLOW-ORIGIN", "CONTENT-TYPE");
    });
});

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<NewCustomerDto>, NewCustomerDtoValidator>();

// Controller Filter, Action Filter
builder.Services.AddScoped<ValidationFilterAttribute>();

DbContextOptions options = new DbContextOptionsBuilder()
.UseSqlite(connectionString)
.Options;
SpengerShopContext db = new SpengerShopContext(options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();
db.Seed();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("myAllowSpecificOrigins");
app.UseAuthorization();

app.UsePathBase("/myapp");
app.MapControllers();

app.Run();
