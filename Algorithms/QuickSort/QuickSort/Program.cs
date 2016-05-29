using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickSort
{
    class Program<T> where T: IComparable<T>
    {
        public static void QuickSortLomuto(List<T> arr, int start, int end)
        {
            if (start >= end)
            {
                return;
            }

            T pivot = arr[start];
            int storeIndex = start + 1;
            T temp;

            for (int i = start + 1; i <= end; i++)
            {
                if (arr[i].CompareTo(pivot) < 0)
                {
                    temp = arr[i];
                    arr[i] = arr[storeIndex];
                    arr[storeIndex] = temp;
                }
                storeIndex++;                   
            }
            storeIndex--;
            temp = arr[start];
            arr[start] = arr[storeIndex];
            arr[storeIndex] = temp;
        }

        public static int QuickSortHoare(List<T> arr, int start, int end)
        {
            var pivot = arr[start];
            int left = start - 1;
            int right = end + 1;
            T temp;

            while (true)
            {
                right--;

                while (arr[right].CompareTo(pivot) > 0)
                {
                    left++;
                }

                while (arr[left].CompareTo(pivot) > 0)
                {
                    if (left < right)
                    {
                        temp = arr[left];
                        arr[left] = arr[right];
                        arr[right] = temp;
                    }
                    else
                    {
                        return right;
                    }
                }
            }
        }
    }
}
