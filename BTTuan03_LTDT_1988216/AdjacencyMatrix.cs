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
            while(T.Count < n - 1)
            {
                if (!IsCircle(T, E[currentPick]))
                {
                    T.Add(E[currentPick]);
                }

                currentPick++;
            }

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
            for(int i = 0; i < listOldEdge.Count; i++)
            {
                listOldEdge.Add(listOld[i]);
            }

            // Thêm cạnh được xét vào
            listOldEdge.Add(edgeAdded); 

            Stack<int[]> stack = new Stack<int[]>();
            bool[] visitedEdge = new bool[listOldEdge.Count]; // Biến đánh dấu cạnh đã duyệt

            List<int[]> listCanhKeNhau = new List<int[]>();
            listCanhKeNhau.Add(listOldEdge[0]);
            visitedEdge[0] = true;
            stack.Push(listOldEdge[0]);

            while(stack.Count > 0)
            {
                int[] curEdge = stack.Pop();
                int u = curEdge[0];
                int v = curEdge[1];
                // Chọn các cạnh có trong List mà chưa được ghé thăm
                for(int i = 0; i < listOldEdge.Count; i++)
                {
                    if(!visitedEdge[i])
                    {
                        if (listOldEdge[i][0] == u || listOldEdge[i][0] == v || listOldEdge[i][1] == u || listOldEdge[i][1] == v)
                        {
                            stack.Push(listOldEdge[i]);
                            visitedEdge[i] = true;
                            listCanhKeNhau.Add(listOldEdge[i]);
                            break;
                        }
                    }

                }
            }

            // Nếu phần tử đầu List và cuối list có chứa đỉnh chung thì có chu trình
            int l = listCanhKeNhau.Count;

            if (l == 1) return false; // Nếu tập cạnh kề nhau chỉ có một phần tử thì không hình thành chu trình

            if(listCanhKeNhau[0][0] == listCanhKeNhau[l-1][0] || listCanhKeNhau[0][0] == listCanhKeNhau[l-1][1] ||
                listCanhKeNhau[0][1] == listCanhKeNhau[l-1][0] || listCanhKeNhau[0][1] == listCanhKeNhau[l-1][1])
            {
                // Có chu trình
                return true;
            }
            
            // Không có chu trình
            return false;
        }
    }
}
