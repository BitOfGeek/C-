using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterpolationSearch
{
    class Program
    {
        public static int InterpolationSearch(int[] arr, int value)
        {
            int start = 0;
            int end = arr.Length - 1;
            int mid;

            while (arr[start] < value && arr[end] >= value)
            {
                mid = start + ((value - arr[start]) * (end - start)) / (arr[end] - arr[start]);

                if (arr[mid] < value)
                {
                    start = mid + 1;
                }
                else if (arr[mid] > value)
                {
                    end = mid - 1;
                }
                else
                {
                    return mid;
                }
                if (arr[start] == value)
                {
                    return start;
                }
                else
                {
                    return -1;
                }
            }
            return 1;
        }
    }
}
