namespace ComputationalGeometry.TrapezoidalMap
{
    class Trapezoid : ITrapezoid
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

        IVertex ITrapezoid.Leftp
        {
            get
            {
                return this.Leftp;
            }
        }

        IVertex ITrapezoid.Rightp
        {
            get
            {
                return this.Rightp;
            }
        }

        ISegment ITrapezoid.Top
        {
            get
            {
                return this.Top;
            }
        }

        ISegment ITrapezoid.Bottom
        {
            get
            {
                return this.Bottom;
            }
        }

        ITrapezoid ITrapezoid.HigherRightNeighbor
        {
            get
            {
                return this.HigherRightNeighbor;
            }
        }

        ITrapezoid ITrapezoid.HigherLeftNeighbor
        {
            get
            {
                return this.HigherLeftNeighbor;
            }
        }

        ITrapezoid ITrapezoid.LowerLeftNeighbor
        {
            get
            {
                return this.LowerLeftNeighbor;
            }
        }

        ITrapezoid ITrapezoid.LowerRightNeighbor
        {
            get
            {
                return this.LowerRightNeighbor;
            }
        }
        
        public bool Contain(Vertex p)
        {
            if (p.X < Leftp.X || p.X > Rightp.X)
                return false;
            if (Top.BelowOf(p) || Bottom.AboveOf(p))
                return true;
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
