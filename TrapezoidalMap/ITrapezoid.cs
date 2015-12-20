using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface ITrapezoid
    {
        IVerticalEdge LeftEdge
        {
            get;
        }

        IVerticalEdge RightEdge
        {
            get;
        }

        ISegment BottomSegment
        {
            get;
        }

        ISegment TopSegment
        {
            get;
        }
    }
}
