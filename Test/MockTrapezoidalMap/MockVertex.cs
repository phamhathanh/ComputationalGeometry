using ComputationalGeometry.Common;
using ComputationalGeometry.TrapezoidalMap;
using System;

namespace ComputationalGeometry.MotionPlanning
{
    class MockVertex : IVertex
    {
        internal Vector2 position;
        internal MockVerticalEdge lower, upper;

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public IVerticalEdge LowerExtension
        {
            get
            {
                return lower;
            }
        }

        public IVerticalEdge UpperExtension
        {
            get
            {
                return upper;
            }
        }
    }
}
