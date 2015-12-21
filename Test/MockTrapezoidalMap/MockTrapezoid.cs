using ComputationalGeometry.TrapezoidalMap;
using System.Collections.Generic;
using System;

namespace ComputationalGeometry.MotionPlanning
{
    class MockTrapezoid : ITrapezoid
    {
        internal MockSegment top, bottom;
        internal MockVerticalEdge[] lefts, rights;
        internal double leftBound, rightBound;

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

        public double LeftBound
        {
            get
            {
                return leftBound;
            }
        }

        public double RightBound
        {
            get
            {
                return rightBound;
            }
        }

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
    }
}
