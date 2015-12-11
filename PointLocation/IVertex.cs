namespace ComputationalGeometry.PointLocation
{
    public interface IVertex
    {
        double X
        {
            get;
        }

        double Y
        {
            get;
        }

        IVerticalEdge Extension
        {
            get;
        }
    }
}
