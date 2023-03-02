using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Application.Services;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Repositories;
using Xunit.Sdk;

namespace Spg.SpengerShop.ApplicationTest
{
    public class ProductServiceTest
    {
        private SpengerShopContext GetContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=SpengerShop_TEST.db")
            .Options;

            SpengerShopContext db = new SpengerShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void Create_Succes_Test()
        {
            // Arrange
            SpengerShopContext db = GetContext();
            Product newProduct = new Product("TestProdukt", 20, "1234567890123", "Testmaterial", new DateTime(2023, 03, 17), null);

            // Act
            new ProductService(new ProductRepository(db)).Create(newProduct);

            // Assert
            Assert.Single(db.Products.ToList());
        }
    }
}