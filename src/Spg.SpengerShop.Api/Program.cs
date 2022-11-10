//using Microsoft.EntityFrameworkCore;
//using Spg.SpengerShop.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//DbContextOptions options = new DbContextOptionsBuilder()
//    .UseSqlite("Data Source=SpengerShop.db")
//    .Options;

//SpengerShopContext db = new SpengerShopContext(options);
//db.Database.EnsureDeleted();
//db.Database.EnsureCreated();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
