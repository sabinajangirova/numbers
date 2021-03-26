using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public class PowderedProduct : Product
    {
        public PowderedProduct(string name, string code, decimal price, string description) : base(name, code, price, description) {
            Unit = Measurment.kilograms;
        }

        public override string ToString()
        {
            return $"{Name} - {Price} for 1 kilogram. {Description}. Type: Powdered";
        }
    }
}
