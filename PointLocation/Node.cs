namespace ComputationalGeometry.PointLocation
{
    public abstract class Node
    {
        public Node Parent { get; set; }
        public Node LeftChildren { get; set; }
        public Node RightChildren { get; set; }
    }
}
