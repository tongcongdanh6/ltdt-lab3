using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTuan03_LTDT_1988216
{
    class Kruskal
    {
        public int V { set; get; }
        public int[] parent { set; get; }
        public int MAXVALUE { set; get; }

        public Kruskal(int vertices)
        {
            V = vertices;
            parent = new int[V];
            MAXVALUE = int.MaxValue;
        }


        public int find(int i)
        {
            while (parent[i] != i)
                i = parent[i];
            return i;
        }

        // Does union of i and j. It returns
        // false if i and j are already in same
        // set.
        public void union1(int i, int j)
        {
            int a = find(i);
            int b = find(j);
            parent[b] = a;
        }

        public void KruskalMST(int[,] weight)
        {
            int minWeight = 0;

            // Initialize sets of disjoint sets.
            for (int i = 0; i < V; i++)
                parent[i] = i;

            for(int i = 0; i < V; i++)
            {
                for(int j = 0; j < V; j++)
                {
                    if (weight[i, j] == 0)
                        weight[i, j] = MAXVALUE;
                }
            }

            // Include minimum weight edges one by one
            int edge_count = 0;

            // In tập cạnh của cây khung
            Console.WriteLine("Tap canh cua cay khung: ");

            while (edge_count < V - 1)
            {
                int min = MAXVALUE, a = -1, b = -1;

                for (int i = 0; i < V; i++)
                {
                    for (int j = 0; j < V; j++)
                    {
                        if (find(i) != find(j) && weight[i, j] < min)
                        {
                            min = weight[i, j];
                            a = i;
                            b = j;
                        }
                    }
                }

                union1(a, b);

                Console.Write("{0} - {1}: {2} \n", a, b, min);
                edge_count++;
                minWeight += min;
            }
            Console.Write("\nTrong so cua cay khung: {0} \n", minWeight);
        }


    }
}
