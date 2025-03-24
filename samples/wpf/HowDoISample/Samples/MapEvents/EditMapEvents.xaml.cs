using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn to draw, edit, or delete shapes using the map's TrackOverlay and EditOverlay.
    /// </summary>
    public partial class EditMapEvents : IDisposable
    {
        public EditMapEvents()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
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
                MapView.CurrentExtent = new RectangleShape(-10786436, 3918518, -10769429, 3906002);

                // Create the layer that will store the drawn shapes
                var featureLayer = new InMemoryFeatureLayer();

                // Add styles for the layer
                featureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = PointStyle.CreateSimpleCircleStyle(GeoColors.Blue, 8, GeoColors.Black);
                featureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Blue, 4, true);
                featureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = AreaStyle.CreateSimpleAreaStyle(GeoColors.Blue, GeoColors.Black);
                featureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add the layer to a LayerOverlay
                var layerOverlay = new LayerOverlay();
                layerOverlay.Layers.Add("featureLayer", featureLayer);

                // Add the LayerOverlay to the map
                MapView.Overlays.Add("layerOverlay", layerOverlay);
                MapView.TrackOverlay.MouseMoved += TrackOverlay_MouseMoved;
                MapView.EditOverlay.VertexMoved += EditOverlay_VertexMoved;

                await MapView.RefreshAsync();
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        private void EditOverlay_VertexMoved(object sender, VertexMovedEditInteractiveOverlayEventArgs e)
        {
            var shape = e.AffectedFeature.GetShape();
        }

        private void TrackOverlay_MouseMoved(object sender, MouseMovedTrackInteractiveOverlayEventArgs e)
        {
            var shape = e.AffectedFeature.GetShape();
        }

        /// <summary>
        /// Update the layer whenever the user switches modes
        /// </summary>
        private async Task UpdateLayerFeaturesAsync(InMemoryFeatureLayer featureLayer, LayerOverlay layerOverlay)
        {
            // If the user switched away from a Drawing Mode, add all the newly drawn shapes in the TrackOverlay into the featureLayer
            foreach (var feature in MapView.TrackOverlay.TrackShapeLayer.InternalFeatures)
            {
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            }
            // Clear out all the TrackOverlay's features
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // If the user switched away from Edit Mode, add all the shapes that were in the EditOverlay back into the featureLayer
            foreach (var feature in MapView.EditOverlay.EditShapesLayer.InternalFeatures)
            {
                featureLayer.InternalFeatures.Add(feature.Id, feature);
            }
            // Clear out all the EditOverlay's features
            MapView.EditOverlay.EditShapesLayer.InternalFeatures.Clear();

            // Refresh the overlays to show latest results
            await MapView.RefreshAsync(new Overlay[] { MapView.TrackOverlay, MapView.EditOverlay, layerOverlay });

            // In case the user was in Delete Mode, remove the event handler to avoid deleting features unintentionally
            MapView.MapClick -= MapView_MapClick;
        }

        /// <summary>
        /// Set the mode to draw polygons on the map
        /// </summary>
        private async void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
                var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

                // Update the layer's features from any previous mode
                await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

                // Set TrackMode to Polygon, which draws a new polygon on the map on mouse click. Double click to finish drawing the polygon.
                MapView.TrackOverlay.TrackMode = TrackMode.Polygon;
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Set the mode to edit drawn shapes
        /// </summary>
        private async void EditShape_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
                var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

                // Update the layer's features from any previous mode
                await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

                // Set TrackMode to None, so that the user will no longer draw shapes
                MapView.TrackOverlay.TrackMode = TrackMode.None;

                // Put all features in the featureLayer into the EditOverlay
                foreach (var feature in featureLayer.InternalFeatures)
                {
                    MapView.EditOverlay.EditShapesLayer.InternalFeatures.Add(feature.Id, feature);
                }
                // Clear all the features in the featureLayer so that the editing features don't overlap with the original shapes
                // In UpdateLayerFeatures, we will add them all back to the featureLayer once the user switches modes
                featureLayer.InternalFeatures.Clear();

                // This method draws all the handles and manipulation points on the map to edit. Essentially putting them all in edit mode.
                MapView.EditOverlay.CalculateAllControlPoints();

                // Refresh the map so that the features properly show that they are in edit mode
                await MapView.RefreshAsync(new Overlay[] { MapView.EditOverlay, layerOverlay });
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Event handler that finds the nearest feature and removes it from the layer
        /// </summary>
        private async void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
        {
            try
            {
                var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
                var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

                // Query the layer for the closest feature within 100 meters
                var closestFeatures = featureLayer.QueryTools.GetFeaturesNearestTo(e.WorldLocation, GeographyUnit.Meter, 1, new Collection<string>(), 100, DistanceUnit.Meter);

                // If a feature was found, remove it from the layer
                if (closestFeatures.Count <= 0) return;
                featureLayer.InternalFeatures.Remove(closestFeatures[0]);

                // Refresh the layerOverlay to show the results
                await MapView.RefreshAsync(layerOverlay);
            }
            catch
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
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