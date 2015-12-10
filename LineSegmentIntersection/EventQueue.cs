using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;

namespace LineSegmentIntersection
{
    public class EventQueue
    {
        private List<EventPoint> eventPoints;
        private int currentPointIndex;

        public EventQueue()
        {
            eventPoints = new List<EventPoint>();
            currentPointIndex = 0;
        }

        public EventPoint GetNextEvent()
        {
            EventPoint nextEvent = null;
            if (eventPoints.Count > currentPointIndex)
            {
                nextEvent = eventPoints[currentPointIndex];
                currentPointIndex++;
            }
            return nextEvent;
        }

        public int InsertEvent(Vertex newEventPoint)
        {
            eventPoints.Add(new EventPoint(newEventPoint));
            return eventPoints.Count;
        }
    }
}
