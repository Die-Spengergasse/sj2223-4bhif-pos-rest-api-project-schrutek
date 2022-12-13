//using Microsoft.EntityFrameworkCore;
//using Spg.SpengerShop.Infrastructure;

using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Spg.SpengerShop.Application.Services;
using Spg.SpengerShop.DbExtensions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

builder.Services.AddTransient<IAddUpdateableProductService, ProductService>();
builder.Services.AddTransient<IReadOnlyProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.ConfigureSqLite(connectionString);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(s => 
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo() 
    { 
        Title= "Spenger Shop - v1", 
        Description="Description about SpengerShop",
        Contact = new OpenApiContact()
        {
            Name = "Martin Schrutek",
            Email = "schrutek@spengergasse.at",
            Url = new Uri("http://www.spengergasse.at")
        },
        License = new OpenApiLicense()
        {
            Name = "Spenger-Licence",
            Url = new Uri("http://www.spengergasse.at/licence")
        },
        Version = "v1"
    }));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:7042");
        policy.WithHeaders("ACCESS-CONTROL-ALLOW-ORIGIN", "CONTENT-TYPE");
    });
});

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
