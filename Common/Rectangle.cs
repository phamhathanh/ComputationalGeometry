namespace ComputationalGeometry.Common
{
    public class Rectangle
    {
        private readonly double top, bottom, left, right;

        public Rectangle(double top, double bottom, double left, double right)
        {
            this.top = top;
            this.bottom = bottom;
            this.left = left;
            this.right = right;
        }

        public Rectangle(Vector2 lowerLeft, Vector2 upperRight)
        {
            this.top = upperRight.y;
            this.bottom = lowerLeft.y;
            this.left = lowerLeft.x;
            this.right = upperRight.x;
        }
    }
}
