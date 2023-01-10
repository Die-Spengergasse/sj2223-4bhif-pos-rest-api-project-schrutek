using PaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spg.SpengerShop.Payment
{
    public class PaymentTest
    {
        public void DoPayment()
        {
            ETP etp = new ETPClient();
            etp.ManualReverseAsync(new ManualReverseRequest() { merchantID = 4711, mpayTID = 123456 });
        }
    }
}
