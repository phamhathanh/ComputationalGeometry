using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface ISegment
    {
        IVertex LeftVertex
        {
            get;
        }

        IVertex RightVertex
        {
            get;
        }

        IEnumerable<ITrapezoid> BottomTrapezoids
        {
            get;
        }

        IEnumerable<ITrapezoid> TopTrapezoids
        {
            get;
        }
    }
}
