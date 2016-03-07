using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class RootDance
    {
        static Dictionary<int, List<int>> friendships;
        static Dictionary<int, bool> visited;
        static int F;
        static int beginerNode;
        static int lenght = 1;
        static int count = 0;
        static int[] path;

        public static Dictionary<int, List<int>> ReadGraph()
        {
            F = int.Parse(Console.ReadLine());
            beginerNode = int.Parse(Console.ReadLine());
            friendships = new Dictionary<int, List<int>>();
            visited = new Dictionary<int, bool>();
            path = new int[16];

            for (int i = 0; i < F; i++)
            {
                int[] input = Console.ReadLine().Split(new char[] { ' ' },
                    StringSplitOptions.RemoveEmptyEntries).Select(n => Convert.ToInt32(n)).ToArray();
              
                if (!friendships.ContainsKey(input[0]))
                {
                    friendships.Add(input[0], new List<int>());
                    visited.Add(input[0], false);
                    friendships[input[0]].Add(input[1]);
                }
                else
                {
                    friendships[input[0]].Add(input[1]);
                }
                if (!friendships.ContainsKey(input[1]))
                {
                    friendships.Add(input[1], new List<int>());
                    visited.Add(input[1], false);
                    friendships[input[1]].Add(input[0]);
                }
                else
                {
                    friendships[input[1]].Add(input[0]);
                }
            }
            return friendships;
        }
        public static int[] DFS(int node)
        {
            
            
                
                if (!visited[node])
                {
                    visited[node] = true;
                    foreach (int n in friendships[node])
                    {
                      
                            lenght++;
                            DFS(n);
                            lenght--;
                        
                    }
                    //Console.WriteLine("node " + node);
                }
                else
                {
                    lenght++;
                    path[count] = lenght;
                    lenght--;
                    count++;
                }
            
                return path;
        }
    
        static void Main(string[] args)
        {
            ReadGraph();
            Console.WriteLine(DFS(beginerNode).Max()-2);
        }
    }

