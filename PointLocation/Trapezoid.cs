using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;

namespace ComputationalGeometry.PointLocation
{
    public class Trapezoid
    {
        public Vertex Leftp { get; set; }
        public Vertex Rightp { get; set; }
        public HalfEdge Top { get; set; }
        public HalfEdge Bottom { get; set; }
        public Trapezoid HigherRightNeighbor { get; set; }
        public Trapezoid HigherLeftNeighbor { get; set; }
        public Trapezoid LowerLeftNeighbor { get; set; }
        public Trapezoid LowerRightNeighbor { get; set; }
    }
}
