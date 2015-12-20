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

        public ConvexPolygon(Vertex[] vertices)
        {
            if (vertices.Length < 3)
                throw new ArgumentException("Insufficent vertices.");

            var face = new Face();

            Vertex firstVertex = vertices[0],
                    secondVertex = vertices[1];

            this.firstEdge = new HalfEdge();
            firstEdge.Origin = firstVertex;
            firstEdge.Face = face;
            face.ConnectedEdge = firstEdge;

            var previousVertex = secondVertex;
            var previousEdge = firstEdge;
            for (int i = 2; i < vertices.Length + 1; i++)
            {
                var edge = new HalfEdge();
                edge.Origin = previousVertex;
                edge.Face = face;
                edge.Previous = previousEdge;
                previousEdge.Next = edge;

                

                previousVertex = vertices[i % vertices.Length];
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

        public static ConvexPolygon operator+(ConvexPolygon polygon1, ConvexPolygon polygon2)
        {
            var resultVertices = new List<Vertex>();
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
                var newVertex = VerticesSum(vertex1, vertex2);
                resultVertices.Add(newVertex);

                Debug.Assert(resultVertices.Count <= resultVerticesBound);

                int compareResult = CompareEdgesByAngleWithXAxis(current1, current2);
                if (compareResult >= 0)
                    current1 = current1.Next;
                if (compareResult <= 0)
                    current2 = current2.Next;
            }
            while (current1 != polygon1.firstEdge || current2 != polygon2.firstEdge);

            return new ConvexPolygon(resultVertices.ToArray());
        }

        private static Vertex VerticesSum(Vertex vertex1, Vertex vertex2)
        {
            Vector2 sumPosition = vertex1.Position + vertex2.Position;
            return new Vertex(sumPosition);
        }

        private static int CompareEdgesByAngleWithXAxis(HalfEdge edge1, HalfEdge edge2)
        {
            Vector2 v1 = edge1.Vector,
                    v2 = edge2.Vector;
            double crossProductZ = v1.x * v2.y - v1.y * v2.x;
            if (crossProductZ > 0)
                return 1;
            if (crossProductZ < 0)
                return -1;
            return 0;
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
