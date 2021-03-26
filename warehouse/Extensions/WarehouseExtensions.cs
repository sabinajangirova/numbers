using System;
using System.Collections.Generic;
using System.IO;
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

        public static bool FileProducts(this Warehouse sender, string fileName)
        {
            try
            {
                using (var writer = new StreamWriter(@fileName))
                {
                    writer.WriteLine("SKU code,Name,Description,Price,Amount");

                    foreach (KeyValuePair<Product, long> p in sender.ProductListing)
                    {
                        writer.WriteLine($"{p.Key.Code},{p.Key.Name},{p.Key.Description},{p.Key.Price},{p.Value} {p.Key.Unit}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
