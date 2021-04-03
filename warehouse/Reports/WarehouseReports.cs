using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Reports
{
    public static class WarehouseReports
    {
        public static async Task<Dictionary<Product, long>> MoreThanThree(this Warehouse sender)
        {
            return await Task.Run(() => { 
                return sender.ProductListing.Where(p => p.Value > 3).OrderBy(p => p.Value).ToDictionary(p => p.Key, p => p.Value);
            });
        }

        public static async Task<List<Product>> UniqueNames(this Warehouse sender)
        {
            return await Task.Run(() => {
                return sender.ProductListing.Keys.OrderBy(p => p.Name).ToList();
            }); 
        }

        public static async Task<Dictionary<Product, long>> TopThree(this Warehouse sender)
        {
            return await Task.Run(() =>
            {
                return sender.ProductListing.OrderByDescending(p => p.Value).Take(3).ToDictionary(p => p.Key, p => p.Value);
            });
        }

        public static async Task<List<Warehouse>> NoPowdered(this Warehouse[] sender)
        {
            return await Task.Run(() =>
            {
                return sender.Where(w => !w.ProductListing.Any(p => p.Key is PowderedProduct)).ToList();
            });
        }

        public static async Task<Dictionary<string, double>> AverageOfProduct(this Warehouse[] sender)
        {
            return await Task.Run(() =>
            {
                Dictionary<string, double> d = new Dictionary<string, double>();
                foreach (Warehouse w in sender)
                {
                    foreach (KeyValuePair<Product, long> p in w.ProductListing)
                    {
                        if (d.ContainsKey(p.Key.Name))
                        {
                            d[p.Key.Name] += (double)p.Value / sender.Length;
                        }
                        else
                        {
                            d.Add(p.Key.Name, (double)p.Value / sender.Length);
                        }
                    }
                }

                return d;
            });

        }
    }
}
