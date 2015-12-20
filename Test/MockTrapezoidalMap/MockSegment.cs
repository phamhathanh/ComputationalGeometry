using ComputationalGeometry.TrapezoidalMap;
using System;
using System.Collections.Generic;

namespace ComputationalGeometry.MotionPlanning
{
    class MockSegment : ISegment
    {
        internal MockTrapezoid[] bottoms, tops;
        internal MockVertex left, right;
        
        public IEnumerable<ITrapezoid> TopTrapezoids
        {
            get
            {
                return tops;
            }
        }

        public IEnumerable<ITrapezoid> BottomTrapezoids
        {
            get
            {
                return bottoms;
            }
        }

        public IVertex LeftVertex
        {
            get
            {
                return left;
            }
        }

        public IVertex RightVertex
        {
            get
            {
                return right;
            }
        }
    }
}
