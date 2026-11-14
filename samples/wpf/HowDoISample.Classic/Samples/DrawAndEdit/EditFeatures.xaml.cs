using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw, edit and delete shapes with the map's TrackOverlay and EditOverlay, and log the events they raise.
    /// </summary>
    public partial class EditFeatures : IDisposable
    {
        private LayerOverlay _layerOverlay;
        private int _logIndex;
        private bool _initialized;

        public ObservableCollection<string> LogMessages { get; } = new ObservableCollection<string>();

        public EditFeatures()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            var demoPolygon = new Feature("POLYGON((-10778500 3915600,-10778500 3910000,-10774040 3910000,-10774040 3915600,-10778500 3915600))");
            var demoPoint = new Feature("POINT(-10773220 3913230)");
            var demoLine = new Feature("LINESTRING(-10780700 3916500, -10780700 3910040)");
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPolygon);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPoint);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoLine);
            Map.EditOverlay.CalculateAllControlPoints();

            // The layer the shapes are saved to when the mode leaves editing.
            var featureLayer = new InMemoryFeatureLayer();
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 4, true);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Blue, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add("featureLayer", featureLayer);
            Map.Overlays.Add("layerOverlay", _layerOverlay);

            Map.TrackOverlay.MouseMoved += TrackOverlay_MouseMoved;
            Map.EditOverlay.VertexMoved += EditOverlay_VertexMoved;

            Map.TrackOverlay.TrackStarted += (_, args) => AppendLog($"TrackOverlay.TrackStarted at ({args.StartedVertex.X:N0}, {args.StartedVertex.Y:N0})");
            Map.TrackOverlay.VertexAdded += (_, args) => AppendLog($"TrackOverlay.VertexAdded at ({args.AddedVertex.X:N0}, {args.AddedVertex.Y:N0})");
            Map.TrackOverlay.TrackEnded += (_, args) => AppendLog($"TrackOverlay.TrackEnded: {args.TrackShape?.GetType().Name}");
            Map.EditOverlay.FeatureDragged += (_, args) => AppendLog($"EditOverlay.FeatureDragged: {args.DraggedFeature?.GetShape()?.GetType().Name}");
            Map.EditOverlay.FeatureResized += (_, args) => AppendLog($"EditOverlay.FeatureResized: {args.ResizedFeature?.GetShape()?.GetType().Name}");
            Map.EditOverlay.FeatureRotated += (_, args) => AppendLog($"EditOverlay.FeatureRotated: {args.RotatedFeature?.GetShape()?.GetType().Name}");
            Map.EditOverlay.VertexAdded += (_, args) => AppendLog($"EditOverlay.VertexAdded at ({args.AddedVertex.X:N0}, {args.AddedVertex.Y:N0})");
            Map.EditOverlay.VertexMoved += (_, args) => AppendLog($"EditOverlay.VertexMoved to ({args.MovedVertex.X:N0}, {args.MovedVertex.Y:N0})");
            Map.EditOverlay.VertexRemoved += (_, args) => AppendLog($"EditOverlay.VertexRemoved at ({args.RemovedVertex.X:N0}, {args.RemovedVertex.Y:N0})");

            _ = Map.RefreshAsync();
        }

        private void AppendLog(string message)
        {
            LogMessages.Add($"{_logIndex++}: {message}");
            LogListBox.ScrollIntoView(LogMessages[LogMessages.Count - 1]);
        }

        private void ClearLog_Click(object sender, RoutedEventArgs e)
        {
            LogMessages.Clear();
        }

        private void EditOverlay_VertexMoved(object sender, VertexMovedEditInteractiveOverlayEventArgs e)
        {
            MeasureResult.Text = GetMeasureResult(e.AffectedFeature.GetShape());
        }

        private void TrackOverlay_MouseMoved(object sender, MouseMovedTrackInteractiveOverlayEventArgs e)
        {
            MeasureResult.Text = GetMeasureResult(e.AffectedFeature.GetShape());
        }

        private static string GetMeasureResult(BaseShape shape)
        {
            // With the map's projection the area and length are measured on the ground.
            var currentProjection = new Projection(Projection.GetSphericalMercatorProjString());
            switch (shape)
            {
                case AreaBaseShape polygon:
                    return polygon.GetArea(currentProjection, AreaUnit.SquareMiles).ToString("N2") + " square miles";
                case LineBaseShape line:
                    return line.GetLength(currentProjection, DistanceUnit.Mile).ToString("N2") + " miles";
                default:
                    return string.Empty;
            }
        }

        private void NavMode_Click(object sender, RoutedEventArgs e)
        {
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            foreach (var feature in Map.TrackOverlay.TrackShapeLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            foreach (var feature in Map.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            Map.TrackOverlay.TrackMode = TrackMode.None;

            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });
        }

        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.Point;
        }

        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.Line;
        }

        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void EditShape_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            foreach (var feature in Map.TrackOverlay.TrackShapeLayer.InternalFeatures)
                Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];
            foreach (var feature in featureLayer.InternalFeatures)
                Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            featureLayer.InternalFeatures.Clear();

            Map.TrackOverlay.TrackMode = TrackMode.None;

            // Puts every shape in edit mode: draws its handles and vertices.
            Map.EditOverlay.CalculateAllControlPoints();

            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });
        }

        private void EditShape_OnUnchecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized) return;

            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];
            foreach (var feature in Map.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });
        }

        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (DeleteShape.IsChecked != true) return;

            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            var closestFeatures = featureLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1, new Collection<string>(), 100, DistanceUnit.Meter);
            if (closestFeatures.Count <= 0) return;

            featureLayer.InternalFeatures.Remove(closestFeatures[0]);
            _ = Map.RefreshAsync(_layerOverlay);
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
