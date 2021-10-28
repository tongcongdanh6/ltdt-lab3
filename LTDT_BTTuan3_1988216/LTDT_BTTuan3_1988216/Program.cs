using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_BTTuan3_1988216
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"../../input2.txt";
            Console.Write("Nhap start = ");
            int start = int.Parse(Console.ReadLine());

            Console.WriteLine(" ===== Prim MST ===== ");
            Prim p = new Prim();
            p.PrimMST(start, filePath);

            Console.WriteLine(" ===== Kruskal MST ===== ");
            Kruskal k = new Kruskal();
            k.KruskalMST(filePath);

            Console.ReadLine();

        }
    }
}
