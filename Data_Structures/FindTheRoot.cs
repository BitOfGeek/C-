using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    class FindTheRoot
    {
        static List<int>[] edges;
        static bool[] hasParent;
        static int N;
        static int M;

        public static void ReadGraph()
        {
            N = int.Parse(Console.ReadLine());
            M = int.Parse(Console.ReadLine());
            edges = new List<int>[M];
            hasParent= new bool[N];

            for (int i = 0; i < M; i++)
            {
                edges[i] = Console.ReadLine().Split(new char[] {' '}, 
                    StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
                int[] splitNode = edges[i].ToArray();
                hasParent[splitNode[1]] = true;
            }
        }
        public static void FindRoot()
        {
            int root=0;
            int count=0;
            for (int i = 0; i < N; i++)
            {
                if (!hasParent[i])
                {
                    count++;
                    if (count == 1)
                        root = i;
                }
            }
            if (count == 1)
                Console.WriteLine("the root is " + root);
            else if (count > 1)
                Console.WriteLine("multiple roots");
            else
                Console.WriteLine("no root");
        }
       
        
    }
    class Test
    {
        static void Main(string[] args)
        {
            Program.ReadGraph();
            Program.FindRoot();
        }
    }

