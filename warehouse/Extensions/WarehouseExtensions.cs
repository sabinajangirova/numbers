using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Extensions
{
    public static class WarehouseExtensions
    {
        private static Logger WExtensionsLog = LogManager.GetCurrentClassLogger();
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
                    var ptype = typeof(Product).GetProperties();
                    string[] firstLine = new string[ptype.Length];
                    int i = 0;
                    foreach(var p in ptype)
                    {
                        var pinf = p.GetCustomAttributes(typeof(DisplayNameAttribute), true);
                        firstLine[i] = (pinf[0] as DisplayNameAttribute).DisplayName;
                        i++;
                    }

                    writer.WriteLine(string.Join(",", firstLine));

                    foreach (KeyValuePair<Product, long> p in sender.ProductListing)
                    {
                        writer.WriteLine($"{p.Key.Name},{p.Key.Code},{p.Key.Price},{p.Key.Description},{p.Key.Unit}");
                        //writer.WriteLine($"{p.Key.Name},{p.Key.Code},{p.Key.Price},{p.Key.Description},{p.Value},{p.Key.Unit}");
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                WExtensionsLog.Error(ex.Message);
                //Console.WriteLine(ex.Message);
            }

            return false;
        }
    }
}
