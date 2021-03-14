using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public class Address
    {
        public string City { get; }
        public string Street { get; }
        public int Building { get; }
        public int Zip { get; }

        public Address(string c, string s, int b, int z)
        {
            City = c;
            Street = s;
            Building = b;
            Zip = z;
        }

        public override string ToString()
        {
            return City + ", " + Street + " " + Building + ", " + Zip;
        }
    }
}
