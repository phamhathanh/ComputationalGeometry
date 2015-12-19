namespace ComputationalGeometry.TrapezoidalMap
{
    class Node
    {
        public Node Parent { get; set; }
        public Node LeftChildren { get; set; }
        public Node RightChildren { get; set; }

        public void SetChildren(ref Node leftChildren, ref Node rightChildren)
        {
            LeftChildren = leftChildren;
            leftChildren.Parent = this;
            RightChildren = rightChildren;
            rightChildren.Parent = this;
        }

        public virtual bool IsVertex()
        {
            return false;
        }

        public virtual bool IsSegment()
        {
            return false;
        }

        public virtual bool IsTrapezoid()
        {
            return false;
        }
         
        public override bool Equals(object obj)
        {
            Node objNode = (Node)obj;
            if (!Parent.Equals(objNode.Parent))
                return false;
            if (!LeftChildren.Equals(objNode.LeftChildren))
                return false;
            if (!RightChildren.Equals(objNode.RightChildren))
                return false;
            return true;
        }
    }
}
