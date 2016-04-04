using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euler_Path_Cycle
{

    class Euler_Path_Cycle
    {
        static Dictionary<String, List<String>> edges;
        static Dictionary<String, bool> visited;
        static List<String> oddDegrees;
        static Queue<String> EulerPathCycle = new Queue<string>();
        static int numberOfVertices;
        static bool path;
        static bool cylce;
        static String oddDFS;

        public static void ReadGraph()
        {
            edges = new Dictionary<String, List<String>>();
            visited = new Dictionary<String, bool>();
            var input = new String[2];

            Console.WriteLine("Enter number of edges: ");
            numberOfVertices = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the edges in the graph (pair of connected verices): "); // въвеждат се двойки сързани върхове (пример: 1 2)

            for (int i = 0; i < numberOfVertices; i++)
            {
                input = Console.ReadLine().Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries).ToArray();

                if (!edges.ContainsKey(input[0]))
                {
                    edges.Add(input[0], new List<String>());
                    visited.Add(input[0], false);
                    edges[input[0]].Add(input[1]);
                }
                else
                {
                    edges[input[0]].Add(input[1]);
                }
                if (!edges.ContainsKey(input[1]))
                {
                    edges.Add(input[1], new List<String>());
                    visited.Add(input[1], false);
                    edges[input[1]].Add(input[0]);
                }
                else
                {
                    edges[input[1]].Add(input[0]);
                }
            }
        }

        public static void CheckForPathOrCycle()
        {
            int countOfOddDegrees = 0;
            oddDegrees = new List<String>();
            foreach (KeyValuePair<String, List<String>> n in edges)
            {
                if (n.Value.Count % 2 != 0)
                {
                    countOfOddDegrees++;
                    oddDegrees.Add(n.Key);
                }
            }
            if (countOfOddDegrees == 0)
            {
                Console.WriteLine("There is an Euler Cycle in this graph.");
                cylce = true;
            }
            else if (countOfOddDegrees == 2)
            {
                Console.WriteLine("There is an Euler Path in this graph.");
                path = true;
            }
            else
            {
                Console.WriteLine("There is no Euler Path or Cycle in this graph.");
            }
        }

        public static void CheckForPathInCertainNodes()
        {
            var enteredNodes = new List<String>();
            var nodesToCheck = new Dictionary<String, List<String>>();
            //Console.WriteLine("Enter number of vertices to check: ");
            //int numberOfNodes = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the vertices: "); // въвеждат се на един ред с интервали върховете които искаме да проверим
            enteredNodes = Console.ReadLine().Split(new char[] { ' ' },
                        StringSplitOptions.RemoveEmptyEntries).ToList();
            //for (int i = 0; i < numberOfNodes; i++)
            //{
            //    nodesToCheck.Add(Console.ReadLine());
            //}
            int countOfOddDegrees = 0;

            foreach (KeyValuePair<String, List<String>> n in edges)
            {
                    if (enteredNodes.Contains(n.Key))
                    {
                        foreach (var temp in n.Value)
                        {
                            if (enteredNodes.Contains(temp))
                            {
                                if(!nodesToCheck.ContainsKey(n.Key))
                                nodesToCheck.Add(n.Key, new List<String>());
                                nodesToCheck[n.Key].Add(temp);
                            }
                        }                        
                    }
            }
            foreach (KeyValuePair<String, List<String>> n in nodesToCheck)
            {
                if (n.Value.Count % 2 != 0)
                {
                    countOfOddDegrees++;
                    oddDegrees.Add(n.Key);
                }
            }
            if (countOfOddDegrees == 0)
            {
                Console.WriteLine("There is an Euler Cycle.");
                
            }
            else if (countOfOddDegrees == 2)
            {
                Console.WriteLine("There is an Euler Path.");
            }
            else
            {
                Console.WriteLine("There is no Euler Path or Cycle.");
            }
        }

        public static void DFS(String node = null)
        {
            if (!visited[node])
            {
                visited[node] = true;
                EulerPathCycle.Enqueue(node);
                foreach (var n in edges[node])
                {
                    if (edges[n].Count == 1)
                    {
                        oddDFS = n;
                        continue;
                    }
                    else
                    {
                        DFS(n);
                    }


                }
                //Console.WriteLine("node " + node);
            }
        }

        public static void callDFS()
        {
            int count1 = 0;
            int count2 = 0;
            if (path == true) // dfs когато съществува път
            {
                foreach (var node in oddDegrees)
                {
                    count1++;
                    DFS(node);
                    if (oddDFS != null)
                    {
                        if (!visited[oddDFS])
                        {
                            EulerPathCycle.Enqueue(oddDFS);
                        }
                    }

                    foreach (KeyValuePair<String, List<String>> n in edges)
                    {
                        visited[n.Key] = false;
                    }
                    count2 = EulerPathCycle.Count;
                    Console.WriteLine("Euler Path {0}", count1);
                    for (int i = 0; i < count2; i++)
                    {
                        Console.WriteLine(EulerPathCycle.Dequeue());
                    }
                }

            }
            if (cylce == true) // dfs когато съществува цикъл
            {
                foreach (KeyValuePair<String, List<String>> node in edges)
                {
                    count1++;
                    DFS(node.Key);
                    foreach (KeyValuePair<String, List<String>> n in edges)
                    {
                        visited[n.Key] = false;
                    }
                    Console.WriteLine("Euler Cycle {0}", count1);
                    count2 = EulerPathCycle.Count;
                    for (int i = 0; i < count2; i++)
                    {
                        Console.WriteLine(EulerPathCycle.Dequeue());
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            
            ReadGraph();
            CheckForPathOrCycle();
            CheckForPathInCertainNodes();
            callDFS();

        }
    }
}
