using ComputationalGeometry.Common;

namespace ComputationalGeometry.TrapezoidalMap
{
    public interface IVertex
    {
        Vector2 Position
        {
            get;
        }

        IVerticalEdge Extension
        {
            get;
        }
    }
}
