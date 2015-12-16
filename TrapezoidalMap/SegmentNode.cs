namespace ComputationalGeometry.TrapezoidalMap
{
    class SegmentNode : Node
    {
        public Segment segment { get; }

        public SegmentNode(Segment segment)
        {
            this.segment = segment;
        }

        public override bool IsSegment()
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            SegmentNode objSegNode = (SegmentNode)obj;
            if (!segment.Equals(objSegNode.segment))
                return false;
            return base.Equals(obj);
        }
    }
}
