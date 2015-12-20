using ComputationalGeometry.Common;
using ComputationalGeometry.TrapezoidalMap;
using System;
using System.Collections.Generic;

namespace ComputationalGeometry.MotionPlanning
{
    class MockTrapezoidalMap : ITrapezoidalMap
    {
        private MockSegment[] segments;
        private MockVertex[] vertices;
        private MockTrapezoid[] trapezoids;
        private MockVerticalEdge[] verticals;

        public IEnumerable<ITrapezoid> Trapezoids
        {
            get
            {
                return trapezoids;
            }
        }

        public IEnumerable<ISegment> Segments
        {
            get
            {
                return segments;
            }
        }

        public ITrapezoid PointLocation(Vector2 point)
        {
            double x = point.x,
                   y = point.y;

            if (x < 0 || x > 4 || y < 0 || y > 4)
                throw new ArgumentException("Point is not inside the rectangle.");

            if (x < 1)
                return trapezoids[0];

            if (x > 3)
                return trapezoids[6];

            if (y < 1)
                return trapezoids[3];

            if (x < 2)
            {
                if (y > 2 * x - 1)
                    return trapezoids[1];

                return trapezoids[2];
            }

            if (y > 7 - 2 * x)
                return trapezoids[4];

            return trapezoids[5];
        }

        public MockTrapezoidalMap()
        {
            // separate a {(0, 0) (4, 4)} square with a {(1, 1) (3, 1) (2, 3)} triangle

            segments = new MockSegment[3];
            for (int i = 0; i < 3; i++)
                segments[i] = new MockSegment();

            vertices = new MockVertex[3];
            for (int i = 0; i < 3; i++)
                vertices[i] = new MockVertex();

            trapezoids = new MockTrapezoid[7];
            for (int i = 0; i < 7; i++)
                trapezoids[i] = new MockTrapezoid();

            verticals = new MockVerticalEdge[6];
            for (int i = 0; i < 6; i++)
                verticals[i] = new MockVerticalEdge();


            segments[0].bottoms = new[] { trapezoids[2] };
            segments[0].tops = new[] { trapezoids[1] };
            segments[0].left = vertices[0];
            segments[0].right = vertices[1];

            segments[1].bottoms = new[] { trapezoids[5] };
            segments[1].tops = new[] { trapezoids[4] };
            segments[1].left = vertices[1];
            segments[1].right = vertices[2];

            segments[2].bottoms = new[] { trapezoids[3] };
            segments[2].tops = new[] { trapezoids[2], trapezoids[5] };
            segments[2].left = vertices[0];
            segments[2].right = vertices[2];


            vertices[0].position = new Vector2(1, 1);
            vertices[0].upper = verticals[0];
            vertices[0].lower = verticals[1];

            vertices[1].position = new Vector2(2, 3);
            vertices[1].upper = verticals[2];
            vertices[1].lower = verticals[3];

            vertices[2].position = new Vector2(3, 1);
            vertices[2].upper = verticals[4];
            vertices[2].lower = verticals[5];


            trapezoids[0].top = null;
            trapezoids[0].bottom = null;

            trapezoids[1].top = null;
            trapezoids[1].bottom = segments[0];

            trapezoids[2].top = segments[0];
            trapezoids[2].bottom = segments[2];

            trapezoids[3].top = segments[2];
            trapezoids[3].bottom = null;

            trapezoids[4].top = null;
            trapezoids[4].bottom = segments[1];

            trapezoids[5].bottom = segments[1];
            trapezoids[5].top = segments[2];

            trapezoids[6].top = segments[2];
            trapezoids[6].bottom = null;


            verticals[0].vertex = vertices[0];
            verticals[0].left = trapezoids[0];
            verticals[0].right = trapezoids[1];

            verticals[1].vertex = vertices[0];
            verticals[1].left = trapezoids[0];
            verticals[1].right = trapezoids[3];

            verticals[2].vertex = vertices[1];
            verticals[2].left = trapezoids[1];
            verticals[2].right = trapezoids[4];

            verticals[3].vertex = vertices[1];
            verticals[3].left = trapezoids[2];
            verticals[3].right = trapezoids[5];

            verticals[4].vertex = vertices[2];
            verticals[4].left = trapezoids[4];
            verticals[4].right = trapezoids[6];

            verticals[5].vertex = vertices[2];
            verticals[5].left = trapezoids[3];
            verticals[5].right = trapezoids[6];
        }
    }
}
