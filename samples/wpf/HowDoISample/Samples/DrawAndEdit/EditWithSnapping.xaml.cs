using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Drag a vertex of the line near a school and it snaps onto it.
    /// </summary>
    public partial class EditWithSnapping : UserControl
    {
        private bool _initialized;
        private const float Tolerance = 25;
        private ShapeFileFeatureLayer _schoolsLayer;

        public EditWithSnapping()
        {
            InitializeComponent();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;

            Map.MapUnit = GeographyUnit.Meter;

            var basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));
            Map.Basemap = basemap;

            _schoolsLayer = new ShapeFileFeatureLayer(SampleShared.Shapefile("Schools.shp"));
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
            var nearest = _schoolsLayer.QueryTools.GetFeaturesNearestTo(e.TargetVertex, GeographyUnit.Meter, 1, ReturningColumnsType.NoColumns);
            if (nearest.Count == 0 || nearest[0].GetShape() is not PointShape school) return;

            var screenDistance = MapUtil.GetScreenDistanceBetweenTwoWorldPoints(
                Map.CurrentExtent, school, e.TargetVertex,
                (float)Map.ActualWidth, (float)Map.ActualHeight);
            if (screenDistance >= Tolerance) return;

            e.TargetVertex.X = school.X;
            e.TargetVertex.Y = school.Y;
        }
    }

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
                // Draws the vertex.
                var pointShape = (PointShape)feature.GetShape();
                canvas.DrawEllipse(pointShape, 5, 5, new GeoSolidBrush(GeoColors.Black), DrawingLevel.LevelOne);

                // Draws the tolerance circle.
                var screenPointF = MapUtil.ToScreenCoordinate(canvas.CurrentWorldExtent, pointShape, canvas.Width, canvas.Height);
                canvas.DrawEllipse(screenPointF, Tolerance * 2, Tolerance * 2, new GeoPen(GeoColors.Black),
                    new GeoSolidBrush(), DrawingLevel.LevelFour, 0, 0, PenBrushDrawingOrder.PenFirst);
            }
        }
    }
}
