using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ComputationalGeometry.Common;

namespace LineSegmentIntersection
{
    /*
     * @description Section 2: LineSegmentIntersection
     * @author Dao Tuan Anh
     */
    public class EventPoint
    {
        public Vertex Vertex { get; set; }
        public IEnumerable<Segment> CorrespondingSegments { get; set; }

        public EventPoint(Vertex vertex)
        {
            this.Vertex = vertex;
        }

        public EventPoint(Vertex vertex, IEnumerable<Segment> correspondingSegments)
        {
            this.Vertex = vertex;
            this.CorrespondingSegments = correspondingSegments;
        }
    }
}
