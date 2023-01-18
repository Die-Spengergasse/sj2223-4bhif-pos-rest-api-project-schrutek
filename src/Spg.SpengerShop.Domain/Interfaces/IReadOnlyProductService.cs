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
        IEnumerable<Product> GetAll();
        Product GetByName(string name);

    }
}
