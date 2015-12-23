using System.Collections.Generic;

namespace ComputationalGeometry.TrapezoidalMap
{
    public class Node
    {
        public List<Node> Parent = new List<Node>();
        public Node LeftChildren { get; set; }
        public Node RightChildren { get; set; }
        public bool IsVertex = false;
        public bool IsSegment = false;
        public bool IsTrapezoid = false;


        public void SetChildren(ref Node leftChildren, ref Node rightChildren)
        {
            LeftChildren = leftChildren;
            leftChildren.Parent.Add(this);
            RightChildren = rightChildren;
            rightChildren.Parent.Add(this);
        }

        public void SetLeftChildren(ref Node leftChildren)
        {
            LeftChildren = leftChildren;
            leftChildren.Parent.Add(this);
        }

        public void SetRightChildren(ref Node rightChildren)
        {
            RightChildren = rightChildren;
            rightChildren.Parent.Add(this);
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
