using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public abstract class Warehouse
    {
        public Address Location { get; }
        public long Surface { get; }
        public Employee Responsible { get; set; }
        public Dictionary<Product, long> ProductListing { get; set; }

        public Warehouse(Address l, long surface, Employee responsible)
        {
            Location = l;
            Surface = surface;
            Responsible = responsible;
            ProductListing = new Dictionary<Product, long>();
        }

        public abstract bool AddProduct (Product p, long amount);

        public void SetResponsible(Employee person)
        {
            Responsible = person;
        }

        public bool TransferProduct(Warehouse w, Product p, long amount)
        {
            if (ProductListing.ContainsKey(p))
            {
                if (ProductListing[p] >= amount)
                {
                    try
                    {
                        w.AddProduct(p, amount);
                        ProductListing[p] -= amount;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    try
                    {
                        w.AddProduct(p, ProductListing[p]);
                        ProductListing[p] = 0;
                        return true;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }

            return false;
        }

        public bool Search(string c)
        {
            foreach(KeyValuePair<Product, long> p in ProductListing)
            {
                if(p.Key.Code == c)
                {
                    return true;
                }
            }

            return false;
        }

        public decimal OverallPrice()
        {
            decimal sum = 0;

            foreach(KeyValuePair<Product, long> p in ProductListing)
            {
                sum += p.Key.Price * p.Value;
            }

            return sum;
        }
    }
}
