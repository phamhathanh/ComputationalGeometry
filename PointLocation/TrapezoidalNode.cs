namespace ComputationalGeometry.PointLocation
{
    public class TrapezoidalNode : Node
    {
        Trapezoidal Trapezoidal;

        public TrapezoidalNode(Trapezoidal trapezoidal)
        {
            this.Trapezoidal = trapezoidal;
            LeftChildren = null;
            RightChildren = null;
        }
    }
}
