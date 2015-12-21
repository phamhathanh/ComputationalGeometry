using ComputationalGeometry.Common;
using ComputationalGeometry.TrapezoidalMap;
using System.Collections.Generic;
using System.Diagnostics;

namespace ComputationalGeometry.MotionPlanning
{
    class RoadMap
    {
        private ITrapezoidalMap trapezoidalMap;
        private HashSet<ITrapezoid> forbiddenSpace;
        private Dictionary<ITrapezoid, Junction> junctionByTrapezoid;

        public RoadMap(ITrapezoidalMap trapezoidalMap, HashSet<ITrapezoid> forbiddenSpace)
        {
            this.trapezoidalMap = trapezoidalMap;
            this.forbiddenSpace = forbiddenSpace;
            this.junctionByTrapezoid = new Dictionary<ITrapezoid, Junction>();

            foreach (var trapezoid in trapezoidalMap.Trapezoids)
            {
                if (forbiddenSpace.Contains(trapezoid))
                    continue;

                var position = GetCenterPoint(trapezoid);
                var junction = new Junction(position);
                junctionByTrapezoid.Add(trapezoid, junction);
            }

            foreach (var vertical in trapezoidalMap.VerticalEdges)
            {
                if (forbiddenSpace.Contains(vertical.LeftTrapezoid))
                {
                    Debug.Assert(forbiddenSpace.Contains(vertical.RightTrapezoid));
                    continue;
                }

                var position = GetCenterPoint(vertical);
                var junction = new Junction(position);

                var leftJunction = junctionByTrapezoid[vertical.LeftTrapezoid];
                Road.Connect(leftJunction, junction);
                var rightJunction = junctionByTrapezoid[vertical.RightTrapezoid];
                Road.Connect(rightJunction, junction);
            }
        }

        private Vector2 GetCenterPoint(ITrapezoid trapezoid)
        {
            double leftEdgeX = trapezoid.LeftBound,
                rightEdgeX = trapezoid.RightBound,
                centerX = (leftEdgeX + rightEdgeX) / 2;

            double topY, bottomY;
            ISegment top = trapezoid.TopSegment,
                bottom = trapezoid.BottomSegment;

            if (top == null)
                topY = trapezoidalMap.BoundingBox.top;
            else
                topY = GetYComponent(top, centerX);

            if (bottom == null)
                bottomY = trapezoidalMap.BoundingBox.bottom;
            else
                bottomY = GetYComponent(bottom, centerX);

            double centerY = (topY + bottomY) / 2;

            return new Vector2(centerX, centerY);
        }

        private Vector2 GetCenterPoint(IVerticalEdge vertical)
        {
            double topY, bottomY,
                centerX = vertical.XPosition;

            var vertex = vertical.Vertex;
            bool isLowerExtension = vertex.LowerExtension == vertical;
            if (isLowerExtension)
            {
                topY = vertex.Position.y;

                var bottom = vertical.LeftTrapezoid.BottomSegment;
                Debug.Assert(vertical.RightTrapezoid.BottomSegment == bottom);

                if (bottom == null)
                    bottomY = trapezoidalMap.BoundingBox.bottom;
                else
                    bottomY = GetYComponent(bottom, centerX);
            }
            else
            {
                bottomY = vertex.Position.y;

                var top = vertical.LeftTrapezoid.TopSegment;
                Debug.Assert(vertical.RightTrapezoid.TopSegment == top);

                if (top == null)
                    topY = trapezoidalMap.BoundingBox.top;
                else
                    topY = GetYComponent(top, centerX);
            }

            var centerY = (topY + bottomY) / 2;
            return new Vector2(centerX, centerY);
        }

        private double GetYComponent(ISegment segment, double xPosition)
        {
            Debug.Assert(segment != null);

            Vector2 left = segment.LeftVertex.Position,
                    right = segment.RightVertex.Position;

            return (xPosition - left.x) * (right.y - left.y) / (right.x - left.x) + left.y;
        }

        public IEnumerable<Vector2> CalculatePath(Vector2 start, Vector2 goal)
        {
            var box = trapezoidalMap.BoundingBox;
            if (!box.Contains(start) || !box.Contains(goal))
                yield break;

            ITrapezoid startingTrapezoid = trapezoidalMap.PointLocation(start),
                        finalTrapezoid = trapezoidalMap.PointLocation(goal);

            if (forbiddenSpace.Contains(startingTrapezoid) || forbiddenSpace.Contains(finalTrapezoid))
                yield break;

            Junction startingJunction = junctionByTrapezoid[startingTrapezoid],
                        finalJunction = junctionByTrapezoid[finalTrapezoid];

            yield return start;

            foreach (var junction in SearchForPath(startingJunction, finalJunction))
                yield return junction.Position;

            yield return goal;
        }

        private IEnumerable<Junction> SearchForPath(Junction start, Junction goal)
        {
            var previous = new Dictionary<Junction, Junction>();
            var queue = new Queue<Junction>();

            queue.Enqueue(start);
            previous.Add(start, null);

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                
                if (current == goal)
                {
                    var path = new Stack<Junction>();
                    while (current != null)
                    {
                        path.Push(current);
                        current = previous[current];
                    }
                    while (path.Count != 0)
                        yield return path.Pop();
                    yield break;
                    // find a way to just yield from bottom
                }

                foreach (var nextStop in current.Neighbors)
                {
                    var isVisited = previous.ContainsKey(nextStop);
                    if (isVisited)
                        continue;
                    
                    queue.Enqueue(nextStop);
                    previous.Add(nextStop, current);
                }
            }

            yield break;
        }
    }
}
