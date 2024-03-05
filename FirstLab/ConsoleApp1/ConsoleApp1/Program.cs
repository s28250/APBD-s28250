using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please answer a number");
            if (int.TryParse(Console.ReadLine(), out int value)) {
                if (value > 0)
                {
                    Console.WriteLine("The value is positive one");
                }
                else if(value < 0)
                {
                    Console.WriteLine("The value is a negative one");
                }
                else
                {
                    Console.Write("Thw value is 0");
                }
            }
            else
            {
                Console.WriteLine("Error");
            }
            Console.WriteLine("Press to exit");
            Console.ReadKey();
        }
    }
}
