using System;

namespace ComputationalGeometry.Common
{
    public struct Vector2
    {
        public readonly double x, y;

        public double Length
        {
            get
            {
                return Math.Sqrt(x * x + y * y);
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
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.x - v2.x, v1.y - v2.y);
        }

        public static Vector2 operator *(double d, Vector2 v)
        {
            return new Vector2(d * v.x, d * v.y);
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
            return v1.x * v2.x + v1.y * v2.y;
        }

        public override string ToString()
        {
            return "(" + x.ToString() + ", " + y.ToString() + ")";
        }
    }
}
