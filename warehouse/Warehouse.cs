using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse
{
    public abstract class Warehouse
    {
        protected string Address { get; }
        protected long Surface { get; }
        protected Employee Responsible { get; set; }
        protected Dictionary<Product, long> ProductListing { get; set; }

        public Warehouse(string address, long surface, Employee responsible)
        {
            Address = address;
            Surface = surface;
            Responsible = responsible;
            ProductListing = new Dictionary<Product, long>();
        }

        public abstract bool AddProduct(Product p, long amount);

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
                    if(w.AddProduct(p, amount))
                    {
                        ProductListing[p] -= amount;
                        return true;
                    }
                 
                }
                else
                {
                    if(w.AddProduct(p, ProductListing[p]))
                    {
                        ProductListing[p] = 0;
                        return true;
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
