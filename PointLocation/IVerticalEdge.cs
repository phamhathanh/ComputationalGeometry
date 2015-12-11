using System.Collections.Generic;

namespace ComputationalGeometry.PointLocation
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
