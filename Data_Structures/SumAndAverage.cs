using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lists{
    class SumAndAverage{

        static void action(List<int> list) {
            double sum=0;
            double count = 0;
            double avr=0;
            
            foreach(var num in list){
                sum += num;
                count++;
            }
            avr = sum / count;
            Console.WriteLine("{0} {1}", sum, avr);
            
        }
        static void Main(string[] args){
           Console.WriteLine("Enter line of numbers: ");
           var list = new List<int>();
           var line = Console.ReadLine();
           var numbers = line.Split(' ');
           foreach (var number in numbers){
               int num;
               if (Int32.TryParse(number, out num)){
                   list.Add(num);
               }
           }
       
            action(list);
        }
    }
}
