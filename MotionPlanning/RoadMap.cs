using ComputationalGeometry.Common;
using ComputationalGeometry.TrapezoidalMap;
using System.Collections.Generic;

namespace ComputationalGeometry.MotionPlanning
{
    class RoadMap
    {
        private ITrapezoidalMap trapezoidalMap;
        private HashSet<ITrapezoid> obstacles;
        private Dictionary<ITrapezoid, Junction> junctionByTrapezoid;

        public RoadMap(ITrapezoidalMap trapezoidalMap, HashSet<ITrapezoid> obstacles)
        {
            this.trapezoidalMap = trapezoidalMap;
            this.obstacles = obstacles;
            this.junctionByTrapezoid = new Dictionary<ITrapezoid, Junction>();

            foreach (var trapezoid in trapezoidalMap.Trapezoids)
            {
                var junction = CreateJunction(trapezoid);
                junctionByTrapezoid.Add(trapezoid, junction);
            }

            foreach (var vertex in trapezoidalMap.Vertices)
            {

            }
        }

        private Junction CreateJunction(ITrapezoid trapezoid)
        {
            var position = GetCenterPoint(trapezoid);
            var junction = new Junction(position);

            return junction;
        }

        private Vector2 GetCenterPoint(ITrapezoid trapezoid)
        {
            double leftEdgeX = trapezoid.LeftBound,
                rightEdgeX = trapezoid.RightBound,
                centerX = (leftEdgeX + rightEdgeX) / 2;

            ISegment top = trapezoid.TopSegment,
                bottom = trapezoid.BottomSegment;
            double centerY = (GetYComponent(top, centerX) + GetYComponent(bottom, centerX)) / 2;

            return new Vector2(centerX, centerY);
        }

        private double GetYComponent(ISegment segment, double xPosition)
        {
            Vector2 left = segment.LeftVertex.Position,
                    right = segment.RightVertex.Position;

            return (xPosition - left.x) * (right.y - left.y) / (right.x - left.x);
        }

        public IEnumerable<Vector2> CalculatePath(Vector2 start, Vector2 goal)
        {
            var box = trapezoidalMap.BoundingBox;
            if (!box.Contains(start) || !box.Contains(goal))
                yield break;

            ITrapezoid startingTrapezoid = trapezoidalMap.PointLocation(start),
                        finalTrapezoid = trapezoidalMap.PointLocation(goal);

            if (obstacles.Contains(startingTrapezoid) || obstacles.Contains(finalTrapezoid))
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
