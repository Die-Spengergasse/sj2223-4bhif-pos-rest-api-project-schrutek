using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class Category : EntityBase, IFindableByGuid
    {
        protected Category() { }
        public Category(string name, Guid guid, Shop shop)
        {
            Name = name;
            ShopNavigation = shop;
        }

        public string Name { get; set; }
        public Guid Guid { get; set; }


        public int ShopNavigationId { get; set; }
        public virtual Shop ShopNavigation { get; private set; } = null!;


        protected List<Product> _products = new();
        public virtual IReadOnlyList<Product> Products => _products;

    }
}
