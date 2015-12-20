using ComputationalGeometry.Common;
using ComputationalGeometry.TrapezoidalMap;
using System;

namespace ComputationalGeometry.MotionPlanning
{
    class MockVertex : IVertex
    {
        internal Vector2 position;
        internal MockVerticalEdge extension;

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public IVerticalEdge Extension
        {
            get
            {
                return extension;
            }
        }
    }
}
