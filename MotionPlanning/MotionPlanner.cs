using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.TrapezoidalMap;

namespace ComputationalGeometry.MotionPlanning
{
    public class MotionPlanner
    {
        // roadmap

        public MotionPlanner(ConvexPolygon robot, IEnumerable<ConvexPolygon> obstacles)
        {
            var cObstacles = new List<ConvexPolygon>();
            foreach (var obstacle in obstacles)
                cObstacles.Add(robot + obstacle);
        }

        private ITrapezoidalMap GenerateTrapezoidalMap()
        {
            throw new NotImplementedException();
        }
    }
}
