using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public class PieceProduct : Product
    {
        public PieceProduct(string name, string code, decimal price, string description) : base(name, code, price, description) {
            Measure Unit = Measure.units;
        }
    }
}
