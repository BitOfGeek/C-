using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseNumbersWithAStack
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter line of numbers: ");
            var stackOfNumbers = new Stack<int>();
            var line = Console.ReadLine();
            var numbers = line.Split(' ');
            foreach (var number in numbers)
            {
                int num;
                if (Int32.TryParse(number, out num))
                {
                    stackOfNumbers.Push(num);
                }
            }
            while (stackOfNumbers.Count >0)
            {
                Console.WriteLine(stackOfNumbers.Pop());
            }
        }
    }
}
