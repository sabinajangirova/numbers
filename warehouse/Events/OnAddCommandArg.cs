using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Events
{
    //similar to OnAddArd but for command class
    class OnAddCommandArg : EventArgs
    {
        public DateTime Time { get; set; }
        public long Amount { get; set; }
        public Product Pt { get; set; }
        public string Message { get; set; }

        public OnAddCommandArg(DateTime t, long a, Product p, string m)
        {
            Time = t;
            Amount = a;
            Pt = p;
            Message = m;
        }
    }
}
