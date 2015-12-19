using System;
using System.Collections.Generic;
using System.Linq;
using ComputationalGeometry.Common;
using System.Diagnostics;

namespace ComputationalGeometry.MotionPlanning
{
    public class Polygon
    {
        private HalfEdge firstEdge;

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
                var vertex = vertices[i];

                var edge = new HalfEdge();
                edge.Origin = previousVertex;
                edge.Face = face;
                edge.Previous = previousEdge;
                previousEdge.Next = edge;

                if (!EdgeIsValid(edge))
                    throw new ArgumentException("Edges cannot intersect each other.");

                previousVertex = vertex;
                previousEdge = edge;
            }

            firstEdge.Previous = previousEdge;
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

        private bool EdgeIsValid(HalfEdge edge)
        {
            foreach (var currentEdge in CurrentEdges())
                if (edge != currentEdge && edge.Intersects(currentEdge))
                    return false;

            return true;
        }

        private IEnumerable<HalfEdge> CurrentEdges()
        {
            var currentEdge = firstEdge;
            while (currentEdge != null)
            {
                yield return currentEdge;
                currentEdge = currentEdge.Next;
            }
        }
    }
}
