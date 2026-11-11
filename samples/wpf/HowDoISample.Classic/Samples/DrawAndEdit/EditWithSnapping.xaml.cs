using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Drag a line's vertex near a school and it snaps onto the school's point.
    /// </summary>
    public partial class EditWithSnapping : IDisposable
    {
        private const float Tolerance = 25;
        private ShapeFileFeatureLayer _schoolsLayer;
        private bool _initialized;

        public EditWithSnapping()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            _schoolsLayer = new ShapeFileFeatureLayer(Path.Combine(AppContext.BaseDirectory, "Data", "Shapefile", "Schools.shp"));
            _schoolsLayer.ZoomLevelSet.ZoomLevel01.CustomStyles.Add(new TolerancePointStyle(Tolerance));
            _schoolsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;
            _schoolsLayer.FeatureSource.ProjectionConverter = new ProjectionConverter(2276, 3857);

            var schoolsOverlay = new LayerOverlay();
            schoolsOverlay.Layers.Add(_schoolsLayer);
            Map.Overlays.Add(schoolsOverlay);

            Map.EditOverlay.VertexMoving += EditOverlay_VertexMoving;

            var lineShape = new LineShape();
            lineShape.Vertices.Add(new Vertex(-10783003, 3918370));
            lineShape.Vertices.Add(new Vertex(-10783070, 3917335));
            lineShape.Vertices.Add(new Vertex(-10781292, 3916438));
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(new Feature(lineShape));

            Map.EditOverlay.CalculateAllControlPoints();

            Map.CenterPoint = new PointShape(-10782870, 3917470);
            Map.CurrentScale = 30000;

            _ = Map.RefreshAsync();
        }

        private void EditOverlay_VertexMoving(object sender, VertexMovingEditInteractiveOverlayEventArgs e)
        {
            var nearestFeatures = _schoolsLayer.QueryTools.GetFeaturesNearestTo(e.TargetVertex, GeographyUnit.Meter, 1, ReturningColumnsType.NoColumns);
            if (nearestFeatures.Count == 0) return;

            if (!(nearestFeatures[0].GetShape() is PointShape school)) return;

            var screenDistance = MapUtil.GetScreenDistanceBetweenTwoWorldPoints(
                Map.CurrentExtent, school, e.TargetVertex,
                (float)Map.ActualWidth, (float)Map.ActualHeight);
            if (screenDistance >= Tolerance) return;

            e.TargetVertex.X = school.X;
            e.TargetVertex.Y = school.Y;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    /// <summary>
    /// Draws each point with a circle around it the size of the snapping tolerance.
    /// </summary>
    internal class TolerancePointStyle : PointStyle
    {
        public float Tolerance { get; set; }

        public TolerancePointStyle(float tolerance)
        {
            Tolerance = tolerance;
        }

        protected override void DrawCore(IEnumerable<Feature> features, GeoCanvas canvas,
            Collection<SimpleCandidate> labelsInThisLayer, Collection<SimpleCandidate> labelsInAllLayers)
        {
            foreach (var feature in features)
            {
                var pointShape = (PointShape)feature.GetShape();
                canvas.DrawEllipse(pointShape, 5, 5, new GeoSolidBrush(GeoColors.Black), DrawingLevel.LevelOne);

                var screenPointF = MapUtil.ToScreenCoordinate(canvas.CurrentWorldExtent, pointShape, canvas.Width, canvas.Height);
                canvas.DrawEllipse(screenPointF, Tolerance * 2, Tolerance * 2, new GeoPen(GeoColors.Black),
                    new GeoSolidBrush(), DrawingLevel.LevelFour, 0, 0, PenBrushDrawingOrder.PenFirst);
            }
        }
    }
}
