using ComputationalGeometry.TrapezoidalMap;
using System.Collections.Generic;

namespace ComputationalGeometry.MotionPlanning
{
    class MockTrapezoid : ITrapezoid
    {
        internal MockSegment top, bottom;
        internal MockVerticalEdge[] lefts, rights;

        public ISegment TopSegment
        {
            get
            {
                return top;
            }
        }

        public ISegment BottomSegment
        {
            get
            {
                return bottom;
            }
        }

        public IEnumerable<IVerticalEdge> LeftEdges
        {
            get
            {
                return lefts;
            }
        }

        public IEnumerable<IVerticalEdge> RightEdges
        {
            get
            {
                return rights;
            }
        }
    }
}
