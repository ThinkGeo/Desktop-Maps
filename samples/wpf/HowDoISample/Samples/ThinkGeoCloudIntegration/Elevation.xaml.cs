using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Learn how to use the ElevationCloudClient class to get elevation data from the ThinkGeo Cloud
    /// </summary>
    public partial class Elevation
    {
        private ElevationCloudClient _elevationCloudClient;

        public Elevation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layers for the shape to be queried and the returned elevation points
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

                // Create a new InMemoryFeatureLayer to hold the shape drawn for the elevation query
                var drawnShapeLayer = new InMemoryFeatureLayer();

                // Create Point, Line, and Polygon styles to display the drawn shape, and apply them across all zoom levels
                drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
                drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Blue);
                drawnShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
                drawnShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Create a new InMemoryFeatureLayer to display the elevation points returned from the query
                var elevationPointsLayer = new InMemoryFeatureLayer();

                // Create a point style for the elevation points
                elevationPointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
                elevationPointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                // Add the feature layers to an overlay, and add the overlay to the map
                var elevationFeaturesOverlay = new LayerOverlay();
                elevationFeaturesOverlay.Layers.Add("Elevation Points Layer", elevationPointsLayer);
                elevationFeaturesOverlay.Layers.Add("Drawn Shape Layer", drawnShapeLayer);
                MapView.Overlays.Add("Elevation Features Overlay", elevationFeaturesOverlay);

                // Add an event to trigger the elevation query when a new shape is drawn
                MapView.TrackOverlay.TrackEnded += OnShapeDrawn;

                // Initialize the ElevationCloudClient with our ThinkGeo Cloud credentials
                _elevationCloudClient = new ElevationCloudClient
                {
                    ClientId = SampleKeys.ClientId2,
                    ClientSecret = SampleKeys.ClientSecret2,
                };

                MapView.CenterPoint = new PointShape(-10776981, 3912345);
                MapView.CurrentScale = 6200;

                await MapView.RefreshAsync();

                // Create a sample line and get elevation along that line
                var sampleShape = new LineShape("LINESTRING(-10776298 3912306,-10776496 3912399,-10776675 3912478,-10776890 3912516,-10777189 3912509,-10777329 3912442,-10777664 3912174)");
                await PerformElevationQueryAsync(sampleShape);
            }
            catch
            {
                // Because async void methods don't return a Task, unhandled exceptions cannot be awaited or caught from outside.
                // Therefore, it's good practice to catch and handle (or log) all exceptions within these "fire-and-forget" methods.
            }
        }

        /// <summary>
        /// Get elevation data using the ElevationCloudClient and update the UI
        /// </summary>
        private async Task PerformElevationQueryAsync(BaseShape queryShape)
        {
            // Get feature layers from the MapView
            var elevationPointsOverlay = (LayerOverlay)MapView.Overlays["Elevation Features Overlay"];
            var drawnShapesLayer = (InMemoryFeatureLayer)elevationPointsOverlay.Layers["Drawn Shape Layer"];
            var elevationPointsLayer = (InMemoryFeatureLayer)elevationPointsOverlay.Layers["Elevation Points Layer"];

            // Clear the existing shapes from the map
            elevationPointsLayer.Open();
            elevationPointsLayer.Clear();
            elevationPointsLayer.Close();
            drawnShapesLayer.Open();
            drawnShapesLayer.Clear();
            drawnShapesLayer.Close();

            // Add the drawn shape to the map
            drawnShapesLayer.InternalFeatures.Add(new Feature(queryShape));

            // Set options from the UI and run the query using the ElevationCloudClient
            var elevationPoints = new Collection<CloudElevationPointResult>();
            const int projectionInSrid = 3857;

            // Show a loading graphic to let users know the request is running
            LoadingImage.Visibility = Visibility.Visible;

            // The point interval distance determines how many elevation points are retrieved for line and area queries
            var pointIntervalDistance = (int)IntervalDistance.Value;
            switch (queryShape.GetWellKnownType())
            {
                case WellKnownType.Point:
                    var drawnPoint = (PointShape)queryShape;
                    var elevation = await _elevationCloudClient.GetElevationOfPointAsync(drawnPoint.X, drawnPoint.Y, projectionInSrid);

                    // The API for getting the elevation of a single point returns a double, so we manually create a CloudElevationPointResult to use as a data source for the Elevations list
                    elevationPoints.Add(new CloudElevationPointResult(elevation, drawnPoint));

                    // Update the UI with the average, highest, and lowest elevations
                    TxtAverageElevation.Text = $"Average Elevation: {elevation:0.00} feet";
                    TxtHighestElevation.Text = $"Highest Elevation: {elevation:0.00} feet";
                    TxtLowestElevation.Text = $"Lowest Elevation: {elevation:0.00} feet";
                    break;
                case WellKnownType.Line:
                    var drawnLine = (LineShape)queryShape;
                    var result = await _elevationCloudClient.GetElevationOfLineAsync(drawnLine, projectionInSrid, pointIntervalDistance, DistanceUnit.Meter, DistanceUnit.Feet);
                    elevationPoints = result.ElevationPoints;

                    // Update the UI with the average, highest, and lowest elevations
                    TxtAverageElevation.Text = $"Average Elevation: {result.AverageElevation:0.00} feet";
                    TxtHighestElevation.Text = $"Highest Elevation: {result.HighestElevationPoint.Elevation:0.00} feet";
                    TxtLowestElevation.Text = $"Lowest Elevation: {result.LowestElevationPoint.Elevation:0.00} feet";
                    break;
                case WellKnownType.Polygon:
                    var drawnPolygon = (PolygonShape)queryShape;
                    result = await _elevationCloudClient.GetElevationOfAreaAsync(drawnPolygon, projectionInSrid, pointIntervalDistance, DistanceUnit.Meter);
                    elevationPoints = result.ElevationPoints;

                    // Update the UI with the average, highest, and lowest elevations
                    TxtAverageElevation.Text = $"Average Elevation: {result.AverageElevation:0.00} feet";
                    TxtHighestElevation.Text = $"Highest Elevation: {result.HighestElevationPoint.Elevation:0.00} feet";
                    TxtLowestElevation.Text = $"Lowest Elevation: {result.LowestElevationPoint.Elevation:0.00} feet";
                    break;
                case WellKnownType.Invalid:
                case WellKnownType.Multipoint:
                case WellKnownType.Multiline:
                case WellKnownType.Multipolygon:
                case WellKnownType.GeometryCollection:
                default:
                    break;
            }

            // Add the elevation result points to the map and list box
            foreach (var elevationPoint in elevationPoints)
            {
                elevationPointsLayer.InternalFeatures.Add(new Feature(elevationPoint.Point));
            }
            LsbElevations.ItemsSource = elevationPoints;

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            await elevationPointsOverlay.RefreshAsync();
        }

        /// <summary>
        /// Disable map drawing after a shape is drawn
        /// </summary>
        private void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Disable drawing mode and clear the drawing layer
            MapView.TrackOverlay.TrackMode = TrackMode.None;
            MapView.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // Validate shape size to avoid queries that are too large
            // Maximum length of a line is 10km
            // Maximum area of a polygon is 10km^2
            if (e.TrackShape.GetWellKnownType() == WellKnownType.Polygon)
            {
                if (((PolygonShape)e.TrackShape).GetArea(GeographyUnit.Meter, AreaUnit.SquareKilometers) > 5)
                {
                    MessageBox.Show("Please draw a smaller polygon (limit: 5km^2)", "Error");
                    return;
                }
            }
            else if (e.TrackShape.GetWellKnownType() == WellKnownType.Line)
            {
                if (((LineShape)e.TrackShape).GetLength(GeographyUnit.Meter, DistanceUnit.Kilometer) > 5)
                {
                    MessageBox.Show("Please draw a shorter line (limit: 5km)", "Error");
                    return;
                }
            }

            // Get elevation data for the drawn shape and update the UI
            _ = PerformElevationQueryAsync(e.TrackShape);
        }

        /// <summary>
        /// Center the map on a point when it's selected in the UI
        /// </summary>
        private void LsbElevations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LsbElevations.SelectedItem == null) return;
            // Set the map extent to the selected point
            var elevationPoint = (CloudElevationPointResult)LsbElevations.SelectedItem;
            var elevationPointLayerBBox = elevationPoint.Point.GetBoundingBox();
            MapView.CenterPoint = elevationPointLayerBBox.GetCenterPoint();
            MapView.CurrentScale = MapUtil.GetScale(MapView.MapUnit, elevationPointLayerBBox, MapView.MapWidth, MapView.MapHeight);
            _ = MapView.RefreshAsync();
        }

        /// <summary>
        /// Set the map to 'Point Drawing Mode' when the user clicks the 'Draw a New Point' button
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Point'
            MapView.TrackOverlay.TrackMode = TrackMode.Point;
        }

        /// <summary>
        /// Set the map to 'Line Drawing Mode' when the user clicks the 'Draw a New Line' button
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Line'
            MapView.TrackOverlay.TrackMode = TrackMode.Line;
        }

        /// <summary>
        /// Set the map to 'Polygon Drawing Mode' when the user clicks the 'Draw a New Polygon' button
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Polygon'
            MapView.TrackOverlay.TrackMode = TrackMode.Polygon;
        }
    }
}