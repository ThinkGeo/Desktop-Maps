using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ThinkGeo.MapSuite
{
    // Renders an S-52 symbol preview using WPF drawing (DrawingGroup -> RenderTargetBitmap).
    // Drawing calls are accumulated into a DrawingGroup and rasterized on GetBitmap().
    internal class S52ObjectsPresenter : IDisposable
    {
        private readonly int imageWidth;
        private readonly int imageHeight;
        private readonly DrawingGroup drawingGroup;

        public S52ObjectsPresenter(int imageWidth, int imageHeight)
        {
            this.imageWidth = imageWidth;
            this.imageHeight = imageHeight;
            this.drawingGroup = new DrawingGroup();
        }

        public void Clear(RGBColor backgroundColor)
        {
            drawingGroup.Children.Clear();
            Brush background = new SolidColorBrush(ToColor(backgroundColor));
            drawingGroup.Children.Add(new GeometryDrawing(background, null,
                new RectangleGeometry(new Rect(0, 0, imageWidth, imageHeight))));
        }

        public void Draw(S52Object drawingObject, RGBColor backgroundColor)
        {
            MappingConverter converter = new MappingConverter(drawingObject.UpperLeftVertex,
                drawingObject.PivotVertex,
                drawingObject.Width + drawingObject.UpperLeftVertex.X,
                drawingObject.Height + drawingObject.UpperLeftVertex.Y,
                imageWidth,
                imageHeight);

            DrawShapes(drawingObject.Shapes, backgroundColor, converter);
            DrawPivot(drawingObject.PivotVertex, converter);
        }

        public BitmapSource GetBitmap()
        {
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext dc = visual.RenderOpen())
            {
                dc.DrawDrawing(drawingGroup);
            }

            RenderTargetBitmap bitmap = new RenderTargetBitmap(imageWidth, imageHeight, 96, 96, PixelFormats.Pbgra32);
            bitmap.Render(visual);
            bitmap.Freeze();
            return bitmap;
        }

        private void DrawPivot(Vertex pivotVertex, MappingConverter converter)
        {
            Brush brush = new SolidColorBrush(Colors.Red);
            Pen pen = new Pen(new SolidColorBrush(Colors.DarkBlue), 2);
            Point point = converter.GetMappingPoint(pivotVertex);
            AddEllipse(point, 5d, brush, pen);
        }

        private void DrawShapes(Collection<DAIShape> shapes, RGBColor backgroundColor, MappingConverter converter)
        {
            foreach (DAIShape shape in shapes)
            {
                if (shape.ShapeType == DAIShapeType.Point)
                {
                    DrawPoint(shape as PointShape, converter);
                }
                else if (shape.ShapeType == DAIShapeType.Line)
                {
                    DrawLine(shape as LineShape, converter);
                }
                else if (shape.ShapeType == DAIShapeType.Circle)
                {
                    DrawCircle(shape as CircleShape, converter);
                }
                else if (shape.ShapeType == DAIShapeType.Area)
                {
                    DrawArea(shape as AreaShape, backgroundColor, converter);
                }
                else if (shape.ShapeType == DAIShapeType.Angle)
                {
                    // can not be supported
                }
            }
        }

        private void DrawPoint(PointShape shape, MappingConverter converter)
        {
            Brush brush = new SolidColorBrush(ToColor(shape.Color));
            Point point = converter.GetMappingPoint(new Vertex(shape.X, shape.Y));
            double radius = converter.GetPenWidth(shape.Width) / 2d;

            AddEllipse(point, radius, brush, null);
        }

        private void DrawLine(LineShape shape, MappingConverter converter)
        {
            Pen pen = new Pen(new SolidColorBrush(ToColor(shape.Color)), converter.GetPenWidth(shape.Width));
            List<Point> points = MapVertexes(shape.Vertexes, converter);

            if (shape.Vertexes[0].GeometryEqual(shape.Vertexes[shape.Vertexes.Count - 1]))
            {
                AddPolyline(points, pen, true, null);
            }
            else
            {
                AddPolyline(points, pen, false, null);
                DrawPoint(new PointShape(shape.Vertexes[0], shape.Width, shape.Color), converter);
                DrawPoint(new PointShape(shape.Vertexes[shape.Vertexes.Count - 1], shape.Width, shape.Color), converter);
            }
        }

        private void DrawCircle(CircleShape shape, MappingConverter converter)
        {
            Pen pen = new Pen(new SolidColorBrush(ToColor(shape.Color)), converter.GetPenWidth(shape.Width));
            Point center = converter.GetMappingPoint(shape.Center);
            double radius = converter.GetMappingDistance(shape.Center, new Vertex(shape.Center.X + shape.Raduis, shape.Center.Y));

            AddEllipse(center, radius, null, pen);
        }

        private void DrawArea(AreaShape shape, RGBColor backgroundColor, MappingConverter converter)
        {
            if (shape.FillPattern == AreaShapeFillPattern.Fill)
            {
                Color fillOuterColor = ToColor(shape.Color);
                Color fillInnerColor = ToColor(backgroundColor);

                foreach (RingShape ring in shape.OuterRings)
                {
                    FillRing(ring, fillOuterColor, converter);
                }

                foreach (RingShape ring in shape.InnerRings)
                {
                    FillRing(ring, fillInnerColor, converter);
                }
            }
            else
            {
                foreach (RingShape ring in shape.OuterRings)
                {
                    DrawRing(ring, converter);
                }

                foreach (RingShape ring in shape.InnerRings)
                {
                    DrawRing(ring, converter);
                }
            }
        }

        private void DrawRing(RingShape ring, MappingConverter converter)
        {
            foreach (DAIShape shape in ring.Shapes)
            {
                if (shape.ShapeType == DAIShapeType.Line)
                {
                    DrawLine(shape as LineShape, converter);
                }
                else if (shape.ShapeType == DAIShapeType.Circle)
                {
                    DrawCircle(shape as CircleShape, converter);
                }
                else if (shape.ShapeType == DAIShapeType.Angle)
                {
                    // TODO : can not be supported
                }
            }
        }

        private void FillRing(RingShape ring, Color fillColor, MappingConverter converter)
        {
            foreach (DAIShape shape in ring.Shapes)
            {
                if (shape.ShapeType == DAIShapeType.Line)
                {
                    FillLineShape(shape as LineShape, fillColor, converter);
                }
                else if (shape.ShapeType == DAIShapeType.Circle)
                {
                    FillCircleShape(shape as CircleShape, fillColor, converter);
                }
                else if (shape.ShapeType == DAIShapeType.Angle)
                {
                    // TODO : can not be supported
                }
            }
        }

        private void FillLineShape(LineShape shape, Color fillColor, MappingConverter converter)
        {
            Brush brush = new SolidColorBrush(fillColor);
            List<Point> points = MapVertexes(shape.Vertexes, converter);
            AddPolyline(points, null, true, brush);
        }

        private void FillCircleShape(CircleShape shape, Color fillColor, MappingConverter converter)
        {
            Brush brush = new SolidColorBrush(fillColor);
            Point center = converter.GetMappingPoint(shape.Center);
            double radius = converter.GetMappingDistance(shape.Center, new Vertex(shape.Center.X + shape.Raduis, shape.Center.Y));

            AddEllipse(center, radius, brush, null);
        }

        private static List<Point> MapVertexes(Collection<Vertex> vertexes, MappingConverter converter)
        {
            List<Point> points = new List<Point>(vertexes.Count);
            foreach (Vertex vertex in vertexes)
            {
                points.Add(converter.GetMappingPoint(vertex));
            }
            return points;
        }

        private void AddEllipse(Point center, double radius, Brush fill, Pen pen)
        {
            if (radius <= 0) radius = 0.5d;
            drawingGroup.Children.Add(new GeometryDrawing(fill, pen, new EllipseGeometry(center, radius, radius)));
        }

        // Adds an accumulated poly-figure. closed=true closes the figure; fill!=null fills it;
        // pen!=null strokes it -- covering GDI+ DrawPolygon / DrawLines / FillPolygon.
        private void AddPolyline(List<Point> points, Pen pen, bool closed, Brush fill)
        {
            if (points.Count == 0) return;

            StreamGeometry geometry = new StreamGeometry();
            using (StreamGeometryContext ctx = geometry.Open())
            {
                ctx.BeginFigure(points[0], fill != null, closed);
                if (points.Count > 1)
                {
                    ctx.PolyLineTo(points.GetRange(1, points.Count - 1), pen != null, false);
                }
            }
            geometry.Freeze();
            drawingGroup.Children.Add(new GeometryDrawing(fill, pen, geometry));
        }

        private static Color ToColor(RGBColor color)
        {
            return Color.FromRgb((byte)color.R, (byte)color.G, (byte)color.B);
        }

        public void Close()
        {
            drawingGroup.Children.Clear();
        }

        public void Dispose()
        {
            Close();
        }
    }
}
