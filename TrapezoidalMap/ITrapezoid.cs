using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface ITrapezoid
    {
        ISegment TopSegment
        {
            get;
        }

        ISegment BottomSegment
        {
            get;
        }
    }
}
