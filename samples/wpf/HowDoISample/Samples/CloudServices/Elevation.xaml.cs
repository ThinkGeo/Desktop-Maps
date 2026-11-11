using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Draw a point, a line or an area and get the elevation of the ground under it.
    /// </summary>
    public partial class Elevation
    {
        private bool _initialized;
        private ElevationCloudClient _elevationCloudClient;
        private readonly DynamicSource _queryShape = new DynamicSource();
        private readonly DynamicSource _elevationPoints = new DynamicSource();
        private Collection<CloudElevationPointResult> _results = new Collection<CloudElevationPointResult>();

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

                var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
                style.Images.Add("star", new PointStyle(PointSymbolType.Star, 20, GeoBrushes.Blue));
                style.AddDynamic(_queryShape);
                style.AddDynamic(_elevationPoints);
                Map.Basemap = new GpuBasemap(style);

                Map.TrackOverlay.TrackEnded += OnShapeDrawn;

                _elevationCloudClient = new ElevationCloudClient
                {
                    ClientId = SampleShared.ClientId,
                    ClientSecret = SampleShared.ClientSecret,
                };

                Map.CenterPoint = new PointShape(-10776981, 3912345);
                Map.CurrentScale = 6200;
                await Map.RefreshAsync();

                var sampleLine = new LineShape("LINESTRING(-10776298 3912306,-10776496 3912399,-10776675 3912478,-10776890 3912516,-10777189 3912509,-10777329 3912442,-10777664 3912174)");
                await GetElevationAsync(sampleLine);
            }
            catch (Exception ex)
            {
                LoadingImage.Visibility = Visibility.Hidden;
                TxtAverageElevation.Text = ex.Message;
            }
        }

        private async Task GetElevationAsync(BaseShape queryShape)
        {
            _queryShape.Clear();
            var asked = new Feature(queryShape);
            switch (queryShape)
            {
                case AreaBaseShape _:
                    _queryShape.SetArea("asked", asked, new GeoColor(10, GeoColors.Blue), GeoColors.Blue, 2f);
                    break;
                case LineBaseShape _:
                    _queryShape.SetLine("asked", asked, GeoColors.Blue, 2f);
                    break;
                case PointBaseShape _:
                    _queryShape.SetPoint("asked", asked, sizeInPixel: 20f, iconName: "star");
                    break;
            }

            LoadingImage.Visibility = Visibility.Visible;
            var interval = IntervalDistance.Value;
            CloudElevationResult result = null;
            switch (queryShape)
            {
                case PointShape point:
                    var elevation = await _elevationCloudClient.GetElevationOfPointAsync(point.X, point.Y, 3857);
                    _results = new Collection<CloudElevationPointResult> { new CloudElevationPointResult(elevation, point) };
                    TxtAverageElevation.Text = $"Elevation: {elevation:0.0} feet";
                    TxtHighestElevation.Text = string.Empty;
                    TxtLowestElevation.Text = string.Empty;
                    break;
                case LineShape line:
                    result = await _elevationCloudClient.GetElevationOfLineAsync(line, 3857, interval, DistanceUnit.Meter, DistanceUnit.Feet);
                    break;
                case PolygonShape polygon:
                    result = await _elevationCloudClient.GetElevationOfAreaAsync(polygon, 3857, interval, DistanceUnit.Meter);
                    break;
            }
            LoadingImage.Visibility = Visibility.Hidden;

            if (result != null)
            {
                _results = result.ElevationPoints;
                TxtAverageElevation.Text = $"Average elevation: {result.AverageElevation:0.0} feet";
                TxtHighestElevation.Text = $"Highest: {result.HighestElevationPoint.Elevation:0.0} feet";
                TxtLowestElevation.Text = $"Lowest: {result.LowestElevationPoint.Elevation:0.0} feet";
            }

            // Along a line each point is listed by how far along it is; elsewhere by its latitude and longitude.
            var rows = new List<object>();
            var toLatLon = new ProjectionConverter(3857, 4326);
            toLatLon.Open();
            var along = 0d;
            _elevationPoints.Clear();
            for (var i = 0; i < _results.Count; i++)
            {
                var point = _results[i].Point;
                _elevationPoints.SetPoint("elevation-" + i, new Feature(point), sizeInPixel: 20f, iconName: "star");

                string where;
                if (queryShape is LineShape)
                {
                    if (i > 0)
                    {
                        along += new LineShape(new[] { new Vertex(_results[i - 1].Point), new Vertex(point) }).GetLength(_mapProjection, DistanceUnit.Meter);
                    }
                    where = $"{along:0} m along the line";
                }
                else
                {
                    var latLon = toLatLon.ConvertToExternalProjection(point.X, point.Y);
                    where = FormattableString.Invariant($"{latLon.Y:0.00000}, {latLon.X:0.00000}");
                }
                rows.Add(new { Where = where, _results[i].Elevation });
            }
            toLatLon.Close();
            LsbElevations.ItemsSource = rows;

            await Map.RefreshAsync();
        }

        private async void OnShapeDrawn(object sender, TrackEndedTrackInteractiveOverlayEventArgs e)
        {
            Map.TrackOverlay.TrackMode = TrackMode.None;
            Map.TrackOverlay.TrackShapeLayer.InternalFeatures.Clear();

            // The service answers lines up to 5 km and areas up to 5 km², measured on the ground.
            if (e.TrackShape is PolygonShape polygon && polygon.GetArea(_mapProjection, AreaUnit.SquareKilometers) > 5)
            {
                MessageBox.Show("Please draw a smaller area (limit: 5 km²)", "Error");
                return;
            }
            if (e.TrackShape is LineShape line && line.GetLength(_mapProjection, DistanceUnit.Kilometer) > 5)
            {
                MessageBox.Show("Please draw a shorter line (limit: 5 km)", "Error");
                return;
            }

            try
            {
                await GetElevationAsync(e.TrackShape);
            }
            catch (Exception ex)
            {
                LoadingImage.Visibility = Visibility.Hidden;
                TxtAverageElevation.Text = ex.Message;
            }
        }

        private void LsbElevations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LsbElevations.SelectedIndex < 0) return;

            Map.CenterPoint = _results[LsbElevations.SelectedIndex].Point;
            _ = Map.RefreshAsync();
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
    }
}
