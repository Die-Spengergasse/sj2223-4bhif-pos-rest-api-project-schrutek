using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class NewProductDto
    {
        /// <summary>
        /// Gibt den Produktnamen zurück, oder legt diesen fest
        /// </summary>
        //[Required()]
        //[MaxLength(20, ErrorMessage = "Länge darf 20 Zeichen nicht überschreiten!")]
        //[MinLength(3, ErrorMessage = "Länge darf 3 Zeichen nicht unterschreiten!")]
        //[StringLength(20, ErrorMessage = "Länge bitte zwischen 3 und 20", MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        //[Required()]
        //[StringLength(13, MinimumLength = 13, ErrorMessage = "Länge muss 13 Zeichen betragen!")]
        public string Ean13 { get; set; } = string.Empty;
        //[Required()]
        public DateTime ExpiryDate { get; set; }
        //[EmailAddress(ErrorMessage = "Das ist keine gültige E-mail-Adresse!")]
        //[RegularExpression("^[A-Za-z0-9]")]
        //public string EMail { get; set; } = string.Empty;
    }
}
