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
                             select reflection + obstacle;

            var trapezoidalMap = GenerateTrapezoidalMap(cObstacles);

            var topRightOfObstacles = from cObstacle in cObstacles
                                      from point in cObstacle.GetPointsAbove()
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

        private ITrapezoidalMap GenerateTrapezoidalMap(IEnumerable<ConvexPolygon> obstacles)
        {
            var edges = from obstacle in obstacles
                        from edge in obstacle.Edges
                        select edge;

            // generate bounding box
            throw new NotImplementedException();
        }
    }
}
