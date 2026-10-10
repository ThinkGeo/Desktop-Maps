using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use the MapsQueryClient to query the WorldMaps dataset available from the ThinkGeo Cloud
    /// </summary>
    public partial class WorldMapsQuery
    {

        private bool _initialized;
        private MapsQueryCloudClient _mapsQueryCloudClient;

        // The query shape and what it found, drawn as geometry with the basemap.
        private readonly DynamicSource _querySource = new DynamicSource();
        private readonly DynamicSource _resultsSource = new DynamicSource();
        private BaseShape _queryShape;

        public WorldMapsQuery()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and feature layers for the queried shapes
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.MapUnit = GeographyUnit.Meter;

            // The query shape and its results go to the GPU as geometry with the
            // basemap; a star icon serves the point results. The query shape is added
            // last so it draws over what it found.
            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));
            style.Images.Add("star-blue", new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue));
            style.Images.Add("star-orange", new PointStyle(PointSymbolType.Star, 20, GeoBrushes.OrangeRed));
            style.AddDynamic(_resultsSource);
            style.AddDynamic(_querySource);
            Map.Basemap = new GpuBasemap(style);

            Map.CenterPoint = new PointShape(-10778720, 3915154);
            Map.CurrentScale = 202090;

            // Add an event to handle new shapes that are drawn on the map
            Map.TrackOverlay.TrackEnded += OnShapeDrawn;

            // Initialize the MapsQueryCloudClient with our ThinkGeo Cloud credentials
            _mapsQueryCloudClient = new MapsQueryCloudClient
            {
                ClientId = SampleKeys.ClientId2,
                ClientSecret = SampleKeys.ClientSecret2,
            };

            // Create a sample shape to run the first query with
            ShowQueryShape(new RectangleShape(-10779877.70, 3915441.00, -10779248.97, 3915119.63));

            // Run the world maps query
            _ = PerformWorldMapsQueryAsync();
        }

        /// <summary>
        /// Get features from the WorldMapsQuery service based on the UI parameters
        /// </summary>
        private async Task PerformWorldMapsQueryAsync()
        {
            // Show an error if trying to query with no query shape
            if (_queryShape == null)
            {
                MessageBox.Show("Please draw a shape to use for the query", "Error");
                return;
            }

            // Set the MapsQuery parameters based on the drawn query shape and the UI
            var queryShape = _queryShape;
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
                    await Map.RefreshAsync();
                    return;
                }
                else
                {
                    MessageBox.Show(ex.Message, "Unexpected Error");
                    await Map.RefreshAsync();
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
                // Each result is added the way its kind is drawn, under its own id; the
                // Clear and the run of Adds land as one rebuild on the next frame.
                var all = new List<BaseShape>();
                _resultsSource.Clear();
                for (var i = 0; i < result.Features.Count; i++)
                {
                    var feature = result.Features[i];
                    var shape = feature.GetShape();
                    all.Add(shape);
                    var id = "result-" + i;
                    switch (shape)
                    {
                        case AreaBaseShape: _resultsSource.AddArea(id, feature, new GeoColor(10, GeoColors.OrangeRed), GeoColors.OrangeRed, 1f); break;
                        case LineBaseShape: _resultsSource.AddLine(id, feature, GeoColors.OrangeRed, 2f); break;
                        default: _resultsSource.AddPoint(id, feature, sizeInPixels: 20f, iconName: "star-orange"); break;
                    }
                }

                var queriedFeaturesBBox = MapUtil.GetBoundingBoxOfItems(all);
                Map.CenterPoint = queriedFeaturesBBox.GetCenterPoint();
                Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, queriedFeaturesBBox, Map.MapWidth, Map.MapHeight);
            }
            else
            {
                MessageBox.Show("No features found in the selected area");
            }

            // Refresh and redraw the map
            await Map.RefreshAsync();
        }

        /// <summary>
        /// Disable drawing mode and draw the new query shape on the map when finished drawing a shape
        /// </summary>
        private async void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            try
            {
                // Disable drawing mode and clear the drawing layer
                Map.TrackOverlay.TrackMode = TrackMode.None;
                Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

                // Show the newly drawn shape and query with it
                ShowQueryShape(e.TrackShape);
                await Map.RefreshAsync(Map.TrackOverlay);

                await PerformWorldMapsQueryAsync();
            }
            catch 
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Set the map to 'Point Drawing Mode' when the user clicks the 'Draw a New Query Point' button
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Point'
            Map.TrackOverlay.TrackMode = TrackMode.Point;

            // Clear the old shapes from the map
            _ = ClearQueryShapesAsync();
        }

        /// <summary>
        /// Set the map to 'Line Drawing Mode' when the user clicks the 'Draw a New Query Line' button
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Line'
            Map.TrackOverlay.TrackMode = TrackMode.Line;

            // Clear the old shapes from the map
            _ = ClearQueryShapesAsync();
        }

        /// <summary>
        /// Set the map to 'Polygon Drawing Mode' when the user clicks the 'Draw a New Query Polygon' button
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Polygon'
            Map.TrackOverlay.TrackMode = TrackMode.Polygon;

            // Clear the old shapes from the map
            _ = ClearQueryShapesAsync();
        }

        /// <summary>
        /// Shows the shape the next query will use: a point as a star, a line or an
        /// area in blue over whatever the last query found.
        /// </summary>
        private void ShowQueryShape(BaseShape shape)
        {
            _queryShape = shape;
            _querySource.Clear();
            var query = new Feature(shape);
            switch (shape)
            {
                case PointShape: _querySource.AddPoint("query", query, sizeInPixels: 20f, iconName: "star-blue"); break;
                case LineBaseShape: _querySource.AddLine("query", query, GeoColors.Blue, 2f); break;
                case AreaBaseShape: _querySource.AddArea("query", query, new GeoColor(10, GeoColors.Blue), GeoColors.Blue, 1f); break;
            }
        }

        /// <summary>
        /// Clear the query shapes from the map
        /// </summary>
        private Task ClearQueryShapesAsync()
        {
            // Clear the old query result and query shape from the map
            _queryShape = null;
            _querySource.Clear();
            _resultsSource.Clear();
            return Task.CompletedTask;
        }
    }
}