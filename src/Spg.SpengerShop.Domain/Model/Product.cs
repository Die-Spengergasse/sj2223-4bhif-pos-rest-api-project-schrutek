namespace Spg.SpengerShop.Domain.Model
{
    public class Product
    {
        public string Name { get; set; } = string.Empty;
        public string Ean13 { get; set; } = string.Empty;
        public int Stock { get; set; }
        public Guid Guid { get; set; }
        public DateTime ExpiaryDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public decimal Price { get; set; }
    }
}
