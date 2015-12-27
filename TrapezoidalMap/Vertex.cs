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

            this.lower = new VerticalEdge()
            {
                Vertex = this,
                XPosition = this.X,
                LeftTrapezoid = GetLowerLeft(),
                RightTrapezoid = GetLowerRight()
            };

            this.upper = new VerticalEdge()
            {
                Vertex = this,
                XPosition = this.X,
                LeftTrapezoid = GetUpperLeft(),
                RightTrapezoid = GetUpperRight()
            };
        }

        private Trapezoid GetLowerLeft()
        {
            var current = Node.LeftChildren;
            bool goRight = true;
            while (true)
            {
                if (goRight)
                    current = current.RightChildren;
                else
                    current = current.LeftChildren;

                if (current.IsSegment)
                    goRight = !goRight;
                if (current.IsTrapezoid)
                    return ((TrapezoidalNode)current).Trapezoid;
            }
            throw new InvalidOperationException();
        }

        private Trapezoid GetLowerRight()
        {
            var current = Node.RightChildren;
            bool goRight = false;
            while (true)
            {
                if (goRight)
                    current = current.RightChildren;
                else
                    current = current.LeftChildren;

                if (current.IsVertex)
                    goRight = !goRight;
                if (current.IsTrapezoid)
                    return ((TrapezoidalNode)current).Trapezoid;
            }
            throw new InvalidOperationException();
        }
        private Trapezoid GetUpperLeft()
        {
            var current = Node.LeftChildren;
            bool goRight = false;
            while (true)
            {
                if (goRight)
                    current = current.RightChildren;
                else
                    current = current.LeftChildren;

                if (current.IsVertex)
                    goRight = true;
                if (current.IsTrapezoid)
                    return ((TrapezoidalNode)current).Trapezoid;
            }
            throw new InvalidOperationException();
        }

        private Trapezoid GetUpperRight()
        {
            var current = Node.RightChildren;
            bool goRight = true;
            while (true)
            {
                if (goRight)
                    current = current.RightChildren;
                else
                    current = current.LeftChildren;

                if (current.IsSegment)
                    goRight = false;
                if (current.IsTrapezoid)
                    return ((TrapezoidalNode)current).Trapezoid;
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
                return upper;
            }
        }

        public IVerticalEdge LowerExtension
        {
            get
            {
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

        public static implicit operator Vertex(Common.Vertex v)
        {
            throw new NotImplementedException();
        }
    }
}
