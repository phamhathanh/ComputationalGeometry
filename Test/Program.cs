using System;
using ComputationalGeometry.Common;
using ComputationalGeometry.MotionPlanning;
using System.Diagnostics;
using System.Collections.Generic;
using ComputationalGeometry.TrapezoidalMap;
using System.Linq;

namespace ComputationalGeometry.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MotionPlannerTest();

            Console.WriteLine("OK, all tests were passed.");
            Console.ReadLine();
        }

        private static void MotionPlannerTest()
        {
            MinkowskiTriangleSumTest();
            MinkowskiRectangleSumTest();
            RoadMapTest();
            UnionTest();
        }

        private static void MinkowskiTriangleSumTest()
        {
            Vector2 v1 = new Vector2(0, -1), v2 = new Vector2(1, 0), v3 = new Vector2(0, 1),
                    w1 = new Vector2(1, -1), w2 = new Vector2(1, 1), w3 = new Vector2(0, 0),
                    u1 = new Vector2(1, -2), u2 = new Vector2(2, -1), u3 = new Vector2(2, 1),
                    u4 = new Vector2(1, 2), u5 = new Vector2(0, 1), u6 = new Vector2(0, -1);
            var polygon1 = new ConvexPolygon(new[] { v1, v2, v3 });
            var polygon2 = new ConvexPolygon(new[] { w1, w2, w3 });
            var polygon3 = new ConvexPolygon(new[] { u1, u2, u3, u4, u5, u6 });
            var sum = ConvexPolygon.MinkowskiSum(polygon1, polygon2);
            Debug.Assert(sum.ToString() == polygon3.ToString());

            Debug.Assert(polygon1.ContainsPoint(new Vector2(0.5, 0.25)));
            Debug.Assert(!polygon1.ContainsPoint(new Vector2(2, 4)));
            Debug.Assert(!polygon1.ContainsPoint(v1));
            Debug.Assert(!polygon1.ContainsPoint((v1 + v2)/2));
        }

        private static void MinkowskiRectangleSumTest()
        {
            Vector2 v1 = new Vector2(0, 0), v2 = new Vector2(1, 0),
                    v3 = new Vector2(1, 1), v4 = new Vector2(0, 1),
                    w1 = new Vector2(1, 1), w2 = new Vector2(2, 1),
                    w3 = new Vector2(2, 2), w4 = new Vector2(1, 2),
                    u1 = new Vector2(1, 1), u2 = new Vector2(3, 1),
                    u3 = new Vector2(3, 3), u4 = new Vector2(1, 3);
            var polygon1 = new ConvexPolygon(new[] { v1, v2, v3, v4 });
            var polygon2 = new ConvexPolygon(new[] { w1, w2, w3, w4 });
            var polygon3 = new ConvexPolygon(new[] { u1, u2, u3, u4 });
            var sum = ConvexPolygon.MinkowskiSum(polygon1, polygon2);
            Debug.Assert(sum.ToString() == polygon3.ToString());
        }

        private static void RoadMapTest()
        {
            var map = new MockTrapezoidalMap();
            var forbidden = new HashSet<ITrapezoid>();
            foreach (var trapezoid in map.Trapezoids)
            {
                var lefts = trapezoid.LeftEdges;
                var rights = trapezoid.RightEdges;
                if (lefts != null && lefts.Count() == 0 || rights != null && rights.Count() == 0)
                    forbidden.Add(trapezoid);
            }

            var roadMap = new RoadMap(map, forbidden);
            var start = new Vector2(0.5, 0.5);
            var goal = new Vector2(3.5, 3.5);
            var path = roadMap.CalculatePath(start, goal);
            string result = "";
            foreach (var point in path)
                result += point.ToString() + " ";



            string correct = "(0.5, 0.5) (0.5, 2) (1, 0.5) (2, 0.5) (3, 0.5) (3.5, 2) (3.5, 3.5) ";
            Debug.Assert(result == correct);
        }

        private static void UnionTest()
        {
            Vector2 v1 = new Vector2(0, -1), v2 = new Vector2(1, 0), v3 = new Vector2(0, 1),
                    w1 = new Vector2(1, -1), w2 = new Vector2(1, 1), w3 = new Vector2(0, 0),
                    u1 = new Vector2(1, -2), u2 = new Vector2(2, -1), u3 = new Vector2(2, 1),
                    u4 = new Vector2(1, 2), u5 = new Vector2(0, 1), u6 = new Vector2(0, -1);
            var polygon1 = new ConvexPolygon(new[] { v1, v2, v3 });
            var polygon2 = new ConvexPolygon(new[] { w1, w2, w3 });
            var polygon3 = new ConvexPolygon(new[] { u1, u2, u3, u4, u5, u6 });
            var sum = polygon1.UnionWith(polygon2);
            Console.WriteLine(sum.ToString());
        }
    }
}
