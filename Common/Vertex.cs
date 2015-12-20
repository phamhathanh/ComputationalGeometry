namespace ComputationalGeometry.Common
{
    public class Vertex
    {
        private readonly Vector2 position;
        public HalfEdge OutEdge { get; set; }

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public Vertex(double x, double y)
        {
            this.position = new Vector2(x, y);
        }

        public Vertex(Vector2 position)
        {
            this.position = position;
        }

        public override string ToString()
        {
            return position.ToString();
        }
    }
}
