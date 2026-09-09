using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;
using ThinkGeo.UI.Wpf;
using ThinkGeo.Gpu;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to use the ElevationCloudClient class to get elevation data from the ThinkGeo Cloud
    /// </summary>
    public partial class Elevation
    {

        private bool _initialized;
        private ElevationCloudClient _elevationCloudClient;
        private readonly InMemoryGeometrySource _queryShape = new InMemoryGeometrySource();
        private readonly InMemoryGeometrySource _elevationPoints = new InMemoryGeometrySource();

        public Elevation()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Set up the map with the ThinkGeo Cloud Maps overlay and a feature layers for the shape to be queried and the returned elevation points
        /// </summary>
        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            try
            {
                Map.MapUnit = GeographyUnit.Meter;

                // The shape being asked about and the points that come back go to the GPU
                // with the basemap. Drawn on an overlay they would be raster tiles, so a
                // fractional zoom would stretch them while the map under them stayed sharp.
                var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey));

                // The GPU draws a marker from a named image, so a point style the GPU has
                // no shape of its own for - a star - is drawn once by the CPU and registered.
                style.Images.Add("star", new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue));
                style.AddGeometry(_queryShape, new GeometryStyle
                {
                    FillColor = new GeoColor(10, GeoColors.Blue),
                    OutlineColor = GeoColors.Blue,
                    OutlineWidthInPixels = 2,
                    LineColor = GeoColors.Blue,
                    LineWidthInPixels = 2,
                });
                style.AddGeometry(_elevationPoints);
                Map.Basemap = new GpuBasemap(style);

                // Add an event to trigger the elevation query when a new shape is drawn
                Map.TrackOverlay.TrackEnded += OnShapeDrawn;

                // Initialize the ElevationCloudClient with our ThinkGeo Cloud credentials
                _elevationCloudClient = new ElevationCloudClient
                {
                    ClientId = SampleKeys.ClientId2,
                    ClientSecret = SampleKeys.ClientSecret2,
                };

                Map.CenterPoint = new PointShape(-10776981, 3912345);
                Map.CurrentScale = 6200;

                await Map.RefreshAsync();

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
            // Show the shape being asked about. It can be a point, a line or a polygon,
            // so the same shape goes to all three calls and each takes the one it draws.
            var asked = new[] { queryShape };
            _queryShape.UpdateAreas(asked);
            _queryShape.UpdateLines(asked);
            _queryShape.UpdatePoints(asked, sizeInPixels: 20f, iconName: "star");

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
            _elevationPoints.UpdatePoints(elevationPoints.Select(elevationPoint => elevationPoint.Point), sizeInPixels: 20f, iconName: "star");
            LsbElevations.ItemsSource = elevationPoints;

            // Hide the loading graphic
            LoadingImage.Visibility = Visibility.Hidden;

            await Map.RefreshAsync();
        }

        /// <summary>
        /// Disable map drawing after a shape is drawn
        /// </summary>
        private void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            // Disable drawing mode and clear the drawing layer
            Map.TrackOverlay.TrackMode = TrackMode.None;
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

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
            var elevationPoint = (CloudElevationPointResult)LsbElevations.SelectedItem;
            var elevationPointLayerBBox = elevationPoint.Point.GetBoundingBox();
            Map.CenterPoint = elevationPointLayerBBox.GetCenterPoint();
            Map.CurrentScale = MapUtil.GetScale(Map.MapUnit, elevationPointLayerBBox, Map.MapWidth, Map.MapHeight);
            _ = Map.RefreshAsync();
        }

        /// <summary>
        /// Set the map to 'Point Drawing Mode' when the user clicks the 'Draw a New Point' button
        /// </summary>
        private void DrawPoint_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Point'
            Map.TrackOverlay.TrackMode = TrackMode.Point;
        }

        /// <summary>
        /// Set the map to 'Line Drawing Mode' when the user clicks the 'Draw a New Line' button
        /// </summary>
        private void DrawLine_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Line'
            Map.TrackOverlay.TrackMode = TrackMode.Line;
        }

        /// <summary>
        /// Set the map to 'Polygon Drawing Mode' when the user clicks the 'Draw a New Polygon' button
        /// </summary>
        private void DrawPolygon_Click(object sender, RoutedEventArgs e)
        {
            // Set the drawing mode to 'Polygon'
            Map.TrackOverlay.TrackMode = TrackMode.Polygon;
        }
    }
}