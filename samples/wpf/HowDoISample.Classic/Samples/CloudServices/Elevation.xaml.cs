using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ThinkGeo.Core;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Draw a point, a line or an area and get the elevation of the ground under it.
    /// </summary>
    public partial class Elevation : IDisposable
    {
        private const double SampleSpacingInMeters = 100;

        private bool _initialized;
        private ElevationCloudClient _elevationCloudClient;
        private InMemoryFeatureLayer _queryShapeLayer;
        private InMemoryFeatureLayer _elevationPointsLayer;
        private LayerOverlay _elevationOverlay;

        // The map is in Spherical Mercator; lengths and areas are measured on the ground through it.
        private readonly Projection _mapProjection = new Projection(3857);

        public Elevation()
        {
            InitializeComponent();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;
            _initialized = true;

            try
            {
                Map.MapUnit = GeographyUnit.Meter;
                Map.Overlays.Add(new ThinkGeoVectorTileOverlay(SampleKeys.ApiKey));

                _queryShapeLayer = new InMemoryFeatureLayer();
                _queryShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
                _queryShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = new LineStyle(GeoPens.Blue);
                _queryShapeLayer.ZoomLevelSet.ZoomLevel01.DefaultAreaStyle = new AreaStyle(GeoPens.Blue, new GeoSolidBrush(new GeoColor(10, GeoColors.Blue)));
                _queryShapeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                _elevationPointsLayer = new InMemoryFeatureLayer();
                _elevationPointsLayer.ZoomLevelSet.ZoomLevel01.DefaultPointStyle = new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue);
                _elevationPointsLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

                _elevationOverlay = new LayerOverlay();
                _elevationOverlay.Layers.Add(_elevationPointsLayer);
                _elevationOverlay.Layers.Add(_queryShapeLayer);
                Map.Overlays.Add(_elevationOverlay);

                Map.TrackOverlay.TrackEnded += OnShapeDrawn;

                _elevationCloudClient = new ElevationCloudClient
                {
                    ClientId = SampleKeys.ClientId2,
                    ClientSecret = SampleKeys.ClientSecret2,
                };

                Map.CenterPoint = new PointShape(-10776981, 3912345);
                Map.CurrentScale = 6200;
                await Map.RefreshAsync();

                var sampleLine = new LineShape("LINESTRING(-10776298 3912306,-10776496 3912399,-10776675 3912478,-10776890 3912516,-10777189 3912509,-10777329 3912442,-10777664 3912174)");
                await GetElevationAsync(sampleLine);
            }
            catch (Exception ex)
            {
                TxtAverageElevation.Text = ex.Message;
            }
        }

        private async Task GetElevationAsync(BaseShape queryShape)
        {
            _queryShapeLayer.InternalFeatures.Clear();
            _queryShapeLayer.InternalFeatures.Add(new Feature(queryShape));
            _elevationPointsLayer.InternalFeatures.Clear();
            LsbElevations.ItemsSource = null;

            LoadingImage.Visibility = Visibility.Visible;
            try
            {
                var elevationPoints = new Collection<CloudElevationPointResult>();
                CloudElevationResult result = null;
                switch (queryShape)
                {
                    case PointShape point:
                        var elevation = await _elevationCloudClient.GetElevationOfPointAsync(point.X, point.Y, 3857);
                        elevationPoints.Add(new CloudElevationPointResult(elevation, point));
                        TxtAverageElevation.Text = $"Elevation: {elevation:0.0} feet";
                        TxtHighestElevation.Text = string.Empty;
                        TxtLowestElevation.Text = string.Empty;
                        break;
                    case LineShape line:
                        result = await _elevationCloudClient.GetElevationOfLineAsync(line, 3857, SampleSpacingInMeters, DistanceUnit.Meter, DistanceUnit.Feet);
                        break;
                    case PolygonShape polygon:
                        result = await _elevationCloudClient.GetElevationOfAreaAsync(polygon, 3857, SampleSpacingInMeters, DistanceUnit.Meter, DistanceUnit.Feet);
                        break;
                }

                if (result != null)
                {
                    elevationPoints = result.ElevationPoints;
                    TxtAverageElevation.Text = $"Average elevation: {result.AverageElevation:0.0} feet";
                    TxtHighestElevation.Text = $"Highest: {result.HighestElevationPoint.Elevation:0.0} feet";
                    TxtLowestElevation.Text = $"Lowest: {result.LowestElevationPoint.Elevation:0.0} feet";
                }

                foreach (var elevationPoint in elevationPoints)
                {
                    _elevationPointsLayer.InternalFeatures.Add(new Feature(elevationPoint.Point));
                }
                LsbElevations.ItemsSource = elevationPoints;
            }
            finally
            {
                LoadingImage.Visibility = Visibility.Hidden;
            }

            await _elevationOverlay.RefreshAsync();
        }

        private async void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.None;
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // The service answers lines up to 5 km and areas up to 5 km², measured on the ground.
            if (e.TrackShape is PolygonShape polygon && polygon.GetArea(_mapProjection, AreaUnit.SquareKilometers) > 5)
            {
                TxtAverageElevation.Text = "Please draw a smaller area (limit: 5 km²).";
                return;
            }
            if (e.TrackShape is LineShape line && line.GetLength(_mapProjection, DistanceUnit.Kilometer) > 5)
            {
                TxtAverageElevation.Text = "Please draw a shorter line (limit: 5 km).";
                return;
            }

            try
            {
                await GetElevationAsync(e.TrackShape);
            }
            catch (Exception ex)
            {
                TxtAverageElevation.Text = ex.Message;
            }
        }

        private async void LsbElevations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!(LsbElevations.SelectedItem is CloudElevationPointResult elevationPoint)) return;

            try
            {
                Map.CenterPoint = elevationPoint.Point;
                await Map.RefreshAsync();
            }
            catch (Exception ex)
            {
                TxtAverageElevation.Text = ex.Message;
            }
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

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
