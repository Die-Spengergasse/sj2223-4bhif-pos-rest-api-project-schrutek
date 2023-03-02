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

            Product newProduct = new Product("Test Product", 20, "132456789123", "Testmaterial", DateTime.Now, null);

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

            Customer newCustomer = new Customer(new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"), Genders.Male, 132456789, "TestFirstName", "TestLastName", "xy@gmail.at", new DateTime(1990, 05, 15), new DateTime(2023, 02, 10), new Address("", "", "", ""));
            db.Customers.Add(newCustomer);
            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
        }

        [Fact]
        public void DomainModel_AddShoppingCartToCustomer_Success_Test()
        {
            // Arrange
            SpengerShopContext db = GetContext();

            Customer newCustomer = new Customer(new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"), Genders.Male, 132456789, "TestFirstName", "TestLastName", "xy@gmail.at", new DateTime(1990, 05, 15), new DateTime(2023, 02, 10), new Address("", "", "", ""));
            db.Customers.Add(newCustomer);

            ShoppingCart newShoppingCart = new ShoppingCart("", ShoppingCartStates.Active, new DateTime(2023, 02, 15), newCustomer, Guid.NewGuid());

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

            Customer newCustomer = new Customer(new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"), Genders.Male, 132456789, "TestFirstName", "TestLastName", "xy@gmail.at", new DateTime(1990, 05, 15), new DateTime(2023, 02, 10), new Address("", "", "", ""));
            db.Customers.Add(newCustomer);

            ShoppingCart newShoppingCart = new ShoppingCart("", ShoppingCartStates.Active, new DateTime(2023, 02, 15), newCustomer, Guid.NewGuid());

            db.SaveChanges();

            Assert.Equal(1, db.Customers.Count());
            Assert.Equal(0, db.ShoppingCarts.Count());
        }
    }
}