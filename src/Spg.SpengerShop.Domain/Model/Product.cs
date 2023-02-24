using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace Spg.SpengerShop.Domain.Model
{
    public class Product
    {
        public string Name { get; private set; } = string.Empty; // PK
        public string Ean13 { get; set; } = string.Empty;
        public int Stock { get; set; }
        //public Guid Guid { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal Price { get; set; }

        protected Product()
        { }
        public Product(string name, string ean13, int stock, DateTime expiryDate, DateTime? deliveryDate, decimal price)
        {
            Name = name;
            Ean13 = ean13;
            Stock = stock;
            //Guid = guid;
            ExpiryDate = expiryDate;
            DeliveryDate = deliveryDate;
            Price = price;
        }
    }
}
