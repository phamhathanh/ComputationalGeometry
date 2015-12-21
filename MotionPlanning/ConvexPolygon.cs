using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;
using System.Diagnostics;

namespace ComputationalGeometry.MotionPlanning
{
    public class ConvexPolygon
    {
        protected HalfEdge firstEdge;

        public IEnumerable<HalfEdge> Edges
        {
            get
            {
                Debug.Assert(firstEdge != null);

                var currentEdge = firstEdge;
                do
                {
                    yield return currentEdge;
                    currentEdge = GetNextEdge(currentEdge);
                }
                while (currentEdge != firstEdge);
            }
        }

        public IEnumerable<Vertex> Vertices
        {
            get
            {
                foreach (var edge in Edges)
                    yield return edge.Origin;
            }
        }

        public ConvexPolygon(Vector2[] points)
        {
            if (points.Length < 3)
                throw new ArgumentException("Insufficent vertices.");

            var face = new Face();

            Vertex firstVertex = new Vertex(points[0]);

            this.firstEdge = new HalfEdge();
            firstEdge.Origin = firstVertex;
            firstEdge.Face = face;
            face.ConnectedEdge = firstEdge;

            var previousEdge = firstEdge;
            foreach (var point in points.Skip(1))
            {
                var edge = new HalfEdge();
                edge.Origin = new Vertex(point);
                edge.Face = face;

                edge.Previous = previousEdge;
                previousEdge.Next = edge;


                previousEdge = edge;
            }

            firstEdge.Previous = previousEdge;
            previousEdge.Next = firstEdge;

            Validate();
        }

        private HalfEdge GetNextEdge(HalfEdge current)
        {
            try
            {
                return current.Next;
            }
            catch (NullReferenceException)
            {
                throw new ArgumentNullException("Edges are not closing.");
            }
        }

        private void Validate()
        {
            HalfEdge current = firstEdge;
            do
            {
                HalfEdge next = current.Next;

                var errorMessage = "Vertices must be listed in counterclockwise order,"
                                + " starting from the vertex with the smallest y-coordinate"
                                + " (and smallest x-coordinate in case of ties)";

                var compareResult = CompareEdgesByAngleWithXAxis(current, next);
                if (compareResult < 0)
                    throw new ArgumentException(errorMessage);

                current = next;
            }
            while (current != firstEdge);
        }

        public bool Overlaps(ConvexPolygon other)
        {
            foreach (var edge1 in this.Edges)
                foreach (var edge2 in other.Edges)
                    if (edge1.Intersects(edge2))
                        return true;
            return false;
        }

        public NotConvexPolygon UnionWith(ConvexPolygon other)
        {
            if (!this.Overlaps(other))
                throw new ArgumentException("Polygons must overlap.");

            foreach (var edge1 in this.Edges)
                foreach (var edge2 in other.Edges)
                    if (edge1.Intersects(edge2))
                    {
                        var intersection = edge1.GetIntersection(edge2);
                    }
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

                int compareResult = CompareEdgesByAngleWithXAxis(current1, current2);
                if (compareResult >= 0)
                    current1 = current1.Next;
                if (compareResult <= 0)
                    current2 = current2.Next;
            }
            while (current1 != polygon1.firstEdge || current2 != polygon2.firstEdge);

            return new ConvexPolygon(resultPoints.ToArray());
        }

        private static int CompareEdgesByAngleWithXAxis(HalfEdge edge1, HalfEdge edge2)
        {
            Vector2 v1 = edge1.Vector,
                    v2 = edge2.Vector;
            return CompareVectorsByAngleWithXAxis(v1, v2);
        }

        private static int CompareVectorsByAngleWithXAxis(Vector2 v1, Vector2 v2)
        {
            double crossProductZ = Vector2.Cross(v1, v2);
            if (crossProductZ > 0)
                return 1;
            if (crossProductZ < 0)
                return -1;
            return 0;
        }

        public ConvexPolygon GetPointReflection(Vector2 point)
        {
            var reflections = new List<Vector2>();
            foreach (var vertex in Vertices)
            {
                reflections.Add(2 * point - vertex.Position);
            }
            return new ConvexPolygon(reflections.ToArray());
        }

        public IEnumerable<Vector2> GetPointsAbove()
        {
            foreach (var edge in Edges)
            {
                var result1 = CompareVectorsByAngleWithXAxis(Vector2.Up, edge.Vector);
                var result2 = CompareVectorsByAngleWithXAxis(Vector2.Down, edge.Vector);
                
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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var vertex in Vertices)
                stringBuilder.Append(vertex.ToString() + " ");

            return stringBuilder.ToString();
        }
    }
}
