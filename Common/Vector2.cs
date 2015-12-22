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

        public static double Cross(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.Y - v1.Y * v2.X;
        }

        public static int CompareByAngleWithXAxis(Vector2 v1, Vector2 v2)
        {
            double crossProductZ = Cross(v1, v2);
            if (crossProductZ > 0)
                return 1;
            if (crossProductZ < 0)
                return -1;
            return 0;
        }

        public static bool operator ==(Vector2 v1, Vector2 v2)
        {
            return v1.X == v2.X && v1.Y == v2.Y;
        }

        public static bool operator !=(Vector2 v1, Vector2 v2)
        {
            return !(v1 == v2);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 && (Vector2)obj == this;
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + 13 * Y.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + X.ToString() + ", " + Y.ToString() + ")";
        }
    }
}
