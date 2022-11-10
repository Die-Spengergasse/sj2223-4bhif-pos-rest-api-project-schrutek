namespace Spg.SpengerShop.Domain.Model
{
    public class Product
    {
        protected Product()
        { }
        public Product(string name, string ean13, int stock, Guid guid, DateTime expiaryDate, DateTime? deliveryDate, decimal price)
        {
            Name = name;
            Ean13 = ean13;
            Stock = stock;
            Guid = guid;
            ExpiaryDate = expiaryDate;
            DeliveryDate = deliveryDate;
            Price = price;
        }

        public string Name { get; private set; } = string.Empty; // PK
        public string Ean13 { get; set; } = string.Empty;
        public int Stock { get; set; }
        public Guid Guid { get; set; }
        public DateTime ExpiaryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal Price { get; set; }


    }
}
