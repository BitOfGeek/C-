using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CombinationsWithRepetition
{
    class Program
    {
        static int n = 3;
        static int k = 2;
        static int[] arr = new int[k];

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
                for (int i = 1; i < n+1; i++)
                {
                    arr[index] = i;
                    Combinations(index + 1);
                }
            }
        }
    }
}
