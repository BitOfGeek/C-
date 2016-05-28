using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseArray
{
    class Program
    {
        public static String[] array;
        public static String[] reversed;
        public static int counter = 0;

        public static void Main()
        {
            array=Console.ReadLine().Split(' ').ToArray();
            reversed = new String[array.Length];
            int n = 0;
            ReverseArray(n);
            Console.WriteLine(string.Join(" ", reversed));
        }
        public static void ReverseArray(int n)
        {
            if (n!=array.Length)
            {
                ReverseArray(n + 1);
                reversed[counter] = array[n];
                counter++;
            }
        }
    }
}
