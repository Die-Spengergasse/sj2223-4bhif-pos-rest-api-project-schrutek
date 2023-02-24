using Microsoft.Extensions.DependencyInjection;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Repository.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SpengerShopContext _db;

        public ProductRepository(SpengerShopContext db)
        {
            _db = db;
        }

        public void Create(Product newProduct)
        {
            _db.Products.Add(newProduct);
            _db.SaveChanges();
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        public Product GetByName(string name)
        {
            return _db.Products.SingleOrDefault(p => p.Name == name) 
                ?? throw new KeyNotFoundException("");
        }
    }
}
