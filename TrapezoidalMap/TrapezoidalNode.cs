using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;

namespace ComputationalGeometry.TrapezoidalMap
{
    public class TrapezoidalNode : Node
    {
        private Func<List<HalfEdge>, Trapezoid> rectangleBoundary;
        Trapezoid Trapezoidal;

        public TrapezoidalNode(Trapezoid trapezoidal)
        {
            this.Trapezoidal = trapezoidal;
            LeftChildren = null;
            RightChildren = null;
        }
    }
}
