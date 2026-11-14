using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using ThinkGeo.Core;
using Point = System.Windows.Point;

namespace ThinkGeo.UI.Wpf.HowDoI
{
    /// <summary>
    /// Follow a vehicle along a GPS route, turning the map so the road ahead points up the screen.
    /// </summary>
    public partial class VehicleNavigation : IDisposable
    {
        private const double DefaultScale = 5000;

        private bool _initialized;
        private bool _disposed;
        private bool _showOverview;
        private bool _holdAnimation;
        // The direction of travel, as the map rotation that puts it up the screen.
        private double _heading;
        private int _currentGpsPointIndex;
        private Collection<Vertex> _gpsPoints;
        private readonly List<Vertex> _visitedVertices = new List<Vertex>();
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        private Marker _vehicleMarker;
        private FeatureLayerWpfDrawingOverlay _routesOverlay;
        private InMemoryFeatureLayer _routeLayer;
        private InMemoryFeatureLayer _visitedRoutesLayer;

        public VehicleNavigation()
        {
            InitializeComponent();
            Unloaded += (sender, e) => _disposed = true;
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            var backgroundOverlay = new ThinkGeoCloudRasterMapsOverlay(SampleKeys.ClientId, SampleKeys.ClientSecret, ThinkGeoCloudRasterMapsMapType.Light_V2_X2)
            {
                ZoomOutTileCacheCapacity = 20
            };
            Map.Overlays.Add(backgroundOverlay);

            Map.DefaultAnimationSettings = new MapAnimationSettings
            {
                Type = MapAnimationType.DrawWithAnimation,
                Duration = 1500,
                Easing = null
            };

            _gpsPoints = CollectGpsData();

            _routeLayer = new InMemoryFeatureLayer();
            _routeLayer.InternalFeatures.Add(new Feature(new LineShape(_gpsPoints)));
            _routeLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Yellow, 6, true);
            _routeLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _visitedRoutesLayer = new InMemoryFeatureLayer();
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.DefaultLineStyle = LineStyle.CreateSimpleLineStyle(GeoColors.Green, 6, true);
            _visitedRoutesLayer.ZoomLevelSet.ZoomLevel01.ApplyUntilZoomLevel = ApplyUntilZoomLevel.Level20;

            _routesOverlay = new FeatureLayerWpfDrawingOverlay();
            _routesOverlay.FeatureLayers.Add(_routeLayer);
            _routesOverlay.FeatureLayers.Add(_visitedRoutesLayer);
            Map.Overlays.Add(_routesOverlay);

            _vehicleMarker = new Marker
            {
                Position = new Point(_gpsPoints[0].X, _gpsPoints[0].Y),
                ImageSource = new BitmapImage(new Uri("/Resources/vehicle_location.png", UriKind.RelativeOrAbsolute)),
                Width = 24,
                Height = 24
            };
            var markerOverlay = new SimpleMarkerOverlay();
            markerOverlay.Markers.Add(_vehicleMarker);
            Map.Overlays.Add(markerOverlay);

            Map.CurrentExtentChangedInAnimation += Map_CurrentExtentChangedInAnimation;

            Map.CenterPoint = new PointShape(_gpsPoints[0]);
            Map.CurrentScale = DefaultScale;

            _ = DriveAsync();
        }

        private async Task DriveAsync()
        {
            await Map.RefreshAsync();

            for (_currentGpsPointIndex = 0; _currentGpsPointIndex < _gpsPoints.Count; _currentGpsPointIndex++)
            {
                try
                {
                    while (_holdAnimation && !_disposed)
                        await Task.Delay(500);

                    await DriveToGpsPointAsync(_currentGpsPointIndex, _cancellationTokenSource.Token);
                }
                catch (OperationCanceledException)
                {
                    await Task.Delay(500);
                }

                if (_disposed)
                    break;
            }
        }

        private async Task DriveToGpsPointAsync(int gpsPointIndex, CancellationToken cancellationToken)
        {
            _heading = GetRotationAngle(gpsPointIndex, _gpsPoints);

            if (_showOverview)
            {
                var start = DateTime.Now;
                while (true)
                {
                    var progress = (DateTime.Now - start).TotalMilliseconds / 1000.0;
                    if (progress > 1)
                        break;

                    UpdateRoutesAndMarker(progress);
                    await _routesOverlay.RefreshAsync();
                    await Task.Delay(10);
                }
            }
            else
            {
                // Keep the vehicle 200 pixels below the center, so more of the road ahead is on screen.
                var centerPoint = MapUtil.OffsetPointWithScreenOffset(new PointShape(_gpsPoints[gpsPointIndex]), 0, 200, _heading, DefaultScale, Map.MapUnit);
                await Map.ZoomToAsync(centerPoint, DefaultScale, _heading, cancellationToken);
            }
        }

