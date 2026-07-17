using System;
using System.Windows;

namespace ThinkGeo.MapSuite
{
    internal class MappingConverter
    {
        private const float hemLength = 30f;
        private float sourceWidth;
        private float sourceHeight;
        private float targetWidth;
        private float targetHeight;
        private float drawingWidth;
        private float drawingHeight;
        private Vertex upperLeftVertex;
        private Vertex pivot;
        private Point drawingUpperLeftPoint;
        private float unitWidth;

        public MappingConverter(Vertex upperLeft, Vertex pivot, float sourceWidth, float srcHeight, float destWidth, float destHeight)
        {
            this.sourceWidth = sourceWidth;
            this.sourceHeight = srcHeight;
            this.targetWidth = destWidth;
            this.targetHeight = destHeight;
            this.upperLeftVertex = upperLeft;
            this.pivot = pivot;
            this.drawingUpperLeftPoint = new Point(0d, 0d);
            this.unitWidth = 1f;

            SetDrawingLocation();
        }

        public Point GetMappingPoint(Vertex vertex)
        {
            // transform to pivot coordinate system
            float virtualX = vertex.X - pivot.X;
            float virtualY = vertex.Y - pivot.Y;

            // restore coordinate system
            double destX = (drawingWidth / sourceWidth) * virtualX + targetWidth / 2 + drawingUpperLeftPoint.X;
            double destY = (drawingHeight / sourceHeight) * virtualY + targetHeight / 2 + drawingUpperLeftPoint.Y;

            return new Point(destX, destY);
        }

        public float GetMappingDistance(Vertex v1, Vertex v2)
        {
            Point p1 = GetMappingPoint(v1);
            Point p2 = GetMappingPoint(v2);

            return (float)Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        public float GetPenWidth(int s52Width)
        {
            return s52Width * unitWidth;
        }

        private void SetDrawingLocation()
        {
            if (sourceWidth > targetWidth)
            {
                drawingWidth = targetWidth;
                drawingHeight = (drawingWidth / sourceWidth) * sourceHeight;

                if (drawingHeight > targetHeight)
                {
                    drawingWidth = (targetHeight / drawingHeight) * drawingWidth;
                    drawingHeight = targetHeight;
                }
            }
            else if (sourceHeight > targetHeight)
            {
                drawingWidth = (targetHeight / sourceHeight) * sourceWidth;
                drawingHeight = targetHeight;
            }
            else
            {
                float widthRatio = targetWidth / sourceWidth;
                float heightRatio = targetHeight / sourceHeight;

                if (heightRatio >= widthRatio)
                {
                    drawingWidth = targetWidth;
                    drawingHeight = (drawingWidth / sourceWidth) * sourceHeight;
                }
                else
                {
                    drawingWidth = (targetHeight / sourceHeight) * sourceWidth;
                    drawingHeight = targetHeight;
                }
            }

            drawingWidth -= hemLength * 2;
            drawingHeight -= hemLength * 2;

            // See P16 of
            //      IHO ECDIS
            // PRESENTATION LIBRARY
            //      USERS'MANUAL
            // each s52 coordinate unit represents 0.01 mm
            // and 1 unit of s52 pen width represents 0.3 mm
            unitWidth = GetMappingDistance(new Vertex(0, 0), new Vertex(0, 30));
        }
    }
}
