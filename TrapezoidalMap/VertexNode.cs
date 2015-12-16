namespace ComputationalGeometry.TrapezoidalMap
{
    class VertexNode : Node
    {
        public Vertex Vertex { get; }

        public VertexNode(Vertex vertex)
        {
            Vertex = vertex;
        }
        public override bool IsVertex()
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            VertexNode objVerNode = (VertexNode)obj;
            if (!Vertex.Equals(objVerNode.Vertex))
                return false;
            return base.Equals(obj);
        }
    }
}
