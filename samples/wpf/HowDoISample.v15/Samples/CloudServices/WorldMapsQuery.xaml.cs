using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
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
        private readonly InMemoryGeometrySource _querySource = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _resultsSource = new InMemoryGeometrySource();
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
            style.AddGeometry(_resultsSource);
            style.AddGeometry(_querySource);
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
                // Sort what came back by kind and hand each kind to its channel
                var points = new List<BaseShape>();
                var lines = new List<BaseShape>();
                var areas = new List<BaseShape>();
                var all = new List<BaseShape>();
                foreach (var feature in result.Features)
                {
                    var shape = feature.GetShape();
                    all.Add(shape);
                    switch (shape)
                    {
                        case AreaBaseShape: areas.Add(shape); break;
                        case LineBaseShape: lines.Add(shape); break;
                        default: points.Add(shape); break;
                    }
                }

                _resultsSource.UpdatePoints(points, sizeInPixels: 20f, iconName: "star-orange");
                _resultsSource.UpdateLines(lines, GeoColors.OrangeRed, 2f);
                _resultsSource.UpdateAreas(areas, new GeoColor(10, GeoColors.OrangeRed), GeoColors.OrangeRed, 1f);

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
            _querySource.UpdatePoints(shape is PointShape ? new[] { shape } : Array.Empty<BaseShape>(), sizeInPixels: 20f, iconName: "star-blue");
            _querySource.UpdateLines(shape is LineBaseShape ? new[] { shape } : Array.Empty<BaseShape>(), GeoColors.Blue, 2f);
            _querySource.UpdateAreas(shape is AreaBaseShape ? new[] { shape } : Array.Empty<BaseShape>(), new GeoColor(10, GeoColors.Blue), GeoColors.Blue, 1f);
        }

        /// <summary>
        /// Clear the query shapes from the map
        /// </summary>
        private Task ClearQueryShapesAsync()
        {
            // Clear the old query result and query shape from the map
            _queryShape = null;
            _querySource.UpdatePoints(Array.Empty<BaseShape>());
            _querySource.UpdateLines(Array.Empty<BaseShape>());
            _querySource.UpdateAreas(Array.Empty<BaseShape>());
            _resultsSource.UpdatePoints(Array.Empty<BaseShape>());
            _resultsSource.UpdateLines(Array.Empty<BaseShape>());
            _resultsSource.UpdateAreas(Array.Empty<BaseShape>());
            return Task.CompletedTask;
        }
    }
}