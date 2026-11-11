using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Core.Styling;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// A vehicle driving recorded GPS points to the Empire State Building, its marker and trail moved through DynamicSource.
    /// </summary>
    public partial class VehicleNavigation
    {
        private const double DefaultScale = 5000;

        private Collection<Vertex> _gpsPoints;
        private readonly List<Vertex> _visitedVertices = new List<Vertex>();
        private int _currentGpsPointIndex;
        private bool _showOverview;
        private bool _initialized;
        private DynamicSource _paths;
        private DynamicSource _vehicle;
        private CancellationTokenSource _drive;
        private CancellationTokenSource _segment;

        public VehicleNavigation()
        {
            InitializeComponent();
            Unloaded += (_, _) => _drive?.Cancel();
        }

        private void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.IsTiltEnabled = true;

            _paths = new DynamicSource();
            _vehicle = new DynamicSource();
            var style = new MapStyle(ThinkGeoVectorStyles.Light, new ThinkGeoVectorTileSource(SampleShared.CloudApiKey) { VectorTileCache = new FileTileCache(SampleShared.VectorTileCacheFolder) });
            style.Images.Add("vehicle", new GeoImage(Path.Combine(AppContext.BaseDirectory, "Resources", "vehicle_location.png")));
            style.AddDynamic(_paths).AddDynamic(_vehicle);
            Map.Basemap = new GpuBasemap(style);
            Map.TiltAngle = 45;

            Map.DefaultAnimationSettings = new MapAnimationSettings
            {
                Type = MapAnimationType.DrawWithAnimation,
                Duration = 1500,
                Easing = null
            };

            _gpsPoints = CollectGpsData();
            _paths.SetLine("route", new Feature(new LineShape(_gpsPoints)), GeoColor.FromHtml("#FFD400"), 6f);

            // A translucent circle around the destination.
            var end = _gpsPoints[_gpsPoints.Count - 1];
            var ring = new List<Vertex>();
            for (var i = 0; i < 36; i++)
            {
                var angle = i * 2 * Math.PI / 36;
                ring.Add(new Vertex(end.X + (55 * Math.Cos(angle)), end.Y + (55 * Math.Sin(angle))));
            }

            ring.Add(ring[0]);
            _paths.SetArea("destination", new Feature(new PolygonShape(new RingShape(ring))), GeoColor.FromArgb(70, 0, 150, 60));
            _vehicle.SetPoint("vehicle", new Feature(_gpsPoints[0].X, _gpsPoints[0].Y), 0f, GeoColors.White, 26f, "vehicle", lieOnMap: true);

            Map.CurrentExtentChangedInAnimation += Map_CurrentExtentChangedInAnimation;

            Map.CenterPoint = new PointShape(_gpsPoints[0]);
            Map.CurrentScale = DefaultScale;

            _drive = new CancellationTokenSource();
            _ = DriveAsync(_drive.Token);
        }

        /// <summary>
        /// One segment per GPS point. Tracking mode flies the camera to each point and the
        /// vehicle follows the flight's progress; overview mode holds the camera and moves
        /// the vehicle on its own clock.
        /// </summary>
        private async Task DriveAsync(CancellationToken token)
        {
            await Map.RefreshAsync();

            for (_currentGpsPointIndex = 0; _currentGpsPointIndex < _gpsPoints.Count; _currentGpsPointIndex++)
            {
                _segment = CancellationTokenSource.CreateLinkedTokenSource(token);
                try
                {
                    if (_showOverview)
                    {
                        var clock = Stopwatch.StartNew();
                        while (clock.ElapsedMilliseconds < 1000)
                        {
                            UpdateRoutesAndMarker(clock.ElapsedMilliseconds / 1000.0);
                            await Task.Delay(16, _segment.Token);
                        }

                        UpdateRoutesAndMarker(1);
                    }
                    else
                    {
                        await FollowVehicleAsync(_currentGpsPointIndex, _segment.Token);
                    }
                }
                catch (OperationCanceledException)
                {
                }
                finally
                {
                    _segment.Dispose();
                    _segment = null;
                }

                if (token.IsCancellationRequested)
                {
                    return;
                }
            }
        }

        private Task FollowVehicleAsync(int gpsPointIndex, CancellationToken cancellationToken)
        {
            var angle = GetRotationAngle(gpsPointIndex, _gpsPoints);

            // The GPS point sits 200 pixels below the center, so more of the road ahead is in view.
            var centerPoint = MapUtil.OffsetPointWithScreenOffset(new PointShape(_gpsPoints[gpsPointIndex]), 0, 200, angle, DefaultScale, Map.MapUnit);
            return Map.ZoomToAsync(centerPoint, DefaultScale, angle, cancellationToken);
        }

        private void Map_CurrentExtentChangedInAnimation(object sender, CurrentExtentChangedInAnimationMapViewEventArgs e)
        {
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
                _paths.SetLine("visited", new Feature(new LineShape(_visitedVertices)), GeoColors.Green, 6f);
            }

            // The marker takes the true bearing of the segment and counters the view rotation itself.
            var heading = Math.Atan2(toPoint.X - fromPoint.X, toPoint.Y - fromPoint.Y) * 180 / Math.PI;
            _vehicle.SetPoint("vehicle", new Feature(x, y), (float)heading, GeoColors.White, 26f, "vehicle", lieOnMap: true);
        }

        private async void OverviewButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (!_initialized)
                return;

            _showOverview = !_showOverview;
            OverviewButton.Content = _showOverview ? "Tracking Mode" : "Overview Mode";
            _segment?.Cancel();

            try
            {
                if (_showOverview)
                {
                    var boundingBox = new LineShape(_gpsPoints).GetBoundingBox();

                    // 1.5 times the scale that fits the route, so the whole drive has room around it.
                    var scale = MapUtil.GetScale(Map.MapUnit, boundingBox, Map.MapWidth, Map.MapHeight) * 1.5;
                    await Map.ZoomToAsync(boundingBox.GetCenterPoint(), scale, 0);
                }
                else if (_currentGpsPointIndex >= _gpsPoints.Count)
                {
                    // Once the drive is over nothing moves the camera, so bring it back to the vehicle.
                    await FollowVehicleAsync(_gpsPoints.Count - 1, CancellationToken.None);
                }
            }
            catch (OperationCanceledException)
            {
                // A newer camera move took over.
            }
        }

        private static Collection<Vertex> CollectGpsData()
        {
            var gpsPoints = new Collection<Vertex>();
            var lines = File.ReadAllLines(Path.Combine(AppContext.BaseDirectory, "Data", "Csv", "vehicle-route.csv"));

            // The GPS points are lat/lon (EPSG:4326); the basemap is Spherical Mercator (EPSG:3857).
            var converter = new ProjectionConverter(4326, 3857);
            converter.Open();

            foreach (var location in lines)
            {
                var posItems = location.Split(',');
                var lat = double.Parse(posItems[0], CultureInfo.InvariantCulture);
                var lon = double.Parse(posItems[1], CultureInfo.InvariantCulture);
                gpsPoints.Add(converter.ConvertToExternalProjection(lon, lat));
            }

            converter.Close();
            return gpsPoints;
        }

        private static double GetRotationAngle(int currentIndex, IReadOnlyList<Vertex> gpsPoints)
        {
            Vertex currentLocation;
            Vertex nextLocation;

            if (currentIndex < gpsPoints.Count - 1)
            {
                currentLocation = gpsPoints[currentIndex];
                nextLocation = gpsPoints[currentIndex + 1];
            }
            else
            {
                currentLocation = gpsPoints[currentIndex - 1];
                nextLocation = gpsPoints[currentIndex];
            }

            if (nextLocation.X - currentLocation.X != 0)
            {
                var dx = nextLocation.X - currentLocation.X;
                var dy = nextLocation.Y - currentLocation.Y;
                return -Math.Atan2(dx, dy) / Math.PI * 180;
            }

            return nextLocation.Y - currentLocation.Y >= 0 ? 0 : 180;
        }
    }
}
