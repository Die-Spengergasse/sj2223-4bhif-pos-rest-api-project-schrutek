//using Microsoft.EntityFrameworkCore;
//using Spg.SpengerShop.Infrastructure;

using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Spg.SpengerShop.Application.Filter;
using Spg.SpengerShop.Application.Mock;
using Spg.SpengerShop.Application.Services;
using Spg.SpengerShop.Application.Validators;
using Spg.SpengerShop.DbExtensions;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.Repository.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Get Connection String from Config
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

// Create services to the container.
builder.Services.AddTransient<IAddUpdateableProductService, ProductService>();
builder.Services.AddTransient<IReadOnlyProductService, ProductService>();

// Old
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IRepository<Customer>, CustomerRepository>();
builder.Services.AddTransient<IRepositoryBase<Product>, RepositoryBase<Product>>();

// Generic Repository
builder.Services.AddTransient<IReadOnlyRepositoryBase<Product>, ReadOnlyRepositoryBase<Product>>();
builder.Services.AddTransient<IReadOnlyRepositoryBase<Category>, ReadOnlyRepositoryBase<Category>>();
builder.Services.AddTransient<IReadOnlyRepositoryBase<Customer>, ReadOnlyRepositoryBase<Customer>>();

builder.Services.AddTransient<IDateTimeService, DateTimeService>();

// Global Filter
builder.Services.AddTransient<HasRoleAttribute>();

// Configure DB
builder.Services.ConfigureSqLite(connectionString);

builder.Services.AddControllers(configure => configure.Filters.Add(new HasRoleAttribute()));

// Configure Swagger
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

// Swagger Documentation (Open API)
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo()
    {
        Title = "Spenger Shop - v1",
        Description = "Description about SpengerShop",
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
        Version = "v1",
    });
    s.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"), true);
    s.IncludeXmlComments("C:\\HTL\\Unterricht\\SJ2223\\4BHIF\\sj2223-4bhif-pos-rest-api-project-schrutek\\src\\Spg.SpengerShop.Domain\\bin\\Debug\\net7.0\\Spg.SpengerShop.Domain.xml", true);
    // Configure Swagger Authorization
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter JWT Token here"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "myAllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("https://localhost:7042");
        policy.WithHeaders("ACCESS-CONTROL-ALLOW-ORIGIN", "CONTENT-TYPE");
    });
});

// Configure API Versioning
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

// Configure Fluent Validation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddTransient<IValidator<NewProductDto>, NewProductDtoValidator>();



// Create DB (JUST FOR TEST PURPOSE!!!!!)
DbContextOptions options = new DbContextOptionsBuilder()
.UseSqlite(connectionString)
.Options;
SpengerShopContext db = new SpengerShopContext(options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();
db.Seed();

// Build Application
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("myAllowSpecificOrigins");
app.UsePathBase("/myapp");

// Configure Controller-Routing
app.MapControllers();

// Configure Extra-Route (minimal API)
app.MapGet("/api", (HttpContext context, IReadOnlyProductService readOnlyProductService, [FromQuery] int id) =>
{
    return "Welcome!!!!";
})
.Produces(200)
.WithName("api")
.WithDescription("Swagger-Description");

app.Run();
