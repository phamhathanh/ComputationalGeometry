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
            throw new NotImplementedException();
        }
    }
}
