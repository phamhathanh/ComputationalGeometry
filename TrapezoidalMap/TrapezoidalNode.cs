namespace ComputationalGeometry.TrapezoidalMap
{
    public class TrapezoidalNode : Node
    {
        public Trapezoid Trapezoid { get; }

        public TrapezoidalNode(Trapezoid trapezoid)
        {
            IsTrapezoid = true;
            Trapezoid = trapezoid;
            LeftChildren = null;
            RightChildren = null;
        }

        /// <summary>
        /// Return a tree that contains 4 trapezoids
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To4Traps(Segment segment)
        {
            // Vertex nodes
            Node root = new VertexNode(segment.LeftVertex);
            Node rightVertexNode = new VertexNode(segment.RightVertex);

            // Segment node
            Node segmentNode = new SegmentNode(segment);

            // Trapezoid nodes
            Trapezoid leftTrap = new Trapezoid(Trapezoid.Leftp, segment.LeftVertex, Trapezoid.Top, Trapezoid.Bottom);
            Trapezoid higherCenterTrap = new Trapezoid(segment.LeftVertex, segment.RightVertex, Trapezoid.Top, segment);
            Trapezoid lowerCenterTrap = new Trapezoid(segment.LeftVertex, segment.RightVertex, segment, Trapezoid.Bottom);
            Trapezoid rightTrap = new Trapezoid(segment.RightVertex, Trapezoid.Rightp, Trapezoid.Top, Trapezoid.Bottom);

            Node leftTrapNode = new TrapezoidalNode(leftTrap);
            Node higherCenterTrapNode = new TrapezoidalNode(higherCenterTrap);
            Node lowerCenterTrapNode = new TrapezoidalNode(lowerCenterTrap);
            Node rightTrapNode = new TrapezoidalNode(rightTrap);

            leftTrap.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor, higherCenterTrap, lowerCenterTrap);
            leftTrap.Node = leftTrapNode;

            higherCenterTrap.SetNeighbor(leftTrap, leftTrap, rightTrap, rightTrap);
            higherCenterTrap.Node = higherCenterTrapNode;

            lowerCenterTrap.SetNeighbor(leftTrap, leftTrap, rightTrap, rightTrap);
            lowerCenterTrap.Node = lowerCenterTrapNode;

            rightTrap.SetNeighbor(higherCenterTrap, lowerCenterTrap, Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);
            rightTrap.Node = rightTrapNode;

            segmentNode.SetChildren(ref higherCenterTrapNode, ref lowerCenterTrapNode);
            rightVertexNode.SetChildren(ref segmentNode, ref rightTrapNode);
            root.SetChildren(ref leftTrapNode,ref rightVertexNode);

            return root;
        }

        /// <summary>
        /// Return a tree that contains right trapezoid and 2 left trapezoids
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To3TrapsRight_2Left(Segment segment)
        {
            // Vertex node
            Node root = new VertexNode(segment.RightVertex);

            // Segment node
            Node segmentNode = new SegmentNode(segment);

            // Trapezoid nodes
            Trapezoid higherTrap = new Trapezoid(segment.LeftVertex, segment.RightVertex, Trapezoid.Top, segment);
            Trapezoid lowerTrap = new Trapezoid(segment.LeftVertex, segment.RightVertex, segment, Trapezoid.Bottom);
            Trapezoid rightTrap = new Trapezoid(segment.RightVertex, Trapezoid.Rightp, Trapezoid.Top, Trapezoid.Bottom);

            Node higherTrapNode = new TrapezoidalNode(higherTrap);
            Node lowerTrapNode = new TrapezoidalNode(higherTrap);
            Node rightTrapNode = new TrapezoidalNode(rightTrap);

            higherTrap.SetNeighbor(null, null, rightTrap, rightTrap);
            higherTrap.Node = higherTrapNode;

            lowerTrap.SetNeighbor(null, null, rightTrap, rightTrap);
            lowerTrap.Node = lowerTrapNode;

            rightTrap.SetNeighbor(higherTrap, lowerTrap, Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);
            rightTrap.Node = rightTrapNode;

            segmentNode.SetChildren(ref higherTrapNode, ref lowerTrapNode);
            root.SetChildren(ref segmentNode, ref rightTrapNode);

            return root;
        }

        /// <summary>
        /// Return a tree that contains left trapezoid and 2 right trapezoids
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To3TrapsLeft_2Right(Segment segment)
        {
            // Vertex node
            Node root = new VertexNode(segment.LeftVertex);

            // Segment node
            Node segmentNode = new SegmentNode(segment);

            // Trapezoid nodes           
            Trapezoid leftTrap = new Trapezoid(Trapezoid.Leftp, segment.LeftVertex, Trapezoid.Top, Trapezoid.Bottom);
            Trapezoid higherTrap = new Trapezoid(segment.LeftVertex, segment.RightVertex, Trapezoid.Top, segment);
            Trapezoid lowerTrap = new Trapezoid(segment.LeftVertex, segment.RightVertex, segment, Trapezoid.Bottom);

            Node higherTrapNode = new TrapezoidalNode(higherTrap);
            Node lowerTrapNode = new TrapezoidalNode(lowerTrap);
            Node leftTrapNode = new TrapezoidalNode(leftTrap);

            higherTrap.SetNeighbor(leftTrap, leftTrap, null, null);
            higherTrap.Node = higherTrapNode;

            lowerTrap.SetNeighbor(leftTrap, leftTrap, null, null);
            lowerTrap.Node = lowerTrapNode;

            leftTrap.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor, higherTrap, lowerTrap);
            leftTrap.Node = leftTrapNode;

            segmentNode.SetChildren(ref higherTrapNode, ref lowerTrapNode);
            root.SetChildren(ref leftTrapNode, ref segmentNode);

            return root;
        }

        /// <summary>
        /// Return a tree that contains right trapezoid and 2 left trapezoids.
        /// Top contains left vertex
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To3TrapsTopLeft(Segment segment)
        {
            Node root = To3TrapsRight_2Left(segment);
            ((TrapezoidalNode)root.LeftChildren.RightChildren).Trapezoid.SetLeftNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains right trapezoid and 2 left trapezoids.
        /// Bottom contains left vertex
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To3TrapsBottomLeft(Segment segment)
        {
            Node root = To3TrapsRight_2Left(segment);
            ((TrapezoidalNode)root.LeftChildren.LeftChildren).Trapezoid.SetLeftNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains right trapezoid and 2 left trapezoids.
        /// Vertical edge contains left vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To3TrapsCenterLeft(Segment segment)
        {
            Node root = To3TrapsRight_2Left(segment);
            ((TrapezoidalNode)root.LeftChildren.LeftChildren).Trapezoid.SetLeftNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.HigherLeftNeighbor);
            ((TrapezoidalNode)root.LeftChildren.RightChildren).Trapezoid.SetLeftNeighbor(Trapezoid.LowerLeftNeighbor, Trapezoid.LowerLeftNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains left trapezoid and 2 right trapezoids
        /// (The rightp of higher trapezoid is rightp of old trapezoid and in top)
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To3TrapsTopRight(Segment segment)
        {
            Node root = To3TrapsLeft_2Right(segment);
            ((TrapezoidalNode)root.RightChildren.RightChildren).Trapezoid.SetRightNeighbor(Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains left trapezoid and 2 right trapezoids
        /// (The rightp of higher trapezoid is rightp of old trapezoid and in bottom)
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To3TrapsBottomRight(Segment segment)
        {
            // Vertex node
            Node root = To3TrapsLeft_2Right(segment);
            ((TrapezoidalNode)root.RightChildren.LeftChildren).Trapezoid.SetRightNeighbor(Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains right trapezoid and 2 left trapezoids.
        /// Vertical edge contains right vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To3TrapsCenterRight(Segment segment)
        {
            Node root = To3TrapsLeft_2Right(segment);
            ((TrapezoidalNode)root.RightChildren.LeftChildren).Trapezoid.SetLeftNeighbor(Trapezoid.HigherRightNeighbor, Trapezoid.HigherRightNeighbor);
            ((TrapezoidalNode)root.RightChildren.RightChildren).Trapezoid.SetLeftNeighbor(Trapezoid.LowerRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2Traps(Segment segment)
        {
            // Segment node
            Node root = new SegmentNode(segment);

            // Trapezoid nodes           
            Trapezoid higherTrap = new Trapezoid(segment.LeftVertex, segment.RightVertex, Trapezoid.Top, segment);
            Trapezoid lowerTrap = new Trapezoid(segment.LeftVertex, segment.RightVertex, segment, Trapezoid.Bottom);

            Node higherTrapNode = new TrapezoidalNode(higherTrap);
            Node lowerTrapNode = new TrapezoidalNode(lowerTrap);

            root.SetChildren(ref higherTrapNode, ref lowerTrapNode);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Top contains right vertex and bottom contains left vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsTopRight_BottomLeft(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor, null, null);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetNeighbor(null, null, Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Right vertical edge contains right vertex and left vertical edge contains left vertex .
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsCenterRight_CenterLeft(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.HigherLeftNeighbor, Trapezoid.HigherRightNeighbor, Trapezoid.HigherRightNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetNeighbor(Trapezoid.LowerLeftNeighbor, Trapezoid.LowerLeftNeighbor, Trapezoid.LowerRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Top contains right vertex and left vertical edge contains left vertex .
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsTopRight_CenterLeft(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.HigherLeftNeighbor, null, null);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetNeighbor(Trapezoid.LowerLeftNeighbor, Trapezoid.LowerLeftNeighbor, Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Bottom contains right vertex and left vertical edge contains left vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsBottomRight_CenterLeft(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.HigherLeftNeighbor, Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetNeighbor(Trapezoid.LowerLeftNeighbor, Trapezoid.LowerLeftNeighbor, null, null);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Top contains left vertex and right vertical edge contains right vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsTopLeft_CenterRight(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetNeighbor(null, null, Trapezoid.HigherRightNeighbor, Trapezoid.HigherRightNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor, Trapezoid.LowerRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Bottom contains left vertex and right vertical edge contains right vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsBottomLeft_CenterRight(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor, Trapezoid.HigherRightNeighbor, Trapezoid.HigherRightNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetNeighbor(null, null, Trapezoid.LowerRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Top contains left vertex and bottom contains right vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsTopLeft_BottomRight(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetNeighbor(null, null, Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor, null, null);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Top contains right vertex, vertical edge doesn't contain left vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsTopRight(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetRightNeighbor(null, null);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetRightNeighbor(Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Bottom contains right vertex, vertical edge doesn't contain left vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsBottomRight(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetRightNeighbor(Trapezoid.HigherRightNeighbor, Trapezoid.LowerRightNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetRightNeighbor(null, null);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Right vertical edge contains right vertex, left vertical edge doesn't contain left vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsCenterRight(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetRightNeighbor(Trapezoid.HigherRightNeighbor, Trapezoid.HigherRightNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetRightNeighbor(Trapezoid.LowerRightNeighbor, Trapezoid.LowerRightNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Top contains left vertex, vertical edge doesn't contain right vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsTopLeft(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetLeftNeighbor(null, null);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetLeftNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Bottom contains left vertex , vertical edge doesn't contain right vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsBottomLeft(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetLeftNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.LowerLeftNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetLeftNeighbor(null, null);

            return root;
        }

        /// <summary>
        /// Return a tree that contains 2 trapezoids.
        /// Left vertical edge contains left vertex , right vertical edge doesn't contain right vertex.
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        public Node To2TrapsCenterLeft(Segment segment)
        {
            Node root = To2Traps(segment);

            ((TrapezoidalNode)root.LeftChildren).Trapezoid.SetLeftNeighbor(Trapezoid.HigherLeftNeighbor, Trapezoid.HigherLeftNeighbor);
            ((TrapezoidalNode)root.RightChildren).Trapezoid.SetLeftNeighbor(Trapezoid.LowerLeftNeighbor, Trapezoid.LowerLeftNeighbor);

            return root;
        }

        public override bool Equals(object obj)
        {
            TrapezoidalNode objTrapNode = (TrapezoidalNode)obj;
            if (!Trapezoid.Equals(objTrapNode.Trapezoid))
                return false;
            return base.Equals(obj);
        }

        public override string ToString()
        {
            return " t ";
        }
    }
}