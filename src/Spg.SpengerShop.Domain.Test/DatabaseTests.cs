using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Linq;
using Xunit;

namespace Spg.SpengerShop.Domain.Test
{
    public class DatabaseTests
    {
        private SpengerShopContext GetContext()
        {
            DbContextOptions options = new DbContextOptionsBuilder()
            .UseSqlite("Data Source=SpengerShop.db")
            .Options;

            SpengerShopContext db = new SpengerShopContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void DomainModel_Create_Product_Success_Test()
        {
            // Arrange
            SpengerShopContext db = GetContext();

            Product newProduct = new Product("Test Product", "132456789123", 10, DateTime.Now, DateTime.Now, 20.90M);

            // Act
            db.Products.Add(newProduct);
            db.SaveChanges();

            // Assert
            Assert.Equal(1, db.Products.Count());
        }

        [Fact]
        public void DomainModel_Create_Customer_Success_Test()
        {
            SpengerShopContext db = GetContext();

            Customer newCustomer = new Customer()
            {
                EMail = "xy@gmail.at",
                FirstName="TestFirstName",
                LastName="TestLastName",
                Gender=GenderTypes.MALE,
                Guid=Guid.NewGuid(),
                RegistrationDateTime=DateTime.Now,
            };

            db.Customers.Add(newCustomer);
            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
        }

        [Fact]
        public void DomainModel_AddShoppingCartToCustomer_Success_Test()
        {
            // Arrange
            SpengerShopContext db = GetContext();

            Customer newCustomer = new Customer()
            {
                EMail = "xy@gmail.at",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Gender = GenderTypes.MALE,
                Guid = Guid.NewGuid(),
                RegistrationDateTime = DateTime.Now,
            };
            db.Customers.Add(newCustomer);

            ShoppingCart newShoppingCart = new ShoppingCart(States.ACTIVE, Guid.NewGuid())
            {
            };

            // Act
            newCustomer.AddShoppingCart(newShoppingCart);

            db.SaveChanges();

            // Assert
            Assert.Equal(1, db.Customers.Count());
            Assert.Equal(1, db.ShoppingCarts.Count());
        }

        [Fact]
        public void DomainModel_AddShoppingCartToCustomer_Navigation_Success_Test()
        {
            SpengerShopContext db = GetContext();

            Customer newCustomer = new Customer()
            {
                EMail = "xy@gmail.at",
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Gender = GenderTypes.MALE,
                Guid = Guid.NewGuid(),
                RegistrationDateTime = DateTime.Now,
            };
            db.Customers.Add(newCustomer);

            ShoppingCart newShoppingCart = new ShoppingCart(States.ACTIVE, Guid.NewGuid())
            {
                CustomerNavigation = newCustomer,
            };

            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
            Assert.Equal(0, db.ShoppingCarts.Count());
        }
    }
}