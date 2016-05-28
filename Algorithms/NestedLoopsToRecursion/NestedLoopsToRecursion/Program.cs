using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NestedLoopsToRecursion
{
    class Program
    {
        static int n = 4;
        static int[] arr = new int[n];

        static void Main()
        {   
            Combinations(0);
        }

        static void Combinations(int index)
        {
            if (index >= arr.Length)
            {
                // A combination was found --> print it
                Console.WriteLine("(" + string.Join(", ", arr) + ")");
            }
            else
            {
                for (int i = 0; i<n ; i++)
                {
                    arr[index] = i;
                    Combinations(index+1);
                }
            }
        }
    }

}
