using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;
using System.Diagnostics;

namespace ComputationalGeometry.MotionPlanning
{
    public class ConvexPolygon : SimplePolygon
    {
        public ConvexPolygon(Vector2[] points) : base(points)
        {
            if (!PolygonIsConvexAndHasSortedVertices())
            {
                var errorMessage = "Vertices must be listed in counterclockwise order,"
                                + " starting from the vertex with the smallest y-coordinate"
                                + " (and smallest x-coordinate in case of ties)";
                throw new ArgumentException(errorMessage);
            }
        }

        private bool PolygonIsConvexAndHasSortedVertices()
        {
            HalfEdge current = firstEdge;
            do
            {
                HalfEdge next = GetNextEdge(current);

                var compareResult = Vector2.CompareByAngleWithXAxis(current.Vector, next.Vector);
                if (compareResult < 0)
                    return false;

                current = next;
            }
            while (current != firstEdge);

            return true;
        }

        public static ConvexPolygon MinkowskiSum(ConvexPolygon polygon1, ConvexPolygon polygon2)
        {
            var resultPoints = new List<Vector2>();
            HalfEdge current1 = polygon1.firstEdge,
                     current2 = polygon2.firstEdge;

#if DEBUG
            int numberOfVertices1 = polygon1.Edges.Count(),
                numberOfVertices2 = polygon2.Edges.Count(),
                resultVerticesBound = numberOfVertices1 + numberOfVertices2;
#endif

            do
            {
                var vertex1 = current1.Origin;
                var vertex2 = current2.Origin;
                var newPoint = vertex1.Position + vertex2.Position;
                resultPoints.Add(newPoint);

                Debug.Assert(resultPoints.Count <= resultVerticesBound);

                int compareResult = Vector2.CompareByAngleWithXAxis(current1.Vector, current2.Vector);
                if (compareResult >= 0)
                    current1 = polygon1.GetNextEdge(current1);
                if (compareResult <= 0)
                    current2 = polygon2.GetNextEdge(current2);
            }
            while (current1 != polygon1.firstEdge || current2 != polygon2.firstEdge);

            return new ConvexPolygon(resultPoints.ToArray());
        }

        public IEnumerable<Vector2> GetPointsAbove()
        {
            foreach (var edge in Edges)
            {
                var result1 = Vector2.CompareByAngleWithXAxis(Vector2.Up, edge.Vector);
                var result2 = Vector2.CompareByAngleWithXAxis(Vector2.Down, edge.Vector);
                
                if (result1 == -1)
                    continue;

                if (result1 == 0)
                    throw new NotImplementedException("Not yet implemented");
                if (result2 == 0)
                    throw new NotImplementedException("Not yet implemented");

                if (result2 == -1)
                    yield return edge.Origin.Position;

                yield break;
            }
        }
    }
}
