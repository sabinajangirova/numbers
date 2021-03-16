using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public class OpenWarehouse : Warehouse
    {
        public OpenWarehouse(Address address, long surface, Employee responsible) : base(address, surface, responsible)
        {
        }
        public override bool AddProduct (Product p, long amount)
        {
            if(amount < 0)
            {
                throw new ArgumentException("The amount of added product cannot be negative");
            }
            if(p is PowderedProduct)
            {
                throw new Exception("You cannot add a powdered product to the open warehouse");
            }

            if (ProductListing.ContainsKey(p))
            {
                ProductListing[p] += amount;
            }
            else
            {
                ProductListing.Add(p, amount);
            }

            return true;
        }
    }
}
