using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsertionSort
{
    class Program<T> where T: IComparable<T>
    {
        static void InsertionSort(List<T> list)
        {
            int n;

            for (int i = 1; i < list.Count; i++)
            {
                T value = list[i];
                n = i - 1;

                while ((n >= 0) && (list[n].CompareTo(value) > 0))
                {
                    list[n + 1] = list[n];
                    n--;
                }
                list[n + 1] = value;
            }
        }
    }
}
