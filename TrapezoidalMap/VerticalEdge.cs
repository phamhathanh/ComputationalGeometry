using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry.TrapezoidalMap
{
    class VerticalEdge : IVerticalEdge
    {
        public ITrapezoid LeftTrapezoid
        {
            get; set;
        }

        public ITrapezoid RightTrapezoid
        {
            get; set;
        }

        public IVertex Vertex
        {
            get; set;
        }

        public double XPosition
        {
            get; set;
        }

        public bool IsLowerExtension
        {
            get; set;
        }

        public VerticalEdge()
        {
        }
    }
}
