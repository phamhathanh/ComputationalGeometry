using System;
using ComputationalGeometry.Common;
using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    class Vertex : IVertex
    {
        private VerticalEdge upper, lower;

        public double X { get; set; }

        public double Y { get; set; }

        public Node Node;

        public Vertex(double x, double y)
        {
            X = x;
            Y = y;
        }

        private VerticalEdge CreateLower()
        {
            return new VerticalEdge()
            {
                Vertex = this,
                XPosition = this.X,
                LeftTrapezoid = GetLowerLeft(),
                RightTrapezoid = GetLowerRight(),
                IsLowerExtension = true
            };
        }

        private VerticalEdge CreateUpper()
        {
            return new VerticalEdge()
            {
                Vertex = this,
                XPosition = this.X,
                LeftTrapezoid = GetUpperLeft(),
                RightTrapezoid = GetUpperRight(),
                IsLowerExtension = false
            };
        }

        private Trapezoid GetLowerLeft()
        {
            var current = Node.LeftChildren;
            bool goRight = true;
            while (true)
            {
                if (current.IsTrapezoid)
                    return ((TrapezoidalNode)current).Trapezoid;

                if (goRight)
                    current = current.RightChildren;
                else
                    current = current.LeftChildren;

                if (current.IsSegment)
                    goRight = !goRight;
            }
            throw new InvalidOperationException();
        }

        private Trapezoid GetLowerRight()
        {
            var current = Node.RightChildren;
            bool goRight = false;
            while (true)
            {
                if (current.IsTrapezoid)
                    return ((TrapezoidalNode)current).Trapezoid;

                if (goRight)
                    current = current.RightChildren;
                else
                    current = current.LeftChildren;

                if (current.IsVertex)
                    goRight = !goRight;
            }
            throw new InvalidOperationException();
        }
        private Trapezoid GetUpperLeft()
        {
            var current = Node.LeftChildren;
            bool goRight = false;
            while (true)
            {
                if (current.IsTrapezoid)
                    return ((TrapezoidalNode)current).Trapezoid;

                if (goRight)
                    current = current.RightChildren;
                else
                    current = current.LeftChildren;

                if (current.IsVertex)
                    goRight = true;
            }
            throw new InvalidOperationException();
        }

        private Trapezoid GetUpperRight()
        {
            var current = Node.RightChildren;
            bool goRight = true;
            while (true)
            {
                if (current.IsTrapezoid)
                    return ((TrapezoidalNode)current).Trapezoid;

                if (goRight)
                    current = current.RightChildren;
                else
                    current = current.LeftChildren;

                if (current.IsSegment)
                    goRight = false;
            }
            throw new InvalidOperationException();
        }

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
        }

        public IVerticalEdge UpperExtension
        {
            get
            {
                if (upper == null)
                    upper = CreateUpper();
                return upper;
            }
        }

        public IVerticalEdge LowerExtension
        {
            get
            {
                if (lower == null)
                    lower = CreateLower();
                return lower;
            }
        }

        public override string ToString()
        {
            return "{" + X + ", " + Y + "}";
        }

        public bool LeftOf(Vector2 p)
        {
            if (X < p.X)
                return true;
            return false;
        }

        public bool RightOf(Vector2 p)
        {
            if (X > p.X)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            Vertex objVertex = (Vertex)obj;
            if (X == objVertex.X && Y == objVertex.Y)
                return true;
            return false;
        }
    }
}
