namespace ComputationalGeometry.TrapezoidalMap
{
    public interface ISegment
    {
        IVertex LeftVertex { get; }

        IVertex RightVertex { get; }
    }
}
