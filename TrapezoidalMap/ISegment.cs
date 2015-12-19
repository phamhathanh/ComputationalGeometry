namespace ComputationalGeometry.TrapezoidalMap
{
    public interface ISegment
    {
        IVertex LeftVertex { get; }

        IVertex RightVertex { get; }

        ITrapezoid Top { get; }

        ITrapezoid Bottom { get; }
    }
}
