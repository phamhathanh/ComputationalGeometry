using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComputationalGeometry.Common;
using ComputationalGeometry.MotionPlanning;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            MotionPlannerTest();

            Console.WriteLine("All tests were passed. OK.");
            Console.ReadLine();
        }

        private static void MotionPlannerTest()
        {
            MinkowskiTriangleSumTest();
            MinkowskiRectangleSumTest();
        }

        private static void MinkowskiTriangleSumTest()
        {
            Vertex v1 = new Vertex(0, -1), v2 = new Vertex(1, 0), v3 = new Vertex(0, 1),
                   w1 = new Vertex(1, -1), w2 = new Vertex(1, 1), w3 = new Vertex(0, 0),
                   u1 = new Vertex(1, -2), u2 = new Vertex(2, -1), u3 = new Vertex(2, 1),
                   u4 = new Vertex(1, 2), u5 = new Vertex(0, 1), u6 = new Vertex(0, -1);
            var polygon1 = new ConvexPolygon(new[] { v1, v2, v3 });
            var polygon2 = new ConvexPolygon(new[] { w1, w2, w3 });
            var polygon3 = new ConvexPolygon(new[] { u1, u2, u3, u4, u5, u6 });
            var sum = polygon1 + polygon2;
            Debug.Assert(sum.ToString() == polygon3.ToString());
        }

        private static void MinkowskiRectangleSumTest()
        {
            Vertex v1 = new Vertex(0, 0), v2 = new Vertex(1, 0),
                   v3 = new Vertex(1, 1), v4 = new Vertex(0, 1),
                   w1 = new Vertex(1, 1), w2 = new Vertex(2, 1),
                   w3 = new Vertex(2, 2), w4 = new Vertex(1, 2),
                   u1 = new Vertex(1, 1), u2 = new Vertex(3, 1),
                   u3 = new Vertex(3, 3), u4 = new Vertex(1, 3);
            var polygon1 = new ConvexPolygon(new[] { v1, v2, v3, v4 });
            var polygon2 = new ConvexPolygon(new[] { w1, w2, w3, w4 });
            var polygon3 = new ConvexPolygon(new[] { u1, u2, u3, u4 });
            var sum = polygon1 + polygon2;
            Debug.Assert(sum.ToString() == polygon3.ToString());
        }
    }
}
