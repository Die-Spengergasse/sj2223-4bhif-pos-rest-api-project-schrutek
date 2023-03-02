using Bogus;
using Microsoft.EntityFrameworkCore;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Infrastructure
{
    public class SpengerShopContext : DbContext
    {
        public DbSet<Shop> Shops => Set<Shop>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Customer> Customers => Set<Customer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Price> Prices => Set<Price>();
        public DbSet<CatPriceType> CatPriceTypes => Set<CatPriceType>();
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

            modelBuilder.Entity<Customer>().OwnsOne(p => p.Address);
            modelBuilder.Entity<Shop>().OwnsOne(p => p.Address);
        }

        public void Seed()
        {
            Randomizer.Seed = new Random(181025);

            List<CatPriceType> catPriceTypes = new List<CatPriceType>()
            {
                new CatPriceType(){ ShortName="Aktion", Description="Aktionspreis", ValidFrom=new DateTime(2021, 01, 01) },
                new CatPriceType(){ ShortName="Normal", Description="Normalpreis", ValidFrom=new DateTime(2021, 01, 01) },
            };
            CatPriceTypes.AddRange(catPriceTypes);
            SaveChanges();


            List<Customer> customers = new Faker<Customer>("de").CustomInstantiator(f =>
                new Customer(
                    f.Random.Guid(),
                    f.Random.Enum<Genders>(),
                    f.Random.Long(111111, 999999),
                    f.Name.FirstName(Bogus.DataSets.Name.Gender.Female),
                    f.Name.LastName(),
                    f.Internet.Email(),
                    f.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-16)),
                    f.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now.AddDays(-2)),
                    new Address(
                        f.Address.StreetName(),
                        f.Address.BuildingNumber(),
                        f.Address.ZipCode(),
                        f.Address.City())
                ))
                .Rules((f, c) =>
                {
                    if (c.Gender == Genders.Male)
                    {
                        c.FirstName = f.Name.FirstName(Bogus.DataSets.Name.Gender.Male);
                    }
                    
                    //c.PhoneNumber = f.Phone.PhoneNumber();
                    c.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
                })
                .Generate(30)
                .ToList();

            Customers.AddRange(customers);
            SaveChanges();


            List<Shop> shops = new Faker<Shop>("de").CustomInstantiator(f =>
                new Shop(
                    f.Company.CompanySuffix(),
                    f.Company.CompanyName(),
                    f.Address.StreetAddress(),
                    f.Company.CatchPhrase(),
                    f.Company.Bs(),
                    new Address(f.Address.StreetName(), f.Address.BuildingNumber(), f.Address.ZipCode(), f.Address.City()),
                    f.Random.Guid()
            ))
            .Rules((f, s) =>
            {
                DateTime? lastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
                s.LastChangeDate = lastChangeDate;
            })
            .Generate(5)
            .ToList();
            Shops.AddRange(shops);
            SaveChanges();


            List<Category> categories = new Faker<Category>("de").CustomInstantiator(f =>
                new Category("", f.Random.Guid(), f.Random.ListItem(shops)) //f.Random.ListItem(f.Commerce.Categories)
            )
            .Rules((f, c) =>
            {
                c.Name = f.Random.ArrayElement(f.Commerce.Categories(10));
            })
            .Generate(30)
            .ToList();
            Categories.AddRange(categories);
            SaveChanges();


            List<Product> products = new Faker<Product>("de").CustomInstantiator(f =>
                new Product(
                    f.Commerce.ProductName(),
                    20,
                    f.Commerce.Ean13(),
                    f.Commerce.ProductMaterial(),
                    f.Date.Between(new DateTime(2022, 01, 01), new DateTime(2024, 12, 31)),
                    f.Random.ListItem(categories))
            )
            .Rules((f, p) =>
            {
                DateTime? deliverydate = p.ExpiryDate?.Date.OrNull(f, 0.5f);
                DateTime? lastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(500)
            .GroupBy(t => t.Name)
            .Select(g => g.First())
            .ToList();
            Products.AddRange(products);
            SaveChanges();


            List<Price> prices = new Faker<Price>().CustomInstantiator(f =>
                new Price(
                    f.Random.Decimal(10, 500),
                    20,
                    f.Random.ListItem(products),
                    catPriceTypes[0]))
            .Rules((f, pr) =>
            {
                pr.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(500)
            .ToList();
            Prices.AddRange(prices);
            SaveChanges();


            List<Price> prices2 = new Faker<Price>().CustomInstantiator(f =>
                new Price(
                    f.Random.Decimal(10, 500),
                    20,
                    f.Random.ListItem(products),
                    catPriceTypes[1]))
            .Rules((f, pr) =>
            {
                pr.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(10)
            .ToList();
            Prices.AddRange(prices2);
            SaveChanges();


            List<ShoppingCart> shoppingCarts = new Faker<ShoppingCart>("de").CustomInstantiator(f =>
                new ShoppingCart(
                    f.Commerce.ProductName(),
                    ShoppingCartStates.Sent,
                    f.Date.Between(DateTime.Now.AddYears(-10), DateTime.Now.AddDays(-10)),
                    f.Random.ListItem(customers),
                    f.Random.Guid())
            )
            .Rules((f, s) =>
            {
                s.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(200)
            .GroupBy(t => t.Guid)
            .Select(g => g.First())
            .ToList();
            ShoppingCarts.AddRange(shoppingCarts);
            SaveChanges();


            List<ShoppingCartItem> shoppingCartItems = new Faker<ShoppingCartItem>("de").CustomInstantiator(f =>
                new ShoppingCartItem(f.Random.ListItem(shoppingCarts), f.Random.ListItem(products))
            )
            .Rules((f, i) =>
            {
                i.LastChangeDate = f.Date.Between(new DateTime(2020, 01, 01), DateTime.Now).Date.OrNull(f, 0.3f);
            })
            .Generate(800)
            .ToList();
            ShoppingCartItems.AddRange(shoppingCartItems);
            SaveChanges();
        }
    }
}
