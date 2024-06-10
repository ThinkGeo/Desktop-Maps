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
    public partial class EditFeatures : IDisposable
    {
        public EditFeatures()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay to show a basic map
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
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

            // Update instructions
            Instructions.Text = "Navigation Mode - The default map state. Allows you to pan and zoom the map with mouse controls.";

            await MapView.RefreshAsync();
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
            var measureResult = string.Empty;
            switch (shape)
            {
                case AreaBaseShape polygon:
                    measureResult = polygon.GetArea(GeographyUnit.Meter, AreaUnit.SquareMiles).ToString("N2");
                    measureResult += " square miles";
                    break;
                case LineBaseShape line:
                    measureResult = line.GetLength(GeographyUnit.Meter, DistanceUnit.Mile).ToString("N2");
                    measureResult += " miles";
                    break;
            }
            return measureResult;
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
        /// Set the mode to normal navigation. This is the default.
        /// </summary>
        private async void NavMode_Click(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to None, so that the user will no longer draw shapes and will be able to navigate the map normally
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            // Update instructions
            Instructions.Text = "Navigation Mode - The default map state. Allows you to pan and zoom the map with mouse controls.";
        }

        /// <summary>
        /// Set the mode to draw points on the map
        /// </summary>
        private async void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to Point, which draws a new point on the map on mouse click
            MapView.TrackOverlay.TrackMode = TrackMode.Point;

            // Update instructions
            Instructions.Text = "Draw Point Mode - Creates a Point Shape where at the location of each left mouse click event on the map.";
        }

        /// <summary>
        /// Set the mode to draw lines on the map
        /// </summary>
        private async void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to Line, which draws a new line on the map on mouse click. Double click to finish drawing the line.
            MapView.TrackOverlay.TrackMode = TrackMode.Line;

            // Update instructions
            Instructions.Text = "Draw Line Mode - Begin creating a Line Shape by left clicking on the map. Each subsequent left click adds another vertex to the line. Double left click to finish creating the Shape. Middle mouse click and drag allows the user to pan the map while drawing the Shape.";
        }

        /// <summary>
        /// Set the mode to draw polygons on the map
        /// </summary>
        private async void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to Polygon, which draws a new polygon on the map on mouse click. Double click to finish drawing the polygon.
            MapView.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Update instructions
            Instructions.Text = "Draw Polygon Mode - Begin creating a Polygon Shape by left clicking on the map. Each subsequent left click adds another vertex to the polygon. Double left click to finish creating the Shape. Middle mouse click and drag allows the user to pan the map while drawing the Shape.";
        }

        /// <summary>
        /// Set the mode to edit drawn shapes
        /// </summary>
        private async void EditShape_Click(object sender, RoutedEventArgs e)
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

            // Update instructions
            Instructions.Text = "Edit Shapes Mode - Allows the user to modify Shapes. Translate, rotate, or scale a shape using the anchor controls around the shape. Line and Polygon Shapes can also be modified: move a vertex by left mouse click and dragging on an existing vertex, add a vertex by left mouse clicking on a line segment, and remove a vertex by double left mouse clicking on an existing vertex.";
        }

        /// <summary>
        /// Set the mode to delete features ont the map
        /// </summary>
        private async void DeleteShape_Click(object sender, RoutedEventArgs e)
        {
            var layerOverlay = (LayerOverlay)MapView.Overlays["layerOverlay"];
            var featureLayer = (InMemoryFeatureLayer)layerOverlay.Layers["featureLayer"];

            // Update the layer's features from any previous mode
            await UpdateLayerFeaturesAsync(featureLayer, layerOverlay);

            // Set TrackMode to None, so that the user will no longer draw shapes
            MapView.TrackOverlay.TrackMode = TrackMode.None;

            // Add the event handler that will delete features on map click
            MapView.MapClick += MapView_MapClick;

            // Update instructions
            Instructions.Text = "Delete Shape Mode - Deletes a shape by left mouse clicking on the shape.";
        }

        /// <summary>
        /// Event handler that finds the nearest feature and removes it from the layer
        /// </summary>
        private async void MapView_MapClick(object sender, MapClickMapViewEventArgs e)
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

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            MapView.Dispose();
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }
    }
}