using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface ITrapezoid
    {
        IEnumerable<IVerticalEdge> LeftEdges
        {
            get;
        }

        IEnumerable<IVerticalEdge> RightEdges
        {
            get;
        }

        double LeftBound
        {
            get;
        }

        double RightBound
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
