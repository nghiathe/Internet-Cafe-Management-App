using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class BillingPrintDTO
    {
        public string BillingID { get; set; }
        public DateTime BillingDate { get; set; }
        public string LastName { get; set; }
        public decimal Amount { get; set; }
    }
}
