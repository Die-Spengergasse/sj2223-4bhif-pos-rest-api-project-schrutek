using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository.Repositories;
using Spg.SpengerShop.RepositoryTest.Helpers;

namespace Spg.SpengerShop.RepositoryTest
{
    public class ProductRepositoryTestMock
    {
        private readonly Mock<SpengerShopContext> _productContextMock = new Mock<SpengerShopContext>();
        private readonly ProductRepository _unitToTest;

        public ProductRepositoryTestMock()
        {
            _unitToTest = new ProductRepository(_productContextMock.Object);
        }

        [Fact]
        public void GetAll_Success_Test()
        {
            // Arrange
            _productContextMock
                .Setup(x => x.Products)
                .ReturnsDbSet(
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