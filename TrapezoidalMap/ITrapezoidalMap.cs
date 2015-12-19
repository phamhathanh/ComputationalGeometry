using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
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
