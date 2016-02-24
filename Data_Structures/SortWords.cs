using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists{
    class SortWords{

        static void sort(List<String> list) {
            list.Sort();
            foreach (var words in list){
                Console.WriteLine(words);
            }
        }
        static void Main(string[] args){

           Console.WriteLine("Enter line of words: ");
           var list = new List<String>();
           var line = Console.ReadLine().Split(' ');
           foreach (var words in line ){
               list.Add(words);
           }
           sort(list);
        }
    
    }
}
