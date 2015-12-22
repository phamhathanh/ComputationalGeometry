using System;

namespace ComputationalGeometry.Common
{
    public struct Rectangle
    {
        public readonly double Left, Right, Bottom, Top;

        public double Width
        {
            get
            {
                return Right - Left;
            }
        }

        public double Height
        {
            get
            {
                return Top - Bottom;
            }
        }

        public Rectangle(double left, double right, double bottom, double top)
        {
            bool isValid = left < right && bottom < top;
            if (!isValid)
                throw new ArgumentException("Parameters are incorrect.");

            this.Left = left;
            this.Right = right;
            this.Bottom = bottom;
            this.Top = top;
        }

        public Rectangle(Vector2 lowerLeft, Vector2 upperRight)
            : this(lowerLeft.X, upperRight.X, lowerLeft.Y, upperRight.Y)
        { }

        public bool Contains(Vector2 point)
        {
            return !(point.Y < Bottom || point.Y > Top || point.X < Left || point.X > Right);
        }
    }
}
