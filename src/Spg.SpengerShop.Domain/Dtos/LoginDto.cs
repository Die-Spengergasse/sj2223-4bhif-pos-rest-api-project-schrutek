using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Dtos
{
    public class LoginDto
    {
        public string UserName { get; set; } = string.Empty;

        // PWD ReadOnlySpan<char>
    }
}
