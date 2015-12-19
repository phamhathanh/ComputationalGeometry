using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry.TrapezoidalMap
{
    class Point
    {
        private readonly double x, y;

        public double X
        {
            get
            {
                return x;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
