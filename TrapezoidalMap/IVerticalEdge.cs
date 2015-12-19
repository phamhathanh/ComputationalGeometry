using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface IVerticalEdge
    {
        double X
        {
            get;
        }

        IEnumerable<IFace> LeftFaces
        {
            get;
        }

        IEnumerable<IFace> RightFaces
        {
            get;
        }

        IVertex Vertex
        {
            get;
        }
    }
}
