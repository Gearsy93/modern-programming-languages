using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace интеграция
{
    public interface ISolver
    {
        bool[] Solve(int M, int[] m, int[] c);
        string GetName();
    }
}