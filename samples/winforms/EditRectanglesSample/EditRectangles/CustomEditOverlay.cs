using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.WinForms;

namespace EditingRectangles
{
    class CustomEditInteractiveOverlay : EditInteractiveOverlay
    {
        public CustomEditInteractiveOverlay()
            : base()
        {
        }

        protected override Feature AddVertexCore(Feature targetFeature, PointShape targetPointShape, double searchingTolerance)
        {
            // Override the base method and disable the function of AddVertex if the shape is the "custom"
            if (targetFeature.ColumnValues.ContainsKey("Edit") && targetFeature.ColumnValues["Edit"] == "rectangle")
            {
                return new Feature();
            }

            return base.AddVertexCore(targetFeature, targetPointShape, searchingTolerance);
        }

        protected override Feature RemoveVertexCore(Feature editShapeFeature, Vertex selectedVertex, double searchingTolerance)
        {
            // Override the base method and disable the function of RemoveVertex if the shape is the "custom"
            if (editShapeFeature.ColumnValues.ContainsKey("Edit") && editShapeFeature.ColumnValues["Edit"] == "rectangle")
            {
                return new Feature();
            }

            return base.RemoveVertexCore(editShapeFeature, selectedVertex, searchingTolerance);
        }

        protected override IEnumerable<Feature> CalculateResizeControlPointsCore(Feature feature)
        {
            // Override the base method and modify the control points for resizing if the shape is the "custom"
            if (feature.ColumnValues.ContainsKey("Edit") && feature.ColumnValues["Edit"] == "rectangle")
            {
                Collection<Feature> resizeControlPoints = new Collection<Feature>();

                PolygonShape polygonShape = feature.GetShape() as PolygonShape;
                if (polygonShape != null)
                {
                    foreach (Vertex vertex in polygonShape.OuterRing.Vertices)
                    {
                        resizeControlPoints.Add(new Feature(vertex, feature.Id));
                    }
                }
                return resizeControlPoints;
            }

            return base.CalculateResizeControlPointsCore(feature);
        }

        protected override Feature ResizeFeatureCore(Feature sourceFeature, PointShape sourceControlPoint, PointShape targetControlPoint)
        {
            // Override the base method and modify the logic for resizing if the shape is the "custom"
            if (sourceFeature.ColumnValues.ContainsKey("Edit") && sourceFeature.ColumnValues["Edit"] == "rectangle")
            {
                PolygonShape polygonShape = sourceFeature.GetShape() as PolygonShape;

                if (polygonShape != null)
                {
                    // If the rectangle is horizontal or vertical, it will use the custom method.
                    if (string.Equals(polygonShape.GetBoundingBox().GetWellKnownText(), polygonShape.GetWellKnownText()))
                    {
                        int fixedPointIndex = GetFixedPointIndex(polygonShape, sourceControlPoint);

                        PointShape fixedPointShape = new PointShape(polygonShape.OuterRing.Vertices[fixedPointIndex]);

                        RectangleShape newRectangleShape = new LineShape(new Vertex[] { new Vertex(fixedPointShape), new Vertex(targetControlPoint) }).GetBoundingBox();

                        return new Feature(newRectangleShape.GetWellKnownBinary(), sourceFeature.Id, sourceFeature.ColumnValues);
                    }
                }
            }

            return base.ResizeFeatureCore(sourceFeature, sourceControlPoint, targetControlPoint);
        }

        private int GetFixedPointIndex(PolygonShape sourcePolygonShape, PointShape sourceControlPointShape)
        {
            int index = 0;
            for (int i = 0; i < sourcePolygonShape.OuterRing.Vertices.Count; i++)
            {
                Vertex vertex = sourcePolygonShape.OuterRing.Vertices[i];
                if (Math.Abs(vertex.X - sourceControlPointShape.X) <= 10E-6 && Math.Abs(vertex.Y - sourceControlPointShape.Y) <= 10E-6)
                {
                    index = i;
                    break;
                }
            }
            int fixedPointIndex = 0;
            if (index <= 2)
            {
                fixedPointIndex = index + 2;
            }
            else
            {
                fixedPointIndex = index - 2;
            }

            return fixedPointIndex;
        }
    }
}
