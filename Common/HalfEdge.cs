using System;

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
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Origin.ToString() + " " + End.ToString();
        }
    }
}
