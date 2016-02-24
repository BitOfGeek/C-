using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CountOfOccurrences
{
    class CountOfOccurrences{ 
        static void occurrences(List<int> list){
            var dictionary = list.GroupBy(num => num)
                        .ToDictionary(group => group.Key, group => group.Count());
            foreach (KeyValuePair<int, int> kvp in dictionary){
                Console.WriteLine("Key = {0}, Value = {1}", kvp.Key, kvp.Value);

            }

        }
        static void Main(string[] args){

            Console.WriteLine("Enter line of numbers: ");
            var list = new List<int>();
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
            occurrences(list);
        }
    
    }
}
