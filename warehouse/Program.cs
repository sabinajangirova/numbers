using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using warehouse.Events;
using warehouse.Extensions;
using warehouse.Patterns.Command;
using warehouse.Reports;
namespace warehouse
{
    class Program
    {
        static void Main(string[] args)
        {
            Warehouse w1 = new OpenWarehouse(new Address("Almaty", "Gogolya", 52, 050488), 50000, new Employee("Sabina", "security"));
            Warehouse w2 = new OpenWarehouse(new Address("Almaty", "Shevchenko", 27, 050400), 10000000, new Employee("Aysana", "manager"));
            Warehouse w3 = new ClosedWarehouse(new Address("Atyrau", "Azattyk", 2, 050008), 1000, new Employee("Tolkyn", "PR"));
            Warehouse w4 = new ClosedWarehouse(new Address("Kostanay", "Satpayev", 13, 051405), 1120000, new Employee("Ben", "volunteer"));

            Invoker in1 = new Invoker();
            Task t = new Task(() =>
            {
                in1.SetCommand(new AddCommand(w1, new PieceProduct("Ice cream", "8551109", 6, "cold"), 100));
                in1.SetCommand(new AddCommand(w1, new PieceProduct("Cookies", "0000001", 10, "Crispy"), 800));
                in1.SetCommand(new AddCommand(w1, new LiquidProduct("Oil", "0840001", 50, "Rasp"), 98040));
                in1.SetCommand(new AddCommand(w1, new PieceProduct("Car", "9520001", 20000, "Black"), 2));
            });
            t.Start();
            t.Wait();

            Task t2 = new Task(() =>
            { in1.Run(); });

            t2.Start();
            t2.Wait();

            var in2 = new Invoker();
            in2.SetCommand(new AddCommand(w2, new PieceProduct("Cookies", "0000001", 10, "Crispy"), 100));
            in2.SetCommand(new AddCommand(w2, new PowderedProduct("Flour", "0008821", 3, "White"), 10000000000));
            in2.SetCommand(new AddCommand(w2, new LiquidProduct("Oil", "0840001", 50, "Rasp"), 600));
            in2.SetCommand(new AddCommand(w2, new PieceProduct("Car", "9520001", 20000, "Black"), 4));

            in2.Run();

            var in3 = new Invoker();
            in3.SetCommand(new AddCommand(w3, new PieceProduct("Cookies", "0000001", 10, "Crispy"), 10));
            in3.SetCommand(new AddCommand(w3, new PowderedProduct("Flour", "0008821", 3, "White"), 100));
            in3.SetCommand(new AddCommand(w3, new LiquidProduct("Oil", "0840001", 50, "Rasp"), 500));
            in3.SetCommand(new AddCommand(w3, new PieceProduct("Car", "9520001", 20000, "Black"), 1));

            in3.Run();

            var in4 = new Invoker();
            in4.SetCommand(new AddCommand(w4, new PieceProduct("Cookies", "0000001", 10, "Crispy"), 602));
            in4.SetCommand(new AddCommand(w4, new PowderedProduct("Flour", "0008821", 3, "White"), 85));
            in4.SetCommand(new AddCommand(w4, new LiquidProduct("Oil", "0840001", 50, "Rasp"), 100));
            in4.SetCommand(new AddCommand(w4, new PieceProduct("Car", "9520001", 20000, "Black"), 1000));

            in4.Run();
                        
            Console.WriteLine(w1.Search("0840001"));
            Console.WriteLine(w1.Search("0008821"));
            Console.WriteLine(w3.Search("0008821"));

            Console.WriteLine(w1.OverallPrice() + w2.OverallPrice() + w3.OverallPrice() + w4.OverallPrice());

            Warehouse[] warehouses = new Warehouse[] { w1, w2, w3, w4 };

            foreach (Product p in w1.ProductIntersection(w2)) {
                Console.WriteLine(p.NameProduct());
            }


            foreach(KeyValuePair<Product, long> p in w2.MoreThanThree())
            {
                Console.WriteLine($"{p.Key.Name} {p.Value}");
            }

            foreach (KeyValuePair<Product, long> p in w2.TopThree())
            {
                Console.WriteLine($"{p.Key.Name} {p.Value}");
            }

            foreach (Product p in w4.UniqueNames())
            {
                Console.WriteLine($"{p.Name}");
            }
            int i = 0;
            foreach(Warehouse w in warehouses.NoPowdered())
            {
                Console.WriteLine(i);
                i++;
            }
            foreach (KeyValuePair<string, double> p in warehouses.AverageOfProduct())
            {
                Console.WriteLine($"{p.Key} {p.Value}");
            }

            w3.FileProducts(@"C:\Users\Мечта\source\repos\numbers\warehouse\1.csv");

            foreach(KeyValuePair<string, Product> p in DirectorySingleTon.Instance.DataBase)
            {
                Console.WriteLine(p.Key + " " + p.Value.ToString());
            }
        }

        public static void OnAdding(Warehouse sender, OnAddArg e)
        {
            Console.WriteLine(e.Message);
        }

        public static void OnAddingCommand(Warehouse sender, OnAddCommandArg e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
