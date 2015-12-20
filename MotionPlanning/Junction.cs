using ComputationalGeometry.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry.MotionPlanning
{
    class Junction
    {
        private readonly Vector2 position;
        private List<Road> roads = new List<Road>();

        public Vector2 Position
        {
            get
            {
                return position;
            }
        }

        public IEnumerable<Junction> Neighbors
        {
            get
            {
                foreach (var road in roads)
                    yield return road.GetNextJunction(this);
            }
        }

        public Junction(Vector2 position)
        {
            this.position = position;
        }

        public void AddRoad(Road road)
        {
            roads.Add(road);
        }

    }
}
