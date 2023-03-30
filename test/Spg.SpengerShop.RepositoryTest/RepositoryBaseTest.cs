using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using Spg.SpengerShop.Repository;
using Spg.SpengerShop.RepositoryTest.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.RepositoryTest
{
    public class RepositoryBaseTest
    {
        [Fact()]
        public void Product_Create_Success_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);
                Product newProduct = new Product(
                    "TestProdukt 02",
                    20,
                    "1234567890123",
                    "Testmaterial",
                    new DateTime(2023, 03, 17),
                    DatabaseUtilities.GetSeedingCategories(DatabaseUtilities.GetSeedingShops()[0], 1)[0]);

                // Act
                new RepositoryBase<Product>(db).Create(newProduct);

                // Assert
                Assert.Equal(2, db.Products.Count());
            }
        }

        [Fact()]
        public void Product_Create_CategoryForeignKeyConstraint_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);
                Product newProduct = new Product(
                    "TestProdukt 02",
                    20,
                    "1234567890123",
                    "Testmaterial",
                    new DateTime(2023, 03, 17),
                    null!);

                // Act + Assert
                Assert.Throws<RepositoryCreateException>(() => new RepositoryBase<Product>(db).Create(newProduct));
            }
        }

        [Fact()]
        public void Product_Create_ProductIsNull_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);
                Product newProduct = null!;

                // Act + Assert
                Assert.Throws<RepositoryCreateException>(() => new RepositoryBase<Product>(db).Create(newProduct));
            }
        }

        [Fact()]
        public void Customer_GetSingleByGuid_Success_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);

                // Act
                Customer active = new ReadOnlyRepositoryBase<Customer>(db).GetSingleOrDefaultByGuid<Customer>(new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"))!;

                // Assert
                Assert.Equal(new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"), active.Guid);
            }
        }

        [Fact()]
        public void Show_GetById_Success_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);

                // Act
                Shop active = new ReadOnlyRepositoryBase<Shop>(db).GetById(2)!;

                // Assert
                Assert.Equal(new Guid("a0a6b711-fd27-4193-8595-325a62d82c5c"), active.Guid);
            }
        }

        [Fact()]
        public void Product_GetById_Success_Test()
        {
            using (SpengerShopContext db = new SpengerShopContext(DatabaseUtilities.GetDbOptions()))
            {
                // Arrange
                DatabaseUtilities.InitializeDatabase(db);

                // Act
                Product active = new ReadOnlyRepositoryBase<Product>(db).GetById("TestProdukt 01")!;

                // Assert
                Assert.Equal("TestProdukt 01", active.Name);
            }
        }
    }
}
