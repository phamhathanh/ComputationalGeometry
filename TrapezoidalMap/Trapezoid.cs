using System;
using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public class Trapezoid : ITrapezoid
    {
        public Vertex Leftp { get; set; }
        public Vertex Rightp { get; set; }
        public Segment Top { get; set; }
        public Segment Bottom { get; set; }
        public Trapezoid HigherRightNeighbor { get; set; }
        public Trapezoid HigherLeftNeighbor { get; set; }
        public Trapezoid LowerLeftNeighbor { get; set; }
        public Trapezoid LowerRightNeighbor { get; set; }

        public Node Node { get; set; }

        public Trapezoid(Vertex leftp, Vertex rightp, Segment top, Segment bottom)
        {
            Leftp = leftp;
            Rightp = rightp;
            Top = top;
            Bottom = bottom;
        }

        public override string ToString()
        {
            string output = "Left point: " + Leftp.ToString();
            output += ";\n Right point: " + Rightp.ToString();
            output += ";\n Top: " + Top.ToString();
            output += ";\n Bottom: " + Bottom.ToString();
            return output;
        }

        public void SetVertex(Vertex leftp, Vertex rightp)
        {
            Leftp = leftp;
            Rightp = rightp;
        }

        public void SetLeftVertex(Vertex leftp)
        {
            Leftp = leftp;
        }

        public void SetRightVertex(Vertex rightp)
        {
            Rightp = rightp;
        }

        public Trapezoid()
        {

        }

        public void SetNeighbor(Trapezoid higherLeft, Trapezoid lowerLeft, Trapezoid higherRight, Trapezoid lowerRight)
        {
            HigherLeftNeighbor = higherLeft;
            LowerLeftNeighbor = lowerLeft;
            HigherRightNeighbor = higherRight;
            LowerRightNeighbor = lowerRight;
        }

        public void SetLeftNeighbor(Trapezoid higherLeft, Trapezoid lowerLeft)
        {
            HigherLeftNeighbor = higherLeft;
            LowerLeftNeighbor = lowerLeft;
        }

        public void SetRightNeighbor(Trapezoid higherRight, Trapezoid lowerRight)
        {
            HigherRightNeighbor = higherRight;
            LowerRightNeighbor = lowerRight;
        }

        public IEnumerable<IVerticalEdge> LeftEdges
        {
            get
            {
                if (LowerLeftNeighbor == null)
                    yield break;
                if (LowerLeftNeighbor == HigherLeftNeighbor)
                {
                    if (Top.LeftVertex.Equals(Leftp))
                    {
                        yield return Leftp.LowerExtension;
                        yield break;
                    }
                    else
                    {
                        yield return Leftp.UpperExtension;
                        yield break;
                    }
                }
                yield return Leftp.LowerExtension;
                yield return Leftp.UpperExtension;
            }
        }

        public IEnumerable<IVerticalEdge> RightEdges
        {
            get
            {
                if (LowerRightNeighbor == null)
                    yield break;
                if (LowerRightNeighbor == HigherRightNeighbor)
                {
                    if (Top.RightVertex.Equals(Rightp))
                    {
                        yield return Rightp.LowerExtension;
                        yield break;
                    }
                    else
                    {
                        yield return Rightp.UpperExtension;
                        yield break;
                    }
                }
                yield return Rightp.LowerExtension;
                yield return Rightp.UpperExtension;
            }
        }

        public double LeftBound
        {
            get
            {
                return Leftp.X;
            }
        }

        public double RightBound
        {
            get
            {
                return Rightp.X;
            }
        }

        public ISegment BottomSegment
        {
            get
            {
                return Bottom;
            }
        }

        public ISegment TopSegment
        {
            get
            {
                return Top;
            }
        }

        public bool Contain(Vertex p)
        {
            if (p.X < Leftp.X || p.X > Rightp.X)
                return false;
            if (Top.BelowOf(p) || Bottom.AboveOf(p))
                return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            Trapezoid objTrap = (Trapezoid)obj;
            if (!Leftp.Equals(objTrap.Leftp))
                return false;
            if (!Rightp.Equals(objTrap.Rightp))
                return false;
            if (!Top.Equals(objTrap.Top))
                return false;
            if (!Bottom.Equals(objTrap.Bottom))
                return false;
            return true;
        }
    }
}
