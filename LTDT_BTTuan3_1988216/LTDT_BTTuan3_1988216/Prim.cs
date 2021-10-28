using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_BTTuan3_1988216
{
    class Prim
    {
        public void PrimMST(int start, string filePath)
        {
            AdjacencyMatrix g = new AdjacencyMatrix();
            g.readAdjacencyMatrix(filePath);

            // Tập đỉnh Y
            List<int> Y = new List<int>();

            // Tập đỉnh V\Y
            List<int> VY = new List<int>();

            // Tập cạnh T chứa kết tập cạnh của cây khung bé nhất
            List<EDGE> T = new List<EDGE>();

            // Khởi tạo V\Y = tập đỉnh V
            for (int i = 0; i < g.n; i++)
                VY.Add(i);

            // Bước 1: Khởi tạo
            Y.Add(start); // Thêm vào tập đỉnh Y điểm khởi đầu
            VY.Remove(VY.Find(e => e == start)); // Xóa đỉnh start ra khỏi list V\Y

            // Bước 2:
            while (T.Count < g.n - 1)
            {
                List<EDGE> pickedEdges = new List<EDGE>(); // List chứa cạnh ứng cử viên
                foreach (int u in Y)
                {
                    foreach (int v in VY)
                    {
                        if (g.a[u, v] > 0)
                        {
                            EDGE e = new EDGE(u, v, g.a[u,v]);
                            pickedEdges.Add(e);
                        }
                    }
                }

                // Tìm cạnh có trọng số nhỏ nhất trong danh sách các cạnh ứng cử viên
                int min = pickedEdges.Min(m => m.weight);

                // Tất cả các cạnh thỏa MIN
                List<EDGE> allMinEdge = pickedEdges.FindAll(e => e.weight == min);

                // Nạp cạnh đầu tiên vào tập T
                T.Add(allMinEdge[0]);

                // Nạp đỉnh w vào tập Y
                Y.Add(allMinEdge[0].w);

                VY.Remove(VY.Find(e => e == allMinEdge[0].w)); // Xóa đỉnh v ra khỏi list V\Y

            }

            // In tập cạnh của cây khung
            Console.WriteLine("Tap canh cua cay khung: ");
            foreach (EDGE e in T)
            {
                Console.WriteLine("{0}-{1}: {2}", e.v, e.w, e.weight);
            }

            int sumOfWeight = T.Sum(x => x.weight); // Tổng trọng số của cây khung
            Console.WriteLine("Trong so cua cay khung: {0}", sumOfWeight);
        }
    }
 }
