using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateSequenceWithAQueue
{
    class CalculateSequenceWithAQueue
    {
        static void Main(string[] args)
        {
            var queueSequences = new Queue<int>();

            Console.WriteLine("Write a nuber: ");
            int N = Convert.ToInt32(Console.ReadLine());
            queueSequences.Enqueue(N);
            for (int i = 0; i < 50; i++)
            {
                var number = queueSequences.Dequeue();
                queueSequences.Enqueue(number + 1);
                queueSequences.Enqueue(number * 2 + 1);
                queueSequences.Enqueue(number + 2);
                Console.WriteLine(number);
            }
            while (queueSequences.Count > 0)
            {
                Console.WriteLine(queueSequences.Dequeue());
            }

        }
    }
}
