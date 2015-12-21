using System.Collections.Generic;
using ComputationalGeometry.Common;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface ITrapezoidalMap
    {
        Rectangle BoundingBox
        {
            get;
        }

        IEnumerable<ITrapezoid> Trapezoids
        {
            get;
        }

        IEnumerable<ISegment> Segments
        {
            get;
        }

        IEnumerable<IVerticalEdge> VerticalEdges
        {
            get;
        }

        IEnumerable<IVertex> Vertices
        {
            get;
        }

        ITrapezoid PointLocation(Vector2 point);
    }
}
