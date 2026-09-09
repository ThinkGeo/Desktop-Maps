using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using ThinkGeo.Core;
using ThinkGeo.Gpu;
using ThinkGeo.UI.Wpf;

namespace ThinkGeo.UI.Wpf.HowDoI.Samples
{
    /// <summary>
    /// Learn how to programmatically zoom, pan, and rotate the map control.
    /// </summary>
    public partial class VehicleNavigation
    {
        private Collection<Vertex> _gpsPoints;
        private readonly List<Vertex> _visitedVertices = new List<Vertex>();
        private int _currentGpsPointIndex;
        private bool _disposed;
        private bool _showOverview;
        private bool _holdAnimation = false;

        private CancellationTokenSource _cancellationTokenSource;
        private readonly ThinkGeoVectorTileSource _cloud = new ThinkGeoVectorTileSource(SampleShared.CloudApiKey);
        private GpuBasemap _backgroundOverlay;

        // The route, the growing trail and the vehicle all ride the marker
        // batch: two PathLines and one icon TrackPoint, updated per animation tick.
        private InMemoryGeometrySource _paths;
        private InMemoryGeometrySource _vehicle;
        private GeoImage _vehicleImage;
        private double _vehicleHeading;
        private bool _initialized;
        private bool _darkTheme;
        private bool _isSwitchingTheme;

        private const double DefaultPitch = 45;
        private const double DefaultScale = 5000;

        public VehicleNavigation()
        {
            InitializeComponent();
            this.Unloaded += VehicleNavigation_Unloaded;
        }

        private void VehicleNavigation_Unloaded(object sender, RoutedEventArgs e)
        {
            this._disposed = true;
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }

        private async void Map_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (_initialized || e.NewSize.Width <= 0 || e.NewSize.Height <= 0) return;

            _initialized = true;
            Map.ZoomStep = 0.15;
            Map.TiltInteractionEnabled = true;
            _cancellationTokenSource = new CancellationTokenSource();

            _paths = new InMemoryGeometrySource();
            _vehicle = new InMemoryGeometrySource();
            // From the file beside the app, not the assembly's embedded resources: an
            // embedded pack URI resolves against whichever EXE is hosting the sample,
            // and a second host (the Checks harness) does not embed it.
            _vehicleImage = new GeoImage("./Resources/vehicle_location.png");

            _backgroundOverlay = new GpuBasemap(ComposeStyle());
            _backgroundOverlay.DrawExtrusionsEnabled = BuildingCheckBox.IsChecked != false;
            Map.Basemap = _backgroundOverlay;
            Map.TiltAngle = DefaultPitch;
            UpdatePitchValueText(DefaultPitch);

            Map.DefaultAnimationSettings = new MapAnimationSettings
            {
                Type = MapAnimationType.DrawWithAnimation,
                Duration = 1500,
                Easing = null
            };

            _gpsPoints = CollectGpsData();
            PushRoutesAndVehicle(_gpsPoints[0].X, _gpsPoints[0].Y);

            Map.CurrentExtentChangedInAnimation += Map_CurrentExtentChangedInAnimation;

            Map.CenterPoint = new PointShape(_gpsPoints[0]);
            Map.CurrentScale = DefaultScale;

            _ = ZoomToGpsPointsAsync(_gpsPoints);
        }

        /// <summary>
        /// The whole scene state in two calls: both route lines through the path
        /// source, the vehicle through the track source. Runs every animation tick;
        /// nothing else about the map changes.
        /// </summary>
        private void PushRoutesAndVehicle(double x, double y)
        {
            if (_paths == null || _gpsPoints == null)
            {
                return;
            }

            var routeColor = _darkTheme ? GeoColor.FromHtml("#41F3FF") : GeoColor.FromHtml("#FFD400");
            var visitedColor = _darkTheme ? GeoColor.FromHtml("#21FF8F") : GeoColors.Green;

            var route = new List<PointShape>(_gpsPoints.Count);
            foreach (var point in _gpsPoints)
            {
                route.Add(new PointShape(point));
            }

            var lines = new List<PathLine> { new PathLine(route, routeColor, 6f) };
            if (_visitedVertices.Count >= 2)
            {
                var visited = new List<PointShape>(_visitedVertices.Count);
                foreach (var vertex in _visitedVertices)
                {
                    visited.Add(new PointShape(vertex));
                }

                lines.Add(new PathLine(visited, visitedColor, 6f));
            }

            _paths.UpdateLines(lines);
            PushDestinationArea();
            _vehicle.UpdatePoints(new[]
            {
                new TrackPoint(x, y, (float)_vehicleHeading, GeoColors.White, 26f, "vehicle"),
            });
        }

