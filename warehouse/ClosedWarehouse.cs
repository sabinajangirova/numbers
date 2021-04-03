using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Events;

namespace warehouse
{
    public class ClosedWarehouse : Warehouse
    {
        public ClosedWarehouse(Address address, long surface, Employee responsible) : base(address, surface, responsible)
        {
        }
    }
}
