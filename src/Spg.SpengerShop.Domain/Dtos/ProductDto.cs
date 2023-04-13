using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; } = string.Empty;
        public string Ean13 { get; set; } = string.Empty;
        public DateTime? ExpiryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public Guid CategoryId { get; set; }
    }
}
