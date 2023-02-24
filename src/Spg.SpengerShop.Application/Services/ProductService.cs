using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services
{
    public class ProductService : IAddUpdateableProductService, IReadOnlyProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Create(Product newProduct)
        {
            _productRepository.Create(newProduct);
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDto> GetAll()
        {
            IEnumerable<Product> result = _productRepository.GetAll();

            return result.Select(p => new ProductDto(p.Name, p.Ean13, p.ExpiryDate, p.DeliveryDate));
        }

        public Product GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
