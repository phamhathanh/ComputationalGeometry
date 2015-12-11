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

        IEnumerable<IFace> TopFaces
        {
            get;
        }

        IEnumerable<IFace> BottomFaces
        {
            get;
        }
    }
}
