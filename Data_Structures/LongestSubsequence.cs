using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongestSubsequence{
    class LongestSubsequence{
        
        static void longSequence(List<int> list){
            int count=0;
            int largestCount=0;
            int largestNum = 0;
            foreach (int x in list) {
                foreach (int y in list){
                    if (x == y) {
                        count++;
                    }
                }
                if (count > largestCount){
                    largestCount = count;
                    largestNum = x;
                }
                count = 0;
            }
            var newList = new List<int>();
            for (int i = 0; i < largestCount; i++){
                newList.Add(largestNum);
            }
            foreach (var num in newList){
                Console.WriteLine(num);
            }


        }

        static void Main(string[] args){
            var list = new List<int>();
            Console.WriteLine("Enter line of numbers: ");
            var line = Console.ReadLine();
            var numbers = line.Split(' ');
            foreach (var number in numbers)
            {
                int num;
                if (Int32.TryParse(number, out num))
                {
                    list.Add(num);
                }
            }
            longSequence(list);
        }
    }
}
