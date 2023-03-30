using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Repositories;
using Spg.SpengerShop.RepositoryTest.Helpers;

namespace Spg.SpengerShop.RepositoryTest
{
    public class ProductRepositoryTestMock_Moq
    {
        private readonly Mock<SpengerShopContext> _db = new Mock<SpengerShopContext>();
        private readonly ProductRepository _unitToTest;

        public ProductRepositoryTestMock_Moq()
        {
            _unitToTest = new ProductRepository(_db.Object);
        }

        [Fact]
        public void GetAll_Success_Test()
        {
            // Arrange
            _db
                .Setup(x => x.Products.ToList())
                .Returns(
                    MockUtilities.GetSeedingProducts(
                        MockUtilities.GetSeedingCategory(
                            MockUtilities.GetSeedingShop())));

            // Act
            var products = _unitToTest.GetAll();

            // Assert
            Assert.Equal(5, products.Count());
        }
    }
}