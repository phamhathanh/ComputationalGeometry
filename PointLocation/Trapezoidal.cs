using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;

namespace ComputationGeometry
{
    public class Trapezoidal
    {
        public Vertex Leftp { get; set; }
        public Vertex Rightp { get; set; }
        public HalfEdge Top { get; set; }
        public HalfEdge Bottom { get; set; }
        public Trapezoidal RightTopNeighbor { get; set; }
        public Trapezoidal LeftTopNeighbor { get; set; }
        public Trapezoidal LeftBottomNeighbor { get; set; }
        public Trapezoidal RightBottomNeighbor { get; set; }
    }
}
