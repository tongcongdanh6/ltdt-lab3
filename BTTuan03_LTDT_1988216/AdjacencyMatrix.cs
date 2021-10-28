using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTuan03_LTDT_1988216
{
    class AdjacencyMatrix
    {
        public int n { set; get; } // Thuộc tính chứa số đỉnh

        public int[,] g { set; get; } // Ma trận kề

        public void Prim(int start)
        {
            // Tập đỉnh Y
            List<int> Y = new List<int>();

            // Tập đỉnh V\Y
            List<int> VY = new List<int>();

            // Tập cạnh T
            // Cấu trúc là List mảng 1 chiều
            // Mảng 1 chiều có cấu trúc [u, v, weight] - đỉnh từ u đến v có trọng số weight
            List<int[]> T = new List<int[]>();

            // Khởi tạo V\Y = tập đỉnh V
            for (int i = 0; i < n; i++)
                VY.Add(i);

            // Bước 1: Khởi tạo
            Y.Add(start); // Thêm vào tập đỉnh Y điểm khởi đầu
            VY.Remove(VY.Find(e => e == start)); // Xóa đỉnh start ra khỏi list V\Y

            // Bước 2:
            while(T.Count < n - 1)
            {
                List<int[]> UCV = new List<int[]>(); // List chứa cạnh ứng cử viên
                foreach (int u in Y)
                {
                    foreach (int v in VY)
                    {
                        if (g[u, v] > 0)
                        {
                            int[] e = new int[3] { u, v, g[u, v] };
                            UCV.Add(e);
                        }
                    }
                }

                // Tìm cạnh có trọng số nhỏ nhất trong danh sách các cạnh ứng cử viên
                int min = UCV.Min(m => m[2]);

                // Tất cả các cạnh thỏa MIN
                List<int[]> allMinEdge = UCV.FindAll(e => e[2] == min);

                // Nạp cạnh đầu tiên vào tập T
                T.Add(allMinEdge[0]);

                // Nạp đỉnh v vào tập Y
                Y.Add(allMinEdge[0][1]);

                VY.Remove(VY.Find(e => e == allMinEdge[0][1])); // Xóa đỉnh v ra khỏi list V\Y

            }



            // In tập cạnh của cây khung
            Console.WriteLine("Tap canh cua cay khung: ");
            foreach(int[] t in T)
            {
                Console.WriteLine("{0}-{1}: {2}", t[0], t[1], t[2]);
            }

            int sumOfWeight = T.Sum(x => x[2]); // Tổng trọng số của cây khung

            Console.WriteLine("Trong so cua cay khung: {0}", sumOfWeight);
        }

        public bool DFS2(int[,] g2, ref Queue<int> dsDuyet, bool[] visited, int start, int goal, ref int[] parent)
        {
            dsDuyet.Enqueue(start);
            visited[start] = true;

            if (start == goal)
            {
                return true;
            }

            for (int i = 0; i < g2.GetLength(0); i++)
            {
                if (g2[start, i] > 0 && (!visited[i]))
                {
                    if (DFS2(g2, ref dsDuyet, visited, i , goal, ref parent))
                    {
                        parent[i] = start;
                        return true;
                    }
                }
            }

            return false;
        }

        public bool checkForCycle(int node, int parent, bool[] vis, List<List<int>> adj)
        {
            vis[node] = true;
            for(int it = 0; it < adj.Count; it++)
            {
                if (vis[it] == false)
                {
                    if (checkForCycle(it, node, vis, adj) == true)
                        return true;
                }
                else if (it != parent)
                    return true;
            }

            return false;
        }

        public bool isCycle(int V, List<List<int>> adj)
        {
            bool[] vis = new bool[V];

            for (int i = 0; i < V; i++)
            {
                if (vis[i] == false)
                {
                    if (checkForCycle(i, -1, vis, adj))
                        return true;
                }
            }

            return false;
        }

/*        public bool IsCircle2(List<int[]> listOld, int[] edgeAdded)
        {
            List<int[]> listOldEdge = new List<int[]>();
            for (int i = 0; i < listOld.Count; i++)
            {
                listOldEdge.Add(listOld[i]);
            }

            listOldEdge.Add(edgeAdded);

            int[,] newAdj = convertEdgeListToAdjMatrix(listOldEdge);

            for(int i = 0; i < n; i++)
            {
                Queue<int> dsDuyetDinh = new Queue<int>();
                bool[] visited = new bool[n];
                int[] parent = new int[n];
                for(int j = 0; j <n; j++)
                {
                    visited[j] = false;
                    parent[j] = -1;
                }

                DFS2(newAdj, ref dsDuyetDinh, visited, i, n - 1, ref parent);

                if(dsDuyetDinh.Count > 0)
                {

                }
            }

            return false;

        }*/

        public void Kruskal()
        {
            List<int[]> E = new List<int[]>(); // Tập cạnh E;

            // Thêm các cạnh vào tập E
            for(int i = 0; i < n - 1; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if(g[i,j] > 0)
                    {
                        int[] edge = new int[3] { i, j, g[i, j] }; // [u,v,weight]
                        E.Add(edge);
                    }
                }
            }

            // Sắp xếp tập cạnh E theo trọng số tăng dần
            E.Sort((a, b) => a[2] - b[2]);

            // Khởi tạo tập cạnh được chọn T
            List<int[]> T = new List<int[]>();

            int currentPick = 0;
            while (T.Count < n - 1)
            {
                List<int[]> tmp = new List<int[]>();
                foreach(int[] t in T)
                {
                    tmp.Add(t);
                }

                tmp.Add(E[currentPick]);

                List<List<int>> adj = convertEdgeListToAdjList(tmp);



/*                if (!isCycle(T, E[currentPick]))
                {
                    T.Add(E[currentPick]);
                }*/

                if (!isCycle(n-1, adj))
                {
                    T.Add(E[currentPick]);
                }

                currentPick++;
            }

            /* int currentPick = 0;*/

            //T.Add(E[0]);
            /*            bool[] isPicked = new bool[E.Count];
                        for (int i = 0; i < E.Count; i++)
                            isPicked[i] = false;

                        while (T.Count < n - 1)
                        {
                            T.Add(E[currentPick]);
                            isPicked[currentPick] = true;

                            int[,] newadj = convertEdgeListToAdjMatrix(T);
                            if ((!isCyclicDisconntected(newadj, n) || T.Count <= 2) && !isPicked[currentPick])
                            {
                                T.Add(E[currentPick]);
                            }

                            currentPick++;
                        }*/

            // In tập cạnh của cây khung
            Console.WriteLine("Tap canh cua cay khung: ");
            foreach (int[] t in T)
            {
                Console.WriteLine("{0}-{1}: {2}", t[0], t[1], t[2]);
            }

            int sumOfWeight = T.Sum(x => x[2]); // Tổng trọng số của cây khung

            Console.WriteLine("Trong so cua cay khung: {0}", sumOfWeight);
        }

        public bool IsCircle(List<int[]> listOld, int[] edgeAdded)
        {
            // T là tập cạnh được truyền vào
            // phần tử bên trong là các cạnh [u,v,weight]

            // Copy các phần tử listOld qua listOldEdge vì vấn đề immutable của list
            List<int[]> listOldEdge = new List<int[]>();
            for (int i = 0; i < listOld.Count; i++)
            {
                listOldEdge.Add(listOld[i]);
            }

            // Thêm cạnh được xét vào
            listOldEdge.Add(edgeAdded);
            bool[] visitedEdge = new bool[listOldEdge.Count];
            int u = edgeAdded[0];
            int v = edgeAdded[1];

            List<int[]> tmp1 = new List<int[]>(); // Luu cac phan tu lien quan toi canh dc them

            for (int i = 0; i < listOldEdge.Count; i++) {
                if (!visitedEdge[i])
                {
                    if (listOldEdge[i][0] == u || listOldEdge[i][0] == v || listOldEdge[i][1] == u || listOldEdge[i][1] == v)
                    {
                        tmp1.Add(listOldEdge[i]);
                        visitedEdge[i] = true;
                        //listCanhKeNhau.Add(listOldEdge[i]);
                        //break;
                    }
                }
            }

            return false;

/*            while(true) {

                Stack<int[]> stack = new Stack<int[]>();
                bool[] visitedEdge = new bool[listOldEdge.Count]; // Biến đánh dấu cạnh đã duyệt

                List<int[]> listCanhKeNhau = new List<int[]>();
                listCanhKeNhau.Add(listOldEdge[listOldEdge.Count - 1]); // Thêm cạnh mới vào tập cạnh kề nhau
                visitedEdge[listOldEdge.Count - 1] = true;
                stack.Push(listOldEdge[listOldEdge.Count - 1]);



                while (stack.Count > 0)
                {
                    int[] curEdge = stack.Pop();
                    int u = curEdge[0];
                    int v = curEdge[1];
                    // Chọn các cạnh có trong List mà chưa được ghé thăm
                    for (int i = 0; i < listOldEdge.Count; i++)
                    {
                        if (!visitedEdge[i])
                        {
                            if (listOldEdge[i][0] == u || listOldEdge[i][0] == v || listOldEdge[i][1] == u || listOldEdge[i][1] == v)
                            {
                                stack.Push(listOldEdge[i]);
                                //visitedEdge[i] = true;
                                //listCanhKeNhau.Add(listOldEdge[i]);
                                //break;
                            }
                        }
                    }
                }

                // Nếu phần tử đầu List và cuối list có chứa đỉnh chung thì có chu trình
                int l = listCanhKeNhau.Count;

                if (l <= 2) return false; // Nếu tập cạnh kề nhau chỉ có từ 2 cạnh trở xuống thì không hình thành chu trình

                if (listCanhKeNhau[0][0] == listCanhKeNhau[l - 1][0] || listCanhKeNhau[0][0] == listCanhKeNhau[l - 1][1] ||
                    listCanhKeNhau[0][1] == listCanhKeNhau[l - 1][0] || listCanhKeNhau[0][1] == listCanhKeNhau[l - 1][1])
                {
                    // Có chu trình
                    return true;
                }
            }*/
        }

        private int[,] convertEdgeListToAdjMatrix(List<int[]> list)
        {
            int[,] adjMatrix = new int[n, n];

            for (int i = 0; i < list.Count; i++)
            {
                adjMatrix[list[i][0], list[i][1]] = list[i][2];
                adjMatrix[list[i][1], list[i][0]] = list[i][2];
            }

            return adjMatrix;
        }

        private List<List<int>> convertEdgeListToAdjList(List<int[]> list)
        {
            List<List<int>> adjList = new List<List<int>>();

            for(int i = 0; i < list.Count; i++)
            {
                List<int> tmp2 = new List<int>();

                foreach(int[] a in list)
                {
                    if(a[0] == i)
                    {
                        tmp2.Add(a[1]);
                    }
                }

                adjList.Add(tmp2);
                
            }

            return adjList;
        }

        /*        public bool isCyclicConntected(int[,] adj, int s,
                                                int V, bool[] visited)
                {

                    // Set parent vertex for every vertex as -1.
                    int[] parent = new int[V];
                    for (int i = 0; i < V; i++)
                        parent[i] = -1;

                    // Create a queue for BFS
                    Queue<int> q = new Queue<int>();

                    // Mark the current node as
                    // visited and enqueue it
                    visited[s] = true;
                    q.Enqueue(s);

                    while (q.Count != 0)
                    {

                        // Dequeue a vertex from
                        // queue and print it
                        int u = q.Dequeue();

                        // Get all adjacent vertices
                        // of the dequeued vertex u.
                        // If a adjacent has not been
                        // visited, then mark it visited
                        // and enqueue it. We also mark parent
                        // so that parent is not considered
                        // for cycle.
                        for (int i = 0; i < n; i++)
                        {
                            for(int j = 0; j < n; j++)
                            {
                                if(adj[i,j] > 0)
                                {
                                    if (!visited[j])
                                    {
                                        visited[j] = true;
                                        q.Enqueue(j);
                                        parent[j] = i;
                                    }
                                }
                                else if(parent[i] != j)
                                {
                                    return true;
                                }

                            }


        *//*                    if (!visited[v] && adj[i])
                            {
                                visited[v] = true;
                                q.Enqueue(v);
                                parent[v] = u;
                            }*/
        /*                    else if (parent[u] != v)
                            {
                                return true;
                            }*//*
                        }
                    }
                    return false;
                }*/



        /*        public bool isCyclicDisconntected(int [,] adj, int V)
                {

                    // Mark all the vertices as not visited
                    bool[] visited = new bool[V];

                    for (int i = 0; i < V; i++)
                    {
                        if (!visited[i] &&
                            isCyclicConntected(adj, i, V, visited))
                        {
                            return true;
                        }
                    }
                    return false;
                }*/

    }

}
