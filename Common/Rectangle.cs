namespace ComputationalGeometry.Common
{
    public struct Rectangle
    {
        public readonly double Left, Right, Bottom, Top;

        public Rectangle(double left, double right, double bottom, double top)
        {
            this.Left = left;
            this.Right = right;
            this.Bottom = bottom;
            this.Top = top;
        }

        public Rectangle(Vector2 lowerLeft, Vector2 upperRight)
        {
            this.Left = lowerLeft.X;
            this.Right = upperRight.X;
            this.Bottom = lowerLeft.Y;
            this.Top = upperRight.Y;
        }

        public bool Contains(Vector2 point)
        {
            return !(point.Y < Bottom || point.Y > Top || point.X < Left || point.X > Right);
        }
    }
}
