using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IReadOnlyProductService
    {
        IQueryable<Product> Products { get; set; }

        IReadOnlyProductService Load();
        IEnumerable<ProductDto> GetData();

        Product GetByName(string name);
    }
}
