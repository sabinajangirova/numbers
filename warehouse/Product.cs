using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public abstract class Product
    {
        public enum Measure
        {
            kilograms,
            liters,
            units,
            tons
        }
        public string Name { get; }
        public string Code { get; }
        public decimal Price { get; }
        public string Description { get; }
        public Measure Unit { get; }
        public Product(string name, string code, decimal price, string description)
        {
            Name = name;
            Code = code;
            Price = price;
            Description = description;
        }

        public override int GetHashCode()
        {
            return Code.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Product))
            {
                return false;
            }
            else return Code == ((Product)obj).Code;
        }
    }
}
