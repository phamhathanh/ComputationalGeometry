using ComputationalGeometry.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ComputationalGeometry.TrapezoidalMap
{
    class Segment : ISegment
    {
        public Vertex LeftVertex { get; set; }

        public Vertex RightVertex { get; set; }

        public List<Node> Nodes = new List<Node>();

        IVertex ISegment.LeftVertex
        {
            get
            {
                return LeftVertex;
            }
        }

        IVertex ISegment.RightVertex
        {
            get
            {
                return RightVertex;
            }
        }

        public IEnumerable<ITrapezoid> BottomTrapezoids
        {
            get
            {
                var rightChildren = from node in Nodes
                                    select ((SegmentNode)node).RightChildren;
                Queue<Node> queue = new Queue<Node>(rightChildren);
                while (queue.Count != 0)
                {
                    var current = queue.Dequeue();
                    if (current.IsTrapezoid)
                        yield return ((TrapezoidalNode)current).Trapezoid;
                    else if (current.IsVertex)
                    {
                        queue.Enqueue(current.LeftChildren);
                        queue.Enqueue(current.RightChildren);
                    }
                    else if (current.IsSegment)
                    {
                        queue.Enqueue(current.LeftChildren);
                    }
                }
            }
        }

        public IEnumerable<ITrapezoid> TopTrapezoids
        {
            get
            {
                var leftChildren = from node in Nodes
                                    select ((SegmentNode)node).LeftChildren;
                Queue<Node> queue = new Queue<Node>(leftChildren);
                while (queue.Count != 0)
                {
                    var current = queue.Dequeue();
                    if (current.IsTrapezoid)
                        yield return ((TrapezoidalNode)current).Trapezoid;
                    else if (current.IsVertex)
                    {
                        queue.Enqueue(current.LeftChildren);
                        queue.Enqueue(current.RightChildren);
                    }
                    else if (current.IsSegment)
                    {
                        queue.Enqueue(current.RightChildren);
                    }
                }
            }
        }

        public Segment(Vertex leftVertex, Vertex rightVertex)
        {
            LeftVertex = leftVertex;
            RightVertex = rightVertex;
        }

        public override string ToString()
        {
            return LeftVertex.ToString() + " --> " + RightVertex.ToString();
        }

        public bool AboveOf(Vector2 p)
        {
            double IntersectY = (LeftVertex.Y - RightVertex.Y) * (p.X - LeftVertex.X) / (LeftVertex.X - RightVertex.X) + LeftVertex.Y;
            if (p.Y < IntersectY)
                return true;
            return false;
        }

        public bool BelowOf(Vector2 p)
        {
            double IntersectY = (LeftVertex.Y - RightVertex.Y) * (p.X - LeftVertex.X) / (LeftVertex.X - RightVertex.X) + LeftVertex.Y;
            if (p.Y > IntersectY)
                return true;
            return false;
        }

        public override bool Equals(object obj)
        {
            Segment objSegment = (Segment)obj;
            if (LeftVertex.Equals(objSegment.LeftVertex) && RightVertex.Equals(objSegment.RightVertex))
                return true;
            return false;
        }
    }
}
