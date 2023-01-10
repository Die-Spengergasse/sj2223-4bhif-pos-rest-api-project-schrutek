using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class NewProductAdminDto
    {
        [Required()]
        [MaxLength(120, ErrorMessage = "Länge darf 120 Zeichen nicht überschreiten!")]
        [MinLength(10, ErrorMessage = "Länge darf 10 Zeichen nicht unterschreiten!")]
        public string Name { get; private set; } = string.Empty;
        [Required()]
        [StringLength(13, MinimumLength = 13, ErrorMessage = "Länge muss 13 Zeichen betragen!")]
        public string Ean13 { get; set; } = string.Empty;
        [Required()]
        public DateTime ExpiryDate { get; set; }
        [Required()]
        public decimal Price { get; set; }
    }
}
