using ComputationalGeometry.MotionPlanning;
using System.Windows;
using System;
using System.Windows.Shapes;
using System.Windows.Media;
using ComputationalGeometry.Common;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace MotionPlanningUI
{
    public partial class MainWindow : Window
    {
        private Vector2 reference;

        public MainWindow()
        {
            InitializeComponent();

            reference = new Vector2(Canvas.GetLeft(robot), Canvas.GetTop(robot));

            DoStuffs();
        }

        private Vector2 Reference
        {
            get
            {
                return reference;
            }
            set
            {
                reference = value;
                Canvas.SetLeft(robot, value.X);
                Canvas.SetTop(robot, value.Y);
            }
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

            DrawPath(new[] { v0 + 125 * v1, w0 + 200 * w1, u0 + 150 * u1, v0 + 300 * v2 });

            Reference = new Vector2(100, 300);
            MoveRobotToPoint(new Vector2(500, 700));
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
                Fill = Brushes.Pink,
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Points = pointCollection
            };

            mainCanvas.Children.Add(polygonImage);
        }

        private void DrawPath(IEnumerable<Vector2> path)
        {
            if (!path.Any())
                throw new ArgumentException("Path cannot be empty.");

            var figure = new PathFigure();

            var start = path.First();
            figure.StartPoint = PointFromVector(start);

            foreach (var vector in path.Skip(1))
            {
                var point = PointFromVector(vector);
                figure.Segments.Add(new LineSegment(point, true));
            }

            var geometry = new PathGeometry();
            geometry.Figures.Add(figure);

            var pathImage = new Path()
            {
                Stroke = Brushes.Black,
                StrokeThickness = 3,
                Data = geometry
            };

            mainCanvas.Children.Add(pathImage);
        }

        private Point PointFromVector(Vector2 vector)
        {
            return new Point(vector.X, vector.Y);
        }

        private void MoveRobotToPoint(Vector2 goal)
        {
            const double speed = 75;

            var length = (goal - reference).Length;
            var duration = new Duration(TimeSpan.FromSeconds(length / speed));
            
            DoubleAnimation xAnimation = new DoubleAnimation(),
                            yAnimation = new DoubleAnimation();

            Timeline.SetDesiredFrameRate(xAnimation, 300);
            Timeline.SetDesiredFrameRate(yAnimation, 300);

            xAnimation.Duration = duration;
            yAnimation.Duration = duration;

            var storyBoard = new Storyboard();
            storyBoard.Duration = duration;

            storyBoard.Children.Add(xAnimation);
            storyBoard.Children.Add(yAnimation);

            Storyboard.SetTarget(xAnimation, robot);
            Storyboard.SetTarget(yAnimation, robot);
            
            Storyboard.SetTargetProperty(xAnimation, new PropertyPath("(Canvas.Left)"));
            Storyboard.SetTargetProperty(yAnimation, new PropertyPath("(Canvas.Top)"));

            xAnimation.To = goal.X;
            yAnimation.To = goal.Y;

            robot.BeginAnimation(Canvas.LeftProperty, xAnimation);
            robot.BeginAnimation(Canvas.TopProperty, yAnimation);

            storyBoard.Begin();
        }
    }
}