        /// <summary>
        /// A translucent circle-ish polygon around the route's end - the same source
        /// carries it, and being ground geometry it is occluded by 3D buildings the
        /// way the route is.
        /// </summary>
        private void PushDestinationArea()
        {
            var end = _gpsPoints[_gpsPoints.Count - 1];
            const double radius = 55;
            const int segments = 36;
            var ring = new List<PointShape>(segments);
            for (var i = 0; i < segments; i++)
            {
                var angle = i * 2 * Math.PI / segments;
                ring.Add(new PointShape(end.X + (radius * Math.Cos(angle)), end.Y + (radius * Math.Sin(angle))));
            }

            var areaColor = _darkTheme ? GeoColor.FromArgb(70, 33, 255, 143) : GeoColor.FromArgb(70, 0, 150, 60);
            _paths.UpdateAreas(new[] { new FillArea(ring, areaColor) });
        }

        private async Task ZoomToGpsPointsAsync(Collection<Vertex> gpsPoints)
        {
            await Map.RefreshAsync();

            for (_currentGpsPointIndex = 0; _currentGpsPointIndex < gpsPoints.Count; _currentGpsPointIndex++)
            {
                try
                {
                    while (_holdAnimation)
                        await Task.Delay(500);

                    await ZoomToGpsPointAsync(gpsPoints, _currentGpsPointIndex, _cancellationTokenSource.Token);
                }
                catch (TaskCanceledException)
                {
                    await Task.Delay(500);
                }

                if (_disposed)
                    break;
            }
        }

        private void Map_CurrentExtentChangedInAnimation(object sender,
            CurrentExtentChangedInAnimationMapViewEventArgs e)
        {
            if (!MapUtil.IsSameDouble(e.FromResolution, e.ToResolution))
                return;

            UpdateRoutesAndMarker(e.Progress);
        }

        private void UpdateRoutesAndMarker(double progress)
        {
            if (_currentGpsPointIndex == 0)
                return;

            if (_currentGpsPointIndex >= _gpsPoints.Count)
                return;

            var fromPoint = _gpsPoints[_currentGpsPointIndex - 1];
            var toPoint = _gpsPoints[_currentGpsPointIndex];

            var x = (toPoint.X - fromPoint.X) * progress + fromPoint.X;
            var y = (toPoint.Y - fromPoint.Y) * progress + fromPoint.Y;

            if (_visitedVertices.Count > 0 && !MapUtil.IsSamePoint(_visitedVertices[_visitedVertices.Count - 1], _gpsPoints[_currentGpsPointIndex - 1]))
            {
                _visitedVertices.RemoveAt(_visitedVertices.Count - 1);
            }

            _visitedVertices.Add(new Vertex(x, y));

            // The vehicle carries the TRUE bearing of the segment it is on; the
            // marker batch counters the view rotation itself, so in tracking mode
            // (map rotated to put this bearing up) the icon points up, and in
            // overview mode it points along the road.
            _vehicleHeading = Math.Atan2(toPoint.X - fromPoint.X, toPoint.Y - fromPoint.Y) * 180 / Math.PI;
            PushRoutesAndVehicle(x, y);
        }

        private async Task ZoomToGpsPointAsync(Collection<Vertex> gpsPoints, int gpsPointIndex, CancellationToken cancellationToken)
        {
            if (gpsPointIndex >= gpsPoints.Count)
                return;

            var angle = GetRotationAngle(gpsPointIndex, gpsPoints);

            if (_showOverview)
            {
                var totalTime = 1000.0; // Set a 1-second animation
                var currentTime = DateTime.Now;

                while (true)
                {
                    if (cancellationToken.IsCancellationRequested)
                        await Task.Delay(500);
                    double duration = (DateTime.Now - currentTime).TotalMilliseconds;
                    var process = duration / totalTime;

                    if (process > 1)
                        break;

                    UpdateRoutesAndMarker(process);

                    await Task.Delay(10); // update every 10 ms
                }
            }
            else
            {
                var currentLocation = gpsPoints[gpsPointIndex];
                var centerPoint = new PointShape(currentLocation);
                // Recenter the map to display the GPS location 200 pixels towards the bottom for improved visibility.
                centerPoint = MapUtil.OffsetPointWithScreenOffset(centerPoint, 0, 200, angle, DefaultScale, Map.MapUnit);

                await Map.ZoomToAsync(centerPoint, DefaultScale, angle, cancellationToken);
            }
        }

