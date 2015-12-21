﻿namespace ComputationalGeometry.Common
{
    public struct Rectangle
    {
        public readonly double left, right, bottom, top;

        public Rectangle(double left, double right, double bottom, double top)
        {
            this.left = left;
            this.right = right;
            this.bottom = bottom;
            this.top = top;
        }

        public Rectangle(Vector2 lowerLeft, Vector2 upperRight)
        {
            this.left = lowerLeft.x;
            this.right = upperRight.x;
            this.bottom = lowerLeft.y;
            this.top = upperRight.y;
        }

        public bool Contains(Vector2 point)
        {
            return !(point.y < bottom || point.y > top || point.x < left || point.x > right);
        }
    }
}
