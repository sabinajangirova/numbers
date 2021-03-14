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
        public override bool AddProduct(Product p, long amount)
        {
            if(amount < 0)
            {
                return false;
            }
            if(p is PowderedProduct)
            {
                return false;
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
