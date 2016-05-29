using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSort
{
    class Program
    {
        static void MergeSort(int[] arr, int start, int end)
        {
            if (start == end)
            {
                return;
            }

            var mid = start + (end - start) / 2;

            MergeSort(arr, start, mid);
            MergeSort(arr, mid + 1, end);

            int left = start, right = mid + 1, result = 0;
            int[] temp = new int[end - start + 1];

            while (left <= mid && right <= end)
            {
                if (arr[left] <= arr[right])
                {
                    temp[result] = arr[left];
                    left++;
                }
                else
                {
                    temp[result] = arr[right];
                    right++;
                }
                result++;
            }

            while (left <= mid)
            {
                temp[result++] = arr[left++];
            }

            while (right <= end)
            {
                temp[result++] = arr[right++];
            }

            Array.Copy(temp, 0, arr, start, temp.Length);
        }
    }
}
