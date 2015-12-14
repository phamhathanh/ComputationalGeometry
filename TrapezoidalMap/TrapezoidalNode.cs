namespace ComputationalGeometry.TrapezoidalMap
{
    class TrapezoidalNode : Node
    {
        public ITrapezoid Trapezoid { get; }

        public TrapezoidalNode(ITrapezoid trapezoid)
        {
            Trapezoid = trapezoid;
            LeftChildren = null;
            RightChildren = null;
        }
    }
}
