using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.RepositoryTest.Helpers
{
    public static class MockUtilities
    {
        public static Shop GetSeedingShop()
        {
            return new Shop("GMBH", "Test Shop 1", "Test Location 1", "IDontKnow 1", "Bs 1",
                new Address("Spengergasse", "20", "1050", "Wien"), new Guid("0c03ceb5-e2a2-4faf-b273-63839505f573"));
        }

        public static Category GetSeedingCategory(Shop shop)
        {
            return new Category("DVD", new Guid("d2616f6e-7424-4b9f-bf81-6aad88183f41"), shop);
        }

        public static Product GetSeedingProduct(Category category)
        {
            return new Product("Test Product 01", 10, "1234567891234", "MyProduct Material 1",
                                new DateTime(2023, 03, 17), category);
        }

        public static List<Product> GetSeedingProducts(Category category)
        {
            return new List<Product>()
            {
                new Product("Mobiltelefon", 20, "1234567890145", "Plastik", new DateTime(2023, 04, 12), category),
                new Product("Apfel", 20, "1234567890178", "Sauer", new DateTime(2023, 06, 19), category),
                new Product("Mobilkran", 20, "1234567890189", "Stahl", new DateTime(2023, 03, 30), category),
                new Product("Zwetscke", 20, "1234567890119", "Schnaps", new DateTime(2023, 05, 28), category),
                new Product("Cooles T-Shirt", 20, "1234567890173", "Stoff", new DateTime(2023, 07, 13), category),
            };
        }

        // ...
    }
}
