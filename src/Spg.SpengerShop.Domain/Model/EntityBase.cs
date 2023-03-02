using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Model
{
    public class EntityBase
    {
        public int Id { get; private set; } // PK
        public DateTime? LastChangeDate { get; set; }
    }
}
