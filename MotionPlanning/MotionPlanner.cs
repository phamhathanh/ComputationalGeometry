using System;
using System.Collections.Generic;
using System.Linq;
using ComputationalGeometry.TrapezoidalMap;
using ComputationalGeometry.Common;

namespace ComputationalGeometry.MotionPlanning
{
    public class MotionPlanner
    {
        private RoadMap roadMap;

        public MotionPlanner(ConvexPolygon robot, IEnumerable<ConvexPolygon> obstacles)
        {
            var reflection = robot.GetPointReflection(Vector2.Zero);
            var cObstacles = from obstacle in obstacles
                             select ConvexPolygon.MinkowskiSum(reflection, obstacle);
            var union = Union(cObstacles.ToArray());

            var trapezoidalMap = GenerateTrapezoidalMap(union);

            var topRightOfObstacles = from obstacle in union
                                      from point in obstacle.GetPointsAbove()
                                      select point;

            var forbiddenSpace = new HashSet<ITrapezoid>();
            foreach (var trapezoid in trapezoidalMap.Trapezoids)
            {
                var top = trapezoid.TopSegment;
                if (top == null)
                    continue;
                var topRight = top.RightVertex.Position;

                var trapezoidIsObstacle = topRightOfObstacles.Contains(topRight);
                if (trapezoidIsObstacle)
                    forbiddenSpace.Add(trapezoid);
            }

            this.roadMap = new RoadMap(trapezoidalMap, forbiddenSpace);
        }

        private ConvexPolygon[] Union(ConvexPolygon[] polygons)
        {
            if (polygons.Length < 1)
                return polygons;

            var output = new List<ConvexPolygon>();

            var queue = new Queue<ConvexPolygon>(polygons);
            while (queue.Count != 0)
            {
                var current = queue.Dequeue();
                bool isMerged = false;
                foreach (var polygon in output)
                {
                    if (!current.Overlaps(polygon))
                        continue;

                    current = current.UnionWith(polygon);
                    isMerged = true;

                    output.Remove(polygon);
                    break;
                }
                if (!isMerged)
                    output.Add(current);
                else
                    queue.Enqueue(current);
            }
            return output.ToArray();

            // NOT TESTED
        }

        private ITrapezoidalMap GenerateTrapezoidalMap(IEnumerable<ConvexPolygon> obstacles)
        {
            var edges = from obstacle in obstacles
                        from edge in obstacle.Edges
                        select edge;

            // generate bounding box
            // must handle empty case
            throw new NotImplementedException();
        }
    }
}
