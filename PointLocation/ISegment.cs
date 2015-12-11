using System.Collections.Generic;

namespace ComputationalGeometry.PointLocation
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
