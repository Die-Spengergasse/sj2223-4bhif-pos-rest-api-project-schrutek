﻿using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.ApplicationTest.Helpers
{
    public static class DatabaseUtilities
    {
        public static DbContextOptions GetDbOptions()
        {
            // Das garantiert eine DB-Connection die von EF Core nicht automatisch geschlossen wird
            SqliteConnection connection = new SqliteConnection("Data Source=:memory:");
            connection.Open();

            return new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;
        }

        public static void InitializeDatabase(SpengerShopContext db)
        {
            db.Database.EnsureCreated();

            db.Shops.AddRange(GetSeedingShops());
            db.SaveChanges();

            db.Categories.AddRange(GetSeedingCategories(db.Shops.Single(s => s.Id == 1)));
            //db.Categories.AddRange(GetSeedingCategories(db.Shops.Single(s => s.Id == 2)));
            db.SaveChanges();

            db.Customers.AddRange(GetSeedingCustomers());
            db.SaveChanges();

            // Seed Products
            db.Products.AddRange(GetSeedingProducts(db.Categories.Single(s => s.Id == 1)));
            db.SaveChanges();

            // Seed ...
            // db.SaveChanges();
        }

        private static List<Shop> GetSeedingShops()
        {
            return new List<Shop>()
            {
                new Shop("GMBH", "Test Shop 1", "Test Location 1", "IDontKnow 1", "Bs 1", new Address("Spengergasse", "20", "1050", "Wien"), new Guid("0c03ceb5-e2a2-4faf-b273-63839505f573")),
                new Shop("GMBH", "Test Shop 2", "Test Location 2", "IDontKnow 2", "Bs 2", new Address("Spengergasse", "21", "1051", "Wien"), new Guid("a0a6b711-fd27-4193-8595-325a62d82c5c")),
            };
        }

        private static List<Category> GetSeedingCategories(Shop shop)
        {
            return new List<Category>()
            {
                new Category("DVD", new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41"), shop),
                new Category("Bücher", new Guid("34993d53-a315-4e4d-aaf8-4406ec5a45b3"), shop),
            };
        }

        private static List<Customer> GetSeedingCustomers()
        {
            return new List<Customer>()
            {
                new Customer(new Guid("6ecfca13-f862-4c74-ac0e-30a2a62dd128"), Genders.Male, 123, "FirstName", "LastName", "test@test.at", new DateTime(1977, 05, 13), new DateTime(2023, 02, 01), new Address("", "", "", ""))
            };
        }

        private static List<Product> GetSeedingProducts(Category category)
        {
            return new List<Product>()
            {
                new Product("TestProdukt", 20, "1234567890123", "Testmaterial", new DateTime(2023, 03, 17), category)
            };
        }

        // ...
    }
}
