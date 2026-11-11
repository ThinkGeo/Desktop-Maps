using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Draw, edit and delete shapes with the map's TrackOverlay and EditOverlay, and log the events they raise.
    /// </summary>
    public partial class EditFeatures
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

            var basemap = new GpuBasemap(new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) }));
            Map.Basemap = basemap;

            Map.CenterPoint = new PointShape(-10778000, 3912000);
            Map.CurrentScale = 77000;

            var demoPolygon = new Feature("POLYGON((-10778500 3915600,-10778500 3910000,-10774040 3910000,-10774040 3915600,-10778500 3915600))");
            var demoPoint = new Feature("POINT(-10773220 3913230)");
            var demoLine = new Feature("LINESTRING(-10780700 3916500, -10780700 3910040)");
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPolygon);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPoint);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoLine);
            Map.EditOverlay.CalculateAllControlPoints();

            // Create the layer that will store the drawn shapes
            var featureLayer = new InMemoryFeatureLayer();

            // Add styles for the layer
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 4, true);
            featureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Blue, GeoColors.Black);
            featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            // Add the layer to a LayerOverlay
            _layerOverlay = new LayerOverlay();
            _layerOverlay.Layers.Add("featureLayer", featureLayer);

            // Add the LayerOverlay to the map
            Map.Overlays.Add("layerOverlay", _layerOverlay);
            Map.TrackOverlay.MouseMoved += TrackOverlay_MouseMoved;
            Map.EditOverlay.VertexMoved += EditOverlay_VertexMoved;

            Map.TrackOverlay.TrackStarted += (_, args) => AppendLog($"TrackOverlay.TrackStarted at ({args.StartedVertex.X:N0}, {args.StartedVertex.Y:N0})");
            Map.TrackOverlay.VertexAdded += (_, args) => AppendLog($"TrackOverlay.VertexAdded at ({args.AddedVertex.X:N0}, {args.AddedVertex.Y:N0})");
            Map.TrackOverlay.TrackEnded += (_, args) => AppendLog($"TrackOverlay.TrackEnded: {args.TrackShape.GetType().Name}");
            Map.EditOverlay.FeatureDragged += (_, args) => AppendLog($"EditOverlay.FeatureDragged: {args.DraggedFeature.GetShape().GetType().Name}");
            Map.EditOverlay.FeatureResized += (_, args) => AppendLog($"EditOverlay.FeatureResized: {args.ResizedFeature.GetShape().GetType().Name}");
            Map.EditOverlay.FeatureRotated += (_, args) => AppendLog($"EditOverlay.FeatureRotated: {args.RotatedFeature.GetShape().GetType().Name}");
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
            var shape = e.AffectedFeature.GetShape();
            MeasureResult.Text = GetMeasureResult(shape);
        }

        private void TrackOverlay_MouseMoved(object sender, MouseMovedTrackInteractiveOverlayEventArgs e)
        {
            var shape = e.AffectedFeature.GetShape();
            MeasureResult.Text = GetMeasureResult(shape);
        }

        private static string GetMeasureResult(BaseShape shape)
        {
            // the background map is in SphericalMercator
            var currentProjection = new Projection(Projection.GetSphericalMercatorProjString());
            var measureResult = string.Empty;
            switch (shape)
            {
                case AreaBaseShape polygon:
                    // pass in the projection to get the accurate area
                    measureResult = polygon.GetArea(currentProjection, AreaUnit.SquareMiles).ToString("N2");
                    measureResult += " square miles";
                    break;
                case LineBaseShape line:
                    // pass in the projection to get the accurate length
                    measureResult = line.GetLength(currentProjection, DistanceUnit.Mile).ToString("N2");
                    measureResult += " miles";
                    break;
            }
            return measureResult;
        }

        /// <summary>
        /// Save the shapes to the layer and return the mouse to panning and zooming.
        /// </summary>
        private void NavMode_Click(object sender, RoutedEventArgs e)
        {
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Move all TrackOverlay features to LayerOverlay
            foreach (var feature in Map.TrackOverlay.TrackShapeLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Move all EditOverlay features to LayerOverlay and clear EditOverlay
            foreach (var feature in Map.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Set TrackMode to None, so that the user will no longer draw shapes and will be able to navigate the map normally
            Map.TrackOverlay.TrackMode = TrackMode.None;

            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });
        }

        /// <summary>
        /// Set the mode to draw points on the map
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Point, which draws a new point on the map on mouse click
            Map.TrackOverlay.TrackMode = TrackMode.Point;
        }

        /// <summary>
        /// Set the mode to draw lines on the map
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Line, which draws a new line on the map on mouse click. Double click to finish drawing the line.
            Map.TrackOverlay.TrackMode = TrackMode.Line;
        }

        /// <summary>
        /// Set the mode to draw polygons on the map
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Polygon, which draws a new polygon on the map on mouse click. Double click to finish drawing the polygon.
            Map.TrackOverlay.TrackMode = TrackMode.Polygon;
        }

        private void EditShape_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            // Move all TrackOverlay features to EditOverlay
            foreach (var feature in Map.TrackOverlay.TrackShapeLayer.InternalFeatures)
                Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Move all layerOverlay features to EditOverlay
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];
            foreach (var feature in featureLayer.InternalFeatures)
                Map.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            featureLayer.InternalFeatures.Clear();

            // Set TrackMode to None, so that the user will no longer draw shapes
            Map.TrackOverlay.TrackMode = TrackMode.None;

            // This method draws all the handles and manipulation points on the map to edit. Essentially putting them all in edit mode.
            Map.EditOverlay.CalculateAllControlPoints();

            // Refresh the map so that the features properly show that they are in edit mode
            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });
        }

        private void EditShape_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Move all EditOverlay features to LayerOverlay
            foreach (var feature in Map.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            Map.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Refresh the overlays to show latest results
            _ = Map.RefreshAsync(new Overlay[] { Map.TrackOverlay, Map.EditOverlay, _layerOverlay });
        }

        /// <summary>
        /// Event handler that finds the nearest feature and removes it from the layer
        /// </summary>
        private void Map_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (DeleteShape.IsChecked != true) return;

            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Query the layer for the closest feature within 100 meters
            var closestFeatures = featureLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1, new Collection<string>(), 100, DistanceUnit.Meter);

            // If a feature was found, remove it from the layer
            if (closestFeatures.Count <= 0) return;
            featureLayer.InternalFeatures.Remove(closestFeatures[0]);

            // Refresh the layerOverlay to show the results
            _ = Map.RefreshAsync(_layerOverlay);
        }
    }
}
