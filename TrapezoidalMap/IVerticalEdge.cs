namespace ComputationalGeometry.TrapezoidalMap
{
    public interface IVerticalEdge
    {
        double XPosition
        {
            get;
        }

        ITrapezoid LeftTrapezoid
        {
            get;
        }

        ITrapezoid RightTrapezoid
        {
            get;
        }

        IVertex Vertex
        {
            get;
        }
    }
}