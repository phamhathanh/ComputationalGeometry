using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface ITrapezoidalMap
    {
        IEnumerable<ITrapezoid> Trapezoids { get; }

        IEnumerable<ISegment> Segments { get; }
    }
}
