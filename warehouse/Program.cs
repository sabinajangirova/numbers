using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            w1.AddProduct(new PieceProduct("Cookies", "0000001", 10, "Crispy"), 800);
            try
            {
                w1.AddProduct(new PowderedProduct("Flour", "0008821", 3, "White"), 100000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            w1.AddProduct(new LiquidProduct("Oil", "0840001", 50, "Rasp"), 98040);
            w1.AddProduct(new MassiveProduct("Car", "9520001", 20000, "Black"), 2);

            w2.AddProduct(new PieceProduct("Cookies", "0000001", 10, "Crispy"), 100);

            try
            {
                w2.AddProduct(new PowderedProduct("Flour", "0008821", 3, "White"), 10000000000);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            w2.AddProduct(new LiquidProduct("Oil", "0840001", 50, "Rasp"), 600);
            w2.AddProduct(new MassiveProduct("Car", "9520001", 20000, "Black"), 4);

            w3.AddProduct(new PieceProduct("Cookies", "0000001", 10, "Crispy"), 10);
            w3.AddProduct(new PowderedProduct("Flour", "0008821", 3, "White"), 100);
            w3.AddProduct(new LiquidProduct("Oil", "0840001", 50, "Rasp"), 500);
            w3.AddProduct(new MassiveProduct("Car", "9520001", 20000, "Black"), 1);

            w4.AddProduct(new PieceProduct("Cookies", "0000001", 10, "Crispy"), 602);
            w4.AddProduct(new PowderedProduct("Flour", "0008821", 3, "White"), 85);
            w4.AddProduct(new LiquidProduct("Oil", "0840001", 50, "Rasp"), 100);
            w4.AddProduct(new MassiveProduct("Car", "9520001", 20000, "Black"), 1000);

            Console.WriteLine(w1.Search("0840001"));
            Console.WriteLine(w1.Search("0008821"));
            Console.WriteLine(w3.Search("0008821"));

            Console.WriteLine(w1.OverallPrice() + w2.OverallPrice() + w3.OverallPrice() + w4.OverallPrice());
        }
    }
}
