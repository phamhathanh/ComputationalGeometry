using System.Collections.Generic;

namespace ComputationalGeometry.PointLocation
{
    interface ITrapezoidalMap
    {
        IEnumerable<IFace> Faces
        {
            get;
        }

        IEnumerable<ISegment> Segments
        {
            get;
        }

    }
}
