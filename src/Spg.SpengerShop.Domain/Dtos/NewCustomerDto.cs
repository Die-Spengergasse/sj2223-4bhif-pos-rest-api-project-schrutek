using Spg.SpengerShop.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class NewCustomerDto
    {
        public GenderTypesDto Gender { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public PhoneNumber? PhoneNumber { get; set; }
        public string SocialSecurityNumber { get; set; } = string.Empty;
    }
}
