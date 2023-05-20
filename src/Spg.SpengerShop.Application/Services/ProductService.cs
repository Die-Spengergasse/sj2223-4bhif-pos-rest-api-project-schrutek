using Spg.SpengerShop.Application.Mock;
using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Exceptions;
using Spg.SpengerShop.Domain.Interfaces;
using Spg.SpengerShop.Domain.Model;
using Spg.SpengerShop.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Services
{
    public class PKObject
    {
        public string Name { get; set; }
    }

    /// <summary>
    /// C.R.U.D.
    /// </summary>
    public class ProductService : IAddUpdateableProductService, IReadOnlyProductService
    {
        private readonly IRepositoryBase<Product> _productRepository;
        private readonly IReadOnlyRepositoryBase<Product> _readOnlyProductRepository;
        private readonly IReadOnlyRepositoryBase<Category> _readOnlyCategoryRepository;
        private readonly IDateTimeService _dateTimeService;

        public IQueryable<Product> Products { get; set; }

        public ProductService(
            IRepositoryBase<Product> productRepository,
            IReadOnlyRepositoryBase<Product> readOnlyProductRepository,
            IReadOnlyRepositoryBase<Category> readOnlyCategoryRepository,
            IDateTimeService dateTimeService)
        {
            _productRepository = productRepository;
            _readOnlyProductRepository = readOnlyProductRepository;
            _readOnlyCategoryRepository = readOnlyCategoryRepository;
            _dateTimeService = dateTimeService;
        }

        public IReadOnlyProductService Load()
        {
            Products = _readOnlyProductRepository
                .GetAll()
                .Result;
            return this;
        }

        public IEnumerable<ProductDto> GetData()
        {
            // TODO: Alternativ: LinQ-Select or AutoMapper
            List<ProductDto> result = new List<ProductDto>();
            foreach (Product product in Products)
            {
                ProductDto dto = new ProductDto() { Name = product.Name, Ean13 = product.Ean13, ExpiryDate = product.ExpiryDate };
            }
            return result;
        }

        // TODO: Implement Paging
        // DO: IEnumerable??<ProductDto> GetDataPaged()

        /// <summary>
        /// Show Product Details
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Product GetByName(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// * ExpiryDate muss 2 Wochen in der Zukunft liegen
        /// * Name muss unique sein
        /// </summary>
        /// <param name="dto"></param>
        public void Create(ProductDto dto)
        {
            // Init
            Category category;
            try
            {
                category = _readOnlyCategoryRepository.GetSingleOrDefaultByGuid<Category>(dto.CategoryId)
                    ?? throw new ProductCreateValidationException("Kategorie wurde nicht gefunden!");
            }
            catch (InvalidOperationException ex)
            {
                throw new ProductCreateValidationException("Kategorie mehrmals vorhanden!", ex);
            }

            // Validation
            // ExpiryDate muss 2 Wochen in der Zukunft liegen
            if (dto.ExpiryDate < _dateTimeService.UtcNow.AddDays(14))
            {
                throw new ProductCreateValidationException("Datum muss 2 Wochen in Zukunft liegen!");
            }
            // Name muss unique sein
            // TODO: Implementation
            if (_readOnlyProductRepository.GetById(dto.Name) is not null)
            {
                throw new ProductCreateValidationException("Das Produkt existiert bereits!");
            }

            // Mapping
            Product newProduct = new Product(dto.Name, 20, dto.Ean13, "M", dto.ExpiryDate, category);

            // Act + Save
            _productRepository.Create(newProduct);

            // [Save]
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
