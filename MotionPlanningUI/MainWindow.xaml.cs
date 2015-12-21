using ComputationalGeometry.MotionPlanning;
using System.Text;
using System.Windows;
using System;
using System.Windows.Shapes;
using System.Windows.Media;
using ComputationalGeometry.Common;

namespace MotionPlanningUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DoStuffs();
        }

        private void DoStuffs()
        {
            Vector2 v0 = new Vector2(100, 150), w0 = new Vector2(600, 300), u0 = new Vector2(300, 500);
            Vector2 v1 = new Vector2(0, -1), v2 = new Vector2(1, 0), v3 = new Vector2(0, 1),
                    w1 = new Vector2(1, -1), w2 = new Vector2(1, 1), w3 = new Vector2(0, 0),
                    u1 = new Vector2(1, -2), u2 = new Vector2(2, -1), u3 = new Vector2(2, 1),
                    u4 = new Vector2(1, 2), u5 = new Vector2(0, 1), u6 = new Vector2(0, -1);
            var polygon1 = new ConvexPolygon(new[] { v0 + 100 * v1, v0 + 100 * v2, v0 + 100 * v3 });
            var polygon2 = new ConvexPolygon(new[] { w0 + 100 * w1, w0 + 100 * w2, w0 + 100 * w3 });
            var polygon3 = new ConvexPolygon(new[] { u0 + 100 * u1, u0 + 100 * u2, u0 + 100 * u3,
                                                     u0 + 100 * u4, u0 + 100 * u5, u0 + 100 * u6 });

            DrawPolygon(polygon1);
            DrawPolygon(polygon2);
            DrawPolygon(polygon3);
        }

        private void DrawPolygon(ConvexPolygon polygon)
        {
            var pointCollection = new PointCollection();
            foreach (var vertex in polygon.Vertices)
            {
                var position = vertex.Position;
                pointCollection.Add(new Point(position.X, position.Y));
            }

            var polygonImage = new System.Windows.Shapes.Polygon()
            {
                Fill = Brushes.Azure,
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Points = pointCollection
            };

            mainCanvas.Children.Add(polygonImage);
        }
    }
}
