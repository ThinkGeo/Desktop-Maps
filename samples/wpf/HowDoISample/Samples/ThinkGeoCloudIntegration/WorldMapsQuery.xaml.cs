using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the MapsQueryClient to query the WorldMaps dataset available from the ThinkGeo Cloud
    /// </summary>
    public partial class WorldMapsQuery : IDisposable
    {
        private MapsQueryCloudClient _mapsQueryCloudClient;

        public WorldMapsQuery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and feature layers for the queried shapes
        /// </summary>
        private async void MapView_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create the background world maps using vector tiles requested from the ThinkGeo Cloud Service. 
                var thinkGeoCloudVectorMapsOverlay = new ThinkGeoCloudVectorMapsOverlay
                {
                    ClientId = SampleKeys.ClientId,
                    ClientSecret = SampleKeys.ClientSecret,
                    MapType = ThinkGeoCloudVectorMapsMapType.Light,
                    // Set up the tile cache for the ThinkGeoCloudVectorMapsOverlay, passing in the location and an ID to distinguish the cache. 
                    TileCache = new FileRasterTileCache(@".\cache", "thinkgeo_vector_light")
                };
                MapView.Overlays.Add(thinkGeoCloudVectorMapsOverlay);

                // Set the map's unit of measurement to meters (Spherical Mercator)
                MapView.MapUnit = GeographyUnit.Meter;

                // Create a new feature layer to display the query shape used to perform the query
                var queryShapeFeatureLayer = new InMemoryFeatureLayer();

                // Add a point, line, and polygon style to the layer. These styles control how the query shape will be drawn
                queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
                queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Blue);
                queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));

                // Apply these styles on all zoom levels. This ensures our shapes will be visible on all zoom levels
                queryShapeFeatureLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Create a new feature layer to display the shapes returned by the query.
                var queriedFeaturesLayer = new InMemoryFeatureLayer();

                // Add a point, line, and polygon style to the layer. These styles control how the returned shapes will be drawn
                queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.OrangeRed);
                queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.OrangeRed);
                queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.OrangeRed, new GeoSolidBrush(new GeoColor(10, GeoColors.OrangeRed)));
                queriedFeaturesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add the feature layers to an overlay, then add that overlay to the map
                var queriedFeaturesOverlay = new LayerOverlay();
                queriedFeaturesOverlay.Layers.Add("Queried Features Layer", queriedFeaturesLayer);
                queriedFeaturesOverlay.Layers.Add("Query Shape Layer", queryShapeFeatureLayer);
                MapView.Overlays.Add("Queried Features Overlay", queriedFeaturesOverlay);

                // Set the map extent to Frisco, TX
                MapView.CenterPoint = new PointShape(-10778720, 3915154);
                MapView.CurrentScale = 202090;

                // Add an event to handle new shapes that are drawn on the map
                MapView.TrackOverlay.TrackEnded += OnShapeDrawn;

                // Initialize the MapsQueryCloudClient with our ThinkGeo Cloud credentials
                _mapsQueryCloudClient = new MapsQueryCloudClient
                {
                    ClientId = SampleKeys.ClientId2,
                    ClientSecret = SampleKeys.ClientSecret2,
                };

                // Create a sample shape and add it to the query shape layer
                var sampleShape = new RectangleShape(-10779877.70, 3915441.00, -10779248.97, 3915119.63);
                queryShapeFeatureLayer.InternalFeatures.Add(new Feature(sampleShape));

                // Run the world maps query
                await PerformWorldMapsQueryAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Get features from the WorldMapsQuery service based on the UI parameters
        /// </summary>
        private async Task PerformWorldMapsQueryAsync()
        {
            // Get the feature layers from the MapView
            var queriedFeaturesOverlay = (LayerOverlay)MapView.Overlays["Queried Features Overlay"];
            var queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];
            var queriedFeaturesLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Queried Features Layer"];

            // Show an error if trying to query with no query shape
            if (queryShapeFeatureLayer.InternalFeatures.Count == 0)
            {
                MessageBox.Show("Please draw a shape to use for the query", "Error");
                return;
            }

            // Set the MapsQuery parameters based on the drawn query shape and the UI
            var queryShape = queryShapeFeatureLayer.InternalFeatures[0].GetShape();
            const int projectionInSrid = 3857;
            var queryLayer = ((ComboBoxItem)CboQueryLayer.SelectedItem).Content.ToString()?.ToLower();

            var result = new CloudMapsQueryResult();

            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            // Perform the world maps query
            try
            {
                switch (((ComboBoxItem)CboQueryType.SelectedItem).Content.ToString())
                {
                    case "Containing":
                        result = await _mapsQueryCloudClient.GetFeaturesContainingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)MaxResults.Value });
                        break;
                    case "Nearest":
                        result = await _mapsQueryCloudClient.GetFeaturesNearestAsync(queryLayer, queryShape, projectionInSrid, (int)MaxResults.Value);
                        break;
                    case "Intersecting":
                        result = await _mapsQueryCloudClient.GetFeaturesIntersectingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)MaxResults.Value });
                        break;
                    case "Overlapping":
                        result = await _mapsQueryCloudClient.GetFeaturesOverlappingAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)MaxResults.Value });
                        break;
                    case "Within":
                        result = await _mapsQueryCloudClient.GetFeaturesWithinAsync(queryLayer, queryShape, projectionInSrid, new CloudMapsQuerySpatialQueryOptions() { MaxResults = (int)MaxResults.Value });
                        break;
                }
            }
            catch (Exception ex)
            {
                // Handle any errors returned from the maps query service
                if (ex is ArgumentException)
                {
                    if (ex.InnerException != null)
                        MessageBox.Show($"{ex.InnerException.Message} {ex.Message}", "Invalid Request");
                    await MapView.RefreshAsync();
                    return;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Unexpected Error");
                    await MapView.RefreshAsync();
                    return;
                }
            }
            finally
            {
                // Hide the loading graphic
                LoadingImage.Visibility = Visibility.Hidden;
            }

            if (result.Features.Count > 0)
            {
                // Add any features found by the query to the map
                foreach (var feature in result.Features)
                {
                    queriedFeaturesLayer.InternalFeatures.Add(feature);
                }

                // Set the map extent to the extent of the query results
                queriedFeaturesLayer.Open();
                MapView.CurrentExtent = queriedFeaturesLayer.GetBoundingBox();
                queriedFeaturesLayer.Close();
            }
            else
            {
                MessageBox.Show("No features found in the selected area");
            }

            // Refresh and redraw the map
            await MapView.RefreshAsync();
        }

        /// <summary>
        /// Disable drawing mode and draw the new query shape on the map when finished drawing a shape
        /// </summary>
        private async void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            try
            {
                // Disable drawing mode and clear the drawing layer
                MapView.TrackOverlay.TrackMode = TrackMode.None;
                MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

                // Get the query shape layer from the MapView
                var queriedFeaturesOverlay = (LayerOverlay)MapView.Overlays["Queried Features Overlay"];
                var queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];

                // Add the newly drawn shape, then redraw the overlay
                queryShapeFeatureLayer.InternalFeatures.Add(new Feature(e.TrackShape));
                await queriedFeaturesOverlay.RefreshAsync();

                await PerformWorldMapsQueryAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Set the map to 'Point Drawing Mode' when the user clicks the 'Draw a New Query Point' button
        /// </summary>
        private async void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the drawing mode to 'Point'
                MapView.TrackOverlay.TrackMode = TrackMode.Point;

                // Clear the old shapes from the map
                await ClearQueryShapesAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Set the map to 'Line Drawing Mode' when the user clicks the 'Draw a New Query Line' button
        /// </summary>
        private async void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the drawing mode to 'Line'
                MapView.TrackOverlay.TrackMode = TrackMode.Line;

                // Clear the old shapes from the map
                await ClearQueryShapesAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Set the map to 'Polygon Drawing Mode' when the user clicks the 'Draw a New Query Polygon' button
        /// </summary>
        private async void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set the drawing mode to 'Polygon'
                MapView.TrackOverlay.TrackMode = TrackMode.Polygon;

                // Clear the old shapes from the map
                await ClearQueryShapesAsync();
            }
            catch 
            {
                // Because async void methods don’t return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it’s good practice to catch and handle (or log) all exceptions within these “fire-and-forget” methods.
            }
        }

        /// <summary>
        /// Clear the query shapes from the map
        /// </summary>
        private async Task ClearQueryShapesAsync()
        {
            // Get the query shape layer from the MapView
            var queriedFeaturesOverlay = (LayerOverlay)MapView.Overlays["Queried Features Overlay"];
            var queryShapeFeatureLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Query Shape Layer"];
            var queriedFeaturesLayer = (InMemoryFeatureLayer)queriedFeaturesOverlay.Layers["Queried Features Layer"];

            // Clear the old query result and query shape from the map
            queriedFeaturesLayer.InternalFeatures.Clear();
            queryShapeFeatureLayer.InternalFeatures.Clear();
            await queriedFeaturesOverlay.RefreshAsync();
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