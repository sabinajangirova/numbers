using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Extensions
{
    public static class ProductExtensions
    {
        public static string NameProduct(this Product p)
        {
            return $"{p.Code} - {p.Name}";
        }
    }
}
