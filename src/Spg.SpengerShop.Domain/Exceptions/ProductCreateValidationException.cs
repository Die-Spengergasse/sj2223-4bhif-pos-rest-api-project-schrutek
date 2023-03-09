using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Domain.Exceptions
{
    public class ProductCreateValidationException : Exception
    {
        public ProductCreateValidationException()
        {
            // Logging
        }

        public ProductCreateValidationException(string message)
            : base(message)
        { }

        public ProductCreateValidationException(string message, Exception innerException)
            : base(message, innerException)
        { }

        private void Log()
        {
            // TODO: Logging
        }
    }
}
