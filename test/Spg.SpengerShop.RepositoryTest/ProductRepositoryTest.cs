using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Repositories;
using Spg.SpengerShop.RepositoryTest.Helpers;

namespace Spg.SpengerShop.RepositoryTest
{
    public class ProductRepositoryTest
    {
        [Fact]
        public void Create_Success_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);
                Product newProduct = new Product(
                    "TestProdukt",
                    20,
                    "1234567890123",
                    "Testmaterial",
                    new DateTime(2023, 03, 17),
                    DatabaseUtilities.GetSeedingCategories(DatabaseUtilities.GetSeedingShops()[0], 1)[0]);

                // Act
                new ProductRepository(db).Create(newProduct);

                // Assert
                Assert.Equal(2, db.Products.Count());
            }
        }
    }
}