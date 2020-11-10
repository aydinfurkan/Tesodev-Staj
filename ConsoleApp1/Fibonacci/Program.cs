using System;
using System.Text;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            double[] numbers = new double[1000];
            
            double fastFibonacci(int num)
            {
                if (num == 1 || num == 2)
                    return 1;

                if (numbers[num] == 0)
                {
                    numbers[num] = fastFibonacci(num - 1) + fastFibonacci(num - 2);
                }
                return numbers[num];
            }
            
            double fibonacci(double num)
            {
                if (num == 1 || num == 2)
                    return 1;
                
                return fibonacci(num - 1) + fibonacci(num - 2);
            }
            
            Console.WriteLine( fastFibonacci(50));
            Console.WriteLine( fibonacci(50));
            
        }
    }
}