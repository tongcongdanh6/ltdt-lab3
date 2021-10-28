using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_BTTuan3_1988216
{
    class AdjacencyMatrix
    {
        public int n;
        public int[,] a;
        public bool readAdjacencyMatrix(string filename)
        {
            if (!File.Exists(filename))
            {
                Console.Write("File khong ton tai");
                return false;
            }

            StreamReader file = new StreamReader(filename);
            int numOfVertices = int.Parse(file.ReadLine()); // Đọc số lượng đỉnh

            n = numOfVertices; // Gán số lượng đỉnh
            a = new int[n, n]; // Khởi tạo ma trận n x n

            string line;
            int curLine = 0;
            while ((line = file.ReadLine()) != null)
            {
                string[] ch = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                for (int i = 0; i < ch.Length; i++)
                {
                    a[curLine, i] = int.Parse(ch[i]);
                }
                curLine++;
            }

            file.Close();

            return true;
        }
        public void showAdjacencyMatrix()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write("{0} ", a[i, j]);
                }
                Console.WriteLine();
            }
        }
    }
}
