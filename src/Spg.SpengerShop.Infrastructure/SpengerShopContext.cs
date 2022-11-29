using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Infrastructure
{
    public class SpengerShopContext : DbContext
    {
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<ShoppingCart> ShoppingCarts => Set<ShoppingCart>();
        public DbSet<ShoppingCartItem> ShoppingCartItems => Set<ShoppingCartItem>();

        public SpengerShopContext()
        { }

        public SpengerShopContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(e => e.Name);
            
            modelBuilder.Entity<Customer>().OwnsOne(p => p.PhoneNumber);
        }

        public void Seed()
        {
            // Seed Customers
            List<Customer> customers = new Faker<Customer>("de").CustomInstantiator(f =>
            new Customer(
                GenderTypes.NA,
                f.Name.FindName(),
                f.Name.LastName(),
                f.Internet.Email(),
                f.Random.Guid(),
                DateTime.Now))
                .Rules((f, c) => 
                {
                    c.Gender = f.Random.Enum<GenderTypes>();
                    switch (c.Gender)
                    {
                        case GenderTypes.MALE:
                            c.FirstName = f.Name.FirstName(Bogus.DataSets.Name.Gender.Male);
                            break;
                        default:
                            c.FirstName = f.Name.FirstName(Bogus.DataSets.Name.Gender.Female);
                            break;
                    }
                })
            .Generate(200)
            .ToList();

            Customers.AddRange(customers);
            SaveChanges();

            // Seed Products
            List<Product> products = new Faker<Product>("de").CustomInstantiator(f =>
            new Product(
                f.Commerce.ProductName(),
                f.Commerce.Ean13(),
                f.Random.Int(0, 20),
                f.Random.Guid(),
                f.Date.Between(DateTime.Now.AddDays(5), DateTime.Now.AddDays(100)),
                f.Date.Between(DateTime.Now.AddDays(2), DateTime.Now.AddDays(14)), 
                f.Random.Decimal(5M, 5000M)))
                .Rules((f, c) =>
                { })
            .Generate(800)
            .GroupBy(g => g.Name)
            .Select(g => g.First())
            .ToList();

            Products.AddRange(products);
            SaveChanges();
        }
    }
}
