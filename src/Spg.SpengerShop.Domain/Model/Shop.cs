using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class Shop
    {
        public int Id { get; private set; }
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        
        //...
    }
}
