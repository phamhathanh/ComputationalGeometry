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

        public Rectangle BoundingBox
        {
            get
            {
                return new Rectangle(0, 4, 0, 4);
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

            verticals = new MockVerticalEdge[3];
            for (int i = 0; i < 3; i++)
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
            vertices[0].extension = verticals[0];

            vertices[1].position = new Vector2(2, 3);
            vertices[1].extension = verticals[1];

            vertices[2].position = new Vector2(3, 1);
            vertices[2].extension = verticals[2];


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
            verticals[0].lefts = new[] { trapezoids[0] };
            verticals[0].rights = new[] { trapezoids[1], trapezoids[3] };

            verticals[1].vertex = vertices[0];
            verticals[1].lefts = new[] { trapezoids[1], trapezoids[2] };
            verticals[1].rights = new[] { trapezoids[4], trapezoids[5] };

            verticals[2].vertex = vertices[1];
            verticals[2].lefts = new[] { trapezoids[4], trapezoids[3] };
            verticals[2].rights = new[] { trapezoids[6] };
        }
    }
}
