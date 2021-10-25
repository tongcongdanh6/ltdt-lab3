using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTTuan03_LTDT_1988216
{
    class FileHandler
    {
        public bool ReadDataFromFile(string filePath, ref AdjacencyMatrix adjMatrix)
        {
            if(!File.Exists(filePath))
            {
                Console.Write("File khong ton tai");
                return false;
            }

            StreamReader file = new StreamReader(filePath);
            int n = int.Parse(file.ReadLine()); // Đọc số lượng đỉnh

            adjMatrix.n = n; // Gán số lượng đỉnh
            adjMatrix.g = new int[n,n]; // Khởi tạo ma trận n x n

            string line;
            int curLine = 0;
            while((line = file.ReadLine()) != null)
            {
               string[] ch = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

               for(int i = 0; i < ch.Length; i++)
               {
                    adjMatrix.g[curLine, i] = int.Parse(ch[i]);
               }
               curLine++;
            }

            file.Close();

            return true;
        }
    }
}
