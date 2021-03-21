using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Extensions
{
    public static class WarehouseExtensions
    {
        public static List<Product> ProductIntersection(this Warehouse sender, Warehouse w){
            return sender.ProductListing.Keys.Intersect(w.ProductListing.Keys).ToList();
        }

        public static void TransferHalf(this Warehouse sender, Warehouse w)
        {
            foreach(KeyValuePair<Product, long> product in sender.ProductListing.Except(w.ProductListing).Where(p => p.Value > 1))
            {
                sender.TransferProduct(w, product.Key, product.Value / 2);
            }
        }
    }
}
