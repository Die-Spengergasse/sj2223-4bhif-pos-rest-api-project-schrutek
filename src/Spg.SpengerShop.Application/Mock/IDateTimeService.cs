using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Application.Mock
{
    public interface IDateTimeService
    {
        DateTime UtcNow { get; }
    }
}
