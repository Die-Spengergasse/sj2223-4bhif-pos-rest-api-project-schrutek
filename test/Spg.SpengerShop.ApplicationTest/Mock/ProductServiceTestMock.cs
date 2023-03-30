using Moq;
using Spg.SpengerShop.Application.Mock;
using Spg.SpengerShop.Application.Services;
using Spg.SpengerShop.ApplicationTest.Helpers;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.ApplicationTest.Mock
{
    public class ProductServiceTestMock
    {
        private readonly Mock<IDateTimeService> _dateTimeServiceMock = new Mock<IDateTimeService>();
        private readonly Mock<IReadOnlyRepositoryBase<Category>> _readOnlyCategoryRepository = new Mock<IReadOnlyRepositoryBase<Category>>();
        private readonly Mock<IReadOnlyRepositoryBase<Product>> _readOnlyProductRepository = new Mock<IReadOnlyRepositoryBase<Product>>();
        private readonly Mock<IRepositoryBase<Product>> _productRepository = new Mock<IRepositoryBase<Product>>();
        ProductService _unitToTest;

        public ProductServiceTestMock()
        {
            _unitToTest = new ProductService(_productRepository.Object, _readOnlyProductRepository.Object, _readOnlyCategoryRepository.Object, _dateTimeServiceMock.Object);
        }

        [Fact]
        public void Create_Succes_Test()
        {
            // Arrange
            ProductDto dto = new ProductDto()
            {
                Name = "TestProdukt 02",
                Ean13 = "1234567890123",
                ExpiryDate = new DateTime(2023, 03, 22),
                DeliveryDate = new DateTime(2023, 03, 07),
                CategoryId = MockUtilities.GetSeedingCategory(MockUtilities.GetSeedingShop()).Guid
            };

            // DateTimeService-Setup
            _dateTimeServiceMock.Setup(d => d.UtcNow).Returns(new DateTime(2023, 03, 07));
            // CategoryRepository-Setup
            _readOnlyCategoryRepository
                .Setup(r => r.GetSingleOrDefaultByGuid<Category>(new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41")))
                .Returns(MockUtilities.GetSeedingCategory(MockUtilities.GetSeedingShop()));

            // Act
            _unitToTest.Create(dto);

            // Assert
            _productRepository.Verify(r => r.Create(It.IsAny<Product>()), Times.Once);
        }

        [Fact]
        public void Create_NameIsNotUnique_Test()
        {
            // Arrange
            ProductDto dto = new ProductDto()
            {
                Name = "TestProdukt 01",
                Ean13 = "1234567890123",
                ExpiryDate = new DateTime(2023, 03, 22),
                DeliveryDate = new DateTime(2023, 03, 07),
                CategoryId = MockUtilities.GetSeedingCategory(MockUtilities.GetSeedingShop()).Guid
            };

            // DateTimeService-Setup
            _dateTimeServiceMock.Setup(d => d.UtcNow).Returns(new DateTime(2023, 03, 07));
            // CategoryRepository-Setup
            _readOnlyCategoryRepository
                .Setup(r => r.GetSingleOrDefaultByGuid<Category>(new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41")))
                .Returns(MockUtilities.GetSeedingCategory(MockUtilities.GetSeedingShop()));
            _readOnlyProductRepository
                .Setup(r => r.GetById<string>(dto.Name))
                .Returns(MockUtilities.GetSeedingProduct(MockUtilities.GetSeedingCategory(MockUtilities.GetSeedingShop())));

            // Act
            // Assert
            AssertExeptionAndMessageAndVerfyRunNever(dto, "Das Produkt existiert bereits!");
        }

        [Fact]
        public void Create_CategoryDoesNotExists_Test()
        {
            // Arrange
            ProductDto dto = new ProductDto()
            {
                Name = "TestProdukt 02",
                Ean13 = "1234567890123",
                ExpiryDate = new DateTime(2023, 03, 22),
                DeliveryDate = new DateTime(2023, 03, 07),
                CategoryId = new Guid("aef70816-2407-47b3-b6cf-ea8744619af7")
            };

            // DateTimeService-Setup
            _dateTimeServiceMock.Setup(d => d.UtcNow).Returns(new DateTime(2023, 03, 07));
            // CategoryRepository-Setup
            _readOnlyCategoryRepository
                .Setup(r => r.GetSingleOrDefaultByGuid<Category>(dto.CategoryId))
                .Returns<Category>(null!);
            _readOnlyProductRepository
                .Setup(r => r.GetById(dto.Name))
                .Returns<Product>(null!);

            // Act
            // Assert
            AssertExeptionAndMessageAndVerfyRunNever(dto, "Kategorie wurde nicht gefunden!");
        }

        [Fact]
        public void Create_CategoryReturnMoreThanOneElement_Test()
        {
            // Arrange
            ProductDto dto = new ProductDto()
            {
                Name = "TestProdukt 02",
                Ean13 = "1234567890123",
                ExpiryDate = new DateTime(2023, 03, 22),
                DeliveryDate = new DateTime(2023, 03, 07),
                CategoryId = new Guid("aef70816-2407-47b3-b6cf-ea8744619af7")
            };

            // DateTimeService-Setup
            _dateTimeServiceMock.Setup(d => d.UtcNow).Returns(new DateTime(2023, 03, 07));
            // CategoryRepository-Setup
            _readOnlyCategoryRepository
                .Setup(r => r.GetSingleOrDefaultByGuid<Category>(dto.CategoryId))
                .Throws(new InvalidOperationException("More than One element found!"));
            _readOnlyProductRepository
                .Setup(r => r.GetById(dto.Name))
                .Returns<Product>(null!);

            // Act
            // Assert
            AssertExeptionAndMessageAndVerfyRunNever(dto, "Kategorie mehrmals vorhanden!");
        }

        private void AssertExeptionAndMessageAndVerfyRunNever(ProductDto dto, string message)
        {
            Exception ex = Assert.Throws<ProductCreateValidationException>(() => _unitToTest.Create(dto));
            Assert.Equal(message, ex.Message);
            _productRepository.Verify(r => r.Create(It.IsAny<Product>()), Times.Never);
        }
    }
}
