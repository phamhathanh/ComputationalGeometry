using System;
using System.Diagnostics;

namespace ComputationalGeometry.Common
{
    public class HalfEdge
    {
        public Vertex Origin { get; set; }
        public HalfEdge Twin { get; set; }
        public HalfEdge Next { get; set; }
        public HalfEdge Previous { get; set; }
        public Face Face { get; set; }

        public Vertex End
        {
            get
            {
                return Next.Origin;
            }
            set
            {
                Next.Origin = value;
            }
        }

        public Vector2 Vector
        {
            get
            {
                return Origin.Position - End.Position;
            }
        }

        public bool Intersects(HalfEdge other)
        {
            Vector2 u0 = this.Origin.Position,
                    u = this.Vector,
                    v0 = other.Origin.Position,
                    v = other.Vector;

            double crossProductZ = Vector2.Cross(u, v);

            if (crossProductZ == 0)
                return true;

            double k = Vector2.Cross(v0 - u0, u) / crossProductZ,
                   l = Vector2.Cross(v0 - u0, v) / crossProductZ;

            if (k <= 0 || k >= 1 || l <= 0 || l >= 1)
                return true;

            return false;
        }

        public Vector2 GetIntersection(HalfEdge other)
        {
            Vector2 u0 = this.Origin.Position,
                    u = this.Vector,
                    v0 = other.Origin.Position,
                    v = other.Vector;

            double crossProductZ = Vector2.Cross(u, v);

            if (crossProductZ == 0)
                throw new ArgumentException("Edges are parallel.");

            double k = Vector2.Cross(v0 - u0, u) / crossProductZ,
                   l = Vector2.Cross(v0 - u0, v) / crossProductZ;

            if (k <= 0 || k >= 1 || l <= 0 || l >= 1)
                throw new ArgumentException("Edges do not intersect each other.");

            Debug.Assert(u0 + k * u == v0 + l * v);
            return u0 + k * u;
        }

        public override string ToString()
        {
            return Origin.ToString() + " " + End.ToString();
        }
    }
}
