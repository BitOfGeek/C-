using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeapSort
{
    class Program
    {
        public void HeapSort(int[] arr)
        {
            int size= arr.Length;
            int mid = (arr.Length - 1) / 2;

            for (mid; mid >= 0; mid--)
            {
                Heapify(arr, arr.Length, mid);
            }

            for (int i = arr.Length - 1; i > 0; i--)
            {
                int temp = arr[i];
                arr[i] = arr[0];
                arr[0] = temp;
                size--;
                Heapify(arr, size, 0);
            }
        }

        public void Heapify(int[] arr, int size, int index)
        {
            int left = (index + 1) * 2 - 1;
            int right = (index + 1) * 2;
            int largest;

            if (left < size && arr[left] > arr[index])
            {
                largest = left;
            }
            else
            {
                largest = index;
            }

            if (right < size && arr[right] > arr[largest])
            {
                largest = right;
            } 
            
            if (largest != index)
            {
                int temp = arr[index];
                arr[index] = arr[largest];
                arr[largest] = temp;
                Heapify(arr, size, largest);
            }
        }
    }
}
