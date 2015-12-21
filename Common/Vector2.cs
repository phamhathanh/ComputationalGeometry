using System;

namespace ComputationalGeometry.Common
{
    public struct Vector2
    {
        public static readonly Vector2 Zero = new Vector2(0, 0),
            Left = new Vector2(-1, 0), Right = new Vector2(1, 0),
            Down = new Vector2(0, -1), Up = new Vector2(0, 1);

        public readonly double X, Y;

        public double Length
        {
            get
            {
                return Math.Sqrt(X * X + Y * Y);
            }
        }

        public Vector2 Normalized
        {
            get
            {
                return this / Length;
            }
        }

        public Vector2(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(double d, Vector2 v)
        {
            return new Vector2(d * v.X, d * v.Y);
        }

        public static Vector2 operator *(Vector2 v, double d)
        {
            return d * v;
        }

        public static Vector2 operator /(Vector2 v, double d)
        {
            return (1 / d) * v;
        }

        public static double Dot(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ")";
        }
    }
}
