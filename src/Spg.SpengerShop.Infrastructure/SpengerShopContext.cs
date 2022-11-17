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

        protected SpengerShopContext()
            : this(new DbContextOptions<DbContext>())
        { }

        public SpengerShopContext(DbContextOptions options)
            : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(e => e.Name);
            
            modelBuilder.Entity<Customer>().OwnsOne(p => p.PhoneNumber);
        }

        public void Test()
        {
            //ShoppingCart sc = new ShoppingCart(States.SENT, Guid.NewGuid());
            //sc.Id = 4711;
        }
    }
}
