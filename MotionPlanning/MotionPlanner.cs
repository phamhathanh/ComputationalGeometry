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
        private Rectangle boundingBox;

        public Rectangle BoundingBox
        {
            get
            {
                return boundingBox;
            }
        }

        public MotionPlanner(IEnumerable<ConvexPolygon> obstacles, ConvexPolygon robot)
        {
            var reflection = robot.GetPointReflection(Vector2.Zero);
            var cObstacles = from obstacle in obstacles
                             select ConvexPolygon.MinkowskiSum(reflection, obstacle);
            var union = Union(cObstacles.ToArray());

            var trapezoidalMap = GenerateTrapezoidalMap(union);
            this.boundingBox = trapezoidalMap.BoundingBox;

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

        public MotionPlanner(IEnumerable<SimplePolygon> obstacles)
        {
            var trapezoidalMap = GenerateTrapezoidalMap(obstacles);
            this.boundingBox = trapezoidalMap.BoundingBox;

            var topRightOfObstacles = from obstacle in obstacles
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

                    current = (ConvexPolygon)current.UnionWith(polygon);
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

        private ITrapezoidalMap GenerateTrapezoidalMap(IEnumerable<SimplePolygon> obstacles)
        {
            var edges = from obstacle in obstacles
                        from edge in obstacle.Edges
                        select edge;

            var vertexByVector = new Dictionary<Vector2, TrapezoidalMap.Vertex>();
            foreach (var edge in edges)
            {
                vertexByVector.Add(edge.Origin.Position, new TrapezoidalMap.Vertex(edge.Origin.Position.X, edge.Origin.Position.Y));
            }

            var segments = from edge in edges
                           select new TrapezoidalMap.Segment(vertexByVector[edge.Origin.Position], vertexByVector[edge.End.Position]);

            TrapezoidalMap1 trapezoidalMap = new TrapezoidalMap1(segments.ToList());
            // must handle empty case
            return trapezoidalMap;
        }

        public IEnumerable<Vector2> CalculatePath(Vector2 start, Vector2 goal)
        {
            return roadMap.CalculatePath(start, goal);
        }
    }
}
