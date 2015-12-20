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

        IVerticalEdge LeftEdge
        {
            get;
        }

        IVerticalEdge RightEdge
        {
            get;
        }
    }
}
