using ComputationalGeometry.TrapezoidalMap;
using System;
using System.Collections.Generic;

namespace ComputationalGeometry.MotionPlanning
{
    class MockVerticalEdge : IVerticalEdge
    {
        internal MockTrapezoid left, right;
        internal MockVertex vertex;

        public double XPosition
        {
            get
            {
                return vertex.position.X;
            }
        }

        public IVertex Vertex
        {
            get
            {
                return vertex;
            }
        }

        public ITrapezoid LeftTrapezoid
        {
            get
            {
                return left;
            }
        }

        public ITrapezoid RightTrapezoid
        {
            get
            {
                return right;
            }
        }

        public bool IsLowerExtension
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
