using Moq;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.ApplicationTest.Helpers
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

        // ...
    }
}
