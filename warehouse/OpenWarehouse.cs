using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Events;

namespace warehouse
{
    public class OpenWarehouse : Warehouse
    {
        public override event Action<Warehouse, Product, long> OnAdd;
        public override event Action<Warehouse, Product, long> OnAdd2;
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
                OnAdd2?.Invoke(this, p, amount);
                throw new Exception("You cannot add a powdered product to the open warehouse");
            }

            OnAdd?.Invoke(this, p, amount);
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
