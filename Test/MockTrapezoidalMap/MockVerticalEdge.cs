using ComputationalGeometry.TrapezoidalMap;
using System;
using System.Collections.Generic;

namespace ComputationalGeometry.MotionPlanning
{
    class MockVerticalEdge : IVerticalEdge
    {
        internal MockTrapezoid[] lefts, rights;
        internal MockVertex vertex;

        public double XPosition
        {
            get
            {
                return vertex.position.x;
            }
        }

        public IVertex Vertex
        {
            get
            {
                return vertex;
            }
        }

        public IEnumerable<ITrapezoid> LeftTrapezoids
        {
            get
            {
                return lefts;
            }
        }

        public IEnumerable<ITrapezoid> RightTrapezoids
        {
            get
            {
                return rights;
            }
        }
    }
}
