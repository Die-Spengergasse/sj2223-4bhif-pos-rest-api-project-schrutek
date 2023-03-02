using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Repositories;

namespace Spg.SpengerShop.RepositoryTest
{
    public class ProductRepositoryTest
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
        public void Create_Success_Test()
        {
            // Arrange
            SpengerShopContext db = GetContext();
            Product newProduct = new Product("TestProdukt", 20, "1234567890123", "Testmaterial", new DateTime(2023, 03, 17), null);

            // Act
            new ProductRepository(db).Create(newProduct);

            // Assert
            Assert.Single(db.Products.ToList());
        }
    }
}