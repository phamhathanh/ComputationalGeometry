using ComputationalGeometry.Common;
using System.Collections.Generic;
using System.Linq;

namespace ComputationalGeometry.TrapezoidalMap
{
    public class TrapezoidalMap : ITrapezoidalMap
    {
        public INode Root { get; set; }
        IEnumerable<Vertex> Vertice;
        IEnumerable<HalfEdge> HalfEdges;
        IEnumerable<Face> Faces;

        /// <summary>
        /// Constructor
        /// </summary>
        public TrapezoidalMap(List<HalfEdge> listEdges)
        {
            int n = listEdges.Count<HalfEdge>();
            IEnumerable<int> randomPermutation = Utilities.RandomPermutation(n);

            Trapezoid RecBoundary = RectangleBoundary(listEdges);

            Root = new TrapezoidalNode(RecBoundary);
        }

        private static Trapezoid RectangleBoundary(List<HalfEdge> listEdges)
        {
            Vertex origin = new Vertex(listEdges[0].Origin.X, listEdges[0].Origin.Y);
            Vertex twinOrigin = new Vertex(listEdges[0].Twin.Origin.X, listEdges[0].Twin.Origin.Y);

            double leftCoordinate = origin.X <= twinOrigin.X ? origin.X : twinOrigin.X;
            double rightCoordinate = origin.X >= twinOrigin.X ? origin.X : twinOrigin.X;
            double topCoordinate = origin.Y >= twinOrigin.Y ? origin.Y : twinOrigin.Y;
            double bottomCoordinate = origin.Y <= twinOrigin.Y ? origin.Y : twinOrigin.Y;

            Trapezoid R = new Trapezoid();
            R.Leftp = new Vertex(leftCoordinate, topCoordinate);
            R.Rightp = new Vertex(rightCoordinate, bottomCoordinate);

            R.Top = new Segment(new Vertex(leftCoordinate, topCoordinate), new Vertex(rightCoordinate, topCoordinate));
            R.Bottom = new Segment(new Vertex(leftCoordinate, bottomCoordinate), new Vertex(rightCoordinate, bottomCoordinate));
            
            return R;
        }

        static Trapezoid PointLocation(Point p, INode root)
        {
            return new Trapezoid();
        }

    }
}
