using System;

namespace ComputationalGeometry.MotionPlanning
{
    class Road
    {
        private readonly Junction junction1, junction2;

        public Road(Junction junction1, Junction junction2)
        {
            this.junction1 = junction1;
            this.junction2 = junction2;

            junction1.AddRoad(this);
            junction2.AddRoad(this);
        }

        public Junction GetNextJunction(Junction starting)
        {
            if (starting == junction1)
                return junction2;
            if (starting == junction2)
                return junction1;

            throw new ArgumentException("Starting junction is not one of the two end points.");
        }
    }
}
