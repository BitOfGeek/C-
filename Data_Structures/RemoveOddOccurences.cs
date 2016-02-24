using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoveOddOccurences{
    class RemoveOddOccurences{
        static List<int> longSequence(List<int> list){
            var dictionary = list.GroupBy(num => num)
                        .ToDictionary(group => group.Key, group => group.Count());
            foreach (KeyValuePair<int, int> kvp in dictionary){
                if (kvp.Value % 2 != 0){
                    list.RemoveAll(item => item == kvp.Key);
                }
            }
            foreach (var num in list){
                Console.WriteLine(num);
            }

            return list;
        }

        static void Main(string[] args){
            var list = new List<int>();
            Console.WriteLine("Enter line of numbers: ");
            var line = Console.ReadLine();
            var numbers = line.Split(' ');
            foreach (var number in numbers){
                int num;
                if (Int32.TryParse(number, out num)) {
                    list.Add(num);
                }
            }
            longSequence(list);
        }
    }
}
