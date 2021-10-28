using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTuan03_LTDT_1988216
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"../../input2.txt";
            AdjacencyMatrix g = new AdjacencyMatrix();
            FileHandler f = new FileHandler();

            f.ReadDataFromFile(filePath, ref g);

            // Thực hiện thuật toán Prim
            Console.Write("Nhap diem bat dau start = ");
            int start = int.Parse(Console.ReadLine());

            Console.WriteLine(" ==== PRIM ===== ");
            g.Prim(start);

            Console.WriteLine();
            Console.WriteLine(" ==== KRUSKAL ===== ");
            //g.Kruskal();

            /*            Kruskal k = new Kruskal(g.n);
                        k.KruskalMST(g.g);*/

            Kruskal2 k = new Kruskal2();
            k.Kruskal(filePath);
            
            /*a.readAdjacencyMatrix(filePath);*/

            /*a.showAdjacencyMatrix();*/

            Console.ReadLine();

        }
    }
}
