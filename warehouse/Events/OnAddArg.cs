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
    }
}
