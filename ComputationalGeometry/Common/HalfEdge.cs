namespace ComputationalGeometry.Common
{
    public class HalfEdge
    {
        public Vertex Origin { get; set; }
        public HalfEdge Twin { get; set; }
        public HalfEdge Next { get; set; }
        public HalfEdge Previous { get; set; }
        public Face Face { get; set; }
    }
}
