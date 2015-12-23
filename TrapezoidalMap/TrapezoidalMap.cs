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
                throw new NotImplementedException();
            }
        }

        public IEnumerable<ISegment> Segments
        {
            get
            {
                throw new NotImplementedException();
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
                throw new NotImplementedException();
            }
        }

        public IEnumerable<IVertex> Vertices
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Construct TrapezoidalMap
        /// </summary>

        public TrapezoidalMap(List<Segment> segments)
        {
            int n = segments.Count();
            IEnumerable<int> RandomPermutation = new RandomPermutation(n);

            Trapezoid RecBoundary = RectangleBoundary(segments);
            Root = new TrapezoidalNode(RecBoundary);
            RecBoundary.Node = Root;

            foreach(int i in RandomPermutation) 
            {
                Vertex leftVertex = segments[i].LeftVertex;
                Vertex rightVertex = segments[i].RightVertex;
                Segment newsegment = segments[i];
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
                    }
                    // Case 4:  Left vertical edge doesn't contain left of segment
                    else
                    {
                        FirstChildren = ((TrapezoidalNode)FirstTrap.Node).To3TrapsLeft_2Right(newsegment);
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
                    }
                    // Case 4:  Left vertical edge doesn't contain left of segment
                    else
                    {
                        LastChildren = ((TrapezoidalNode)LastTrap.Node).To3TrapsRight_2Left(newsegment);
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
                    }


                    if (ListInterTraps.Count > 2)
                    {

                        List<Node> ListMdiChildren = new List<Node>();
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

                            ListMdiChildren.Add(MidChildren);
                        }

                        // Merge trapezoids


                        // 2 first Trapezoids
                        if(FirstChildren.RightChildren.IsTrapezoid)
                        {
                            if(((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.Rightp == null)
                            {
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid;
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)ListMdiChildren.First().LeftChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.LeftChildren = MergeNode;
                                ListMdiChildren.First().LeftChildren = MergeNode;
                            }
                            else
                            {
                                Trapezoid FirstLowerTrap = ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid;
                                Trapezoid SecondLowerTrap = ((TrapezoidalNode)ListMdiChildren.First().RightChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstLowerTrap.Leftp, SecondLowerTrap.Rightp, FirstLowerTrap.Top, FirstLowerTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren = MergeNode;
                                ListMdiChildren.First().RightChildren = MergeNode;
                            }
                        }
                        else
                        {
                            if (((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid.Rightp == null)
                            {
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid;
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)ListMdiChildren.First().LeftChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren.LeftChildren = ListMdiChildren.First().LeftChildren = MergeNode;
                            }
                            else
                            {
                                Trapezoid FirstLowerTrap = ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid;
                                Trapezoid SecondLowerTrap = ((TrapezoidalNode)ListMdiChildren.First().RightChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstLowerTrap.Leftp, SecondLowerTrap.Rightp, FirstLowerTrap.Top, FirstLowerTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren.RightChildren = MergeNode;
                                ListMdiChildren.First().RightChildren = MergeNode;
                            }
                        }

                        if (ListMdiChildren.Count > 1)
                            // Còn trường hợp chưa xử lí
                            for (int j = 0; j < ListMdiChildren.Count - 1; j++)
                            {
                                if (((TrapezoidalNode)ListMdiChildren[j].LeftChildren).Trapezoid.Rightp == null)
                                {
                                    Trapezoid FirstHigherTrap = ((TrapezoidalNode)ListMdiChildren[j].LeftChildren).Trapezoid;
                                    Trapezoid SecondHigherTrap = ((TrapezoidalNode)ListMdiChildren[j + 1].LeftChildren).Trapezoid;
                                    Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                    Node MergeNode = new TrapezoidalNode(MergeTrap);
                                    ListMdiChildren[j].LeftChildren = MergeNode;
                                    ListMdiChildren[j + 1].LeftChildren = MergeNode;
                                }
                                else
                                {
                                    Trapezoid FirstLowerTrap = ((TrapezoidalNode)ListMdiChildren[j].RightChildren).Trapezoid;
                                    Trapezoid SecondLowerTrap = ((TrapezoidalNode)ListMdiChildren[j + 1].RightChildren).Trapezoid;
                                    Trapezoid MergeTrap = new Trapezoid(FirstLowerTrap.Leftp, SecondLowerTrap.Rightp, FirstLowerTrap.Top, FirstLowerTrap.Bottom);
                                    Node MergeNode = new TrapezoidalNode(MergeTrap);
                                    ListMdiChildren[j].RightChildren = MergeNode;
                                    ListMdiChildren[j + 1].RightChildren = MergeNode;
                                }
                            }

                        // 2 last Trapezoids
                        if (LastChildren.LeftChildren.IsTrapezoid)
                        {
                            if (((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.Leftp == null)
                            {
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid;
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)ListMdiChildren.Last().LeftChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                LastChildren.LeftChildren = MergeNode;
                                ListMdiChildren.Last().LeftChildren = MergeNode;
                            }
                            else
                            {
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid;
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)ListMdiChildren.Last().RightChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                LastChildren.RightChildren = MergeNode;
                                ListMdiChildren.Last().RightChildren = MergeNode;
                            }
                        }
                        else
                        {
                            if (((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid.Leftp == null)
                            {
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid;
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)ListMdiChildren.Last().LeftChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                LastChildren.LeftChildren.LeftChildren = MergeNode;
                                ListMdiChildren.Last().LeftChildren = MergeNode;
                            }
                            else
                            {
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid;
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)ListMdiChildren.Last().RightChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                LastChildren.LeftChildren.RightChildren = MergeNode;
                                ListMdiChildren.Last().RightChildren = MergeNode;
                            }
                        }
                    }

                    else
                    {
                        if(FirstChildren.RightChildren.IsTrapezoid && LastChildren.LeftChildren.IsTrapezoid)
                        {
                            if (((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.Rightp == null)
                            {
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid;
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.LeftChildren = MergeNode;
                                LastChildren.LeftChildren = MergeNode;
                                ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)LastChildren.RightChildren).Trapezoid, ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid);
                                ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid, ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid);
                            }
                            else
                            {
                                Trapezoid FirstLowerTrap = ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid;
                                Trapezoid SecondLowerTrap = ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstLowerTrap.Leftp, SecondLowerTrap.Rightp, FirstLowerTrap.Top, FirstLowerTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren = MergeNode;
                                LastChildren.RightChildren = MergeNode;
                                ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid, ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid);
                                ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid, ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid);
                            }
                        }

                        else if(FirstChildren.RightChildren.IsTrapezoid)
                        {
                            if (((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.Rightp == null)
                            {
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid;
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.LeftChildren = MergeNode;
                                LastChildren.LeftChildren.LeftChildren = MergeNode;
                                ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid, ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid);
                                ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid, ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid);
                            }
                            else
                            {
                                Trapezoid FirstLowerTrap = ((TrapezoidalNode)FirstChildren.RightChildren).Trapezoid;
                                Trapezoid SecondLowerTrap = ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstLowerTrap.Leftp, SecondLowerTrap.Rightp, FirstLowerTrap.Top, FirstLowerTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren = MergeNode;
                                LastChildren.LeftChildren.RightChildren = MergeNode;
                                ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid, ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid);
                                ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid, ((TrapezoidalNode)FirstChildren.LeftChildren).Trapezoid);
                            }
                        }
                        else if(LastChildren.LeftChildren.IsTrapezoid)
                        {
                            if (((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid.Rightp == null)
                            {
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid;
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren.LeftChildren = MergeNode;
                                LastChildren.LeftChildren = MergeNode;
                                ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)LastChildren.RightChildren).Trapezoid, ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid);
                                ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid, ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid);
                            }
                            else
                            {
                                Trapezoid FirstLowerTrap = ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid;
                                Trapezoid SecondLowerTrap = ((TrapezoidalNode)LastChildren.RightChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstLowerTrap.Leftp, SecondLowerTrap.Rightp, FirstLowerTrap.Top, FirstLowerTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren.RightChildren = MergeNode;
                                LastChildren.RightChildren = MergeNode;
                                ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid, ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid);
                                ((TrapezoidalNode)LastChildren.LeftChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid, ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid);
                            }
                        }
                        else
                        {
                            if (((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid.Rightp == null)
                            {
                                Trapezoid FirstHigherTrap = ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid;
                                Trapezoid SecondHigherTrap = ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstHigherTrap.Leftp, SecondHigherTrap.Rightp, FirstHigherTrap.Top, FirstHigherTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren.LeftChildren = MergeNode;
                                LastChildren.LeftChildren.LeftChildren = MergeNode;
                                ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid, ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid);
                                ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid, ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid);
                            }
                            else
                            {
                                Trapezoid FirstLowerTrap = ((TrapezoidalNode)FirstChildren.RightChildren.RightChildren).Trapezoid;
                                Trapezoid SecondLowerTrap = ((TrapezoidalNode)LastChildren.LeftChildren.RightChildren).Trapezoid;
                                Trapezoid MergeTrap = new Trapezoid(FirstLowerTrap.Leftp, SecondLowerTrap.Rightp, FirstLowerTrap.Top, FirstLowerTrap.Bottom);
                                Node MergeNode = new TrapezoidalNode(MergeTrap);
                                FirstChildren.RightChildren.RightChildren = MergeNode;
                                LastChildren.LeftChildren.RightChildren = MergeNode;
                                ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid.SetRightNeighbor(((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid, ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid);
                                ((TrapezoidalNode)LastChildren.LeftChildren.LeftChildren).Trapezoid.SetLeftNeighbor(((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid, ((TrapezoidalNode)FirstChildren.RightChildren.LeftChildren).Trapezoid);
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
            ListInterTraps.Add(Location(segment.LeftVertex));

            while (!ListInterTraps.Last().Contain(segment.RightVertex))
            {
                if (segment.BelowOf(ListInterTraps.Last().Rightp))
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
        Trapezoid RectangleBoundary(IEnumerable<Segment> segments)
        {
            double leftX = 0;
            double rightX = 0;
            double topY = 0;
            double bottomY = 0;

            foreach(Segment segment in segments)
            {
                leftX = leftX < segment.LeftVertex.X ? leftX : segment.LeftVertex.X;

                rightX = rightX > segment.RightVertex.X ? rightX : segment.RightVertex.X;

                topY = topY > segment.LeftVertex.Y ? topY : segment.LeftVertex.Y;
                topY = topY > segment.RightVertex.Y ? topY : segment.RightVertex.Y;

                bottomY = bottomY < segment.LeftVertex.Y? bottomY : segment.LeftVertex.Y;
                bottomY = bottomY < segment.RightVertex.Y ? bottomY : segment.RightVertex.Y;
            }

            --leftX;
            ++rightX;
            ++topY;
            --bottomY;

            this.boundingBox = new Rectangle(leftX, rightX, bottomY, topY);

            Trapezoid R = new Trapezoid();
            R.Leftp = new Vertex(leftX, topY);
            R.Rightp = new Vertex(rightX, bottomY);

            R.Top = new Segment(R.Leftp, new Vertex(rightX, topY));
            R.Bottom = new Segment(new Vertex(leftX, bottomY), R.Rightp);
            
            return R;
        }

        /// <summary>
        /// Find trapezoid that contain p
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Trapezoid Location(Vertex p)
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
            Vertex p = new Vertex(point.X, point.Y);
            return Location(p);
        }
    }
}