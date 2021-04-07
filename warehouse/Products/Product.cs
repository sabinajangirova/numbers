using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public abstract class Product
    {
        [DisplayName("Product name")]
        public string Name { get; }

        [DisplayName("SKU Code")]
        public string Code { get; }

        [DisplayName("Price")]
        public decimal Price { get; }
        [DisplayName("Description")]
        public string Description { get; }
        
        public enum Measurment
        {
            kilograms, 
            litres, 
            units,
            tons
        }
        [DisplayName("Unit")]
        public Measurment Unit { get; set; }
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

        public abstract override string ToString();
    }
}
