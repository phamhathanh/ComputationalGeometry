using ComputationalGeometry.Common;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ComputationalGeometry.TrapezoidalMap
{
    public class TrapezoidalMap : ITrapezoidalMap
    {
        Node Root = new Node();
        private Rectangle boundingBox;

        public IEnumerable<ITrapezoid> Trapezoids
        {
            get
            {
                foreach (var node in AllNodes())
                {
                    if (node.IsTrapezoid)
                        yield return ((TrapezoidalNode)node).Trapezoid;
                }
            }
        }

        private IEnumerable<Node> AllNodes()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(Root);
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();
                yield return item;
                queue.Enqueue(item.LeftChildren);
                queue.Enqueue(item.RightChildren);
            }
        }

        public IEnumerable<ISegment> Segments
        {
            get
            {
                foreach (var node in AllNodes())
                {
                    if (node.IsSegment)
                        yield return ((SegmentNode)node).segment;
                }
            }
        }

        public Rectangle BoundingBox
        {
            get
            {
                return boundingBox;
            }
        }

        public IEnumerable<IVerticalEdge> VerticalEdges
        {
            get
            {
                foreach (var node in AllNodes())
                {
                    if (node.IsVertex)
                    {
                        var vertex = ((VertexNode)node).Vertex;
                        yield return vertex.LowerExtension;
                        yield return vertex.UpperExtension;
                    }
                }
            }
        }

        public IEnumerable<IVertex> Vertices
        {
            get
            {
                foreach (var node in AllNodes())
                {
                    if (node.IsVertex)
                        yield return ((VertexNode)node).Vertex; 
                }
            }
        }

        /// <summary>
        /// Construct TrapezoidalMap
        /// </summary>

        public TrapezoidalMap(List<Common.Segment> segments)
        {
            int n = segments.Count();
            IEnumerable<int> RandomPermutation = new RandomPermutation(n);

            Trapezoid RecBoundary = RectangleBoundary(segments);
            Root = new TrapezoidalNode(RecBoundary);
            RecBoundary.Node = Root;

            foreach (int i in RandomPermutation)
            {
                Vertex leftVertex;
                Vertex rightVertex;
                if (segments[i].Vertex1.Position.X < segments[i].Vertex2.Position.X)
                {
                    leftVertex = segments[i].Vertex1;
                    rightVertex = segments[i].Vertex2;
                }
                else
                {
                    leftVertex = segments[i].Vertex2;
                    rightVertex = segments[i].Vertex1;
                }

                Segment newsegment = new Segment(leftVertex, rightVertex);
                var ListInterTraps = FindIntersectedTraps(newsegment);                               // List of trapezoids that intersect segment


                // Update search structure
                // SECTION 1: a trapezoid contain segment                                           
                //

                if (ListInterTraps.Count == 1)
                {
                    Trapezoid interTrap = ListInterTraps.Last();                                    // Intersected trapezoid

                    // Case1: Top contains left of segment, bottom contains right of segment
                    if (leftVertex.Equals(interTrap.Top.LeftVertex) && rightVertex.Equals(interTrap.Bottom.RightVertex))
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To2TrapsTopLeft_BottomRight(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case2: Top contains left of segment and right vertical edge contains right of segment
                    else if (leftVertex.Equals(interTrap.Top.LeftVertex) && interTrap.Rightp.X == rightVertex.X)
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To2TrapsTopLeft_CenterRight(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case3: Bottom contains left of segment, top contains right of segment
                    else if (leftVertex.Equals(interTrap.Bottom.LeftVertex) && rightVertex.Equals(interTrap.Top.RightVertex))
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To2TrapsTopRight_BottomLeft(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case4: Bottom contains left of segment and right vertical edge contains right of segment
                    else if (leftVertex.Equals(interTrap.Bottom.LeftVertex) && interTrap.Rightp.X == rightVertex.X)
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To2TrapsBottomLeft_CenterRight(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case5: Left vertical edge contains left of segment and top contains right of segment
                    else if (leftVertex.X == interTrap.Leftp.X && rightVertex.Equals(interTrap.Top.RightVertex))
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To2TrapsTopRight_CenterLeft(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case6: Left vertical edge contains left of segment and bottom contains right of segment
                    else if (leftVertex.X == interTrap.Leftp.X && rightVertex.Equals(interTrap.Bottom.RightVertex))
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To2TrapsBottomRight_CenterLeft(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case7: Left vertical edge contains left of segment and right vertical edge contains right of segment
                    else if (leftVertex.X == interTrap.Leftp.X && rightVertex.X == interTrap.Rightp.X)
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To2TrapsCenterRight_CenterLeft(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case8: Top contains left of segment and right vertical edge doesn't contain right of segment
                    else if (leftVertex.Equals(interTrap.Top.LeftVertex))
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To3TrapsTopLeft(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case9: Bottom contains left of segment and right vertical edge doesn't contain right of segment
                    else if (leftVertex.Equals(interTrap.Bottom.LeftVertex))
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To3TrapsBottomLeft(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case10: Left vertical edge contains left of segment and right vertical edge doesn't contain right of segment
                    else if (leftVertex.X == interTrap.Leftp.X)
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To3TrapsCenterLeft(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case11: Left vertical edge doesn't contains left of segment and top contain right of segment
                    else if (rightVertex.Equals(interTrap.Top.RightVertex))
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To3TrapsTopRight(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case12: Left vertical edge doesn't contains left of segment and bottom contain right of segment
                    else if (rightVertex.Equals(interTrap.Bottom.RightVertex))
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To3TrapsBottomRight(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case13: Left vertical edge doesn't contains left of segment and right vertical edge contain right of segment
                    else if (rightVertex.X == interTrap.Rightp.X)
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To3TrapsCenterRight(newsegment);
                        foreach (Node parent in interTrap.Node.Parent)
                        {
                            if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                parent.SetLeftChildren(ref Children);
                            else
                                parent.SetRightChildren(ref Children);
                        }
                    }

                    // Case14: Left vertical edge doesn't contains left of segment and right vertical edge doesn't contain right of segment
                    else
                    {
                        Node Children = ((TrapezoidalNode)interTrap.Node).To4Traps(newsegment);
                        if (interTrap.Node.Parent.Count != 0)
                            foreach (Node parent in interTrap.Node.Parent)
                            {
                                if (parent.LeftChildren.IsTrapezoid && ((TrapezoidalNode)parent.LeftChildren).Trapezoid.Equals(interTrap))
                                    parent.SetLeftChildren(ref Children);
                                else
                                    parent.SetRightChildren(ref Children);
                            }
                        else
                            Root = Children;
                    }
                }

                //
                // SECTION 2: many trapezoids intersect segment
                //

                else
                {
                    List<int> WaitTraps = new List<int>();
                    bool HigherTrapsWait = false;
                    bool LowerTrapsWait = false;
                    List<Node> ListChildrens = new List<Node>();
                    bool FirstExcess = false;
                    bool LastExcess = false;

                    // First trapezoid

                    Trapezoid FirstTrap = ListInterTraps[0];
                    Node FirstChildren = new Node();
                    // Case 1: Top contains left of segment
                    if (FirstTrap.Top.LeftVertex.Equals(newsegment.LeftVertex))
                    {
                        FirstChildren = ((TrapezoidalNode)FirstTrap.Node).To2TrapsTopLeft(newsegment);
                        if (FirstTrap.Top.RightVertex.Equals(FirstTrap.Rightp))
                        {
                            ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.SetVertex(newsegment.LeftVertex, FirstTrap.Rightp);
                            ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid.SetVertex(newsegment.LeftVertex, null);
                        }
                        else
                        {
                            ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid.SetVertex(newsegment.LeftVertex, FirstTrap.Rightp);
                            ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.SetVertex(newsegment.LeftVertex, null);
                        }
                        ListChildrens.Add(FirstChildren);
                    }
                    // Case 2: Bottom contains left of segment
                    else if (FirstTrap.Bottom.LeftVertex.Equals(newsegment.LeftVertex))
                    {
                        FirstChildren = ((TrapezoidalNode)FirstTrap.Node).To2TrapsBottomLeft(newsegment);
                        if (FirstTrap.Top.RightVertex.Equals(FirstTrap.Rightp))
                        {
                            ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.SetVertex(newsegment.LeftVertex, FirstTrap.Rightp);
                            ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid.SetVertex(newsegment.LeftVertex, null);
                        }
                        else
                        {
                            ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid.SetVertex(newsegment.LeftVertex, FirstTrap.Rightp);
                            ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.SetVertex(newsegment.LeftVertex, null);
                        }
                        ListChildrens.Add(FirstChildren);
                    }
                    // Case 3:  Left vertical edge contains left of segment
                    else if (FirstTrap.Leftp.X == newsegment.LeftVertex.X)
                    {
                        FirstChildren = ((TrapezoidalNode)FirstTrap.Node).To2TrapsCenterLeft(newsegment);
                        if (FirstTrap.Top.RightVertex.Equals(FirstTrap.Rightp))
                        {
                            ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.SetVertex(newsegment.LeftVertex, FirstTrap.Rightp);
                            ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid.SetVertex(newsegment.LeftVertex, null);
                        }
                        else
                        {
                            ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid.SetVertex(newsegment.LeftVertex, FirstTrap.Rightp);
                            ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.SetVertex(newsegment.LeftVertex, null);
                        }
                        ListChildrens.Add(FirstChildren);
                    }
                    // Case 4:  Left vertical edge doesn't contain left of segment
                    else
                    {
                        FirstChildren = ((TrapezoidalNode)FirstTrap.Node).To3TrapsLeft_2Right(newsegment);
                        FirstExcess = true;
                        if (FirstTrap.Top.RightVertex.Equals(FirstTrap.Rightp))
                        {
                            ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid.SetVertex(newsegment.LeftVertex, FirstTrap.Rightp);
                            ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid.SetVertex(newsegment.LeftVertex, null);
                        }
                        else
                        {
                            ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid.SetVertex(newsegment.LeftVertex, FirstTrap.Rightp);
                            ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid.SetVertex(newsegment.LeftVertex, null);
                        }
                        ListChildrens.Add(FirstChildren.RightChildren);
                    }

                    // Middle trapezoids

                    for (int j = 1; j < ListInterTraps.Count - 1; j++)
                    {
                        Trapezoid MidTrap = ListInterTraps[j];
                        Node MidChildren = ((TrapezoidalNode)MidTrap.Node).To2Traps(newsegment);

                        if (MidTrap.Top.LeftVertex.Equals(MidTrap.Leftp))
                        {
                            ((TrapezoidalNode)MidChildren.LeftChildren).Trapezoid.SetLeftVertex(MidTrap.Leftp);
                            ((TrapezoidalNode)MidChildren.RightChildren).Trapezoid.SetLeftVertex(null);
                        }
                        else
                        {
                            ((TrapezoidalNode)MidChildren.RightChildren).Trapezoid.SetLeftVertex(MidTrap.Leftp);
                            ((TrapezoidalNode)MidChildren.LeftChildren).Trapezoid.SetLeftVertex(null);
                        }

                        if (MidTrap.Top.RightVertex.Equals(MidTrap.Rightp))
                        {
                            ((TrapezoidalNode)MidChildren.LeftChildren).Trapezoid.SetRightVertex(MidTrap.Rightp);
                            ((TrapezoidalNode)MidChildren.RightChildren).Trapezoid.SetRightVertex(null);
                        }
                        else
                        {
                            ((TrapezoidalNode)MidChildren.RightChildren).Trapezoid.SetRightVertex(MidTrap.Rightp);
                            ((TrapezoidalNode)MidChildren.LeftChildren).Trapezoid.SetRightVertex(null);
                        }
                        ListChildrens.Add(MidChildren);
                    }

                    // Last trapezoid

                    Trapezoid LastTrap = ListInterTraps.Last();
                    Node LastChildren = new Node();
                    // Case 1: Top contains right of segment
                    if (LastTrap.Top.RightVertex.Equals(newsegment.RightVertex))
                    {
                        LastChildren = ((TrapezoidalNode)LastTrap.Node).To2TrapsTopRight(newsegment);
                        if (LastTrap.Top.LeftVertex.Equals(LastTrap.Leftp))
                        {
                            ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.SetVertex(LastTrap.Leftp, newsegment.RightVertex);
                            ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid.SetVertex(null, newsegment.RightVertex);
                        }
                        else
                        {
                            ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid.SetVertex(LastTrap.Leftp, newsegment.RightVertex);
                            ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.SetVertex(null, newsegment.RightVertex);
                        }
                        ListChildrens.Add(LastChildren);
                    }
                    // Case 2: Bottom contains right of segment
                    else if (LastTrap.Bottom.RightVertex.Equals(newsegment.RightVertex))
                    {
                        LastChildren = ((TrapezoidalNode)LastTrap.Node).To2TrapsBottomRight(newsegment);
                        if (LastTrap.Top.LeftVertex.Equals(LastTrap.Leftp))
                        {
                            ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.SetVertex(LastTrap.Leftp, newsegment.RightVertex);
                            ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid.SetVertex(null, newsegment.RightVertex);
                        }
                        else
                        {
                            ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid.SetVertex(LastTrap.Leftp, newsegment.RightVertex);
                            ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.SetVertex(null, newsegment.RightVertex);
                        }
                        ListChildrens.Add(LastChildren);
                    }
                    // Case 3:  Left vertical edge contains right of segment
                    else if (LastTrap.Rightp.X == newsegment.RightVertex.X)
                    {
                        LastChildren = ((TrapezoidalNode)LastTrap.Node).To2TrapsCenterRight(newsegment);
                        if (LastTrap.Top.LeftVertex.Equals(LastTrap.Leftp))
                        {
                            ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.SetVertex(LastTrap.Leftp, newsegment.RightVertex);
                            ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid.SetVertex(null, newsegment.RightVertex);
                        }
                        else
                        {
                            ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid.SetVertex(LastTrap.Leftp, newsegment.RightVertex);
                            ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.SetVertex(null, newsegment.RightVertex);
                        }
                        ListChildrens.Add(LastChildren);
                    }
                    // Case 4:  Left vertical edge doesn't contain left of segment
                    else
                    {
                        LastChildren = ((TrapezoidalNode)LastTrap.Node).To3TrapsRight_2Left(newsegment);
                        LastExcess = true;
                        if (LastTrap.Top.LeftVertex.Equals(LastTrap.Leftp))
                        {
                            ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid.SetVertex(LastTrap.Leftp, newsegment.RightVertex);
                            ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid.SetVertex(null, newsegment.RightVertex);
                        }
                        else
                        {
                            ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid.SetVertex(LastTrap.Leftp, newsegment.RightVertex);
                            ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid.SetVertex(null, newsegment.RightVertex);
                        }
                        ListChildrens.Add(LastChildren.LeftChildren);
                    }

                    ((TrapezoidalNode)ListChildrens[0].LeftChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)ListChildrens[1].LeftChildren).Trapezoid, ((TrapezoidalNode)ListChildrens[1].LeftChildren).Trapezoid);
                    ((TrapezoidalNode)ListChildrens[0].RightChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)ListChildrens[1].RightChildren).Trapezoid, ((TrapezoidalNode)ListChildrens[1].RightChildren).Trapezoid);

                    ((TrapezoidalNode)ListChildrens.Last().LeftChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)ListChildrens[ListChildrens.Count - 2].LeftChildren).Trapezoid, ((TrapezoidalNode)ListChildrens[ListChildrens.Count - 2].LeftChildren).Trapezoid);
                    ((TrapezoidalNode)ListChildrens.Last().RightChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)ListChildrens[ListChildrens.Count - 2].RightChildren).Trapezoid, ((TrapezoidalNode)ListChildrens[ListChildrens.Count - 2].RightChildren).Trapezoid);

                    for (int j = 1; j < ListChildrens.Count - 1; j++)
                    {
                        ((TrapezoidalNode)ListChildrens[j].LeftChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)ListChildrens[j + 1].LeftChildren).Trapezoid, ((TrapezoidalNode)ListChildrens[j + 1].LeftChildren).Trapezoid);
                        ((TrapezoidalNode)ListChildrens[j].RightChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)ListChildrens[j + 1].RightChildren).Trapezoid, ((TrapezoidalNode)ListChildrens[j + 1].RightChildren).Trapezoid);
                        ((TrapezoidalNode)ListChildrens[j].LeftChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)ListChildrens[j - 1].LeftChildren).Trapezoid, ((TrapezoidalNode)ListChildrens[j + 1].LeftChildren).Trapezoid);
                        ((TrapezoidalNode)ListChildrens[j].RightChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)ListChildrens[j - 1].RightChildren).Trapezoid, ((TrapezoidalNode)ListChildrens[j + 1].RightChildren).Trapezoid);
                    }

                    bool continueCheck = true;
                    while (continueCheck)
                    {
                        continueCheck = false;
                        for (int j = 0; j < ListChildrens.Count - 1; j++)
                        {
                            if (((TrapezoidalNode)ListChildrens[i].LeftChildren).Trapezoid.Rightp == null)
                            {
                                continueCheck = true;
                                Trapezoid oldLeftTrap = ((TrapezoidalNode)ListChildrens[i].LeftChildren).Trapezoid;
                                Trapezoid oldRightTrap = ((TrapezoidalNode)ListChildrens[i + 1].LeftChildren).Trapezoid;
                                Trapezoid newTrap = new Trapezoid(oldLeftTrap.Leftp, oldRightTrap.Rightp, oldLeftTrap.Top, oldLeftTrap.Bottom);
                                newTrap.SetNeighbor(oldLeftTrap.HigherLeftNeighbor, oldLeftTrap.LowerLeftNeighbor, oldRightTrap.HigherRightNeighbor, oldRightTrap.LowerRightNeighbor);
                                TrapezoidalNode newNode = new TrapezoidalNode(newTrap);
                                ListChildrens[i].LeftChildren = newNode;
                                ListChildrens[i + 1].LeftChildren = newNode;
                            }

                            if (((TrapezoidalNode)ListChildrens[i].RightChildren).Trapezoid.Rightp == null)
                            {
                                continueCheck = true;
                                Trapezoid oldLeftTrap = ((TrapezoidalNode)ListChildrens[i].RightChildren).Trapezoid;
                                Trapezoid oldRightTrap = ((TrapezoidalNode)ListChildrens[i + 1].RightChildren).Trapezoid;
                                Trapezoid newTrap = new Trapezoid(oldLeftTrap.Leftp, oldRightTrap.Rightp, oldLeftTrap.Top, oldLeftTrap.Bottom);
                                newTrap.SetNeighbor(oldLeftTrap.HigherLeftNeighbor, oldLeftTrap.LowerLeftNeighbor, oldRightTrap.HigherRightNeighbor, oldRightTrap.LowerRightNeighbor);
                                TrapezoidalNode newNode = new TrapezoidalNode(newTrap);
                                ListChildrens[i].RightChildren = newNode;
                                ListChildrens[i + 1].RightChildren = newNode;
                            }

                        }
                    }
                }
            }
        }

        public TrapezoidalMap()
        {
        }

        public override string ToString()
        {
            string strMap = "a";
            Node curr = Root;
            while(curr!=null)
            {
                strMap += curr.ToString();
                curr = curr.LeftChildren;
            }

            return strMap;
        }

        /// <summary>
        /// Return list of trapezoids that intersect segment
        /// </summary>
        /// <param name="segment"></param>
        /// <returns></returns>
        List<Trapezoid> FindIntersectedTraps(Segment segment)
        {
            List<Trapezoid> ListInterTraps = new List<Trapezoid>();
            ListInterTraps.Add((Trapezoid)Location(segment.LeftVertex.Position));

            while (!ListInterTraps.Last().Contain(segment.RightVertex.Position))
            {
                if (segment.BelowOf(ListInterTraps.Last().Rightp.Position))
                    ListInterTraps.Add(ListInterTraps.Last().LowerRightNeighbor);
                else
                    ListInterTraps.Add(ListInterTraps.Last().HigherRightNeighbor);
            }
            return ListInterTraps;
        }


        /// <summary>
        /// Construct rectangle boundary of a list of half - edges
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        Trapezoid RectangleBoundary(IEnumerable<Common.Segment> segments)
        {
            double left = 0;
            double right = 0;
            double top = 0;
            double bottom = 0;

            foreach(Common.Segment segment in segments)
            {
                left = left < segment.Vertex1.Position.X ? left : segment.Vertex1.Position.X;
                left = left < segment.Vertex2.Position.X ? left : segment.Vertex2.Position.X;

                right = right > segment.Vertex1.Position.X ? right : segment.Vertex1.Position.X;
                right = right > segment.Vertex2.Position.X ? right : segment.Vertex2.Position.X;

                top = top > segment.Vertex1.Position.Y ? top : segment.Vertex1.Position.Y;
                top = top > segment.Vertex2.Position.Y ? top : segment.Vertex2.Position.Y;

                bottom = bottom < segment.Vertex1.Position.Y ? bottom : segment.Vertex1.Position.Y;
                bottom = bottom < segment.Vertex2.Position.Y ? bottom : segment.Vertex2.Position.Y;
            }

            --left;
            ++right;
            ++top;
            --bottom;

            this.boundingBox = new Rectangle(left, right, bottom, top);

            Trapezoid R = new Trapezoid();
            R.Leftp = new Vertex(left, top);
            R.Rightp = new Vertex(right, bottom);

            R.Top = new Segment(R.Leftp, new Vertex(right, top));
            R.Bottom = new Segment(new Vertex(left, bottom), R.Rightp);
            
            return R;
        }

        /// <summary>
        /// Find trapezoid that contain p
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private Trapezoid Location(Vector2 p)
        {
            Node curNode = Root;
            while (!curNode.IsTrapezoid)
            {
                if (curNode.IsSegment)
                {
                    if (((SegmentNode)curNode).segment.BelowOf(p))
                        curNode = curNode.LeftChildren;
                    else
                        curNode = curNode.RightChildren;
                }
                else
                {
                    if (((VertexNode)curNode).Vertex.LeftOf(p))
                        curNode = curNode.RightChildren;
                    else
                        curNode = curNode.LeftChildren;
                }
            } 
            return ((TrapezoidalNode)curNode).Trapezoid;
        }

        public ITrapezoid PointLocation(Vector2 point)
        {
            return Location(point);
        }
    }
}