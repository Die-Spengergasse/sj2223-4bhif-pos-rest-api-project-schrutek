using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Authorisation
{
    public class AdAuthService : IAuthService
    {
        public (UserDto?, bool) CheckCredentials(string userName, ReadOnlySpan<char> password)
        {
            throw new NotImplementedException();
        }
    }
}
