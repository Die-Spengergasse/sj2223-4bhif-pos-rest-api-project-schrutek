using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class Price : EntityBase, IFindableByGuid
    {
        public Guid Guid { get; private set; }
        public decimal Nett { get; set; }
        public int Tax { get; set; }
        public decimal Gross { get { return Nett * ((Tax / 100) + 1); } }

        public string ProductNavigationId { get; set; }
        public virtual Product ProductNavigation { get; private set; } = null!;
        public int CatPriceTypeNavigationId { get; set; }
        public virtual CatPriceType CatPriceTypeNavigation { get; private set; } = null!;

        protected Price() { }
        public Price(decimal nett, int tax, Product productNavigation, CatPriceType catPriceType)
        {
            Nett = nett;
            Tax = tax;
            ProductNavigation = productNavigation;
            CatPriceTypeNavigation = catPriceType;
        }
    }
}
