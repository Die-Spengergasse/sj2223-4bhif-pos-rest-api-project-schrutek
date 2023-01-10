using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class ProductDto
    {
        public ProductDto(string name, string ean13, DateTime expiryDate, DateTime? deliveryDate)
        {
            Name = name;
            Ean13 = ean13;
            ExpiryDate = expiryDate;
            DeliveryDate = deliveryDate;
        }

        public string Name { get; set; } = string.Empty;
        public string Ean13 { get; set; } = string.Empty;
        public DateTime ExpiryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
    }
}
