using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Exceptions
{
    public class CustomerValidationExcpetion : Exception
    {
        public CustomerValidationExcpetion()
        {
            // Logging
        }

        public CustomerValidationExcpetion(string message)
            : base(message)
        { }

        public CustomerValidationExcpetion(string message, Exception innerException)
            : base(message, innerException)
        { }

        private void Log()
        {
            // TODO: Logging
        }
    }
}