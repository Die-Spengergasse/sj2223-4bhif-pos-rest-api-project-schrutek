using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class NewCustomerDto
    {
        public GenderTypesDto Gender { get; set; }
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Min: 5, Max: 20!!")]
        public string FirstName { get; set; } = string.Empty;
        [StringLength(10, MinimumLength = 5, ErrorMessage = "Min: 5, Max: 10!!")]
        public string LastName { get; set; } = string.Empty;
        [EmailAddress()]
        public string EMail { get; set; } = string.Empty;
        public PhoneNumber? PhoneNumber { get; set; }
        public string SocialSecurityNumber { get; set; } = string.Empty;
    }
}
