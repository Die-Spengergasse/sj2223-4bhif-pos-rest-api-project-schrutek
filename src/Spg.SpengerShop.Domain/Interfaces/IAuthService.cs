using Spg.SpengerShop.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Interfaces
{
    public interface IAuthService
    {
        (UserDto?, bool) CheckCredentials(string userName, ReadOnlySpan<char> password);
    }
}
