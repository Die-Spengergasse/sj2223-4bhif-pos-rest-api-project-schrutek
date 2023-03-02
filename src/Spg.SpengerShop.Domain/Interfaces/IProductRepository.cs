using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IProductRepository
    {
        void Create(Product newProduct);

        IEnumerable<Product> GetAll();
    }
}
