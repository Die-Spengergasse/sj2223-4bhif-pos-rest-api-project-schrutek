using Microsoft.EntityFrameworkCore;
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
using Xunit.Sdk;

namespace Spg.SpengerShop.ApplicationTest
{
    public class ProductServiceTest //: IClassFixture<ProductServiceFixture>
    {
        [Fact]
        public void Create_Succes_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);

                IReadOnlyRepositoryBase<Product> _readonlyProductRepository = new ReadOnlyRepositoryBase<Product>(db);
                IReadOnlyRepositoryBase<Category> _readonlyCategoryRepository = new ReadOnlyRepositoryBase<Category>(db);
                IRepositoryBase<Product> _productRepositoryBase = new RepositoryBase<Product>(db);
                IDateTimeService _dateTimeService = new DateTimeServiceMock();
                ProductDto dto = new ProductDto() 
                { 
                    Name = "TestProdukt 02", 
                    Ean13 = "1234567890123",
                    ExpiryDate = new DateTime(2023, 03, 22),
                    DeliveryDate = new DateTime(2023, 03, 07), 
                    CategoryId = db.Categories.Single(c => c.Id == 1).Guid 
                };

                // Act
                new ProductService(_productRepositoryBase, _readonlyProductRepository, _readonlyCategoryRepository, _dateTimeService).Create(dto);

                // Assert
                Assert.Equal(2, db.Products.Count());
            }
        }

        [Fact()]
        public void Create_Create_ExpiryDateNotInFuture_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);

                IReadOnlyRepositoryBase<Product> _readonlyProductRepository = new ReadOnlyRepositoryBase<Product>(db);
                IReadOnlyRepositoryBase<Category> _readonlyCategoryRepository = new ReadOnlyRepositoryBase<Category>(db);
                IRepositoryBase<Product> _productRepositoryBase = new RepositoryBase<Product>(db);
                IDateTimeService _dateTimeService = new DateTimeServiceMock();

                ProductService unitToTest = new ProductService(_productRepositoryBase, _readonlyProductRepository, _readonlyCategoryRepository, _dateTimeService);

                ProductDto dto = new ProductDto()
                {
                    Name = "TestProdukt 02",
                    Ean13 = "1234567890123",
                    ExpiryDate = new DateTime(2023, 03, 17),
                    DeliveryDate = new DateTime(2023, 03, 07),
                    CategoryId = db.Categories.Single(c => c.Id == 1).Guid
                };

                // Act + Assert
                Assert.Throws<ProductCreateValidationException>(() => unitToTest.Create(dto));
            }
        }
    }
}