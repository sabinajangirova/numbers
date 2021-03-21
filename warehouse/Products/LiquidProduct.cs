using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public class LiquidProduct : Product
    {
        public LiquidProduct(string name, string code, decimal price, string description) : base(name, code, price, description) {
            Unit = Measurment.litres;
        }
    }

}
