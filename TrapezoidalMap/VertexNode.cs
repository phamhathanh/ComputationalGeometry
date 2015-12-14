namespace ComputationalGeometry.TrapezoidalMap
{
    class VertexNode : Node
    {
        public IVertex Vertex { get; }

        public VertexNode(IVertex vertex)
        {
            Vertex = vertex;
        }
    }
}
