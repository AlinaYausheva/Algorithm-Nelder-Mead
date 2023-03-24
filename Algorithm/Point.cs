using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithm_Nelder_Mead
{
    public class Point
    {
        public double[] coordinate;

        public Point(int n)
        {
            coordinate = new double[n];
        }
    }
}
