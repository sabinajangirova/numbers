using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace numbers
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new Random();
            int[] numbers = new int[10];

            for(int i = 0; i < 10; i++)
            {
                numbers[i] = r.Next(0, Int32.MaxValue);
            }
            long sum = 0;
            for(int i = 0; i < 10; i++)
            {
                Console.WriteLine(numbers[i] + " " +IsPrime(numbers[i]) + " and " + IsEven(numbers[i]));
                sum += numbers[i];
            }
            
            Console.WriteLine("The sum is " + sum);
        }

        public static string IsEven(int n)
        {
            if(n % 2 == 0)
            {
                return "even";
            }

            return "not even";
        }
        public static string IsPrime(int n)
        {
            if(n <= 1)
            {
                return "is not prime";
            } 

            for(int i = 2; i <= Math.Sqrt(n); i++)
            {
                if(n%i == 0)
                {
                    return "is not prime";
                }
            }

            return "is prime";
        }
    }
}
