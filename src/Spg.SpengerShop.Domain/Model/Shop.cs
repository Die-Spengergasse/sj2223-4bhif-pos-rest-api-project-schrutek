using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class Shop : EntityBase, IFindableByGuid
    {
        protected Shop() { }
        public Shop(string companySuffix, string name, string location, string catchPhrase, string bs, Address address, Guid guid)
        {
            CompanySuffix = companySuffix;
            Name = name;
            Location = location;
            CatchPhrase = catchPhrase;
            Bs = bs;
            Address = address;
            Guid = guid;
        }

        public Guid Guid { get; private set; }
        public string Name { get; set; }
        public string CompanySuffix { get; set; }
        public string Location { get; set; }
        public string CatchPhrase { get; set; }
        public string Bs { get; set; }
        public Address? Address { get; set; }


        protected List<Category> _categories = new();
        public virtual IReadOnlyList<Category> Categories => _categories; // 1..n

    }
}
