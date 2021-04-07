using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using warehouse.Events;
using warehouse.Extensions;
using warehouse.Patterns.Command;
using warehouse.Reports;
namespace warehouse
{
    class Program
    {
        private static Logger log = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Warehouse w1 = new OpenWarehouse(new Address("Almaty", "Gogolya", 52, 050488), 50000, new Employee("Sabina", "security"));
            Warehouse w2 = new OpenWarehouse(new Address("Almaty", "Shevchenko", 27, 050400), 10000000, new Employee("Aysana", "manager"));
            Warehouse w3 = new ClosedWarehouse(new Address("Atyrau", "Azattyk", 2, 050008), 1000, new Employee("Tolkyn", "PR"));
            Warehouse w4 = new ClosedWarehouse(new Address("Kostanay", "Satpayev", 13, 051405), 1120000, new Employee("Ben", "volunteer"));

            var cs1 = new CancellationTokenSource();
            var ct1 = cs1.Token;
            var in1 = new Invoker();
                        
            var task1 = Task.Run(() =>
            {
                in1.SetCommand(new AddCommand(w1, new PieceProduct("Ice cream", "8551109", 6, "cold"), 100));
                in1.SetCommand(new AddCommand(w1, new PieceProduct("Cookies", "0000001", 10, "Crispy"), 800));
                in1.SetCommand(new AddCommand(w1, new LiquidProduct("Oil", "0840001", 50, "Rasp"), 98040));
                in1.SetCommand(new AddCommand(w1, new PieceProduct("Car", "9520001", 20000, "Black"), 2));
            }, ct1);

            foreach(Product p in w1.ProductListing.Keys)
            {
                //Console.WriteLine(p);
                log.Info(p);
            }

            
            var cs2 = new CancellationTokenSource();
            var ct2 = cs2.Token;
            var in2 = new Invoker();
            var task2 = Task.Run(() =>
            {
                in2.SetCommand(new AddCommand(w2, new PieceProduct("Cookies", "0000001", 10, "Crispy"), 100));
                in2.SetCommand(new AddCommand(w2, new PowderedProduct("Flour", "0008821", 3, "White"), 10000000000));
                in2.SetCommand(new AddCommand(w2, new LiquidProduct("Oil", "0840001", 50, "Rasp"), 600));
                in2.SetCommand(new AddCommand(w2, new PieceProduct("Car", "9520001", 20000, "Black"), 4));
            }, ct2);
            
            
            var cs3 = new CancellationTokenSource();
            var ct3 = cs3.Token;
            var in3 = new Invoker();
            var task3 = Task.Run(() =>
            {
                in3.SetCommand(new AddCommand(w3, new PieceProduct("Cookies", "0000001", 10, "Crispy"), 10));
                in3.SetCommand(new AddCommand(w3, new PowderedProduct("Flour", "0008821", 3, "White"), 100));
                in3.SetCommand(new AddCommand(w3, new LiquidProduct("Oil", "0840001", 50, "Rasp"), 500));
                in3.SetCommand(new AddCommand(w3, new PieceProduct("Car", "9520001", 20000, "Black"), 1));
            }, ct3);
            
            var cs4 = new CancellationTokenSource();
            var ct4 = cs4.Token;
            var in4 = new Invoker();
            var task4 = Task.Run(() =>
            {
                in4.SetCommand(new AddCommand(w4, new PieceProduct("Cookies", "0000001", 10, "Crispy"), 602));
                in4.SetCommand(new AddCommand(w4, new PowderedProduct("Flour", "0008821", 3, "White"), 85));
                in4.SetCommand(new AddCommand(w4, new LiquidProduct("Oil", "0840001", 50, "Rasp"), 100));
                in4.SetCommand(new AddCommand(w4, new PieceProduct("Car", "9520001", 20000, "Black"), 1000));
            }, ct4);

            Task.WaitAll(task1, task2, task3, task4);

            log.Info($"Searched 0840001 in w1. {w1.Search("0840001")}");
            log.Info($"Searched 0008821 in w1. {w1.Search("0008821")}");
            log.Info($"Searched 0008821 in w1. {w3.Search("0008821")}");
            //Console.WriteLine(w1.Search("0840001"));
            //Console.WriteLine(w1.Search("0008821"));
            //Console.WriteLine(w3.Search("0008821"));

            log.Info($"Overall price of products on the warehouses: {w1.OverallPrice() + w2.OverallPrice() + w3.OverallPrice() + w4.OverallPrice()}");
            //Console.WriteLine(w1.OverallPrice() + w2.OverallPrice() + w3.OverallPrice() + w4.OverallPrice());

