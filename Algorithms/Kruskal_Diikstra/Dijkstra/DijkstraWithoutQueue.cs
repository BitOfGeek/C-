namespace Dijkstra
{
    using System;
    using System.Collections.Generic;

    public static class DijkstraWithoutQueue
    {
        public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int destinationNode)
        {
            int n = graph.GetLength(0);
            int[] distance = new int[n];
            var used = new bool[n];
            int?[] previous = new int?[n];

            for (int i = 0; i < n; i++)
            {
                distance[i] = int.MaxValue;
            }

            distance[sourceNode] = 0;

            while (true)
            {
                int midDistance = int.MaxValue;
                int minNode = 0;

                for (int node = 0; node < n; node++)
                {
                    if (!used[node] && distance[node] < midDistance)
                    {
                        midDistance = distance[node];
                        minNode = node;
                    }

                    if (midDistance == int.MaxValue)
                    {
                        break;
                    }

                    used[minNode] = true;
                }
            }
        }
    }
}
