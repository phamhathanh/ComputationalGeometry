namespace ComputationalGeometry.Common
{
    public class Vertex
    {
        private readonly double x, y;
        public HalfEdge OutEdge { get; set; }

        public double X
        {
            get
            {
                return x;
            }
        }

        public double Y
        {
            get
            {
                return y;
            }
        }

        public Vertex(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
