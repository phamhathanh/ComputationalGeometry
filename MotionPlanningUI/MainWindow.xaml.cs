using ComputationalGeometry.Common;
using ComputationalGeometry.MotionPlanning;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace MotionPlanningUI
{
    public partial class MainWindow : Window
    {
        private MotionPlanner planner;
        private double ratio;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += (s,e) => Init();
        }

        private void Init()
        {
            Vector2 v1 = new Vector2(1, 1), v2 = new Vector2(3, 1), v3 = new Vector2(2, 3);
            var vertices = new[] { v1, v2, v3 };
            var polygon1 = new SimplePolygon(vertices);


            planner = new MotionPlanner(new[] { polygon1 });

            Vector2 start = new Vector2(0.5, 0.5),
                    goal = new Vector2(3.5, 3.5);
            var path = planner.CalculatePath(start, goal);

            DrawPolygon(polygon1);
            DrawPath(path);
            DrawBox();

            ResizeToFitContent();
        }

        private Point PointFromVector(Vector2 vector)
        {
            return new Point(vector.X, vector.Y);
        }

        private void ResizeToFitContent()
        {
            if (planner == null)
                return;

            var box = planner.BoundingBox;
            var width = box.Width;
            var height = box.Height;

            var window = (Grid)Application.Current.MainWindow.Content;
            var windowWidth = window.ActualWidth;
            var windowHeight = window.ActualHeight;

            var xRatio = windowWidth / width;
            var yRatio = windowHeight / height;
            if (xRatio < yRatio)
                ratio = xRatio;
            else
                ratio = yRatio;
            
            RescaleCanvas();
        }

        private void DrawBox()
        {
            var box = planner.BoundingBox;
            var width = box.Width;
            var height = box.Height;

            var rectangle = new System.Windows.Shapes.Rectangle()
            {
                Fill = Brushes.Azure,
                Stroke = Brushes.Black,
                StrokeThickness = 2,
                Width = width,
                Height = height                
            };

            Canvas.SetZIndex(rectangle, -1);
            mainCanvas.Children.Add(rectangle);
        }

        private void DrawPolygon(SimplePolygon polygon)
        {
            var pointCollection = new PointCollection();
            foreach (var vertex in polygon.Vertices)
            {
                var position = vertex.Position;
                pointCollection.Add(new Point(position.X, position.Y));
            }

            var polygonImage = new Polygon()
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

        private void RescaleCanvas()
        {
            foreach (UIElement element in mainCanvas.Children)
            {
                if (element is Polygon)
                    ScalePolygon((Polygon)element);
                else if (element is Path)
                    ScalePath((Path)element);
                else if (element is System.Windows.Shapes.Rectangle)
                    ScaleBox((System.Windows.Shapes.Rectangle)element);
                else
                    throw new InvalidOperationException("Something is wrong.");
            }
        }

        private void ScaleBox(System.Windows.Shapes.Rectangle box)
        {
            box.Width *= ratio;
            box.Height *= ratio;
        }

        private void ScalePolygon(Polygon polygon)
        {
            var newPoints = new PointCollection();
            foreach (var point in polygon.Points)
                newPoints.Add(ScalePoint(point));
            polygon.Points = newPoints;
        }

        private void ScalePath(Path path)
        {
            var geometry = (PathGeometry)path.Data;
            var figure = geometry.Figures.First();
            figure.StartPoint = ScalePoint(figure.StartPoint);
            foreach (LineSegment segment in figure.Segments)
                segment.Point = ScalePoint(segment.Point);
        }

        private Point ScalePoint(Point original)
        {
            var newX = original.X * ratio;
            var newY = original.Y * ratio;
            return new Point(newX, newY);
        }
    }
}
