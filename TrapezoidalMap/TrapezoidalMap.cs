using ComputationalGeometry.Common;
using System.Collections.Generic;
using System.Linq;
using System;

namespace ComputationalGeometry.TrapezoidalMap
{
    class TrapezoidalMap : ITrapezoidalMap
    {
        Node Root = new Node();

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

        /// <summary>
        /// Construct TrapezoidalMap
        /// </summary>
        public TrapezoidalMap(List<HalfEdge> edges)
        {
            int n = edges.Count();
            IEnumerable<int> RandomPermutation = Utilities.RandomPermutation(n);

            Trapezoid RecBoundary = RectangleBoundary(edges);
            Root = new TrapezoidalNode(RecBoundary);
            RecBoundary.Node = Root;

            foreach(int i in RandomPermutation) 
            {
                var ListTrapezoids = new List<Trapezoid>();                                 // List of trapezoids that intersect segment
                Vertex leftVertex;
                Vertex rightVertex;

                if (edges[i].Origin.X < edges[i].Twin.Origin.X)
                {
                    leftVertex = new Vertex(edges[i].Origin.X, edges[i].Origin.Y);
                    rightVertex = new Vertex(edges[i].Twin.Origin.X, edges[i].Twin.Origin.Y);
                }
                else
                {
                    leftVertex = new Vertex(edges[i].Twin.Origin.X, edges[i].Twin.Origin.Y);
                    rightVertex = new Vertex(edges[i].Origin.X, edges[i].Origin.Y);
                }
                Segment segment = new Segment(leftVertex, rightVertex);
                ListTrapezoids.Add(Location(leftVertex));

                while (!ListTrapezoids.Last().Contain(rightVertex))
                {
                    if (segment.BelowOf(ListTrapezoids.Last().Rightp))
                        ListTrapezoids.Add(ListTrapezoids.Last().LowerRightNeighbor);
                    else
                        ListTrapezoids.Add(ListTrapezoids.Last().HigherRightNeighbor);
                }

                if(ListTrapezoids.Count == 1)
                {
                    // Construct new tree.


                

                }

                //
                // Continue: đã tìm được tất cả các hình thang giao với cạnh mới
                //
            }
        }

        /// <summary>
        /// Construct rectangle boundary of a list of half - edges
        /// </summary>
        /// <param name="edges"></param>
        /// <returns></returns>
        Trapezoid RectangleBoundary(IEnumerable<HalfEdge> edges)
        {
            double leftX = 0;
            double rightX = 0;
            double topY = 0;
            double bottomY = 0;

            foreach(HalfEdge edge in edges)
            {
                leftX = leftX < edge.Origin.X ? leftX : edge.Origin.X;
                leftX = leftX < edge.Twin.Origin.X ? leftX : edge.Twin.Origin.X;

                rightX = rightX > edge.Origin.X ? rightX : edge.Origin.X;
                rightX = rightX > edge.Twin.Origin.X ? rightX : edge.Twin.Origin.X;

                topY = topY > edge.Origin.Y ? topY : edge.Origin.Y;
                topY = topY > edge.Twin.Origin.Y ? topY : edge.Twin.Origin.Y;

                bottomY = bottomY < edge.Origin.Y ? bottomY : edge.Origin.Y;
                bottomY = bottomY < edge.Twin.Origin.Y ? bottomY : edge.Twin.Origin.Y;
            }

            --leftX;
            ++rightX;
            ++topY;
            --bottomY;

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
        Trapezoid Location(Vertex p)
        {
            Node curNode = Root;
            while (!curNode.IsTrapezoid())
            {
                if (curNode.IsSegment())
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

    }
}