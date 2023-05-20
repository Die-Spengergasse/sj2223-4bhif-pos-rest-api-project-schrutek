using Spg.SpengerShop.Domain.Dtos;
using Spg.SpengerShop.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Authorisation
{
    public class DbAuthService : IAuthService
    {
        public (UserDto?, bool) CheckCredentials(string userName, ReadOnlySpan<char> password)
        {
            // PWD hashen
            //                          PWD gehashed == DB-Hash (+ Salt)
            if (userName == "anna" && password     == "geheim")
            {
                UserDto user = new UserDto()
                {
                    UserName = userName,
                    FirstName = "Anna",
                    LastName = "Theke",
                    EMail = "anna@theke.at",
                    Role = "guest"
                };
                return (user, true);
            }
            // alternativ Excpetion ...
            return (null, false);
        }
    }
}