        private void Map_CurrentExtentChangedInAnimation(object sender, CurrentExtentChangedInAnimationMapViewEventArgs e)
        {
            // The marker is not rotated with the map, so it turns as the map turns.
            _vehicleMarker.RotationAngle = Map.RotationAngle - _heading;

            if (!MapUtil.IsSameDouble(e.FromResolution, e.ToResolution))
                return;

            UpdateRoutesAndMarker(e.Progress);
        }

        private void UpdateRoutesAndMarker(double progress)
        {
            if (_currentGpsPointIndex == 0 || _currentGpsPointIndex >= _gpsPoints.Count)
                return;

            var fromPoint = _gpsPoints[_currentGpsPointIndex - 1];
            var toPoint = _gpsPoints[_currentGpsPointIndex];
            var x = (toPoint.X - fromPoint.X) * progress + fromPoint.X;
            var y = (toPoint.Y - fromPoint.Y) * progress + fromPoint.Y;

            if (_visitedVertices.Count > 0 && !MapUtil.IsSamePoint(_visitedVertices[_visitedVertices.Count - 1], fromPoint))
            {
                _visitedVertices.RemoveAt(_visitedVertices.Count - 1);
            }
            _visitedVertices.Add(new Vertex(x, y));
            if (_visitedVertices.Count >= 2)
            {
                _visitedRoutesLayer.InternalFeatures.Clear();
                _visitedRoutesLayer.InternalFeatures.Add(new Feature(new LineShape(_visitedVertices)));
            }

            _vehicleMarker.RotationAngle = Map.RotationAngle - _heading;
            _vehicleMarker.Position = new Point(x, y);

            _ = _routesOverlay.RefreshAsync();
        }

        private static Collection<Vertex> CollectGpsData()
        {
            var gpsPoints = new Collection<Vertex>();

            // The route is latitude,longitude (EPSG:4326); the map is in EPSG:3857.
            var converter = new ProjectionConverter(4326, 3857);
            converter.Open();
            foreach (var line in File.ReadLines(Path.Combine(AppContext.BaseDirectory, "Data", "Csv", "vehicle-route.csv")))
            {
                var parts = line.Split(',');
                if (parts.Length < 2
                    || !double.TryParse(parts[0], NumberStyles.Float, CultureInfo.InvariantCulture, out var latitude)
                    || !double.TryParse(parts[1], NumberStyles.Float, CultureInfo.InvariantCulture, out var longitude))
                {
                    continue;
                }
                gpsPoints.Add(converter.ConvertToExternalProjection(longitude, latitude));
            }
            converter.Close();

            return gpsPoints;
        }

        private static double GetRotationAngle(int currentIndex, IReadOnlyList<Vertex> gpsPoints)
        {
            var from = currentIndex < gpsPoints.Count - 1 ? gpsPoints[currentIndex] : gpsPoints[currentIndex - 1];
            var to = currentIndex < gpsPoints.Count - 1 ? gpsPoints[currentIndex + 1] : gpsPoints[currentIndex];

            if (to.X - from.X == 0)
                return to.Y - from.Y >= 0 ? 0 : 180;

            return -Math.Atan2(to.X - from.X, to.Y - from.Y) / Math.PI * 180;
        }

        private async void OverviewButton_Click(object sender, RoutedEventArgs e)
        {
            _holdAnimation = true;
            _showOverview = !_showOverview;
            OverviewButton.Content = _showOverview ? "Tracking Mode" : "Overview Mode";

            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                if (_showOverview)
                {
                    var boundingBox = _routeLayer.GetBoundingBox();
                    var scale = MapUtil.GetScale(Map.MapUnit, boundingBox, Map.MapWidth, Map.MapHeight) * 1.5;
                    await Map.ZoomToAsync(boundingBox.GetCenterPoint(), scale, 0);
                }
                else
                {
                    var vehicle = new PointShape(_vehicleMarker.Position.X, _vehicleMarker.Position.Y);
                    var center = MapUtil.OffsetPointWithScreenOffset(vehicle, 0, 200, _heading, DefaultScale, Map.MapUnit);
                    await Map.ZoomToAsync(center, DefaultScale, _heading);
                }
            }
            catch (OperationCanceledException)
            {
            }
            finally
            {
                _holdAnimation = false;
            }
        }

        public void Dispose()
        {
            _disposed = true;
            _cancellationTokenSource.Cancel();
            Map.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
