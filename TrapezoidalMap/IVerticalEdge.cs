using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface IVerticalEdge
    {
        double XPosition
        {
            get;
        }

        IEnumerable<ITrapezoid> LeftTrapezoids
        {
            get;
        }

        IEnumerable<ITrapezoid> RightTrapezoids
        {
            get;
        }

        IVertex Vertex
        {
            get;
        }
    }
}
