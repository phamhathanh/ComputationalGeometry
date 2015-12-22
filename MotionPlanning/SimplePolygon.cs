using System;
using System.Collections.Generic;
using System.Linq;
using ComputationalGeometry.Common;
using System.Diagnostics;
using System.Text;

namespace ComputationalGeometry.MotionPlanning
{
    public class SimplePolygon
    {
        protected HalfEdge firstEdge;

        public IEnumerable<HalfEdge> Edges
        {
            get
            {
                Debug.Assert(firstEdge != null);

                HalfEdge currentEdge = firstEdge;
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

        public SimplePolygon(Vector2[] points)
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

            if (!PolygonIsSimpleAndHasSortedVertices())
                throw new ArgumentException("Polygon is not simple.");
        }

        private bool PolygonIsSimpleAndHasSortedVertices()
        {
            foreach (var edge1 in Edges)
                foreach (var edge2 in Edges)
                    if (edge1 != edge2 && edge1.Intersects(edge2))
                        return false;
            // there are better ways than brute force

            var edgeFromBottomVertex = (from edge in Edges
                                        orderby edge.Origin.Position.Y
                                        orderby edge.Origin.Position.X
                                        select edge).First();

            var edgeToBottomVertex = edgeFromBottomVertex.Previous;
            double crossProductZ = Vector2.Cross(edgeToBottomVertex.Vector, edgeFromBottomVertex.Vector);

            if (crossProductZ > 0)
                return true;

            Debug.Assert(crossProductZ < 0);

            return false;
        }

        protected HalfEdge GetNextEdge(HalfEdge current)
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

        public bool Overlaps(SimplePolygon other)
        {
            foreach (var edge1 in this.Edges)
                foreach (var edge2 in other.Edges)
                    if (edge1.Intersects(edge2))
                        return true;

            if (other.ContainsPoint(this.firstEdge.Origin.Position))
                return true;
            if (this.ContainsPoint(other.firstEdge.Origin.Position))
                return true;

            return false;
        }

        public bool ContainsPoint(Vector2 point)
        {
            bool isInside = false;
            foreach (var edge in Edges)
            {
                Vector2 origin = edge.Origin.Position,
                        end = edge.End.Position;
                if (((origin.Y > point.Y) != (end.Y > point.Y)) &&
                (point.X < (origin.X - end.X) * (point.Y - end.Y) / (origin.Y - end.Y) + end.X))
                    isInside = !isInside;
            }
            return isInside;
        }

        public SimplePolygon UnionWith(SimplePolygon other)
        {
            throw new NotImplementedException();

            if (!this.Overlaps(other))
                throw new ArgumentException("Polygons must overlap.");

            foreach (var edge1 in this.Edges)
                foreach (var edge2 in other.Edges)
                    if (edge1.Intersects(edge2))
                    {
                        var intersection = edge1.GetIntersection(edge2);
                    }
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

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();

            foreach (var vertex in Vertices)
                stringBuilder.Append(vertex.ToString() + " ");

            return stringBuilder.ToString();
        }
    }
}
