using ComputationalGeometry.Common;
using ComputationalGeometry.TrapezoidalMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry.MotionPlanning
{
    class RoadMap
    {
        private ITrapezoidalMap trapezoidalMap;
        private Dictionary<ITrapezoid, Junction> junctionByTrapezoid;

        public RoadMap(ITrapezoidalMap trapezoidalMap)
        {
            this.trapezoidalMap = trapezoidalMap;
            this.junctionByTrapezoid = new Dictionary<ITrapezoid, Junction>();

            foreach (var trapezoid in trapezoidalMap.Trapezoids)
            {
                var junction = CreateJunction(trapezoid);
            }
        }

        private Junction CreateJunction(ITrapezoid trapezoid)
        {
            throw new NotImplementedException();
        }

        private Vector2 GetCenterPoint(ITrapezoid trapezoid)
        {
            double leftEdgeX = trapezoid.LeftEdge.XPosition,
                rightEdgeX = trapezoid.RightEdge.XPosition,
                centerX = (leftEdgeX + rightEdgeX) / 2;

            throw new NotImplementedException();
        }

        public IEnumerable<Vector2> CalculatePath(Vector2 start, Vector2 goal)
        {
            ITrapezoid startingTrapezoid = trapezoidalMap.PointLocation(start),
                        finalTrapezoid = trapezoidalMap.PointLocation(goal);
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

            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                
                if (current == goal)
                {
                    var path = new Stack<Junction>();
                    do
                    {
                        path.Push(current);
                        current = previous[current];
                    }
                    while (current != start);
                    while (path.Count != 0)
                        yield return path.Pop();
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
