using System.Collections.ObjectModel;
using ThinkGeo.MapSuite;
using ThinkGeo.MapSuite.Drawing;
using ThinkGeo.MapSuite.Layers;
using ThinkGeo.MapSuite.Shapes;
using ThinkGeo.MapSuite.Styles;
using ThinkGeo.MapSuite.Wpf;

namespace SnapToLayer
{
    public enum ToleranceCoordinates { World, Screen };

    class SnapToLayerEditInteractiveOverlay : EditInteractiveOverlay
    {
        private PointStyle controlPointStyle;
        private PointStyle draggedControlPointStyle;
        private InMemoryFeatureLayer toSnapInMemoryFeatureLayer;
        private float tolerance;
        private DistanceUnit toleranceUnit;
        private GeographyUnit geographyUnit;

        private RectangleShape currentWorldExtent;
        private float mapWidth;
        private float mapHeight;
        private ToleranceCoordinates toleranceType;

        //Property for the non dragged control point style.
        public PointStyle ControlPointStyle
        {
            get { return controlPointStyle; }
            set { controlPointStyle = value; }
        }

        //Property for the dragged control point style.
        public PointStyle DraggedControlPointStyle
        {
            get { return draggedControlPointStyle; }
            set { draggedControlPointStyle = value; }
        }

        //InMemoryFeatureLayer for the layer to be snapped to.
        public InMemoryFeatureLayer ToSnapInMemoryFeatureLayer
        {
            get { return toSnapInMemoryFeatureLayer; }
            set { toSnapInMemoryFeatureLayer = value; }
        }

        public ToleranceCoordinates ToleranceType
        {
            get { return toleranceType; }
            set { toleranceType = value; }
        }

        public float Tolerance
        {
            get { return tolerance; }
            set { tolerance = value; }
        }

        public DistanceUnit ToleranceUnit
        {
            get { return toleranceUnit; }
            set { toleranceUnit = value; }
        }


        protected override Feature MoveVertexCore(Feature sourceFeature, PointShape sourceControlPoint, PointShape targetControlPoint)
        {
            VertexMovingEditInteractiveOverlayEventArgs vertexMovingEditInteractiveOverlayEventArgs = new VertexMovingEditInteractiveOverlayEventArgs(false, sourceFeature, new Vertex(targetControlPoint));

            PointShape snapPointShape = null;
            if (toleranceType == ToleranceCoordinates.Screen)
            {
                snapPointShape = FindNearestSnappingPointPixel(targetControlPoint);
            }
            else
            {
                snapPointShape = FindNearestSnappingPoint(targetControlPoint);
            }

            if (snapPointShape != null)
            {
                vertexMovingEditInteractiveOverlayEventArgs.MovingVertex = new Vertex(snapPointShape);
            }

            return base.MoveVertexCore(sourceFeature, sourceControlPoint, new PointShape(vertexMovingEditInteractiveOverlayEventArgs.MovingVertex));
        }

        //Function to find if dragged control point is within the tolerance of a vertex of layer in screen (pixels) coordinates.
        private PointShape FindNearestSnappingPointPixel(PointShape targetPointShape)
        {
            toSnapInMemoryFeatureLayer.Open();
            Collection<Feature> toSnapInMemoryFeatures = toSnapInMemoryFeatureLayer.FeatureSource.GetFeaturesNearestTo(targetPointShape, GeographyUnit.Meter, 1, ReturningColumnsType.AllColumns);
            toSnapInMemoryFeatureLayer.Close();

            if (toSnapInMemoryFeatures.Count == 1)
            {
                MultipolygonShape multipolygonShape = (MultipolygonShape)toSnapInMemoryFeatures[0].GetShape();

                foreach (PolygonShape polygonShape in multipolygonShape.Polygons)
                {
                    foreach (Vertex vertex in polygonShape.OuterRing.Vertices)
                    {
                        PointShape toSnapPointShape = new PointShape(vertex);
                        float screenDistance = ExtentHelper.GetScreenDistanceBetweenTwoWorldPoints(currentWorldExtent, toSnapPointShape, targetPointShape, mapWidth, mapHeight);

                        if (screenDistance <= tolerance)
                        {
                            return new PointShape(toSnapPointShape.X, toSnapPointShape.Y);
                        }
                    }
                }
            }
            return null;
        }

        //Function to find if dragged control point is within the tolerance of a vertex of layer in world coordinates.
        private PointShape FindNearestSnappingPoint(PointShape targetPointShape)
        {
            toSnapInMemoryFeatureLayer.Open();
            Collection<Feature> toSnapInMemoryFeatures = toSnapInMemoryFeatureLayer.FeatureSource.GetFeaturesNearestTo(targetPointShape, GeographyUnit.Meter, 1, ReturningColumnsType.AllColumns);
            toSnapInMemoryFeatureLayer.Close();

            if (toSnapInMemoryFeatures.Count == 1)
            {
                PolygonShape polygonShape = (PolygonShape)toSnapInMemoryFeatures[0].GetShape();

                foreach (Vertex vertex in polygonShape.OuterRing.Vertices)
                {
                    PointShape toSnapPointShape = new PointShape(vertex);
                    double Distance = toSnapPointShape.GetDistanceTo(targetPointShape, geographyUnit, toleranceUnit);

                    if (Distance <= tolerance)
                    {
                        return new PointShape(toSnapPointShape.X, toSnapPointShape.Y);
                    }
                }
            }
            return null;
        }

        protected override void DrawTileCore(GeoCanvas geoCanvas)
        {
            //Sets the geography Unit used in FindNearestSnappingPoint function
            geographyUnit = geoCanvas.MapUnit;
            currentWorldExtent = geoCanvas.CurrentWorldExtent;
            mapWidth = geoCanvas.Width;
            mapHeight = geoCanvas.Height;

            //Draws the Edit Shapes as default.
            Collection<SimpleCandidate> labelsInAllLayers = new Collection<SimpleCandidate>();
            EditShapesLayer.Open();
            EditShapesLayer.Draw(geoCanvas, labelsInAllLayers);
            geoCanvas.Flush();

            //Draws the control points. 
            ExistingControlPointsLayer.Open();
            Collection<Feature> controlPoints = ExistingControlPointsLayer.FeatureSource.GetAllFeatures(ReturningColumnsType.AllColumns);

            //Loops thru the control points.
            foreach (Feature feature in controlPoints)
            {
                //Looks at the value not selected to draw the control point as not dragged.
                if (!SelectedControlPointLayer.InternalFeatures.Contains(feature))
                {
                    draggedControlPointStyle.Draw(new[] { feature }, geoCanvas, labelsInAllLayers, labelsInAllLayers);
                }
            }
            controlPointStyle.Draw(SelectedControlPointLayer.InternalFeatures, geoCanvas, labelsInAllLayers, labelsInAllLayers);
        }
    }
}
