using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.Services;
using Spg.SpengerShop.DbExtensions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";

// Create services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAddUpdateableProductService, ProductService>();
builder.Services.AddTransient<IReadOnlyProductService, ProductService>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.ConfigureSqLite(connectionString);

//IServiceProvider provider = builder.Services.BuildServiceProvider();
//SpengerShopContext db = provider.GetRequiredService<SpengerShopContext>();
DbContextOptions options = new DbContextOptionsBuilder()
.UseSqlite(connectionString)
.Options;
SpengerShopContext db = new SpengerShopContext(options);
db.Database.EnsureDeleted();
db.Database.EnsureCreated();
db.Seed();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
