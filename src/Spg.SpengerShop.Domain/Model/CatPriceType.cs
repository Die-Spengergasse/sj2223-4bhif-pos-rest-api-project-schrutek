using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class CatPriceType : EntityBase
    {
        public CatPriceType() { }
        public CatPriceType(string shortName, string description)
        {
            ShortName = shortName;
            Description = description;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
    }
}
