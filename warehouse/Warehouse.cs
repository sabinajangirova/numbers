using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using warehouse.Events;

namespace warehouse
{
    public abstract class Warehouse
    {
        private Logger WarehouseLogger = LogManager.GetCurrentClassLogger();
        public Address Location { get; }
        public long Surface { get; }
        public Employee Responsible { get; set; }
        public Dictionary<Product, long> ProductListing { get; set; }
        
        public delegate void AddHandler(Warehouse sender, OnAddArg e);
        public event AddHandler Notify;
        
        public Warehouse(Address l, long surface, Employee responsible)
        {
            Location = l;
            Surface = surface;
            Responsible = responsible;
            ProductListing = new Dictionary<Product, long>();
            
        }

        
        public bool AddProduct(Product p, long amount)
        {
            if (amount < 0)
            {
                throw new ArgumentException("The amount of added product cannot be negative");
            }
            if (this.GetType().IsAssignableFrom(typeof(OpenWarehouse)) && p is PowderedProduct)
            {
                Notify?.Invoke(this, new OnAddArg(DateTime.Now, amount, p, $"Tried to add a powdered product {p.Name} in amount of {amount} {p.Unit} to an open warehouse at time {DateTime.Now}"));
                throw new Exception("You cannot add a powdered product to the open warehouse");
            }

            Notify?.Invoke(this, new OnAddArg(DateTime.Now, amount, p, $"Successfully added a product {p.Name} in amount of {amount} {p.Unit} to an open warehouse at time {DateTime.Now}"));
            if (ProductListing.ContainsKey(p))
            {
                ProductListing[p] += amount;
            }
            else
            {
                ProductListing.Add(p, amount);
            }

            if(!DirectorySingleTon.Instance.DataBase.ContainsKey(p.Code)) DirectorySingleTon.Instance.DataBase.Add(p.Code, p);
            return true;
        }
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
                        WarehouseLogger.Error(ex.Message);
                        //Console.WriteLine(ex.Message);
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
                        WarehouseLogger.Error(ex.Message);
                        //Console.WriteLine(ex.Message);
                    }
                }
            }

            return false;
        }

        public bool Search(string c){ return ProductListing.Any(p => p.Key.Code == c); }

        public decimal OverallPrice(){ return ProductListing.Sum(p => p.Key.Price * p.Value); }
    }
}
