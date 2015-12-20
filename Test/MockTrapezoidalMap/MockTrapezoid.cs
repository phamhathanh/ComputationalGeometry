using ComputationalGeometry.TrapezoidalMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry.MotionPlanning
{
    class MockTrapezoid : ITrapezoid
    {
        internal MockSegment top, bottom;

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
