using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public class MassiveProduct : Product
    {
        public MassiveProduct(string name, string code, decimal price, string description) : base(name, code, price, description) {
            Unit = Measurment.tons;
        }

        public override string ToString()
        {
            return $"{Name} - {Price} for 1 tone. {Description}. Type: Massive";
        }
    }
}

