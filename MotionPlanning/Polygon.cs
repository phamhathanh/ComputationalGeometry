using System;
using System.Collections.Generic;
using System.Linq;
using ComputationalGeometry.Common;
using System.Diagnostics;

namespace ComputationalGeometry.MotionPlanning
{
    public class Polygon
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
                    currentEdge = NextEdge(currentEdge);
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

        public Polygon(Vertex[] vertices)
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
            for (int i = 2; i < vertices.Length; i++)
            {
                var edge = new HalfEdge();
                edge.Origin = previousVertex;
                edge.Face = face;
                edge.Previous = previousEdge;
                previousEdge.Next = edge;

                previousVertex = vertices[i];
                previousEdge = edge;
            }

            firstEdge.Previous = previousEdge;

            if (!PolygonIsValid())
                throw new ArgumentException("Vertices are invalid.");
        }

        private HalfEdge NextEdge(HalfEdge current)
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

        protected virtual bool PolygonIsValid()
        {
            return PolygonIsSimple();
        }

        private bool PolygonIsSimple()
        {
            foreach (var edge1 in Edges)
                foreach (var edge2 in Edges)
                    if (edge1 != edge2 && edge1.Intersects(edge2))
                        return false;
            // there are better ways than brute force

            return true;
        }
    }
}
