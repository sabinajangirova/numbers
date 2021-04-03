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
        public OpenWarehouse(Address address, long surface, Employee responsible) : base(address, surface, responsible)
        {
        }

    }
}
