namespace ComputationalGeometry.TrapezoidalMap
{
    public interface IVerticalEdge
    {
        double X { get; }

        IVertex OriginVertex { get; }

        IVertex ExtraVertex { get; }
    }
}
