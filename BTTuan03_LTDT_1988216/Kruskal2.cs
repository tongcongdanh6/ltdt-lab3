using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTuan03_LTDT_1988216
{
    class Kruskal2
    {
        private List<EDGE> InitListEdge(AdjacencyMatrix2 g)
        {
            List<EDGE> edgeList = new List<EDGE>();

            // Thêm các cạnh vào tập edgeList
            for (int i = 0; i < g.n - 1; i++)
            {
                for (int j = i + 1; j < g.n; j++)
                {
                    if (g.a[i, j] > 0)
                    {
                        EDGE newEdge = new EDGE();
                        newEdge.v = i;
                        newEdge.w = j;
                        newEdge.weight = g.a[i, j];
                        edgeList.Add(newEdge);
                    }
                }
            }

            return edgeList;
        }

        private void SortListEdge(ref List<EDGE> list)
        {
            // Sắp xếp tập cạnh E theo trọng số tăng dần
            list.Sort((a, b) => a.weight - b.weight);
        }


        public void Kruskal(string filePath)
        {
            AdjacencyMatrix2 g = new AdjacencyMatrix2();
            g.readAdjacencyMatrix(filePath);

            List<EDGE> T = new List<EDGE>();
            int nT = 0;

            List<EDGE> lstEdges = InitListEdge(g);
            int nEdgeCount = lstEdges.Count;

            int[] label = new int[g.n];

            for (int i = 0; i < g.n; i++)
                label[i] = i;

            // Sắp xếp danh sách cạnh theo trọng số tăng dần
            SortListEdge(ref lstEdges);

            // LƯU Ý: lstEdges giờ đã được sắp xếp

            int eMinIndex = 0; // chỉ mục của cạnh nhỏ nhất

            while (nT < g.n - 1){ 
                // trong khi chưa đạt số cạnh tối đa của cây khung
                if (eMinIndex < nEdgeCount)
                { 
                    // cạnh e nhỏ nhất chưa xét
                    //Cạnh này có tạo thành chu trình khi thêm vào không?
                    if (!IsCircle(lstEdges, eMinIndex, ref label))
                    {
                        // thêm cạnh có chỉ mục eMinIndex vào cây
                        T.Add(lstEdges[eMinIndex]);
                        nT++;
                    }
                    eMinIndex++;
                }
            }

            // in cây khung từ mảng 
            Console.WriteLine("Tap canh cua cay khung: ");
            foreach (EDGE e in T)
            {
                Console.WriteLine("{0}-{1}: {2}", e.v, e.w, e.weight);
            }

            int sumOfWeight = T.Sum(x => x.weight); // Tổng trọng số của cây khung
            Console.WriteLine("Trong so cua cay khung: {0}", sumOfWeight);

        }

        private bool IsCircle(List<EDGE> listEdges, int idx, ref int[] label)
        {
            // Kiểm tra nhãn của hai đỉnh ứng với cạnh idx có giống nhau hay không
            // Nếu có, trả về true, cạnh này tạo thành chu trình.
            if (label[listEdges[idx].v] == label[listEdges[idx].w])
                return true;
            else
            {
                // Nếu nhãn không giống nhau
                if (label[listEdges[idx].v] > label[listEdges[idx].w])
                    label[listEdges[idx].v] = label[listEdges[idx].w];
               
                for (int i = 0; i < label.Length; i++)
                {
                    if (label[i] == label[listEdges[idx].w])
                        label[i] = label[listEdges[idx].v];
                }
            }
            return false;
        }

    }
}
