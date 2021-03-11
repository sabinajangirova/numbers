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
            Warehouse w1 = new OpenWarehouse("almaty", 100, new Employee("sabina", "security"));
            Warehouse w2 = new OpenWarehouse("atyrau", 1000, new Employee("aizhan", "manager"));
            Warehouse w3 = new ClosedWarehouse("astana", 1010, new Employee("madina", "leader"));

            Console.WriteLine(w1.AddProduct(new PieceProduct("chocolate", "0110082", 90, "sweet"), 100));
            Console.WriteLine(w1.AddProduct(new PieceProduct("chocolate", "0110082", 90, "sweet"), 100));
            Console.WriteLine(w1.AddProduct(new PieceProduct("cheese", "01100/4", 1000, "yellow"), 100));

            Console.WriteLine(w2.AddProduct(new PowderedProduct("flour", "0110482", 5, "white"), 85));
            Console.WriteLine(w2.AddProduct(new LiquidProduct("juice", "05292", 20, "refreshing"), 50));
            Console.WriteLine(w2.AddProduct(new MassiveProduct("car", "859f8r", 10000, "cheap"), 2));

            Console.WriteLine(w3.AddProduct(new PieceProduct("pencil", "0182082", 10, "sharp"), 1000));
            Console.WriteLine(w3.AddProduct(new PowderedProduct("flour", "0110482", 5, "white"), 85));

            Console.WriteLine(w1.Search("000000"));
            Console.WriteLine(w1.Search("0110082"));
            Console.WriteLine(w2.Search("0110482"));

            Console.WriteLine(w1.OverallPrice());
            Console.WriteLine(w2.OverallPrice());
            Console.WriteLine(w2.OverallPrice());
        }
    }
}
