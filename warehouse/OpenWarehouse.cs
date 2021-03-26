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
       /* public override event Action<Warehouse, Product, long> OnAdd;
        public override event Action<Warehouse, Product, long> OnAdd2;*/
        public OpenWarehouse(Address address, long surface, Employee responsible) : base(address, surface, responsible)
        {
        }

    }
}
