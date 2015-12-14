namespace ComputationalGeometry.TrapezoidalMap
{
    abstract class Node : INode
    {
        public INode Parent { get; set; }
        public INode LeftChildren { get; set; }
        public INode RightChildren { get; set; }
    }
}
