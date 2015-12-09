using ComputationalGeometry.Common;
using ComputationGeometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PointLocation
{
    public class TrapezoidalMap
    {
        public static VertexNode Root { get; set; }
        public static IEnumerable<Vertex> Vertices { get; set; }
        public static IEnumerable<HalfEdge> HalfEdges { get; set; }
        public static IEnumerable<Face> Faces { get; set; }

        public TrapezoidalMap(List<HalfEdge> listEdges)
        {
            int n = listEdges.Count<HalfEdge>();
            IEnumerable<int> randomPermutation = Algorithm.RandomPermutation(n);

            Trapezoidal RecBoundary = RectangleBoundary(listEdges);

        }

        private Trapezoidal RectangleBoundary(List<HalfEdge> listEdges)
        {
            Vertex origin = listEdges[0].Origin;
            Vertex twinOrigin = listEdges[0].Twin.Origin;

            double leftCoordinate = origin.X <= twinOrigin.X ? origin.X : twinOrigin.X;
            double rightCoordinate = origin.X >= twinOrigin.X ? origin.X : twinOrigin.X;
            double topCoordinate = origin.Y >= twinOrigin.Y ? origin.Y : twinOrigin.Y;
            double bottomCoordinate = origin.Y <= twinOrigin.Y ? origin.Y : twinOrigin.Y;

            Trapezoidal R = new Trapezoidal();
            R.Leftp = new Vertex(leftCoordinate, topCoordinate);
            R.Rightp = new Vertex(rightCoordinate, bottomCoordinate);

            R.Top = new HalfEdge();
            R.Bottom = new HalfEdge();
            R.Top.Origin = R.Leftp;
            R.Bottom.Origin = R.Rightp;
            
            return R;
        }
    }
}
