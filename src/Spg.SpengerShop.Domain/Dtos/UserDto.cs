using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EMail { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string Role { get; set; } = string.Empty;
        public string? Signature { get; set; }
    }
}
