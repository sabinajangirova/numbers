using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Events
{
    public class OnAddArg : EventArgs
    {
        public DateTime Time { get; set; }
        public long Amount { get; set; }
        public Product Pt { get; set; }
        public string Message { get; set; }

        public OnAddArg(DateTime t, long a, Product p, string m)
        {
            Time = t;
            Amount = a;
            Pt = p;
            Message = m;
        }
    }
}
