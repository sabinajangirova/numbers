using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Events;

namespace warehouse
{
    public abstract class Warehouse
    {
        public Address Location { get; }
        public long Surface { get; }
        public Employee Responsible { get; set; }
        public Dictionary<Product, long> ProductListing { get; set; }
        public abstract event Action<Warehouse, Product, long> OnAdd;
        public abstract event Action<Warehouse, Product, long> OnAdd2;
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

        public bool Search(string c){ return ProductListing.Any(p => p.Key.Code == c); }

        public decimal OverallPrice(){ return ProductListing.Sum(p => p.Key.Price * p.Value); }

        public static void OnAddHandler(Warehouse sender, Product p, long amount)
        {
            if (sender.ProductListing.ContainsKey(p))
            {
                Console.WriteLine($"The amount of {p.Name} on the warehouse  was {sender.ProductListing[p]}, added {amount} {p.Unit}");
            }
            else
            {
                Console.WriteLine($"The amount of {p.Name} on the warehouse  was 0, added {amount} {p.Unit}");
            }
        }

        public static void OnAddInvalid(Warehouse sender, Product p, long amount)
        {
            Console.WriteLine($"Cannot add {p.Name} in amount of {amount} {p.Unit} because open warehouse cannot store powdered products");
        }
    }
}