        private static Collection<Vertex> CollectGpsData()
        {
            var gpsPoints = new Collection<Vertex>();

            // Resolve data file path: try relative path first, then absolute path from exe directory
            string dataPath = @"./Data/Csv/vehicle-route.csv";
            if (!File.Exists(dataPath))
            {
                var exeDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                dataPath = System.IO.Path.Combine(exeDir, @"Data/Csv/vehicle-route.csv");
            }

            var lines = File.ReadAllLines(dataPath);

            // Convert GPS Points from Lat/Lon (srid:4326) to Spherical Mercator (Srid:3857), which is the projection of the base map
            var converter = new ProjectionConverter(4326, 3857);
            converter.Open();

            foreach (var location in lines)
            {
                var posItems = location.Split(',');
                var lat = double.Parse(posItems[0], CultureInfo.InvariantCulture);
                var lon = double.Parse(posItems[1], CultureInfo.InvariantCulture);
                var vertexInSphericalMercator = converter.ConvertToExternalProjection(lon, lat);
                gpsPoints.Add(vertexInSphericalMercator);
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

            double angle;
            if (nextLocation.X - currentLocation.X != 0)
            {
                var dx = nextLocation.X - currentLocation.X;
                var dy = nextLocation.Y - currentLocation.Y;

                angle = -Math.Atan2(dx, dy) / Math.PI * 180; // get the angle in degrees
            }
            else
            {
                angle = nextLocation.Y - currentLocation.Y >= 0 ? 0 : 180;
            }

            return angle;
        }

        private void RefreshCancellationTokenAsync()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void BuildingCheckBox_CheckedChanged(object sender, RoutedEventArgs e)
        {
            if (!_initialized || _backgroundOverlay == null)
                return;

            _backgroundOverlay.DrawExtrusionsEnabled = BuildingCheckBox.IsChecked == true;
            _ = Map.RefreshAsync();
        }

        private void PitchSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            var pitch = Math.Max(0, Math.Min(80, e.NewValue));
            UpdatePitchValueText(pitch);

            if (_initialized)
            {
                Map.TiltAngle = pitch;
                _ = Map.RefreshAsync();
            }
        }

        private void DarkThemeButton_OnClick(object sender, RoutedEventArgs e)
        {
            _ = ToggleThemeAsync();
        }

        private async Task ToggleThemeAsync()
        {
            if (!_initialized || _isSwitchingTheme)
                return;

            _isSwitchingTheme = true;
            try
            {
                _darkTheme = !_darkTheme;
                DarkThemeButton.Content = _darkTheme ? "Light Theme" : "Dark Theme";

                // Light and dark read the SAME vector tiles - a theme is a restyle,
                // not a new basemap. SetStyleAsync keeps the map, the camera, the
                // running animation and the fetched tiles; only the styling recompiles
                // and the tile buffers rebuild, replacing on screen as they arrive.
                await _backgroundOverlay.SetStyleAsync(ComposeStyle());

                // The route colors are theme-inked into the paths, so re-push them.
                if (_visitedVertices.Count > 0)
                {
                    var last = _visitedVertices[_visitedVertices.Count - 1];
                    PushRoutesAndVehicle(last.X, last.Y);
                }
                else if (_gpsPoints != null)
                {
                    PushRoutesAndVehicle(_gpsPoints[0].X, _gpsPoints[0].Y);
                }
            }
            finally
            {
                _isSwitchingTheme = false;
            }
        }

        /// <summary>
        /// The style for the current theme: the basemap document, buildings included.
        /// The routes and the vehicle ride this style's marker batch, so they register
        /// on every build.
        /// </summary>
        private MapStyle ComposeStyle()
        {
            var style = new MapStyle(_darkTheme ? ThinkGeoVectorStyles.Dark : ThinkGeoVectorStyles.Light, _cloud);
            style.Images.Add("vehicle", _vehicleImage);
            style.AddGeometry(_paths)
                .AddGeometry(_vehicle);
            return style;
        }

        private void UpdatePitchValueText(double pitch)
        {
            if (PitchValueText != null)
                PitchValueText.Text = $"{pitch:0}°";
        }

        private void OverviewButton_OnClick(object sender, RoutedEventArgs e)
        {
            _ = SwitchView();
        }

        private async Task SwitchView()
        {
            _holdAnimation = true;

            _showOverview = !_showOverview;
            if (_showOverview)
            {
                OverviewButton.Content = "Tracking Mode";

                RefreshCancellationTokenAsync();

                var route = new LineShape(_gpsPoints);
                var boundingBox = route.GetBoundingBox();
                var center = boundingBox.GetCenterPoint();

                // Multiply the current scale by 1.5 to zoom out 50%.
                var scale = MapUtil.GetScale(Map.MapUnit, boundingBox, Map.MapWidth, Map.MapHeight) * 1.5;

                await Map.ZoomToAsync(center, scale, 0);
            }
            else
            {
                OverviewButton.Content = "Overview Mode";
            }

            _holdAnimation = false;
        }

        public void Dispose()
        {
            Map.Dispose();
            GC.SuppressFinalize(this);

            _disposed = true;
        }
    }
}