            Warehouse[] warehouses = new Warehouse[] { w1, w2, w3, w4 };

            foreach(KeyValuePair<Product, long> p in w2.MoreThanThree().Result)
            {
                log.Info($"More than three units of product {p.Key.Name} {p.Value}");
                //Console.WriteLine($"{p.Key.Name} {p.Value}");
            }

            foreach (KeyValuePair<Product, long> p in w2.TopThree().Result)
            {
                log.Info($"Top three in amount {p.Key.Name} {p.Value}");
                //Console.WriteLine($"{p.Key.Name} {p.Value}");
            }

            foreach (Product p in w4.UniqueNames().Result)
            {
                log.Info($"Unique names {p.Name}");
                //Console.WriteLine($"{p.Name}");
            }

            int i = 0; //to count warehouses with powdered products
            foreach(Warehouse w in warehouses.NoPowdered().Result)
            {
                log.Info($"{i+1} warehouse with powdered products");
                //Console.WriteLine(i);
                i++;
            }
            foreach (KeyValuePair<string, double> p in warehouses.AverageOfProduct().Result)
            {
                log.Info($"Average of product {p.Key} {p.Value}");
                //Console.WriteLine($"{p.Key} {p.Value}");
            }

            w3.FileProducts(@"C:\Users\Мечта\source\repos\numbers\warehouse\1.csv");

            foreach(KeyValuePair<string, Product> p in DirectorySingleTon.Instance.DataBase)
            {
                log.Info($"Catalog {p.Key} {p.Value.ToString()}");
                //Console.WriteLine(p.Key + " " + p.Value.ToString());
            }
            
            Stopwatch s = new Stopwatch();
            s.Start();            
            var tsk1 = Task.Run(() => ControlSum(10, "sabina"));
            var tsk2 = Task.Run(() => ControlSum(10, "sabina"));
            tsk1.Wait();
            tsk2.Wait();
            s.Stop();
            log.Info($"Time taken: {s.Elapsed}");
            //Console.WriteLine($"Time taken: {s.Elapsed}");

            for(int lim = 4; lim < 128; lim = lim * 2)
            {
                s.Start();
                var tarr = new Task[lim];
                for (int j = 0; j < lim; j++)
                {
                    tarr[j] = Task.Run(() => ControlSum(100, "sabina"));
                }
                Task.WaitAll(tarr);
                s.Stop();
                log.Info($"Time taken: {s.Elapsed}");
                //Console.WriteLine($"Time taken: {s.Elapsed}");
            }

            s.Start();
            Task[] tfarr = new Task[16];

            for(int k = 0; k < 16; k++)
            {
                tfarr[k] = Task.Factory.StartNew(() => ControlSum(100, "sabina"));
            }
            
            Task.WaitAll(tfarr);
            log.Info($"Time taken by Task.Factory: {s.Elapsed}");
            //Console.WriteLine($"Time taken by Task.Factory: {s.Elapsed}");

            cs1.Cancel();
            cs2.Cancel();
            cs3.Cancel();
            cs4.Cancel();

            var assembly = Assembly.LoadFrom(@"C:\Program Files (x86)\Reference Assemblies\Microsoft\Framework\.NETFramework\v4.7.2\mscorlib.dll");
            foreach (var c in assembly.GetTypes().Where(p => p.FullName.Contains("System.Collections.Generic") && p.IsClass).ToList())
            {
                log.Info(c.Name);
            }

            var t = typeof(string);
            foreach(var inter in t.GetInterfaces())
            {
                log.Info(inter.Name);
            }
        }

        public static void OnAdding(Warehouse sender, OnAddArg e)
        {
            log.Info($"Event: {e.Message}");
            //Console.WriteLine(e.Message);
        }

        public static void OnAddingCommand(Warehouse sender, OnAddCommandArg e)
        {
            log.Info($"Event: {e.Message}");
            //Console.WriteLine(e.Message);
        }

        public static void ControlSum(int n, string s)
        {
            log.Info("Starting to compute hash");
            //Console.WriteLine("Starting to compute hash");
            for (int i = 0; i < n; i++)
            {
                var m = MD5.Create();
                m.ComputeHash(Encoding.ASCII.GetBytes(s));                
            }
            log.Info("Ended computing hash");
            //Console.WriteLine("Ended computing hash");
        }
    }
}
