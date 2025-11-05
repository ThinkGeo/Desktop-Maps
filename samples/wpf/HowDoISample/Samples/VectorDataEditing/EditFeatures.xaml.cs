using System;
using System.Collections.ObjectModel;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to draw, edit, or delete shapes using the map's TrackOverlay and EditOverlay.
    /// </summary>
    public partial class EditFeatures : IDisposable
    {
        public EditFeatures()
        {
            InitializeComponent();
        }

        private bool _initialized;
        LayerOverlay _layerOverlay = null;

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            // Set the map's unit of measurement to meters(Spherical Mercator)
            MapView.MapUnit = GeographyUnit.Meter;

            // Add Cloud Maps as a background overlay
            var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
            {
                ClientId = SampleKeys.ClientId,
                ClientSecret = SampleKeys.ClientSecret,
                MapType = ThinkGeoCloudVectorMapsMapType.Light,
                // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
            };
            MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

            // Set the map extent
            MapView.CenterPoint = new PointShape(-10778000, 3912000);
            MapView.CurrentScale = 77000;
            MapView.MapClick += MapView_MapClick;

            var demoPolygon = new Feature("POLYGON((-10778500 3915600,-10778500 3910000,-10774040 3910000,-10774040 3915600,-10778500 3915600))");
            var demoPoint = new Feature("POINT(-10773220 3913230)");
            var demoLine = new Feature("LINESTRING(-10780700 3916500, -10780700 3910040)");
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPolygon);
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoPoint);
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(demoLine);
            MapView.EditOverlay.CalculateAllControlPoints();

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
            MapView.Overlays.Add("layerOverlay", _layerOverlay);
            MapView.TrackOverlay.MouseMoved += TrackOverlay_MouseMoved;
            MapView.EditOverlay.VertexMoved += EditOverlay_VertexMoved;

            // Update instructions
            Instructions.Text =
                "Edit Shapes Mode — Use anchor handles to translate, rotate, or scale shapes. Drag an existing vertex to move it. Click on a segment to add a vertex. Double-click an existing vertex to remove it.";

            _ = MapView.RefreshAsync();
            _initialized = true;
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
        /// Set the mode to normal navigation. This is the default.
        /// </summary>
        private void NavMode_Click(object sender, RoutedEventArgs e)
        {
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Move all TrackOverlay features to LayerOverlay
            foreach (var feature in MapView.TrackOverlay.TrackShapeLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Move all EditOverlay features to LayerOverlay and clear EditOverlay
            foreach (var feature in MapView.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Set TrackMode to None, so that the user will no longer draw shapes and will be able to navigate the map normally
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            // Update instructions
            Instructions.Text = "Navigation Mode — Default map state. Use mouse to pan and zoom the map.";

            _ = MapView.RefreshAsync(new Overlay[] { MapView.TrackOverlay, MapView.EditOverlay, _layerOverlay });
        }

        /// <summary>
        /// Set the mode to draw points on the map
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Point, which draws a new point on the map on mouse click
            MapView.TrackOverlay.TrackMode = TrackMode.Point;

            // Update instructions
            Instructions.Text = "Draw Point Mode — Click anywhere on the map to create a point. Hold the middle mouse button to pan.";
        }

        /// <summary>
        /// Set the mode to draw lines on the map
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Line, which draws a new line on the map on mouse click. Double click to finish drawing the line.
            MapView.TrackOverlay.TrackMode = TrackMode.Line;

            // Update instructions
            Instructions.Text = "Draw Line Mode — Click to add vertices; double-click to finish the line. Hold the middle mouse button to pan. Hold Shift to enable North–South / East–West snapping.";
        }

        /// <summary>
        /// Set the mode to draw polygons on the map
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set TrackMode to Polygon, which draws a new polygon on the map on mouse click. Double click to finish drawing the polygon.
            MapView.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Update instructions
            Instructions.Text =
                "Draw Polygon Mode — Click to add vertices; double-click to finish the polygon. Hold the middle mouse button to pan. Hold Shift to enable North–South / East–West snapping.";
        }

        private void EditShape_OnChecked(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            // Move all TrackOverlay features to EditOverlay
            foreach (var feature in MapView.TrackOverlay.TrackShapeLayer.InternalFeatures)
                MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Move all layerOverlay features to EditOverlay
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];
            foreach (var feature in featureLayer.InternalFeatures)
                MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
            featureLayer.InternalFeatures.Clear();

            // Set TrackMode to None, so that the user will no longer draw shapes
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            // This method draws all the handles and manipulation points on the map to edit. Essentially putting them all in edit mode.
            MapView.EditOverlay.CalculateAllControlPoints();

            // Refresh the map so that the features properly show that they are in edit mode
            _ = MapView.RefreshAsync(new Overlay[] { MapView.TrackOverlay, MapView.EditOverlay, _layerOverlay });

            // Update instructions
            Instructions.Text =
                "Edit Shapes Mode — Use anchor handles to translate, rotate, or scale shapes. Drag an existing vertex to move it. Click on a segment to add a vertex. Double-click an existing vertex to remove it.";
        }

        private void EditShape_OnUnchecked(object sender, RoutedEventArgs e)
        {
            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Move all EditOverlay features to LayerOverlay
            foreach (var feature in MapView.EditOverlay.EditShapesLayer.InternalFeatures)
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Refresh the overlays to show latest results
            _ = MapView.RefreshAsync(new Overlay[] { MapView.TrackOverlay, MapView.EditOverlay, _layerOverlay });
        }

        /// <summary>
        /// Event handler that finds the nearest feature and removes it from the layer
        /// </summary>
        private void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            if (DeleteShape.IsChecked != null && !DeleteShape.IsChecked.Value)
                return;

            var featureLayer = (InMemoryFeatureLayer)_layerOverlay.Layers["featureLayer"];

            // Query the layer for the closest feature within 100 meters
            var closestFeatures = featureLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1, new Collection<string>(), 100, DistanceUnit.Meter);

            // If a feature was found, remove it from the layer
            if (closestFeatures.Count <= 0) return;
            featureLayer.InternalFeatures.Remove(closestFeatures[0]);

            // Refresh the layerOverlay to show the results
            _ = MapView.RefreshAsync(_layerOverlay);
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}