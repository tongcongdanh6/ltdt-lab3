using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LTDT_BTTuan3_1988216
{
    class EDGE
    {
        public int v { set; get; }
        public int w  {set; get; }
        public int weight { set; get; }

        public EDGE() { }

        public EDGE(int src, int dest, int Weight) 
        {
            v = src;
            w = dest;
            weight = Weight;
        }
    }
}
